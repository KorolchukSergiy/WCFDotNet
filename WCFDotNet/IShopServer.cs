using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using WCFDotNet.DataContracts;

namespace WCFDotNet
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IShopServer" in both code and config file together.
    [ServiceContract]
    public interface IShopServer
    {
        [OperationContract]
        DCUser GetUser(string login, string password);

        [OperationContract]
        List<DCItemFromShop> GetDCItemFromShop();

        [OperationContract]
        void UserOff(int Id);

        [OperationContract]
        void UserOn(int Id);

        [OperationContract]
        void SaleItems(List<DCSaleItem> DCSaleItems);

        [OperationContract]
        List<DCItemFromProvider> GetItemsFromProvider();

        [OperationContract]
        void BuyItems(DCBuyItem[] BuyItems);
    }
}
