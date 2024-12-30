using SampleCrudApp.Contracts;
using SampleCrudApp.Contracts.Dto;

namespace SampleCrudApp.Services
{
    public class PersonQueryService : IPersonQueryService
    {
        private readonly Context context;

        public PersonQueryService(Context context)
        {
            this.context = context;
        }

        public async Task<PersonViewModel> GetPersonAsync(GetPersonRequest request)
        {
            var person = context.People.FirstOrDefault(P => P.Id == request.Id);
            if (person == null)
            {
                throw new Exception("Person not found.");
            }

            return new PersonViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                NationalCode = person.NationalCode,
                BirthDay = person.BirthDay,
            };
        }
    }
}
