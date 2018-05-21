using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BllDTO;
namespace BLL
{
    public class BllLogic
    {
        Function DalFunction = new Function();

        public DTOUser GetUser(string login, string Password)
        {
            User Tmpuser = DalFunction.GetUser(login, Password);
            DTOUser GetUser = null;
            if (Tmpuser != null)
            {
                GetUser = new DTOUser
                {
                    Id = Tmpuser.Id,
                    Login = Tmpuser.Login,
                    Password = Tmpuser.Password,
                    Name = Tmpuser.Name,
                    Surname = Tmpuser.Surname,
                    Online = Tmpuser.Online,
                    Post = new DTOPost
                    {
                        Id = Tmpuser.Post.Id,
                        Name = Tmpuser.Post.Name,
                        Salary = Tmpuser.Post.Salary
                    },                 
                };
                if (Tmpuser.Provider != null)
                {
                    GetUser.DTOProvider = new DTOProvider
                    {
                        Id = Tmpuser.Provider.Id,
                        Name = Tmpuser.Provider.Name
                    };
                }
            }
           
            return GetUser;
        }

        public List<DTOItemFromShop> GetItemsFromShop()
        {
            List<DTOItemFromShop> GetListItems = new List<DTOItemFromShop>();
            List<ItemFromShop> TmpListItems = DalFunction.GetItemsFromShop();
            List<CpuFromShop> TmpListCPU
                = TmpListItems.Where(x => x is CpuFromShop)
                                    .Select(x => (x as CpuFromShop)).ToList();
            List<MotherBoardFromShop> TmpListMB
                = TmpListItems.Where(x =>
                                     x is MotherBoardFromShop)
                                         .Select(x =>
                                             (x as MotherBoardFromShop)).ToList();

            GetListItems.AddRange(ConvertCpuToDTOCpu(TmpListCPU));
            GetListItems.AddRange(ConvertMBToDTOMB(TmpListMB));
            return GetListItems;
        }

        private List<DTOItemFromShop> ConvertCpuToDTOCpu(List<CpuFromShop> TmpList)
        {
            List<DTOItemFromShop> GetList = new List<DTOItemFromShop>();
            GetList.AddRange(TmpList.Select(x => new DTOCpuFromShop
            {
                Id = x.Id,
                Name = x.Name,
                SalaryPrice = x.SalaryPrice,
                Quantity = x.Quantity,
                Cash = x.Cash,
                Core = x.Core,
                CpuSocket = x.CpuSocket,
                Frequency = x.Frequency,
                Image = x.Image,
                Threads = x.Threads,
                Video = x.Video,
                Producer = new DTOProducer
                {
                    Id = x.Producer.Id,
                    Name = x.Producer.Name
                }
            }
            ).ToList());

            return GetList;
        }

        private List<DTOItemFromShop>
            ConvertMBToDTOMB(List<MotherBoardFromShop> TmpList)
        {
            List<DTOItemFromShop> GetList = new List<DTOItemFromShop>();
            GetList.AddRange(TmpList.Select(x => new DTOMotherBoardFromShop
            {
                Id = x.Id,
                Name = x.Name,
                SalaryPrice = x.SalaryPrice,
                Quantity = x.Quantity,
                Image = x.Image,
                ChipSet = x.ChipSet,
                MBSocket = x.MBSocket,
                PciE = x.PciE,
                RAM = x.RAM,
                USB = x.USB,
                Producer = new DTOProducer
                {
                    Id = x.Producer.Id,
                    Name = x.Producer.Name
                }
            }
           ).ToList());

            return GetList;
        }

        private ItemFromShop ConvertToItemFromShop(DTOItemFromShop Item)
        {
            ItemFromShop GetItem = null;
            if (Item is DTOCpuFromShop)
            {
                GetItem = ConvertToItemFromShop(Item as DTOCpuFromShop);
            }
            else if (Item is DTOMotherBoardFromShop)
            {
                GetItem = ConvertToMBFromShop(Item as DTOMotherBoardFromShop);
            }
            return GetItem;
        }

        private ItemFromShop ConvertToItemFromShop(DTOCpuFromShop DTOCpu)
        {
            return new CpuFromShop
            {
                Id = DTOCpu.Id,
                Name = DTOCpu.Name,
                SalaryPrice = DTOCpu.SalaryPrice,
                Quantity = DTOCpu.Quantity,
                Cash = DTOCpu.Cash,
                Core = DTOCpu.Core,
                CpuSocket = DTOCpu.CpuSocket,
                Frequency = DTOCpu.Frequency,
                Image = DTOCpu.Image,
                Threads = DTOCpu.Threads,
                Video = DTOCpu.Video,
                Producer = new Producer
                {
                    Id = DTOCpu.Producer.Id,
                    Name = DTOCpu.Name
                }
            };
        }

        private ItemFromShop ConvertToMBFromShop(DTOMotherBoardFromShop DTOMB)
        {
            return new MotherBoardFromShop
            {
                Id = DTOMB.Id,
                ChipSet = DTOMB.ChipSet,
                Image = DTOMB.Image,
                MBSocket = DTOMB.MBSocket,
                RAM = DTOMB.RAM,
                Name = DTOMB.Name,
                PciE = DTOMB.PciE,
                Quantity = DTOMB.Quantity,
                SalaryPrice = DTOMB.SalaryPrice,
                USB = DTOMB.USB,
                Producer = new Producer
                {
                    Id = DTOMB.Producer.Id,
                    Name = DTOMB.Name
                }
            };

        }

