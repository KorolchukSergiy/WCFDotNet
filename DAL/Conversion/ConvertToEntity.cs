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
                  .ForMember("Post",opt=>opt.MapFrom(x=>shop.Posts.
                            Where(p=>p.Name==dalUser.Post).Single())));
            user = Mapper.Map<DalUser, User>(dalUser);
            return user;
        }

        public static Producer DalProducerToProducer(DalProducer dalProducer)
        {
            Producer producer = new Producer();
            Mapper.Initialize(Cfg => Cfg.CreateMap<DalProducer, Producer>()
                    .ForMember("ItemFromProviders", opt => new List<ItemFromProvider>())
                    .ForMember("ItemFromShops",opt=> new List<ItemFromShop>()));
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

        //public static Post DalPostToPost(DalPost dalPost)
        //{
        //    Post post = new Post();
        //    Mapper.Initialize(Cfg=>Cfg.CreateMap<DalPost,Post>()
        //        .ForMember)
        //    return post;
        //}
    }
}