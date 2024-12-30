using SampleCrudApp.Contracts.Dto;
using System.ServiceModel;

namespace SampleCrudApp.Contracts
{
    [ServiceContract]
    public interface IPersonCommandService
    {
        [OperationContract]
        Task<PersonViewModel> CreateAsync(CreatePersonRequest request);

        [OperationContract]
        Task<PersonViewModel> UpdateAsync(UpdatePersonRequest request);

        [OperationContract]
        Task<PersonViewModel> DeleteAsync(DeletePersonRequest request);
    }
}
