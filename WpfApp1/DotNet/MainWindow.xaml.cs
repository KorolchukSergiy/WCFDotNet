using DAL;
using DAL.DataModel;
using MahApps.Metro.Controls;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DotNet
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        private Brush DefaultBrush;
        static public bool exit = false;
        User user = null;
        public MainWindow()
        {
            InitializeComponent();
            Progress.Visibility = Visibility.Hidden;
            DefaultBrush = LogInBox.Background;
            ShowCloseButton = false;
        }

        private void RegNewProvider_Click(object sender, MouseButtonEventArgs e)
        {

        }
        /// <summary>
        /// Close programm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// block control for authorization period
        /// </summary>
        private void BlockControl()
        {
            ExitButton.IsEnabled = false;
            LoginButton.IsEnabled = false;
            LogInBox.IsEnabled = false;
            passwordBox.IsEnabled = false;
            RegNewProvider.IsEnabled = false;

        }
        /// <summary>
        /// unlock control upon completion of authorization
        /// </summary>
        private void UnBlockControl()
        {
            ExitButton.IsEnabled = true;
            LoginButton.IsEnabled = true;
            LogInBox.IsEnabled = true;
            passwordBox.IsEnabled = true;
            RegNewProvider.IsEnabled = true;
        }
        /// <summary>
        /// check if there is a user and whether it is available
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void LogIn_Click(object sender, RoutedEventArgs e)
        {
            BlockControl();
            string login = LogInBox.Text;
            string password = passwordBox.Password;
            Progress.Visibility = Visibility.Visible;

            user = await Task.Run(() =>
                  {
                      DALFunction Dal = new DALFunction();
                      return Dal.GetUser(login, password);
                  });

            if (user != null)
            {
                if (user.Online == true)
                {
                    LogInBox.Text = "This User is Online";
                }
                else if (user.Online == false)
                {
                    CheckLogin(user);
                }
                else
                    IncorectLogin();
            }
            else
                IncorectLogin();

            Progress.Visibility = Visibility.Hidden;
            UnBlockControl();
        }
        /// <summary>
        /// checking the user's position
        /// </summary>
        /// <param name="user"></param>
        private void CheckLogin(User user)
        {
            string Position = user.Post.Name;

            switch (Position)
            {
                case "Seller":
                    Seller SellerWindow = new Seller();
                    SellerWindow.SetUser(user);
                    this.Visibility = Visibility.Hidden;
                    SellerWindow.ShowDialog();
                    user = null;
                    break;
                case "Manager":
                    Manager ManagerWindow = new Manager();
                    ManagerWindow.SetUser(user);
                    this.Visibility = Visibility.Hidden;
                    ManagerWindow.ShowDialog();
                    user = null;
                    break;
                case "Director":
                    break;
                case "Provider":
                    break;
                default:
                    IncorectLogin();
                    break;
            }
            LogInBox.Text = string.Empty;
            passwordBox.Password = string.Empty;
            if (exit)
                Close();
            else
                this.Visibility = Visibility.Visible;
        }
        /// <summary>
        /// message to incorrect login
        /// </summary>
        private async void IncorectLogin()
        {
            passwordBox.Password = string.Empty;
            LogInBox.Background = Brushes.Red;
            passwordBox.Background = Brushes.Red;
            await Task.Run(() => { Thread.Sleep(1000); });
            LogInBox.Background = DefaultBrush;
            passwordBox.Background = DefaultBrush;
        }
        /// <summary>
        /// triggers the LogIn button by pressing the enter button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                LogIn_Click(null, null);
            }
        }
    }
}