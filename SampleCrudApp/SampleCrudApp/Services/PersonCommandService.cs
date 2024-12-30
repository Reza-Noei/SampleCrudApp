using Grpc.Core;
using SampleCrudApp.Contracts;
using SampleCrudApp.Contracts.Dto;
using System.Runtime.CompilerServices;
using Google.Rpc;
using Google.Protobuf.WellKnownTypes;

namespace SampleCrudApp.Services
{
    public class PersonCommandService : IPersonCommandService
    {
        private readonly Context context;

        public PersonCommandService(Context context)
        {
            this.context = context;
        }

        public async Task<PersonViewModel> CreateAsync(CreatePersonRequest request)
        {
            ArgumentNotNullOrEmpty(request.FirstName);
            ArgumentNotNullOrEmpty(request.LastName);
            ArgumentNotNullOrEmpty(request.NationalCode);

            Domain.Person person = new Domain.Person(request.FirstName, request.LastName, request.NationalCode, request.BirthDay);
            context.People.Add(person);
         
            await context.SaveChangesAsync();

            return new PersonViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                NationalCode = person.NationalCode,
                BirthDay = person.BirthDay,
            };
        }

        public async Task<PersonViewModel> DeleteAsync(DeletePersonRequest request)
        {
            var person = context.People.FirstOrDefault(P => P.Id == request.Id);
            if (person == null)
            {
                throw new RpcException(status: new Grpc.Core.Status(StatusCode.Unavailable, detail: "Person not found."));
            }

            context.People.Remove(person);
            await context.SaveChangesAsync();

            return new PersonViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                NationalCode = person.NationalCode,
                BirthDay = person.BirthDay,
            };
        }

        public async Task<PersonViewModel> UpdateAsync(UpdatePersonRequest request)
        {
            ArgumentNotNullOrEmpty(request.FirstName);
            ArgumentNotNullOrEmpty(request.LastName);
            ArgumentNotNullOrEmpty(request.NationalCode);
            
            var person = context.People.FirstOrDefault(P => P.Id == request.Id);
            if (person == null)
            {
                throw new Exception("Person not found.");
            }

            person.Update(request.FirstName, request.LastName, request.NationalCode);
            
            await context.SaveChangesAsync();

            return new PersonViewModel
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                NationalCode = person.NationalCode,
                BirthDay = person.BirthDay,
            };
        }

        public static void ArgumentNotNullOrEmpty(string value, [CallerArgumentExpression(nameof(value))] string? paramName = null)
        {
            if (string.IsNullOrEmpty(value))
            {                
                var status = new Google.Rpc.Status
                {
                    Code = (int)Code.InvalidArgument,
                    Message = "Bad request",
                    Details =
                    {
                        Any.Pack(new Google.Rpc.BadRequest
                        {
                            FieldViolations =
                            {
                                new Google.Rpc.BadRequest.Types.FieldViolation { Field = paramName, Description = "Value is null or empty" }
                            }
                        })
                    }
                };

                throw status.ToRpcException();
            }
        }
    }
}
