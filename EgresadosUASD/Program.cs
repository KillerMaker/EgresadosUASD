using EgresadosUASD.Models.Contact;
using EgresadosUASD.Models.JobExperience;
using EgresadosUASD.Repository;
using Microsoft.AspNetCore.Mvc;

namespace EgresadosUASD
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddScoped(opt => new AdoAdapter(builder.Configuration.GetConnectionString("EgresadosUASD")));
            builder.Services.AddScoped<ContactRepository>();
            builder.Services.AddScoped<JobExperienceRepository>();

            var app = builder.Build();

            app.UseHttpsRedirection();

            app.MapGet("uasd/egresados/contact",async ([FromQuery] int id, [FromServices] ContactRepository repo) =>
            {
                return await repo.Get(id);
            });

            app.MapPost("uasd/egresados/contact", async ([FromBody] Contact contact, [FromServices] ContactRepository repo) =>
            {
                await repo.Create(contact);

                return new { Message = "Contacto creado correctamente" };
            });

            app.MapDelete("uasd/egresados/contact", async ([FromQuery] int id, [FromServices] ContactRepository repo) =>
            {
                await repo.Delete(id);

                return new { Message = "Contacto eliminado correctamente" };
            });


            app.MapGet("uasd/egresados/jobExperience", async ([FromQuery] int id, [FromServices] JobExperienceRepository repo) =>
            {
                return await repo.Get(id);
            });

            app.MapPost("uasd/egresados/jobExperience", async ([FromBody] JobExperience job, [FromServices] JobExperienceRepository repo) =>
            {
                await repo.Create(job);

                return new { Message = "Experiencia de trabajo creada correctamente" };
            });

            app.MapDelete("uasd/egresados/jobExperience", async ([FromQuery] int id, [FromServices] JobExperienceRepository repo) =>
            {
                await repo.Delete(id);

                return new { Message = "Experiencia de trabajo eliminada correctamente" };
            });

            app.Run();
        }
    }
}