using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.DTODal;
namespace DAL.Conversion
{
    public class DalConvertToDTO
    {
        public static DalUser UserToDTODalUser(User user)
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

        public static DalCpuFromShop CpuToDTODalCpuShop(CpuFromShop Cpu)
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
                DalUsers = post.Users.Select(x => UserToDTODalUser(x)).ToList()
            };
            return GetBllPost;
        }

        static public DalMotherBoardFromShop MBFromShopToDalMbfromShop(MotherBoardFromShop MBShop)
        {
            DalMotherBoardFromShop DalMBFromShop = new DalMotherBoardFromShop
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

        static public DalProducer ProducerToDalProducer(Producer producer)
        {
            DalProducer dalProducer = new DalProducer
            {
                Id=producer.Id,
                Name=producer.Name,
                //ItemFromProviders=producer.ItemFromProviders.Select
            };
            return dalProducer;
        }
    }
}
