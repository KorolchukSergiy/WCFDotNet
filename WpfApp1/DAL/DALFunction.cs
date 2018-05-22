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

        /// <summary>
        /// returns the information of the authorized user. 
        /// if the wrong login is entered or the password returns null
        /// </summary>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>

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
        /// <summary>
        /// returns a list of Cpu that are in the store database 
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// returns a list of MotherBoard that are in the store database
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Convert byte array to BitmapImage
        /// </summary>
        /// <param name="image"></param>
        /// <returns></returns>
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

        /// <summary>
        /// gives the user status offline status
        /// </summary>
        /// <param name="Id"></param>
        public void UserOff(int Id)
        {
            _service.UserOff(Id);
        }

        /// <summary>
        /// gives the user status online status
        /// </summary>
        /// <param name="Id"></param>
        public void UserOn(int Id)
        {
            _service.UserOn(Id);
        }

        /// <summary>
        /// Convert BitmapImage to byte array
        /// </summary>
        /// <param name="Image"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert CpuFromShop with Dal model to Wcf DataContract
        /// </summary>
        /// <param name="CPU"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert MotherBoardFromShop with Dal model to Wcf DataContract
        /// </summary>
        /// <param name="MB"></param>
        /// <returns></returns>
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

        /// <summary>
        /// checks type and  Convert ItemFromShop with Dal model to Wcf DataContract
        /// </summary>
        /// <param name="SaleItem"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert SaleItem with Dal model to Wcf DataContract
        /// </summary>
        /// <param name="SaleItem"></param>
        /// <returns></returns>
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

        /// <summary>
        /// transfer to WCF sheet of items for sale
        /// </summary>
        /// <param name="SaleItems"></param>
        public void SaleItems(List<SaleItem> SaleItems)
        {
            List<DCSaleItem> DCSaleItems = SaleItems.Select(x => ConvertToDCSaleItems(x)).ToList();
            _service.SaleItems(DCSaleItems.ToArray());
        }

        /// <summary>
        /// returns a list of ItemFromProvider that are in the database
        /// </summary>
        /// <returns></returns>
        public List<ItemFromProvider> GetItemsFromProvider()
        {
            return _service.GetItemsFromProvider().Select(x => ConvertToItemProvider(x)).ToList();
        }

        /// <summary>
        /// checks type and  Convert ItemFromProvider with Wcf DataContract to Dal Model
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert CpuFromProvider with Wcf DataContract to Dal Model
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert MotherBoardFromProvider with Wcf DataContract to Dal Model
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
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

        /// <summary>
        /// transfer to WCF sheet of items for Buy
        /// </summary>
        /// <param name="BuyItems"></param>
        public void BuyItem(List<BuyItem> BuyItems)
        {
            _service.BuyItems(BuyItems.Select(x=> ConvertToDCBuyItem(x)).ToArray());
        }

        /// <summary>
        /// Convert BuyItem with Dal Model to DataContract WCf
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
        public DCBuyItem ConvertToDCBuyItem(BuyItem Item)
        {
            return new DCBuyItem
            {
                Quantity = Item.Quantity,
                TimeBuy = Item.TimeBuy,
                ItemFromProvider = ConvertToItemProvider(Item.ItemFromProvider)
            };
        }

        /// <summary>
        /// checks type and Convert ItemFromProvider with Dal Model to Wcf DataContract
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert CpuFromProvider with Dal Model to Dal Model Wcf DataContract
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert MotherBoardFromProvider with Dal Model to Wcf DataContract
        /// </summary>
        /// <param name="Item"></param>
        /// <returns></returns>
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
