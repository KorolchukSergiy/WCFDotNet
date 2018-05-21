using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFDotNet.DataContracts
{
    [DataContract]
    [KnownType(typeof(DCProducer))]
    [KnownType(typeof(DCMBFromShop))]
    [KnownType(typeof(DCCpuFromShop))]
    [KnownType(typeof(DCSaleItem))]
    public class DCItemFromShop
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public DCProducer Producer { get; set; }
        [DataMember]
        public decimal SalaryPrice { get; set; }
        [DataMember]
        public byte[] Image { get; set; }
    }
}