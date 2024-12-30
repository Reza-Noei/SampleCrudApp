using System.Runtime.Serialization;

namespace SampleCrudApp.Contracts.Dto
{
    [DataContract]
    public class GetPersonRequest
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
    }
}
