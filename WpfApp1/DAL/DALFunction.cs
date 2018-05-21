using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using DAL.DataModel;
using DAL.ServiceNS;
using System.Windows.Media.Imaging;
using System.Windows;

namespace DAL
{
    public class DALFunction
    {
        private readonly ShopServerClient _service = new ShopServerClient();

        public User GetUser(string login, string password)
        {
            User Getuser = null;
            DCUser Tmpuser = null;
            Tmpuser = _service.GetUser(login, password);
            if (Tmpuser != null)
            {
                Getuser = new User
                {
                    Id = Tmpuser.Id,
                    Login = Tmpuser.Login,
                    Password = Tmpuser.Password,
                    Name = Tmpuser.Name,
                    Surname = Tmpuser.Surname,
                    Online = Tmpuser.Online,
                    Post = new Post
                    {
                        Id = Tmpuser.Post.Id,
                        Name = Tmpuser.Post.Name,
                        Salary = Tmpuser.Post.Salary
                    }
                };
                if (Tmpuser.Provider!=null)
                {
                    Getuser.Provider = new Provider
                    {
                        Id = Tmpuser.Provider.Id,
                        Name = Tmpuser.Provider.Name
                    };
                }
            }
           
            return Getuser;
        }

        public List<CpuFromShop> GetCpuFromShop()
        {
            List<CpuFromShop> GetList = new List<CpuFromShop>();
            List<DCCpuFromShop> TmpList = _service.GetDCItemFromShop()
                                           .Where(x => x is DCCpuFromShop)
                                           .Select(x => (x as DCCpuFromShop)).ToList();
            GetList.AddRange(TmpList.Select(x => new CpuFromShop
            {
                Id = x.Id,
                Name = x.Name,
                SalaryPrice = x.SalaryPrice,
                Threads = x.Threads,
                Cash = x.Cash,
                Core = x.Core,
                CpuSocket = x.CpuSocket,
                Frequency = x.Frequency,
                Quantity = x.Quantity,
                Video = x.Video,
                Producer = new Producer
                {
                    Id = x.Producer.Id,
                    Name = x.Producer.Name
                },
                Image = ConvertToImage(x.Image)
            }
            ).ToList());
            return GetList;
        }

        public List<MBFromShop> GetMBFromShop()
        {
           
            List<MBFromShop> GetList = new List<MBFromShop>();
            List<DCMBFromShop> TmpList = _service.GetDCItemFromShop()
                                           .Where(x => x is DCMBFromShop)
                                           .Select(x => (x as DCMBFromShop)).ToList();
           
            GetList.AddRange(TmpList.Select(x => new MBFromShop
            {
                Id = x.Id,
                Name = x.Name,
                SalaryPrice = x.SalaryPrice,
                ChipSet = x.ChipSet,
                MBSocket = x.MBSocket,
                PciE = x.PciE,
                RAM = x.RAM,
                USB = x.USB,
                Quantity = x.Quantity,
                Producer = new Producer
                {
                    Id = x.Producer.Id,
                    Name = x.Producer.Name
                },
                Image = ConvertToImage(x.Image)
            }
            ).ToList());

            return GetList;
        }

        public BitmapImage ConvertToImage(byte[] image)
        {
            BitmapImage GetImage = new BitmapImage();
            using (var ms = new MemoryStream(image))
            {
                GetImage.BeginInit();
                GetImage.CacheOption = BitmapCacheOption.OnLoad;
                GetImage.StreamSource = ms;
                GetImage.EndInit();
            }

            return GetImage;
        }

        public void UserOff(int Id)
        {
            _service.UserOff(Id);
        }

        public void UserOn(int Id)
        {
            _service.UserOn(Id);
        }

        private Byte[] ConvertImageToBytes(BitmapImage Image)
        {
            byte[] GetImage = null;
            JpegBitmapEncoder encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(Image));
            using (MemoryStream ms = new MemoryStream())
            {

                encoder.Save(ms);
                GetImage = ms.ToArray();
            }
            return GetImage;
        }

        private DCCpuFromShop ConvertToDCCPUShop(CpuFromShop CPU)
        {
            return new DCCpuFromShop
            {
                Id = CPU.Id,
                Quantity = CPU.Quantity,
                Cash = CPU.Cash,
                Core = CPU.Core,
                CpuSocket = CPU.CpuSocket,
                Frequency=CPU.Frequency,
                Image= ConvertImageToBytes(CPU.Image),
                Name=CPU.Name,
                SalaryPrice=CPU.SalaryPrice,
                Threads=CPU.Threads,
                Video=CPU.Video,
                Producer= new DCProducer
                {
                    Id=CPU.Producer.Id,
                    Name=CPU.Producer.Name
                }
            };
        }

        private DCMBFromShop ConvertToDCMBShop(MBFromShop MB)
        {
            return new DCMBFromShop
            {
                Id = MB.Id,
                RAM = MB.RAM,
                SalaryPrice = MB.SalaryPrice,
                ChipSet=MB.ChipSet,
                MBSocket=MB.MBSocket,
                Name=MB.Name,
                PciE=MB.PciE,
                Quantity=MB.Quantity,
                USB=MB.USB,
                Image= ConvertImageToBytes(MB.Image),
                Producer = new DCProducer
                {
                    Id=MB.Producer.Id,
                    Name =MB.Producer.Name
                }
            };         
        }

