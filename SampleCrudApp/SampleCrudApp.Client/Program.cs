using Google.Rpc;
using Grpc.Core;
using Grpc.Health.V1;
using Grpc.Net.Client;
using ProtoBuf.Grpc.Client;
using SampleCrudApp.Contracts;
using System.Text.Json;

namespace SampleCrudApp.Client
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7287");

            try
            {
                var client = new Health.HealthClient(channel);
                var response = await client.CheckAsync(new HealthCheckRequest());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unhealthy status.");
                Console.ReadKey();
                return -1;
            }

            var personCommandService = channel.CreateGrpcService<IPersonCommandService>();
            var personQueryService = channel.CreateGrpcService<IPersonQueryService>();

            try
            {
                var deletePerson3Response = await personCommandService.DeleteAsync(new Contracts.Dto.DeletePersonRequest
                {
                    Id = 2
                });
            }
            catch (RpcException ex)
            {
                var message = ex.Status.Detail;
            }

            Contracts.Dto.PersonViewModel createPersonResponse = null;

            try
            {
                createPersonResponse = await personCommandService.CreateAsync(new Contracts.Dto.CreatePersonRequest
                {
                    FirstName = null,
                    LastName = "Noei",
                    NationalCode = "001640286",
                    BirthDay = DateTime.Now.AddYears(-30)
                });
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Server error: {ex.Status.Detail}");
                var badRequest = ex.GetRpcStatus()?.GetDetail<BadRequest>();
                if (badRequest != null)
                {
                    foreach (var fieldViolation in badRequest.FieldViolations)
                    {
                        Console.WriteLine($"Field: {fieldViolation.Field}");
                        Console.WriteLine($"Description: {fieldViolation.Description}");
                    }
                }
            }

            try
            {
                var updatePersonResponse = await personCommandService.UpdateAsync(new Contracts.Dto.UpdatePersonRequest
                {
                    Id = createPersonResponse.Id,
                    FirstName = "Amirhossein",
                    LastName = "Noei",
                    NationalCode = "001640288",
                    BirthDay = DateTime.Now.AddYears(-31)
                });
            }
            catch (RpcException ex)
            {
                Console.WriteLine($"Server error: {ex.Status.Detail}");
                var badRequest = ex.GetRpcStatus()?.GetDetail<BadRequest>();
                if (badRequest != null)
                {
                    foreach (var fieldViolation in badRequest.FieldViolations)
                    {
                        Console.WriteLine($"Field: {fieldViolation.Field}");
                        Console.WriteLine($"Description: {fieldViolation.Description}");
                    }
                }
            }

            var getPersonResponse = await personQueryService.GetPersonAsync(new Contracts.Dto.GetPersonRequest
            {
                Id = createPersonResponse.Id
            });

            var deletePersonResponse = await personCommandService.DeleteAsync(new Contracts.Dto.DeletePersonRequest
            {
                Id = createPersonResponse.Id
            });

            Console.ReadKey();

            return 0;
        }
    }
}
