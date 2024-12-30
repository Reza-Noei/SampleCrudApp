using SampleCrudApp.Contracts.Dto;
using System.ServiceModel;

namespace SampleCrudApp.Contracts
{
    [ServiceContract]
    public interface IPersonQueryService
    {
        [OperationContract]
        Task<PersonViewModel> GetPersonAsync(GetPersonRequest request);
    }
}
