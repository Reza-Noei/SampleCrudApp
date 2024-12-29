using System.Runtime.Serialization;

namespace SampleCrudApp.DataTransferObjects
{
    [DataContract]
    public class DeleteUserRequest
    {
        [DataMember(Order = 1)]
        public int Id { get; set; }
    }
}
