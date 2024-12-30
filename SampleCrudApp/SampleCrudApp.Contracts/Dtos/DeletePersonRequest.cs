using System.Runtime.Serialization;

namespace SampleCrudApp.Contracts.Dto
{
    [DataContract]
    public class DeletePersonRequest
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
    }
}
