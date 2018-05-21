using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Drawing;

namespace DAL
{
    public class Function
    {
        private Shop shop = new Shop();

        public User GetUser(string login, string password)
        {
            User GetUser = shop.Users.Where(x => x.Login.Equals(login)
                                            && x.Password.Equals(password)).FirstOrDefault();
            return GetUser;
        }

        public List<Post> GetListPost()
        {
            List<Post> GetListPost = null;
            GetListPost = shop.Posts.Local.ToList();
            return GetListPost;

        }

        public Post GetPostUser(int UserId)
        {
            Post UserLoginPost = null;
            User tmpUser = shop.Users.Where(x => x.Id == UserId).First();
            UserLoginPost = shop.Posts.First(x => x.Id == tmpUser.Post.Id);
            return UserLoginPost;
        }

        public List<ItemFromShop> GetItemsFromShop()
        {
            return shop.ItemFromShops.ToList();
        }

        public void UserOff(int Id)
        {
            shop.Users.First(x => x.Id == Id).Online = false;
        }

        public void UserOn(int Id)
        {
            shop.Users.First(x => x.Id == Id).Online = true;
        }

        public void SaleItem(List<SaleItem> SaleItems)
        {
            foreach (var item in SaleItems)
            {
                item.ItemFromShop = shop.ItemFromShops.First(x => x.Id == item.ItemFromShop.Id);
                item.ItemFromShop.Quantity -= item.Quantity;
            }
            shop.SaleItems.AddRange(SaleItems);
            shop.SaveChanges();
        }

        public List<ItemFromProvider> GetItemFromProviders()
        {
            return shop.ItemFromProviders.ToList();
        }

        public void BuyItems(List<BuyItem> BuyItems)
        {
            foreach (var item in BuyItems)
            {
                item.ItemFromProvider = shop.ItemFromProviders.First(x => x.Id == item.ItemFromProvider.Id);

                ItemFromShop tmpItem = shop.ItemFromShops.FirstOrDefault
                                        (x => x.Name == item.ItemFromProvider.Name &&
                                        x.Producer.Id == item.ItemFromProvider.Producer.Id);

                if (tmpItem != null)
                {
                    tmpItem.Quantity += item.Quantity;
                }
                else
                {
                    if(item.ItemFromProvider is CpuFromProvider)
                    {
                        tmpItem = CreateCpuShop(item.ItemFromProvider as CpuFromProvider);
                        tmpItem.Quantity = item.Quantity;
                    }
                    else if(item.ItemFromProvider is MotherBoardFromProvider)
                    {
                        tmpItem = CreateMotherBoardShop(item.ItemFromProvider as MotherBoardFromProvider);
                        tmpItem.Quantity = item.Quantity;
                    }
                    shop.ItemFromShops.Add(tmpItem);
                }
            }
            shop.SaveChanges();
        }

        public ItemFromShop CreateCpuShop(CpuFromProvider Item)
        {
            return new CpuFromShop
            {
                Name = Item.Name,
                Cash = Item.Cash,
                Frequency= Item.Frequency,
                Core= Item.Core,
                CpuSocket= Item.CpuSocket,
                Threads= Item.Threads,
                Image= Item.Image,
                SalaryPrice= Item.BuyPrice*1.5m,
                Video= Item.Video,
                Producer=shop.Producers.First(x=>x.Id== Item.Producer.Id)
            };
        }

        public ItemFromShop CreateMotherBoardShop(MotherBoardFromProvider Item)
        {
            return new MotherBoardFromShop
            {
                Name= Item.Name,
                Image= Item.Image,
                USB= Item.USB,
                ChipSet= Item.ChipSet,
                PciE= Item.PciE,
                MBSocket= Item.MBSocket,
                RAM= Item.RAM,
                SalaryPrice= Item.BuyPrice*1.5m,
                Producer= shop.Producers.First(x => x.Id == Item.Producer.Id)
            };

        }
    }
}
