using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFDotNet.DataContracts
{
    [DataContract]
    [KnownType(typeof(DCCpuFromProvider))]
    [KnownType(typeof(DCMBFromProvider))]
    [KnownType(typeof(DCBuyItem))]
    [KnownType(typeof(DCProducer))]
    [KnownType(typeof(DCProvider))]

    public class DCItemFromProvider
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public virtual DCProducer Producer { get; set; }
        [DataMember]
        public virtual DCProvider Provider { get; set; }
        [DataMember]
        public decimal BuyPrice { get; set; }
        [DataMember]
        public byte[] Image { get; set; }
    }
}