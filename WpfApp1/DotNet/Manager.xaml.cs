using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DAL;
using DAL.DataModel;
using Microsoft.Win32;
using System.IO;

namespace DotNet
{
    /// <summary>
    /// Логика взаимодействия для Manager.xaml
    /// </summary>
    public partial class Manager : MetroWindow
    {
        DALFunction DalFunc = new DALFunction();
        List<CpuFromProvider> ListCpuProvider = null;
        List<MBFromProvider> ListMotherBoardProvider = null;
        List<ItemFromShop> ListItemShop = new List<ItemFromShop>();
        List<BuyItem> ListBuyItem = new List<BuyItem>();
        User User = null;
        public Manager()
        {
            InitializeComponent();
            ShowCloseButton = false;
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            DalFunc.UserOff(User.Id);
            Close();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            MainWindow.exit = true;
            DalFunc.UserOff(User.Id);
            Close();
        }
        /// <summary>
        /// initialize the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Load(object sender, RoutedEventArgs e)
        {
            ListItemShop.Clear();
            ListItemShop.AddRange(DalFunc.GetCpuFromShop());
            ListItemShop.AddRange(DalFunc.GetMBFromShop());
            var TmpList = DalFunc.GetItemsFromProvider();
            if (TmpList != null)
            {
                ListCpuProvider = TmpList.Where(x => x is CpuFromProvider).Select(x => (x as CpuFromProvider)).ToList();
                ListMotherBoardProvider = TmpList.Where(x => x is MBFromProvider).Select(x => (x as MBFromProvider)).ToList();
                CpuDataGrid.ItemsSource = null;
                CpuDataGrid.ItemsSource = ListCpuProvider;
                MotherBDataGrid.ItemsSource = null;
                MotherBDataGrid.ItemsSource = ListMotherBoardProvider;
                GridItemShop.ItemsSource = null;
                GridItemShop.ItemsSource = ListItemShop;
                ADDBoxCpu();
                ADDBoxMotherB();
            }
            DalFunc.UserOn(User.Id);
            AddBoxShopItemTree();
        }
        /// <summary>
        /// Set treeview for Shop Item
        /// </summary>
        private void AddBoxShopItemTree()
        {
            Component.Items.Clear();
            foreach (var item in ListItemShop.Select(x => x.ToString()).Distinct().ToList())
            {
                CheckBox TmpCheckBox = new CheckBox
                {
                    Content = item,
                    Background = Brushes.Transparent,
                    Foreground = Brushes.GreenYellow,
                    IsChecked = true
                };
                TmpCheckBox.Checked += CheckComponent;
                TmpCheckBox.Unchecked += CheckComponent;
                Component.Items.Add(TmpCheckBox);
            }
        }
        /// <summary>
        /// Checked CheckBox for ShopItem, display ShopItem depending on checked CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckComponent(object sender, RoutedEventArgs e)
        {
            string component = string.Empty;
            foreach (var item in Component.Items)
            {
                if ((item as CheckBox).IsChecked == true)
                {
                    component += (item as CheckBox).Content;
                }
            }
            List<ItemFromShop> BindingItemShop = ListItemShop.Where(x =>
                                      component.IndexOf(x.ToString()) >= 0).ToList();
            GridItemShop.ItemsSource = BindingItemShop;
        }
        /// <summary>
        /// Set CheckBox in all treeview for MotherBoard TabItem
        /// </summary>
        private void ADDBoxMotherB()
        {
            AddBoxInMotherBProducerTree();
            AddBoxInMotherBSocketTree();
            AddBoxInMotherBChipSetTree();
            AddBoxInMotherBTypeRamTree();
            AddBoxInMotherBProvider();
        }
        /// <summary>
        /// Set CheckBox in TreeItem MotherBoard Producer
        /// </summary>
        private void AddBoxInMotherBProducerTree()
        {
            MotherBProducerTree.Items.Clear();
            foreach (var item in ListMotherBoardProvider.Select(x => x.Producer.Name).Distinct().ToList())
            {
                CheckBox TmpCheckBox = new CheckBox
                {
                    Content = item,
                    Background = Brushes.Transparent,
                    Foreground = Brushes.GreenYellow,
                    IsChecked = true
                };
                TmpCheckBox.Checked += CheckmotherBoard;
                TmpCheckBox.Unchecked += CheckmotherBoard;
                MotherBProducerTree.Items.Add(TmpCheckBox);
            }
        }
        /// <summary>
        /// Set CheckBox in TreeItem MotherBoard Socket
        /// </summary>
        private void AddBoxInMotherBSocketTree()
        {
            MotherBSocketTree.Items.Clear();
            foreach (var item in ListMotherBoardProvider.Select(x => x.MBSocket).Distinct().ToList())
            {
                CheckBox TmpCheckBox = new CheckBox
                {
                    Content = item,
                    Background = Brushes.Transparent,
                    Foreground = Brushes.GreenYellow,
                    IsChecked = true
                };
                TmpCheckBox.Checked += CheckmotherBoard;
                TmpCheckBox.Unchecked += CheckmotherBoard;
                MotherBSocketTree.Items.Add(TmpCheckBox);
            }
        }
        /// <summary>
        /// Set CheckBox in TreeItem MotherBoard ChipSet
        /// </summary>
        private void AddBoxInMotherBChipSetTree()
        {
            MotherBChipSetTree.Items.Clear();
            foreach (var item in ListMotherBoardProvider.Select(x => x.ChipSet).Distinct().ToList())
            {
                CheckBox TmpCheckBox = new CheckBox
                {
                    Content = item,
                    Background = Brushes.Transparent,
                    Foreground = Brushes.GreenYellow,
                    IsChecked = true
                };
                TmpCheckBox.Checked += CheckmotherBoard;
                TmpCheckBox.Unchecked += CheckmotherBoard;
                MotherBChipSetTree.Items.Add(TmpCheckBox);
            }
        }
        /// <summary>
        /// Set CheckBox in TreeItem MotherBoard TypeRam
        /// </summary>
        private void AddBoxInMotherBTypeRamTree()
        {
            MotherBTypeRamTree.Items.Clear();
            foreach (var item in ListMotherBoardProvider.Select(x => x.RAM).Distinct().ToList())
            {
                CheckBox TmpCheckBox = new CheckBox
                {
                    Content = item,
                    Background = Brushes.Transparent,
                    Foreground = Brushes.GreenYellow,
                    IsChecked = true
                };
                TmpCheckBox.Checked += CheckmotherBoard;
                TmpCheckBox.Unchecked += CheckmotherBoard;
                MotherBTypeRamTree.Items.Add(TmpCheckBox);
            }
        }
        /// <summary>
        /// Set CheckBox in TreeItem MotherBoard Provider
        /// </summary>
        private void AddBoxInMotherBProvider()
        {
            MotherBProvider.Items.Clear();
            foreach (var item in ListMotherBoardProvider.Select(x => x.Provider.Name).Distinct().ToList())
            {
                CheckBox TmpCheckBox = new CheckBox
                {
                    Content = item,
                    Background = Brushes.Transparent,
                    Foreground = Brushes.GreenYellow,
                    IsChecked = true
                };
                TmpCheckBox.Checked += CheckmotherBoard;
                TmpCheckBox.Unchecked += CheckmotherBoard;
                MotherBProvider.Items.Add(TmpCheckBox);
            }
        }
        /// <summary>
        /// Checked CheckBox for MotherBoard, display MotherBoard depending on checked CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckmotherBoard(object sender, RoutedEventArgs e)
        {

            string producer = string.Empty;
            string chipset = string.Empty;
            string socket = string.Empty;
            string ram = string.Empty;
            string provider = string.Empty;

            StringToCheckMotherBoard(ref producer, ref chipset, ref socket, ref ram, ref provider);

            List<MBFromProvider> BindingMB = ListMotherBoardProvider.Where(x =>
                                      ram.IndexOf(x.RAM.ToString()) >= 0
                                      && producer.IndexOf(x.Producer.Name) >= 0
                                      && socket.IndexOf(x.MBSocket) >= 0
                                      && chipset.IndexOf(x.ChipSet) >= 0
                                      && provider.IndexOf(x.Provider.Name) >= 0).ToList();
            MotherBDataGrid.ItemsSource = BindingMB;
        }
        /// <summary>
        /// collect information about the selected CheckBox MotherBoard
        /// </summary>
        /// <param name="producer"></param>
        /// <param name="chipset"></param>
        /// <param name="socket"></param>
        /// <param name="ram"></param>
        /// <param name="provider"></param>
        private void StringToCheckMotherBoard
          (ref string producer, ref string chipset, ref string socket, ref string ram, ref string provider)
        {
            foreach (var item in MotherBProducerTree.Items)
            {
                if ((item as CheckBox).IsChecked == true)
                {
                    producer += (item as CheckBox).Content;
                }
            }

            foreach (var item in MotherBChipSetTree.Items)
            {
                if ((item as CheckBox).IsChecked == true)
                {
                    chipset += (item as CheckBox).Content;
                }
            }

            foreach (var item in MotherBSocketTree.Items)
            {
                if ((item as CheckBox).IsChecked == true)
                {
                    socket += (item as CheckBox).Content;
                }
            }

            foreach (var item in MotherBTypeRamTree.Items)
            {
                if ((item as CheckBox).IsChecked == true)
                {
                    ram += (item as CheckBox).Content;
                }
            }
            foreach (var item in MotherBProvider.Items)
            {
                if ((item as CheckBox).IsChecked == true)
                {
                    provider += (item as CheckBox).Content;
                }
            }
        }
        /// <summary>
        /// Set CheckBox in all treeview for Cpu TabItem
        /// </summary>
        private void ADDBoxCpu()
        {
            AddBoxInCpuProducerTree();
            AddBoxInCpuCoreTree();
            AddBoxInCpuSocketTree();
            AddBoxInCpuVideoTree();
            AddBoxInCpuProvider();
        }
        /// <summary>
        /// Set CheckBox in TreeItem Cpu Provider
        /// </summary>
        private void AddBoxInCpuProvider()
        {
            CpuProvider.Items.Clear();
            foreach (var item in ListCpuProvider.Select(x => x.Provider.Name).Distinct().ToList())
            {
                CheckBox TmpCheckBox = new CheckBox
                {
                    Content = item,
                    Background = Brushes.Transparent,
                    Foreground = Brushes.GreenYellow,
                    IsChecked = true
                };
                TmpCheckBox.Checked += CheckCpu;
                TmpCheckBox.Unchecked += CheckCpu;
                CpuProvider.Items.Add(TmpCheckBox);
            }
        }
        /// <summary>
        /// Set CheckBox in TreeItem Cpu Video
        /// </summary>
        private void AddBoxInCpuVideoTree()
        {
            CpuVideoTree.Items.Clear();
            foreach (var item in ListCpuProvider.Select(x => x.Video).Distinct().ToList())
            {
                CheckBox TmpCheckBox = new CheckBox
                {
                    Content = item,
                    Background = Brushes.Transparent,
                    Foreground = Brushes.GreenYellow,
                    IsChecked = true
                };
                TmpCheckBox.Checked += CheckCpu;
                TmpCheckBox.Unchecked += CheckCpu;
                CpuVideoTree.Items.Add(TmpCheckBox);
            }
        }
        /// <summary>
        /// Set CheckBox in TreeItem Cpu Socket
        /// </summary>
        private void AddBoxInCpuSocketTree()
        {
            CpuSocketTree.Items.Clear();
            foreach (var item in ListCpuProvider.Select(x => x.CpuSocket).Distinct().ToList())
            {
                CheckBox TmpCheckBox = new CheckBox
                {
                    Content = item,
                    Background = Brushes.Transparent,
                    Foreground = Brushes.GreenYellow,
                    IsChecked = true
                };
                TmpCheckBox.Checked += CheckCpu;
                TmpCheckBox.Unchecked += CheckCpu;
                CpuSocketTree.Items.Add(TmpCheckBox);
            }
        }
        /// <summary>
        /// Set CheckBox in TreeItem Cpu Core
        /// </summary>
        private void AddBoxInCpuCoreTree()
        {
            CpuCoreTree.Items.Clear();
            foreach (var item in ListCpuProvider.Select(x => x.Core).Distinct().ToList())
            {
                CheckBox TmpCheckBox = new CheckBox
                {
                    Content = item,
                    Background = Brushes.Transparent,
                    Foreground = Brushes.GreenYellow,
                    IsChecked = true
                };
                TmpCheckBox.Checked += CheckCpu;
                TmpCheckBox.Unchecked += CheckCpu;
                CpuCoreTree.Items.Add(TmpCheckBox);
            }
        }
        /// <summary>
        /// Set CheckBox in TreeItem Cpu Producer
        /// </summary>
        private void AddBoxInCpuProducerTree()
        {
            CpuProducerTree.Items.Clear();
            foreach (var item in ListCpuProvider.Select(x => x.Producer.Name).Distinct().ToList())
            {
                CheckBox TmpCheckBox = new CheckBox
                {
                    Content = item,
                    Background = Brushes.Transparent,
                    Foreground = Brushes.GreenYellow,
                    IsChecked = true
                };
                TmpCheckBox.Checked += CheckCpu;
                TmpCheckBox.Unchecked += CheckCpu;
                CpuProducerTree.Items.Add(TmpCheckBox);
            }
        }
        /// <summary>
        /// Checked CheckBox for Cpu, display Cpu depending on checked CheckBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckCpu(object sender, RoutedEventArgs e)
        {

            string producer = string.Empty;
            string core = string.Empty;
            string socket = string.Empty;
            string video = string.Empty;
            string provider = string.Empty;
            StringToCheckCPU(ref producer, ref core, ref socket, ref video, ref provider);

            List<CpuFromProvider> BindingCpu = ListCpuProvider.Where(x =>
                                      core.IndexOf(x.Core.ToString()) >= 0
                                      && producer.IndexOf(x.Producer.Name) >= 0
                                      && socket.IndexOf(x.CpuSocket) >= 0
                                      && video.IndexOf(x.Video) >= 0
                                      && provider.IndexOf(x.Provider.Name) >= 0).ToList();
            CpuDataGrid.ItemsSource = BindingCpu;
        }
        /// <summary>
        /// collect information about the selected CheckBox Cpu
        /// </summary>
        /// <param name="producer"></param>
        /// <param name="core"></param>
        /// <param name="socket"></param>
        /// <param name="video"></param>
        /// <param name="provider"></param>
        private void StringToCheckCPU
           (ref string producer, ref string core, ref string socket, ref string video, ref string provider)
        {
            foreach (var item in CpuProducerTree.Items)
            {
                if ((item as CheckBox).IsChecked == true)
                {
                    producer += (item as CheckBox).Content;
                }
            }

            foreach (var item in CpuCoreTree.Items)
            {
                if ((item as CheckBox).IsChecked == true)
                {
                    core += (item as CheckBox).Content;
                }
            }

            foreach (var item in CpuSocketTree.Items)
            {
                if ((item as CheckBox).IsChecked == true)
                {
                    socket += (item as CheckBox).Content;
                }
            }

            foreach (var item in CpuVideoTree.Items)
            {
                if ((item as CheckBox).IsChecked == true)
                {
                    video += (item as CheckBox).Content;
                }
            }

            foreach (var item in CpuProvider.Items)
            {
                if ((item as CheckBox).IsChecked == true)
                {
                    provider += (item as CheckBox).Content;
                }
            }
        }
        /// <summary>
        /// Set authorized User
        /// </summary>
        /// <param name="user"></param>
        public void SetUser(User user)
        {
            User = user;
        }
        /// <summary>
        /// refresh all TabItems and TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh(object sender, RoutedEventArgs e)
        {
            Load(null,null);
        }
        /// <summary>
        /// update data when changing TabItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Find_Click(object sender, RoutedEventArgs e)
        {
            if (MyTabContol.SelectedIndex == 0)
            {
                CpuDataGrid.ItemsSource = ListCpuProvider.Where
                                             (x => x.Name.IndexOf
                                             (FindBox.Text, 0, StringComparison.OrdinalIgnoreCase)
                                             >= 0).ToList();
            }
            else if (MyTabContol.SelectedIndex == 1)
            {
                MotherBDataGrid.ItemsSource = ListMotherBoardProvider.Where
                                                (x => x.Name.IndexOf
                                                (FindBox.Text, 0, StringComparison.OrdinalIgnoreCase)
                                                >= 0).ToList();
            }
            else if (MyTabContol.SelectedIndex == 2)
            {
                GridItemShop.ItemsSource = ListItemShop.Where
                                                (x => x.Name.IndexOf
                                                (FindBox.Text, 0, StringComparison.OrdinalIgnoreCase)
                                                >= 0).ToList();
            }

        }
        /// <summary>
        /// adding an item to the price list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPriceList(object sender, RoutedEventArgs e)
        {
             DALFunction dALFunction = new DALFunction();     

            foreach (TabItem item in MyTabContol.Items)
            {
                if (item.IsSelected && QuantityNumber.Value >= 1 && (string)item.Header!= "Shop Item")
                {
                    foreach (var dataitem in (item.Content as Grid).Children)
                    {
                        if (dataitem is DataGrid)
                        {
                            var ItemShop = ((dataitem as DataGrid).SelectedItem as ItemFromProvider);

                            if (ListBuyItem.Where(x => x.ItemFromProvider.Id == ItemShop.Id)
                                                            .FirstOrDefault() != null)
                            {

                                ListBuyItem.FirstOrDefault(x => x.ItemFromProvider.Id == ItemShop.Id)
                                                                .Quantity += (int)QuantityNumber.Value;
                            }
                            else
                            {
                                ListBuyItem.Add(new BuyItem
                                {
                                    Quantity = (int)QuantityNumber.Value,
                                    TimeBuy = DateTime.Now.Date,
                                    ItemFromProvider = ItemShop
                                });
                            }
                        }
                    }
                }
            }
            QuantityNumber.Value = 0;
            BuyDataGrid.ItemsSource = null;
            BuyDataGrid.ItemsSource = ListBuyItem;
        }
        /// <summary>
        /// Delete Item with salery list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (BuyDataGrid.SelectedItem != null)
            {
                var Id = (BuyDataGrid.SelectedItem as BuyItem).ItemFromProvider.Id;
                var DeleteItem = ListBuyItem.Where(x => x.ItemFromProvider.Id == Id).Single();
                ListBuyItem.Remove(DeleteItem);
            }
            BuyDataGrid.ItemsSource = null;
            BuyDataGrid.ItemsSource = ListBuyItem;
        }
        /// <summary>
        /// printed a check and Buy items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print(object sender, RoutedEventArgs e)
        {
            string Bill = string.Empty;
            if (ListBuyItem.Count > 0)
            {
                SaveFileDialog SaveSaleList = new SaveFileDialog();
                if (SaveSaleList.ShowDialog() == true)
                {
                    Bill += DateTime.Now;
                    Bill += "\n";
                    int i = 1;
                    foreach (var item in ListBuyItem)
                    {
                        Bill += $"  {i}:  Name: {item.ItemFromProvider.Name} Producer:" +
                            $" {item.ItemFromProvider.Producer.Name}" +
                            $" Quantity: {item.Quantity} Price:" +
                            $" Provider: {item.ItemFromProvider.Provider.Name}" +
                            $" {item.Quantity * item.ItemFromProvider.BuyPrice} \n";
                    }
                    File.WriteAllText(SaveSaleList.FileName, Bill);
                    DALFunction DalFunc = new DALFunction();
                    DalFunc.BuyItem(ListBuyItem);
                    ListBuyItem.Clear();
                    BuyDataGrid.ItemsSource = null;
                    Load(null, null);
                }            

            }
        }
        /// <summary>
        /// Delete Item with salery list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearBuyList(object sender, RoutedEventArgs e)
        {
            ListBuyItem.Clear();
            BuyDataGrid.ItemsSource = null;
            BuyDataGrid.ItemsSource = ListBuyItem;
        }
    }
}