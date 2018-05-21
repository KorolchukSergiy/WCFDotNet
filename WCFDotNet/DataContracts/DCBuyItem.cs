using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFDotNet.DataContracts
{
    [DataContract]
    [KnownType(typeof(DCItemFromProvider))]
    [KnownType(typeof(DCCpuFromProvider))]
    [KnownType(typeof(DCMBFromProvider))]
    [KnownType(typeof(DCProducer))]
    [KnownType(typeof(DCProvider))]
    public class DCBuyItem
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int Quantity { get; set; }
        [DataMember]
        public DateTime TimeBuy { get; set; }
        [DataMember]
        public DCItemFromProvider ItemFromProvider { get; set; }
    }
}