using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTODal;
using AutoMapper;
using DAL;
namespace DAL.Conversion
{
    public class ConvertToEntity
    {
        public static User DalUserToUser(DalUser dalUser)
        {
            User user = new User();
            Shop shop = new Shop();
            Mapper.Initialize(Cfg => Cfg.CreateMap<DalUser, User>()
                  .ForMember("Post", opt => opt.MapFrom(x => shop.Posts.
                               Where(p => p.Name == dalUser.Post).Single())));
            user = Mapper.Map<DalUser, User>(dalUser);
            return user;
        }

        public static Producer DalProducerToProducer(DalProducer dalProducer)
        {
            Producer producer = new Producer();
            Mapper.Initialize(Cfg => Cfg.CreateMap<DalProducer, Producer>()
                    .ForMember("ItemFromProviders", opt => new List<ItemFromProvider>())
                    .ForMember("ItemFromShops", opt => new List<ItemFromShop>()));
            producer = Mapper.Map<DalProducer, Producer>(dalProducer);
            return producer;
        }

        public static Provider DalProviderToProvider(DalProvider dalProvider)
        {
            Provider provider = new Provider();
            Mapper.Initialize(Cfg => Cfg.CreateMap<DalProvider, Provider>()
                .ForMember("ItemFromProviders", opt => new List<ItemFromProvider>()));
            provider = Mapper.Map<DalProvider, Provider>(dalProvider);
            return provider;
        }

        public static ItemFromProvider
            DAlItemProviderToItemProvider(DalItemFromProvider DalItem)
        {
            ItemFromProvider item = null;
            if (DalItem is DalCpuFromProvider)
            {
                item = DalCpuProviderToCpuProvider
                    (DalItem as DalCpuFromProvider);
            }
            else if (DalItem is DalMotherBoardFromProvider)
            {
                item = DalCpuProviderToCpuProvider
                    (DalItem as DalMotherBoardFromProvider);
            }
            return item;
        }

        public static CpuFromProvider
            DalCpuProviderToCpuProvider(DalCpuFromProvider DalCpu)
        {
            Shop shop = new Shop();
            CpuFromProvider Cpu = new CpuFromProvider();
            Mapper.Initialize(Cfg => Cfg.
                CreateMap<DalCpuFromProvider, CpuFromProvider>().
                    ForMember("Producer", opt =>
                        shop.Producers.Where(x => x.Name == DalCpu.Producer).Single()).
                    ForMember("Provider", opt =>
                         shop.Providers.Where(x => x.Name == DalCpu.Provider).Single()));
            Cpu = Mapper.Map<DalCpuFromProvider, CpuFromProvider>(DalCpu);
            return Cpu;
        }

        public static MotherBoardFromProvider
            DalCpuProviderToCpuProvider(DalMotherBoardFromProvider dalMB)
        {
            Shop shop = new Shop();
            MotherBoardFromProvider MB = new MotherBoardFromProvider();
            Mapper.Initialize(Cfg => Cfg.
                CreateMap<DalMotherBoardFromProvider, MotherBoardFromProvider>().
                    ForMember("Producer", opt =>
                        shop.Producers.Where(x => x.Name == dalMB.Producer).Single()).
                    ForMember("Provider", opt =>
                        shop.Providers.Where(x => x.Name == dalMB.Provider).Single()));
            MB = Mapper.Map<DalMotherBoardFromProvider, MotherBoardFromProvider>(dalMB);
            return MB;
        }

        public static BuyItem
            DalBuyItemToBuyItem(DalBuyItem dalBuyItem)
        {
            Shop shop = new Shop();
            BuyItem buyItem = new BuyItem();
            Mapper.Initialize(Cfg => Cfg.CreateMap<DalBuyItem, BuyItem>()
                .ForMember("DalItemFromProvider", opt =>
                     shop.ItemFromProviders.Where(x =>
                        x.Id == dalBuyItem.DalItemFromProvider.Id).Single()));
            buyItem = Mapper.Map<DalBuyItem, BuyItem>(dalBuyItem);
            return buyItem;
        }

        public static ItemFromShop
            DalItemShopToItemShop(DalItemFromShop dalitem)
        {
            ItemFromShop item = null;
            if (dalitem is DalCpuFromShop)
            {
                item = DalCpuShopToCpuShop(dalitem as DalCpuFromShop);
            }
            else if (dalitem is DalMotherBoardFromShop)
            {
                item = DalMBShopToMBShop(dalitem as DalMotherBoardFromShop);
            }
            return item;
        }

        public static CpuFromShop
            DalCpuShopToCpuShop(DalCpuFromShop dalcpu)
        {
            CpuFromShop cpu = new CpuFromShop();
            Shop shop = new Shop();
            Mapper.Initialize(Cfg => Cfg.CreateMap<DalCpuFromShop, CpuFromShop>().
                ForMember("Producer", opt =>
                     shop.Producers.Where(x => x.Name == dalcpu.Producer).Single()));
            cpu = Mapper.Map<DalCpuFromShop, CpuFromShop>(dalcpu);
            return cpu;
        }

        public static MotherBoardFromShop
            DalMBShopToMBShop(DalMotherBoardFromShop DalMB)
        {
            MotherBoardFromShop MBShop = null;
            Shop shop = new Shop();
            Mapper.Initialize(Cfg => Cfg.CreateMap<DalMotherBoardFromShop, MotherBoardFromShop>().
                ForMember("Producer", opt =>
                     shop.Producers.Where(x => x.Name == DalMB.Producer).Single()));
            MBShop = Mapper.Map<DalMotherBoardFromShop, MotherBoardFromShop>(DalMB);
            return MBShop;
        }

        public static SaleItem
            DalSaleToSase(DalSaleItem dalSaleItem)
        { 
            SaleItem saleItem = null;
            Shop shop = new Shop();
            Mapper.Initialize(Cfg => Cfg.CreateMap<DalSaleItem, SaleItem>().
                ForMember("ItemFromShop", opt =>
                     shop.ItemFromShops.Where(x => x.Id == dalSaleItem.ItemFromShop.Id).Single()));
            saleItem = Mapper.Map<DalSaleItem, SaleItem>(dalSaleItem);
            return saleItem;
        }
    }
}