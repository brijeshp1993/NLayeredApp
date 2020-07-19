using System;
using System.Runtime.Serialization;

namespace Demo.DistributedServices.Models
{
    [Serializable]
    [DataContract]
    public class CustomerDTO
    {
        [DataMember(Name = "FirstName", IsRequired = true)]
        public string Name { get; set; }

        [DataMember]
        public string Address { get; set; }

        [OnDeserializing]
        public void OnDeserializing(StreamingContext context)
        {
            Address = Address ?? "Missing";
        }

    }
}