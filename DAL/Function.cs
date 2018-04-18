using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.IO;
using System.Drawing;
using DAL.DTODal;
using DAL.Conversion;

namespace DAL
{
    public class Function
    {
        public DalUser GetUser(string login, string password)
        {
            DalUser GetUser = null;
            using (Shop shop = new Shop())
            {
                User tmpuser = shop.Users.Where(x => x.Login.Equals(login)
                                             && x.Password.Equals(password)).
                                           FirstOrDefault();
                if(tmpuser != null)
                {
                    GetUser = DalConvertToDTO.UserToDalUser(tmpuser);
                }
              
            };
            return GetUser;
        }

        public List<Post> GetListPost()
        {
            List<Post> GetListPost = null;
            using (Shop shop = new Shop())
            {
                GetListPost = shop.Posts.Local.ToList();
            };
            return GetListPost;

        }

        public DalPost GetPostUser(DalUser user)
        {
            DalPost UserLoginPost = null;

            using (Shop shop = new Shop())
            {
                User tmpUser = shop.Users.Where(x => x.Id == user.Id).First();
                UserLoginPost = DalConvertToDTO.PostToDalPost(tmpUser.Post);
            };
            return UserLoginPost;
        }

        public List<DalCpuFromShop> GetListCpu()
        {
            var GetList = new List<DalCpuFromShop>();
            using (Shop shop = new Shop())
            {
                var tmplist= shop.ItemFromShops.Where(x =>  x is CpuFromShop).Select(x=> (x as CpuFromShop )).ToList();
                GetList = tmplist.Select(x => DalConvertToDTO.CpuToDalCpuShop(x)).ToList();
            };

            return GetList;
        }
    }
}
