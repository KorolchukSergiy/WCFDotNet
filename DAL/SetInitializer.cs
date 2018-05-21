using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Drawing;
using System.Windows;
using Microsoft.Win32;

namespace DAL
{
    internal class SetInitializer<T> : DropCreateDatabaseIfModelChanges<Shop>
    {
        protected override void Seed(Shop context)
        {
            Producer Intel = new Producer
            {
                Name = "Intel",
                ItemFromProviders = new List<ItemFromProvider>(),
                ItemFromShops = new List<ItemFromShop>()

            };
            Producer AMD = new Producer
            {
                Name = "AMD",
                ItemFromProviders = new List<ItemFromProvider>(),
                ItemFromShops = new List<ItemFromShop>()
            };
            Producer MSI = new Producer
            {
                Name = "MSI",
                ItemFromProviders = new List<ItemFromProvider>(),
                ItemFromShops = new List<ItemFromShop>()
            };

            Provider Enter = new Provider
            {
                Name = "Enter"
            };
            Provider I5 = new Provider
            {
                Name = "I5"
            };

            MemoryStream ms = new MemoryStream();
            Image img = Image.FromFile(@"D:\I7700.jpg");
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            CpuFromShop I7700 = new CpuFromShop
            {
                Name = "I7 7700",
                Producer = Intel,
                CpuSocket = "1151",
                Video = "Intel HD",
                Core = 4,
                Threads = 8,
                Cash = "8 Mb",
                SalaryPrice = 10000,
                Image = ms.ToArray(),
                Quantity=2,
                Frequency=3300,
            };

            ms = new MemoryStream();
            img = Image.FromFile(@"D:\I7700.jpg");
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            CpuFromShop Ryzen71800X = new CpuFromShop
            {
                Name = "Ryzen 7 1800X",
                Producer = AMD,
                CpuSocket = "Am4",
                Video = "None",
                Core = 8,
                Threads = 8,
                Cash = "4 Mb",
                SalaryPrice = 9000,
                Image = ms.ToArray(),
                Quantity = 3,
                Frequency=3500

            };
            
            ms = new MemoryStream();
            img = Image.FromFile(@"D:\I7700.jpg");
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            CpuFromShop I55600 = new CpuFromShop
            {
                Name = "I5 5600",
                Producer = Intel,
                CpuSocket = "1151",
                Video = "Intel HD",
                Core = 4,
                Threads = 8,
                Cash = "8 Mb",
                SalaryPrice = 5000,
                Image = ms.ToArray(),
                Quantity = 2,
                Frequency = 3300,
            };

            ms = new MemoryStream();
            img = Image.FromFile(@"D:\I7700.jpg");
            img.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

            MotherBoardFromShop H110MProD = new MotherBoardFromShop
            {
                Name = "H110M Pro-D",
                Producer = MSI,
                MBSocket = "1151",
                ChipSet = "H110M",
                RAM = "DDR-4",
                PciE = "1X Pci-E 3.0 X16, 1X Pci-E 3.0 X8",
                USB = "8X USB 2.0, 2X USB 3.0",
                SalaryPrice = 1500,
                Image = ms.ToArray(),
                Quantity = 1
            };

            context.ItemFromShops.AddRange(new List<ItemFromShop> { I7700, Ryzen71800X, H110MProD, I55600 });
            context.Producers.AddRange(new List<Producer> { Intel, AMD, MSI });
            context.Providers.AddRange(new List<Provider> { Enter, I5 });
            context.SaveChanges();

            Post Worker = new Post
            {
                Name="Seller",
                Salary=5000,
                Users = new List<User>()
            };

            Post Manager = new Post
            {
                Name = "Manager",
                Salary = 10000,
                Users = new List<User>()
            };

            Post Director = new Post
            {
                Name = "Director",
                Salary = 15000,
                Users = new List<User>()
            };

            Post Provider = new Post
            {
                Name = "Provider",
                Salary = null,
                Users = new List<User>()
            };


            User Seller = new User
            {
                Name = "Andriy",
                Surname ="Ruduk",
                Login= "Ruduk",
                Password= "Ruduk",
                Post = Worker,
                Online = false,
                Provider = null
            };

            User UserManager = new User
            {
                Name = "Olexandr",
                Surname = "Podik",
                Login = "Podik",
                Password = "Podik",
                Post = Manager,
                Online=false,
                Provider = null
            };
            User UserDirector = new User
            {
                Name = "Sergiy",
                Surname = "Korolchuk",
                Login = "Korolchuk",
                Password = "Korolchuk",
                Post = Director,
                Online = false,
                Provider = null
            };

            User UserProvider = new User
            {
                Name = "Olexandr",
                Surname = "Martinuk",
                Login = "Martinuk",
                Password = "Martinuk",
                Post = Provider,
                Online = false,
                Provider = Enter

            };

            User TMPUser = new User
            {
                Name = "test",
                Surname = "test",
                Login = "test",
                Password = "test",
                Post = Provider,
                Online = false,
                Provider = I5
            };
            context.Users.AddRange(new List<User>
            { Seller, UserManager, UserDirector, UserProvider,TMPUser });

            ItemFromProvider I97900 = new CpuFromProvider
            {
                Name = "I9 7900",
                Frequency = 3000,
                BuyPrice = 20000,
                Cash = "24 mb",
                Core = 18,
                Threads = 36,
                CpuSocket = "2011",
                Image = ms.ToArray(),
                Producer = Intel,
                Provider = Enter,
                Video = "None"

            };
            context.ItemFromProviders.Add(I97900);

            I97900 = new CpuFromProvider
            {
                Name = "I9 7900",
                Frequency = 3000,
                BuyPrice = 19000,
                Cash = "24 mb",
                Core = 18,
                Threads = 36,
                CpuSocket = "2011",
                Image = ms.ToArray(),
                Producer = Intel,
                Provider = I5,
                Video = "None"
            };
            context.ItemFromProviders.Add(I97900);

            ItemFromProvider I38100 = new CpuFromProvider
            {
                Name = "I3 8100",
                Frequency = 3200,
                BuyPrice = 1500,
                Cash = "4 mb",
                Core = 4,
                Threads = 4,
                CpuSocket = "1151",
                Image = ms.ToArray(),
                Producer = Intel,
                Provider = Enter,
                Video = "None"

            };
            context.ItemFromProviders.Add(I38100);
            ItemFromProvider MB1 = new MotherBoardFromProvider
            {
                Provider = I5,
                Name = "Test",
                BuyPrice = 2000,
                ChipSet = "H110M",
                Image = ms.ToArray(),
                MBSocket="2011",
                PciE = "1X Pci-E 3.0 X16, 1X Pci-E 3.0 X8",
                USB = "8X USB 2.0, 2X USB 3.0",
                RAM="DDR-4",
                Producer= MSI,
            };
            context.ItemFromProviders.Add(MB1);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}