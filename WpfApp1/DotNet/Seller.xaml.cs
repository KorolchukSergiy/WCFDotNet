using DAL;
using DAL.DataModel;
using MahApps.Metro.Controls;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DotNet
{
    /// <summary>
    /// Логика взаимодействия для Seller.xaml
    /// </summary>
    public partial class Seller : MetroWindow
    {
        User User = null;
        List<CpuFromShop> ListCpu;
        List<MBFromShop> ListMotherBoard;
        List<SaleItem> saleItems = new List<SaleItem>();
        public Seller()
        {

            InitializeComponent();
            ShowCloseButton = false;

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
        /// initialize the window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SellerWindoowLoaded(object sender, RoutedEventArgs e)
        {
            DALFunction DalFunc = new DALFunction();
            ListCpu = DalFunc.GetCpuFromShop();
            ListMotherBoard = DalFunc.GetMBFromShop();

            AddBoxCpu();
            AddBoxMB();

            CpusDataGrid.ItemsSource = null;
            CpusDataGrid.ItemsSource = ListCpu;

            MotherBDataGrid.ItemsSource = null;
            MotherBDataGrid.ItemsSource = ListMotherBoard;
            MyTabContol.SelectionChanged += TabControl_SelectionChanged;
            CpusDataGrid.SelectionChanged += Selecteditem;
            MotherBDataGrid.SelectionChanged += Selecteditem;
            DalFunc.UserOn(User.Id);
        }
        /// <summary>
        /// Set treeview for MotherBoard TabItem
        /// </summary>
        private void AddBoxMB()
        {

            AddBoxInMotherBChipSetTree();
            AddBoxInMotherBProducerTree();
            AddBoxInMotherBRamTree();
            AddBoxInMotherBSocketTree();
        }
        /// <summary>
        /// Set treeview for Cpu TabItem
        /// </summary>
        private void AddBoxCpu()
        {
            AddBoxInCpuProducerTree();
            AddBoxInCpuCoreTree();
            AddBoxInCpuSocketTree();
            AddBoxInCpuVideoTree();
        }
        /// <summary>
        /// Set CheckBox in TreeItem for Cpu Producer
        /// </summary>
        private void AddBoxInCpuProducerTree()
        {
            CpuProducerTree.Items.Clear();
            foreach (var item in ListCpu.Select(x => x.Producer.Name).Distinct().ToList())
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
        /// Set CheckBox in TreeItem for Cpu Core
        /// </summary>
        private void AddBoxInCpuCoreTree()
        {
            CpuCoreTree.Items.Clear();
            foreach (var item in ListCpu.Select(x => x.Core).Distinct().ToList())
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
        ///  Set CheckBox in TreeItem for Cpu Socket
        /// </summary>
        private void AddBoxInCpuSocketTree()
        {
            CpuSocketTree.Items.Clear();
            foreach (var item in ListCpu.Select(x => x.CpuSocket).Distinct().ToList())
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
        /// Set CheckBox in TreeItem for Cpu Video
        /// </summary>
        private void AddBoxInCpuVideoTree()
        {
            CpuVideoTree.Items.Clear();
            foreach (var item in ListCpu.Select(x => x.Video).Distinct().ToList())
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
        /// Set CheckBox in TreeItem for MotherBoard Producer
        /// </summary>
        private void AddBoxInMotherBProducerTree()
        {
            MotherBProducerTree.Items.Clear();
            foreach (var item in ListMotherBoard.Select(x => x.Producer).Distinct().ToList())
            {
                CheckBox TmpCheckBox = new CheckBox
                {
                    Content = item.Name,
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
        /// Set CheckBox in TreeItem for MotherBoard Socket
        /// </summary>
        private void AddBoxInMotherBSocketTree()
        {
            MotherBSocketTree.Items.Clear();
            foreach (var item in ListMotherBoard.Select(x => x.MBSocket).Distinct().ToList())
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
        /// Set CheckBox in TreeItem for MotherBoard ChipSet
        /// </summary>
        private void AddBoxInMotherBChipSetTree()
        {
            MotherBChipSetTree.Items.Clear();
            foreach (var item in ListMotherBoard.Select(x => x.ChipSet).Distinct().ToList())
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
        /// Set CheckBox in TreeItem for MotherBoard TypeRam
        /// </summary>
        private void AddBoxInMotherBRamTree()
        {
            MotherBTypeRamTree.Items.Clear();
            foreach (var item in ListMotherBoard.Select(x => x.RAM).Distinct().ToList())
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

        private void LogOut(object sender, RoutedEventArgs e)
        {
            DALFunction DalFunc = new DALFunction();
            DalFunc.UserOff(User.Id);
            Close();
        }

        private void Exit(object sender, RoutedEventArgs e)
        {
            DALFunction DalFunc = new DALFunction();
            DalFunc.UserOff(User.Id);
            MainWindow.exit = true;
            Close();
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
            StringToCheckCPU(ref producer, ref core, ref socket, ref video);

            List<CpuFromShop> BindingCpu = ListCpu.Where(x =>
                                      core.IndexOf(x.Core.ToString()) >= 0
                                      && producer.IndexOf(x.Producer.Name) >= 0
                                      && socket.IndexOf(x.CpuSocket) >= 0
                                      && video.IndexOf(x.Video) >= 0).ToList();
            CpusDataGrid.ItemsSource = BindingCpu;
        }
        /// <summary>
        /// collect information about the selected CheckBox Cpu
        /// </summary>
        /// <param name="producer"></param>
        /// <param name="core"></param>
        /// <param name="socket"></param>
        /// <param name="video"></param>
        private void StringToCheckCPU
            (ref string producer, ref string core, ref string socket, ref string video)
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

            StringToCheckMotherBoard(ref producer, ref chipset, ref socket, ref ram);

            List<MBFromShop> BindingMB = ListMotherBoard.Where(x =>
                                      ram.IndexOf(x.RAM.ToString()) >= 0
                                      && producer.IndexOf(x.Producer.Name) >= 0
                                      && socket.IndexOf(x.MBSocket) >= 0
                                      && chipset.IndexOf(x.ChipSet) >= 0).ToList();
            MotherBDataGrid.ItemsSource = BindingMB;
        }
        /// <summary>
        /// collect information about the selected CheckBox MotherBoard
        /// </summary>
        /// <param name="producer"></param>
        /// <param name="chipset"></param>
        /// <param name="socket"></param>
        /// <param name="ram"></param>
        private void StringToCheckMotherBoard
          (ref string producer, ref string chipset, ref string socket, ref string ram)
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
        }
        /// <summary>
        /// Find and Display Item by Name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Find_Click(object sender, RoutedEventArgs e)
        {
            if (MyTabContol.SelectedIndex == 0)
            {
                CpusDataGrid.ItemsSource = ListCpu.Where
                                             (x => x.Name.IndexOf
                                             (FindBox.Text, 0, StringComparison.OrdinalIgnoreCase)
                                             >= 0).ToList();
            }
            else if (MyTabContol.SelectedIndex == 1)
            {
                MotherBDataGrid.ItemsSource = ListMotherBoard.Where
                                                (x => x.Name.IndexOf
                                                (FindBox.Text, 0, StringComparison.OrdinalIgnoreCase)
                                                >= 0).ToList();
            }
        }
        /// <summary>
        /// update data when changing TabItem
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_SelectionChanged
            (object sender, SelectionChangedEventArgs e)
        {
            CpuCoreTree.Items.Clear();
            CpuProducerTree.Items.Clear();
            CpuSocketTree.Items.Clear();
            CpuVideoTree.Items.Clear();
            MotherBChipSetTree.Items.Clear();
            MotherBProducerTree.Items.Clear();
            MotherBSocketTree.Items.Clear();
            MotherBTypeRamTree.Items.Clear();
            AddBoxCpu();
            AddBoxMB();

            CpusDataGrid.ItemsSource = ListCpu;
            MotherBDataGrid.ItemsSource = ListMotherBoard;
            FindBox.Text = string.Empty;
        }
        /// <summary>
        /// setting the maximum value for item sales
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Selecteditem(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as DataGrid).SelectedIndex >= 0)
            {
                int TotalQuntity = 0;

                TotalQuntity = ((sender as DataGrid).SelectedItem as ItemFromShop).Quantity;

                int SaleQuantity = 0;
                if (saleItems.Count > 0)
                {
                    var saleitem = saleItems.Where(x => x.ItemFromShop.Id ==
                                                   ((sender as DataGrid).SelectedItem as ItemFromShop).Id)
                                                   .SingleOrDefault();
                    if (saleitem != null)
                    {
                        SaleQuantity = saleitem.Quantity;
                    }
                }
                QuantityNumber.Maximum = (TotalQuntity - SaleQuantity) * 1.0;
              
            }
            QuantityNumber.Value = 0;
        }
        /// <summary>
        /// adding an item to the price list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddPriceList(object sender, RoutedEventArgs e)
        {
            DALFunction dALFunction = new DALFunction();
            List<ItemFromShop> TmpList = new List<ItemFromShop>();
            TmpList.AddRange(ListCpu);
            TmpList.AddRange(ListMotherBoard);

            foreach (TabItem item in MyTabContol.Items)
            {
                if (item.IsSelected && QuantityNumber.Value >= 1)
                {
                    foreach (var dataitem in (item.Content as Grid).Children)
                    {
                        if (dataitem is DataGrid)
                        {
                            var ItemShop = ((dataitem as DataGrid).SelectedItem as ItemFromShop);

                            if (saleItems.Where(x => x.ItemFromShop.Id == ItemShop.Id)
                                                            .FirstOrDefault() != null)
                            {
                                var SaleItemQ = saleItems.Single(x => x.ItemFromShop.Id == ItemShop.Id).Quantity;
                                if ((SaleItemQ += (int)QuantityNumber.Value) <= ItemShop.Quantity)
                                {
                                    saleItems.Single(x => x.ItemFromShop.Id == ItemShop.Id).Quantity
                                         += (int)QuantityNumber.Value;
                                }
                                else
                                {
                                    MessageBox.Show("test3");
                                }
                            }
                            else
                            {
                                saleItems.Add(new SaleItem
                                {
                                    Quantity = (int)QuantityNumber.Value,
                                    TimeSale = DateTime.Now.Date,
                                    ItemFromShop = TmpList.Single(x => x.Id == ItemShop.Id)
                                });
                            }
                            //Selecteditem(dataitem, null);
                        }
                    }
                }
            }
            SalaryDataGrid.ItemsSource = null;
            SalaryDataGrid.ItemsSource = saleItems;
        }
        /// <summary>
        /// Clear Salery List
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearSaleryList(object sender, RoutedEventArgs e)
        {
            saleItems.Clear();
            SalaryDataGrid.ItemsSource = null;
        }
        /// <summary>
        /// Delete Item with salery list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            if (SalaryDataGrid.SelectedItem != null)
            {
                var Id = (SalaryDataGrid.SelectedItem as SaleItem).ItemFromShop.Id;
                var DeleteItem = saleItems.Where(x => x.ItemFromShop.Id == Id).Single();
                saleItems.Remove(DeleteItem);
            }
            SalaryDataGrid.ItemsSource = null;
            SalaryDataGrid.ItemsSource = saleItems;
        }
        /// <summary>
        /// printed a check and sold items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Print(object sender, RoutedEventArgs e)
        {
            string Bill = string.Empty;
            if (saleItems.Count > 0)
            {
                SaveFileDialog SaveSaleList = new SaveFileDialog();
                if (SaveSaleList.ShowDialog() == true)
                {
                    Bill += DateTime.Now;
                    Bill += "\n";
                    int i = 1;
                    foreach (var item in saleItems)
                    {
                        Bill += $"  {i}:  Name: {item.ItemFromShop.Name} Producer:" +
                            $" {item.ItemFromShop.Producer.Name}" +
                            $" Quantity: {item.Quantity} Price:" +
                            $" {item.Quantity * item.ItemFromShop.SalaryPrice} \n";
                    }
                    File.WriteAllText(SaveSaleList.FileName, Bill);
                    DALFunction DalFunc = new DALFunction();
                    DalFunc.SaleItems(saleItems);
                    SalaryDataGrid.ItemsSource = null;
                    SellerWindoowLoaded(null, null);
                }
            }

        }
        /// <summary>
        /// refresh all TabItems and TreeView
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Refresh(object sender, RoutedEventArgs e)
        {
            SellerWindoowLoaded(null, null);
        }
    }
}