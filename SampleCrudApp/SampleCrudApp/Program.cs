using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ProtoBuf.Grpc.Server;
using SampleCrudApp.Contracts;
using SampleCrudApp.Services;

namespace SampleCrudApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services
                .AddGrpcHealthChecks()
                .AddCheck("Sample", () => HealthCheckResult.Healthy());

            AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);

            builder.Services.AddCodeFirstGrpc(options =>
            {
                options.EnableDetailedErrors = true;
                options.MaxReceiveMessageSize = 50 * 1024 * 1024; // 50 MB
                options.MaxSendMessageSize = 50 * 1024 * 1024; // 50 MB
            });

            builder.Services.AddDbContext<Context>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IPersonQueryService, PersonQueryService>();
            builder.Services.AddScoped<IPersonCommandService, PersonCommandService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }            

            app.MapGrpcService<PersonQueryService>();
            app.MapGrpcService<PersonCommandService>();

            app.MapGrpcHealthChecksService();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();
            app.Run();
        }
    }
}
