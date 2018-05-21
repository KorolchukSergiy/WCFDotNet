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
    [KnownType(typeof(DCUser))]
    [KnownType(typeof(DCBuyItem))]
    public class DCProvider
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public virtual ICollection<DCItemFromProvider> ItemFromProviders { get; set; }
        [DataMember]
        public virtual DCUser User { get; set; }
    }
}