        private SaleItem ConvertToSaleItem(DTOSaleItem SaleItem)
        {
            return new SaleItem
            {
                Id = SaleItem.Id,
                Quantity = SaleItem.Quantity,
                TimeSale = SaleItem.TimeSale,
                ItemFromShop = ConvertToItemFromShop(SaleItem.ItemFromShop)
            };

        }

        public void UserOff(int Id)
        {
            DalFunction.UserOff(Id);
        }

        public void UserOn(int Id)
        {
            DalFunction.UserOn(Id);
        }

        public void SaleItems(List<DTOSaleItem> DTOSaleItems)
        {
            List<SaleItem> SaleItems = DTOSaleItems.Select(x => ConvertToSaleItem(x)).ToList();
            DalFunction.SaleItem(SaleItems);
        }

        public List<DTOItemFromProvider> GetDTOItemFromProviders()
        {
            return DalFunction.GetItemFromProviders().Select(x => ConvertToDTOItemProvider(x)).ToList();
        }

        public DTOItemFromProvider ConvertToDTOItemProvider(ItemFromProvider Item)
        {
            DTOItemFromProvider GetItem = null;
            if (Item is CpuFromProvider)
            {
                GetItem = ConvertToDTOCPUProvider(Item as CpuFromProvider);
            }
            else if (Item is MotherBoardFromProvider)
            {
                GetItem = ConvertToDTOMBProvider(Item as MotherBoardFromProvider);
            }
            return GetItem;
        }

        public DTOMotherBoardFromProvider ConvertToDTOMBProvider(MotherBoardFromProvider Item)
        {
            return new DTOMotherBoardFromProvider
            {
                Id = Item.Id,
                Name = Item.Name,
                PciE = Item.PciE,
                RAM = Item.RAM,
                BuyPrice = Item.BuyPrice,
                ChipSet = Item.ChipSet,
                Image = Item.Image,
                MBSocket = Item.MBSocket,
                USB = Item.USB,
                Provider = new DTOProvider
                {
                    Id = Item.Provider.Id,
                    Name = Item.Provider.Name
                },
                Producer = new DTOProducer
                {
                    Id = Item.Producer.Id,
                    Name = Item.Producer.Name
                }
            };
        }

        public DTOCpuFromProvider ConvertToDTOCPUProvider(CpuFromProvider Item)
        {
            return new DTOCpuFromProvider
            {
                Id = Item.Id,
                Name = Item.Name,
                BuyPrice = Item.BuyPrice,
                Image = Item.Image,
                Cash = Item.Cash,
                Core = Item.Core,
                CpuSocket = Item.CpuSocket,
                Frequency = Item.Frequency,
                Threads = Item.Threads,
                Video = Item.Video,
                Provider = new DTOProvider
                {
                    Id = Item.Provider.Id,
                    Name = Item.Provider.Name
                },
                Producer = new DTOProducer
                {
                    Id = Item.Producer.Id,
                    Name = Item.Producer.Name
                }
            };
        }

        public void BuyItem(List<DTOBuyItem> DTOBuyItems)
        {
            DalFunction.BuyItems(DTOBuyItems.Select(x => ConvertToBuyItem(x)).ToList());
        }

        public BuyItem ConvertToBuyItem(DTOBuyItem Item)
        {
            return new BuyItem
            {
                Quantity = Item.Quantity,
                TimeBuy = Item.TimeBuy,
                ItemFromProvider = ConvertToItemProvider(Item.ItemFromProvider)
            };
        }

        public ItemFromProvider ConvertToItemProvider(DTOItemFromProvider Item)
        {
            ItemFromProvider GetItem = null;
            if(Item is DTOCpuFromProvider)
            {
                GetItem = ConvertToCpuProvider(Item as DTOCpuFromProvider);
            }
            if (Item is DTOMotherBoardFromProvider)
            {
                GetItem = ConvertToMotherBProvider(Item as DTOMotherBoardFromProvider);
            }
            return GetItem;
        }

        public CpuFromProvider ConvertToCpuProvider(DTOCpuFromProvider Item)
        {
            return new CpuFromProvider
            {
                Id = Item.Id,
                Name = Item.Name,
                BuyPrice= Item.BuyPrice,
                Image= Item.Image,
                Threads= Item.Threads,
                Cash= Item.Cash,
                Core= Item.Core,
                CpuSocket= Item.CpuSocket,
                Frequency= Item.Frequency,
                Video= Item.Video,
                Producer= new Producer
                {
                    Id= Item.Producer.Id,
                    Name= Item.Producer.Name
                },
                Provider = new Provider
                {
                    Id= Item.Provider.Id,
                    Name= Item.Provider.Name
                }
            };
        }

        public MotherBoardFromProvider ConvertToMotherBProvider(DTOMotherBoardFromProvider Item)
        {
            return new MotherBoardFromProvider
            {
                Id = Item.Id,
                Name = Item.Name,
                BuyPrice = Item.BuyPrice,
                Image = Item.Image,
                RAM = Item.RAM,
                ChipSet = Item.ChipSet,
                MBSocket = Item.MBSocket,
                PciE = Item.PciE,
                USB = Item.USB,
                Producer = new Producer
                {
                    Id = Item.Producer.Id,
                    Name = Item.Producer.Name
                },
                Provider = new Provider
                {
                    Id = Item.Provider.Id,
                    Name = Item.Provider.Name
                }
            };
        }
    }
}
