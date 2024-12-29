using System.Runtime.Serialization;

namespace SampleCrudApp.DataTransferObjects
{
    [DataContract]
    public class UpdatePersonRequest
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }

        [DataMember(Order = 2)]
        public string FirstName { get; set; }

        [DataMember(Order = 3)]
        public string LastName { get; set; }

        [DataMember(Order = 4)]
        public string NationalCode { get; set; }

        [DataMember(Order = 5)]
        public DateTime BirthDay { get; set; }
    }
}
