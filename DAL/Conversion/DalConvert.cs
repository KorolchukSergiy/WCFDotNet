using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTODal;
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
                Post = user.Post.Name
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
                Socket = Cpu.CpuSocket,
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
                Salary = post.Salary,
                DalUsers = post.Users.Select(x => UserToDalUser(x)).ToList()
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
                    Socket = MBShop.MBSocket,
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
                    Provider= CpuProvider.Provider.Name
                };
            return DalCpuProvider;
        }

        static public DalProducer
            ProducerToDalProducer(Producer producer)
        {
            DalProducer dalProducer = new DalProducer
            {
                Id = producer.Id,
                Name = producer.Name,
                ItemFromProviders = producer.ItemFromProviders.Select
                (x => ItemProviderToDalItemProvider(x)).ToList(),
                ItemFromShops = producer.ItemFromShops.Select
                (x => ItemShopToDalItemShop(x)).ToList(),
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


    }
}
