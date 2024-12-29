using System.Runtime.Serialization;

namespace SampleCrudApp.DataTransferObjects
{
    [DataContract]
    public class CreatePersonRequest
    {
        [DataMember(Order = 1)]
        public string FirstName { get; set; }

        [DataMember(Order = 2)] 
        public string LastName { get; set; }

        [DataMember(Order = 3)]
        public string NationalCode { get; set; }

        [DataMember(Order = 4)]
        public DateTime BirthDay { get; set; }
    }
}
