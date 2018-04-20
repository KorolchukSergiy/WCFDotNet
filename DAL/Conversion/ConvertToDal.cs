using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTODal;
using AutoMapper;

namespace DAL.Conversion
{
    public class EntityConvertToDTO
    {
        public static DalUser UserToDalUser(User user)
        {
            DalUser GetUser = new DalUser()
            {
                Id = user.Id,
                Login = user.Login,
                Password = user.Password,
                Name = user.Name,
                Surname = user.Surname,
                Post = user.Post.Name,
                Online=user.Online
            };
            return GetUser;
        }

        public static DalCpuFromShop
            CpuToDalCpuShop(CpuFromShop Cpu)
        {
            DalCpuFromShop GetCpu = new DalCpuFromShop()
            {
                Id = Cpu.Id,
                Name = Cpu.Name,
                Producer = Cpu.Producer.Name,
                Quantity = Cpu.Quantity,
                SalaryPrice = Cpu.SalaryPrice,
                Cash = Cpu.Cash,
                Core = Cpu.Core,
                Threads = Cpu.Threads,
                Frequency = Cpu.Frequency,
                Image = Cpu.Image,
                Video = Cpu.Video
            };
            return GetCpu;
        }

        static public DalPost PostToDalPost(Post post)
        {
            DalPost GetBllPost = new DalPost
            {
                Id = post.Id,
                Name = post.Name,
                Salary = post.Salary
            };
            return GetBllPost;
        }

        static public DalMotherBoardFromShop
            MBFromShopToDalMbFromShop(MotherBoardFromShop MBShop)
        {
            DalMotherBoardFromShop DalMBFromShop
                = new DalMotherBoardFromShop
                {
                    Id = MBShop.Id,
                    Name = MBShop.Name,
                    ChipSet = MBShop.ChipSet,
                    Image = MBShop.Image,
                    PciE = MBShop.PciE,
                    RAM = MBShop.RAM,
                    Producer = MBShop.Producer.Name,
                    Quantity = MBShop.Quantity,
                    SalaryPrice = MBShop.SalaryPrice,
                    MBSocket = MBShop.MBSocket,
                    USB = MBShop.USB,
                };
            return DalMBFromShop;
        }

        public static DalMotherBoardFromProvider
            MBproviderToDalMBProvider(MotherBoardFromProvider MBProvider)
        {
            DalMotherBoardFromProvider DalMBProvider =
                new DalMotherBoardFromProvider
                {
                    Id = MBProvider.Id,
                    Name = MBProvider.Name,
                    ChipSet = MBProvider.ChipSet,
                    Image = MBProvider.Image,
                    PciE = MBProvider.PciE,
                    RAM = MBProvider.RAM,
                    Producer = MBProvider.Producer.Name,
                    MBSocket = MBProvider.MBSocket,
                    USB = MBProvider.USB,
                    BuyPrice = MBProvider.BuyPrice,
                    Provider = MBProvider.Provider.Name
                };
            return DalMBProvider;
        }

        public static DalCpuFromProvider
            CpuProviderToDalCpuProvider(CpuFromProvider CpuProvider)
        {
            DalCpuFromProvider DalCpuProvider =
                new DalCpuFromProvider
                {
                    Id = CpuProvider.Id,
                    Name = CpuProvider.Name,
                    Producer = CpuProvider.Producer.Name,
                    Cash = CpuProvider.Cash,
                    Core = CpuProvider.Core,
                    Threads = CpuProvider.Threads,
                    Frequency = CpuProvider.Frequency,
                    Image = CpuProvider.Image,
                    CpuSocket = CpuProvider.CpuSocket,
                    Video = CpuProvider.Video,
                    Provider = CpuProvider.Provider.Name
                };
            return DalCpuProvider;
        }

        static public DalProducer
            ProducerToDalProducer(Producer producer)
        {
            DalProducer dalProducer = new DalProducer
            {
                Id = producer.Id,
                Name = producer.Name
            };
            return dalProducer;
        }

        private static DalItemFromProvider
            ItemProviderToDalItemProvider(ItemFromProvider x)
        {
            DalItemFromProvider dalItemFromProvider = null;
            if (x is CpuFromProvider)
            {
                dalItemFromProvider =
                    CpuProviderToDalCpuProvider((x as CpuFromProvider));
            }
            else if (x is MotherBoardFromProvider)
            {
                dalItemFromProvider =
                    MBproviderToDalMBProvider((x as MotherBoardFromProvider));
            }
            return dalItemFromProvider;
        }

        private static DalItemFromShop
            ItemShopToDalItemShop(ItemFromShop x)
        {
            DalItemFromShop dalItemFromShop = null;
            if (x is CpuFromShop)
            {
                dalItemFromShop =
                    CpuToDalCpuShop((x as CpuFromShop));
            }
            else if (x is MotherBoardFromShop)
            {
                dalItemFromShop =
                    MBFromShopToDalMbFromShop((x as MotherBoardFromShop));
            }
            return dalItemFromShop;
        }

        public static DalProvider
            ProviderToDalProvider(Provider provider)
        {
            DalProvider dalProvider = new DalProvider()
            {
                Id = provider.Id,
                Name = provider.Name,
            };
            return dalProvider;
        }

        public static DalSaleItem
            SaleItemToDalSaleItem(SaleItem saleItem)
        {
            DalSaleItem dalSaleItem = new DalSaleItem();
            Mapper.Initialize(Cfg => Cfg.CreateMap<SaleItem, DalSaleItem>()
                            .ForMember("ItemFromShop", opt => opt.
                                 MapFrom(c => ItemShopToDalItemShop(c.ItemFromShop))));
            dalSaleItem = Mapper.Map<SaleItem, DalSaleItem>(saleItem);
            return dalSaleItem;
        }

        public static DalBuyItem
            BuyItemToDalBuyItem(BuyItem buyItem)
        {
            DalBuyItem dalBuyItem = new DalBuyItem();
            Mapper.Initialize(Cfg => Cfg.CreateMap<BuyItem, DalBuyItem>()
                           .ForMember("ItemFromProvider", opt => opt.
                               MapFrom(c => ItemProviderToDalItemProvider(c.ItemFromProvider))));
            dalBuyItem = Mapper.Map<BuyItem, DalBuyItem>(buyItem);
            return dalBuyItem;
        }
    }
}