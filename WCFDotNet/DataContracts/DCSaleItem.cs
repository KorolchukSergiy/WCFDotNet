using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFDotNet.DataContracts
{
    [DataContract]
    public class DCSaleItem
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public DateTime SaleTime { get; set; }
        [DataMember]
        public DCItemFromShop ItemFromShop { get; set; }
    }
}