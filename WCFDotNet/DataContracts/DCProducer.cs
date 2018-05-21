using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFDotNet.DataContracts
{
    [DataContract]
    [KnownType(typeof(DCBuyItem))]
    public class DCProducer
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}