        private DCItemFromShop ConvertToDCItemShop(ItemFromShop SaleItem)
        {
            DCItemFromShop GetItem = null;
            if(SaleItem is CpuFromShop)
            {
                GetItem = ConvertToDCCPUShop(SaleItem as CpuFromShop);
            }
            else if(SaleItem is MBFromShop)
            {
                GetItem = ConvertToDCMBShop(SaleItem as MBFromShop);
            }
            return GetItem;
        }

        private DCSaleItem ConvertToDCSaleItems(SaleItem SaleItem)
        {
            return new DCSaleItem
            {
                Id = SaleItem.Id,
                Quantity = SaleItem.Quantity,
                SaleTime = SaleItem.TimeSale,
                ItemFromShop = ConvertToDCItemShop(SaleItem.ItemFromShop)
            };
        }

        public void SaleItems(List<SaleItem> SaleItems)
        {
            List<DCSaleItem> DCSaleItems = SaleItems.Select(x => ConvertToDCSaleItems(x)).ToList();
            _service.SaleItems(DCSaleItems.ToArray());
        }

        public List<ItemFromProvider> GetItemsFromProvider()
        {
            return _service.GetItemsFromProvider().Select(x => ConvertToItemProvider(x)).ToList();
        }

        public ItemFromProvider ConvertToItemProvider(DCItemFromProvider Item)
        {
            ItemFromProvider GetItem = null;
            if(Item is DCCpuFromProvider)
            {
                GetItem = ConvertToCpuProvider(Item as DCCpuFromProvider);
            }
            else if(Item is DCMBFromProvider)
            {
                GetItem = ConvertToMBProvider(Item as DCMBFromProvider);
            }
            return GetItem;
        }

        public CpuFromProvider ConvertToCpuProvider(DCCpuFromProvider Item)
        {
            return new CpuFromProvider
            {
                Id = Item.Id,
                Name = Item.Name,
                BuyPrice = Item.BuyPrice,
                Image = ConvertToImage(Item.Image),
                Cash = Item.Cash,
                Core = Item.Core,
                CpuSocket = Item.CpuSocket,
                Frequency = Item.Frequency,
                Threads = Item.Threads,
                Video = Item.Video,
                Provider = new Provider
                {
                    Id = Item.Provider.Id,
                    Name = Item.Provider.Name
                },
                Producer = new Producer
                {
                    Id = Item.Producer.Id,
                    Name = Item.Producer.Name
                }
            };
        }

        public MBFromProvider ConvertToMBProvider(DCMBFromProvider Item)
        {
            return new MBFromProvider
            {
                Id = Item.Id,
                Name = Item.Name,
                PciE = Item.PciE,
                RAM = Item.RAM,
                BuyPrice = Item.BuyPrice,
                ChipSet = Item.ChipSet,
                Image = ConvertToImage(Item.Image),
                MBSocket = Item.MBSocket,
                USB = Item.USB,
                Provider = new Provider
                {
                    Id = Item.Provider.Id,
                    Name = Item.Provider.Name
                },
                Producer = new Producer
                {
                    Id = Item.Producer.Id,
                    Name = Item.Producer.Name
                }
            };
        }

        public void BuyItem(List<BuyItem> BuyItems)
        {
            _service.BuyItems(BuyItems.Select(x=> ConvertToDCBuyItem(x)).ToArray());
        }

        public DCBuyItem ConvertToDCBuyItem(BuyItem Item)
        {
            return new DCBuyItem
            {
                Quantity = Item.Quantity,
                TimeBuy = Item.TimeBuy,
                ItemFromProvider = ConvertToItemProvider(Item.ItemFromProvider)
            };
        }

        public DCItemFromProvider ConvertToItemProvider(ItemFromProvider Item)
        {
            DCItemFromProvider GetItem = null;
            if (Item is CpuFromProvider)
            {
                GetItem = ConvertToCpuProvider(Item as CpuFromProvider);
            }
            if (Item is MBFromProvider)
            {
                GetItem = ConvertToMotherBProvider(Item as MBFromProvider);
            }
            return GetItem;
        }

        public DCCpuFromProvider ConvertToCpuProvider(CpuFromProvider Item)
        {
            return new DCCpuFromProvider
            {
                Id = Item.Id,
                Name = Item.Name,
                BuyPrice = Item.BuyPrice,
                Image = ConvertImageToBytes(Item.Image),
                Threads = Item.Threads,
                Cash = Item.Cash,
                Core = Item.Core,
                CpuSocket = Item.CpuSocket,
                Frequency = Item.Frequency,
                Video = Item.Video,
                Producer = new DCProducer
                {
                    Id = Item.Producer.Id,
                    Name = Item.Producer.Name
                },
                Provider = new DCProvider
                {
                    Id = Item.Provider.Id,
                    Name = Item.Provider.Name
                }
            };
        }

        public DCMBFromProvider ConvertToMotherBProvider(MBFromProvider Item)
        {
            return new DCMBFromProvider
            {
                Id = Item.Id,
                Name = Item.Name,
                BuyPrice = Item.BuyPrice,
                Image = ConvertImageToBytes(Item.Image),
                RAM = Item.RAM,
                ChipSet = Item.ChipSet,
                MBSocket = Item.MBSocket,
                PciE = Item.PciE,
                USB = Item.USB,
                Producer = new DCProducer
                {
                    Id = Item.Producer.Id,
                    Name = Item.Producer.Name
                },
                Provider = new DCProvider
                {
                    Id = Item.Provider.Id,
                    Name = Item.Provider.Name
                }
            };
        }
    }
}
