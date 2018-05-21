using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WCFDotNet.DataContracts
{
    [DataContract]
    [KnownType(typeof(DCPost))]
    [KnownType(typeof(DCProvider))]
    public class DCUser
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public DCPost Post { get; set; }
        [DataMember]
        public bool Online { get; set; }
        [DataMember]
        public DCProvider Provider { get; set; }
    }
}