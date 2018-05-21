using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using BLL;
using BllDTO;
using WCFDotNet.DataContracts;

namespace WCFDotNet
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ShopServer" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ShopServer.svc or ShopServer.svc.cs at the Solution Explorer and start debugging.
    public class ShopServer : IShopServer
    {
        BllLogic bllLogic = new BllLogic();

        public List<DCItemFromShop> GetDCItemFromShop()
        {
            List<DCItemFromShop> GetListItems = new List<DCItemFromShop>();
            List<DTOItemFromShop> TmpListItems = bllLogic.GetItemsFromShop();
            List<DTOCpuFromShop> TmpListCPU
                = TmpListItems.Where(x => x is DTOCpuFromShop)
                                    .Select(x => (x as DTOCpuFromShop)).ToList();
            List<DTOMotherBoardFromShop> TmpListMB
                = TmpListItems.Where(x =>
                                     x is DTOMotherBoardFromShop)
                                         .Select(x =>
                                             (x as DTOMotherBoardFromShop)).ToList();

            GetListItems.AddRange(ConvertCpuToDTOCpu(TmpListCPU));
            GetListItems.AddRange(ConvertMBToDTOMB(TmpListMB));
            return GetListItems;
        }

        public DCUser GetUser(string login, string password)
        {
            DTOUser Tmpuser = bllLogic.GetUser(login, password);
            DCUser GetUser = null;
            if(Tmpuser!=null)
            {
                GetUser = new DCUser
                {
                    Id = Tmpuser.Id,
                    Login = Tmpuser.Login,
                    Password = Tmpuser.Password,
                    Name = Tmpuser.Name,
                    Surname = Tmpuser.Surname,
                    Online = Tmpuser.Online,
                    Post = new DCPost
                    {
                        Id = Tmpuser.Post.Id,
                        Name = Tmpuser.Post.Name,
                        Salary = Tmpuser.Post.Salary
                    },                  
                };
                if(Tmpuser.DTOProvider!=null)
                {
                    GetUser.Provider = new DCProvider
                    {
                        Id = Tmpuser.DTOProvider.Id,
                        Name = Tmpuser.DTOProvider.Name
                    };
                }
            }
            return GetUser;
        }

        private List<DCItemFromShop> ConvertCpuToDTOCpu(List<DTOCpuFromShop> TmpList)
        {
            List<DCItemFromShop> GetList = new List<DCItemFromShop>();
            GetList.AddRange(TmpList.Select(x => new DCCpuFromShop
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
                Producer = new DCProducer
                {
                    Id = x.Producer.Id,
                    Name = x.Producer.Name
                }
            }
            ).ToList());

            return GetList;
        }

        private List<DCItemFromShop>
            ConvertMBToDTOMB(List<DTOMotherBoardFromShop> TmpList)
        {
            List<DCItemFromShop> GetList = new List<DCItemFromShop>();
            GetList.AddRange(TmpList.Select(x => new DCMBFromShop
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
                Producer = new DCProducer
                {
                    Id = x.Producer.Id,
                    Name = x.Producer.Name
                }
            }
           ).ToList());

            return GetList;
        }

        public void UserOff(int Id)
        {
            bllLogic.UserOff(Id);
        }

        public void UserOn(int Id)
        {
            bllLogic.UserOn(Id);
        }

        private DTOItemFromShop ConvertToDTOItem(DCItemFromShop DCItem)
        {
            DTOItemFromShop GetItem = null;
            if(DCItem is DCCpuFromShop)
            {
                GetItem = ConvertToDTOCPUShop(DCItem as DCCpuFromShop);
            }
            if(DCItem is DCMBFromShop)
            {
                GetItem = ConvertToDTOMBShop(DCItem as DCMBFromShop);
            }
            return GetItem;
        }

        private DTOItemFromShop ConvertToDTOCPUShop(DCCpuFromShop DCCPU)
        {
            return new DTOCpuFromShop
            {
                Id = DCCPU.Id,
                Image = DCCPU.Image,
                Cash = DCCPU.Cash,
                Core = DCCPU.Core,
                CpuSocket = DCCPU.CpuSocket,
                Frequency = DCCPU.Frequency,
                Name = DCCPU.Name,
                Threads = DCCPU.Threads,
                Quantity = DCCPU.Quantity,
                SalaryPrice = DCCPU.SalaryPrice,
                Video = DCCPU.Video,
                Producer = new DTOProducer
                {
                    Id = DCCPU.Producer.Id,
                    Name = DCCPU.Producer.Name
                }
            };
        }
        
        private DTOMotherBoardFromShop ConvertToDTOMBShop(DCMBFromShop DCMB)
        {
            return new DTOMotherBoardFromShop
            {
                Id= DCMB.Id,
                Image=DCMB.Image,
                RAM =DCMB.RAM,
                ChipSet=DCMB.ChipSet,
                MBSocket=DCMB.MBSocket,
                Name=DCMB.Name,
                PciE=DCMB.PciE,
                Quantity=DCMB.Quantity,
                SalaryPrice=DCMB.SalaryPrice,
                USB=DCMB.USB,
                Producer= new DTOProducer
                {
                    Id =DCMB.Producer.Id,
                    Name=DCMB.Producer.Name,                
                }
            };

        }

        private DTOSaleItem ConvertToDTOSaleItem(DCSaleItem DCSaleItem)
        {
            return new DTOSaleItem
            {
                Id = DCSaleItem.Id,
                Quantity = DCSaleItem.Quantity,
                TimeSale = DCSaleItem.SaleTime,
                ItemFromShop = ConvertToDTOItem(DCSaleItem.ItemFromShop)
            };
        }

        public void SaleItems(List<DCSaleItem> DCSaleItems)
        {
            List<DTOSaleItem> DTOSaleItems = DCSaleItems.Select(x => ConvertToDTOSaleItem(x)).ToList();
            bllLogic.SaleItems(DTOSaleItems);
        }

        public List<DCItemFromProvider> GetItemsFromProvider()
        {
            return bllLogic.GetDTOItemFromProviders().Select(x => ConvertToDCItemProvider(x)).ToList();
        }

        public DCItemFromProvider ConvertToDCItemProvider(DTOItemFromProvider Item)
        {
            DCItemFromProvider GetItem = null;
            if(Item is DTOCpuFromProvider)
            {
                GetItem = ConvertToDCCpu(Item as DTOCpuFromProvider);
            }
            else if(Item is DTOMotherBoardFromProvider)
            {
                GetItem = ConvertToDCMB(Item as DTOMotherBoardFromProvider);
            }

            return GetItem;
        }

        public DCCpuFromProvider ConvertToDCCpu(DTOCpuFromProvider Item)
        {
            return new DCCpuFromProvider
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
                Provider = new DCProvider
                {
                    Id = Item.Provider.Id,
                    Name = Item.Provider.Name
                },
                Producer = new DCProducer
                {
                    Id = Item.Producer.Id,
                    Name = Item.Producer.Name
                }
            };
        }

        public DCMBFromProvider ConvertToDCMB(DTOMotherBoardFromProvider Item)
        {
            return new DCMBFromProvider
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
                Provider = new DCProvider
                {
                    Id = Item.Provider.Id,
                    Name = Item.Provider.Name
                },
                Producer = new DCProducer
                {
                    Id = Item.Producer.Id,
                    Name = Item.Producer.Name
                }
            };
        }
        
        public DCProvider ConvertToDCProvider(DTOProvider Provider)
        {
            DCProvider GetProvider = null;
            if (Provider != null)
            {
                GetProvider = new DCProvider
                {
                    Id = Provider.Id,
                    Name = Provider.Name
                };
            };
            return GetProvider;
        }

        public void BuyItems(DCBuyItem[] BuyItems)
        {
            bllLogic.BuyItem(BuyItems.Select(x => ConvertToBuyItem(x)).ToList());
        }

        public DTOBuyItem ConvertToBuyItem(DCBuyItem Item)
        {
            return new DTOBuyItem
            {
                Quantity = Item.Quantity,
                TimeBuy = Item.TimeBuy,
                ItemFromProvider = ConvertToItemProvider(Item.ItemFromProvider)
            };
        }

        public DTOItemFromProvider ConvertToItemProvider(DCItemFromProvider Item)
        {
            DTOItemFromProvider GetItem = null;
            if (Item is DCCpuFromProvider)
            {
                GetItem = ConvertToCpuProvider(Item as DCCpuFromProvider);
            }
            if (Item is DCMBFromProvider)
            {
                GetItem = ConvertToMotherBProvider(Item as DCMBFromProvider);
            }
            return GetItem;
        }

        public DTOCpuFromProvider ConvertToCpuProvider(DCCpuFromProvider Item)
        {
            return new DTOCpuFromProvider
            {
                Id= Item.Id,
                Name = Item.Name,
                BuyPrice = Item.BuyPrice,
                Image = Item.Image,
                Threads = Item.Threads,
                Cash = Item.Cash,
                Core = Item.Core,
                CpuSocket = Item.CpuSocket,
                Frequency = Item.Frequency,
                Video = Item.Video,
                Producer = new DTOProducer
                {
                    Id = Item.Producer.Id,
                    Name = Item.Producer.Name
                },
                Provider = new DTOProvider
                {
                    Id = Item.Provider.Id,
                    Name = Item.Provider.Name
                }
            };
        }

        public DTOMotherBoardFromProvider ConvertToMotherBProvider(DCMBFromProvider Item)
        {
            return new DTOMotherBoardFromProvider
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
                Producer = new DTOProducer
                {
                    Id = Item.Producer.Id,
                    Name = Item.Producer.Name
                },
                Provider = new DTOProvider
                {
                    Id = Item.Provider.Id,
                    Name = Item.Provider.Name
                }
            };
        }
    }
}
