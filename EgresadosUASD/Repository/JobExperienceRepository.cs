using EgresadosUASD.Models.Contact;
using EgresadosUASD.Models.JobExperience;
using EgresadosUASD.Utility;
using Microsoft.Data.SqlClient;

namespace EgresadosUASD.Repository
{
    public class JobExperienceRepository : IRepository<JobExperience>
    {
        private AdoAdapter _dbAdapter;
        public JobExperienceRepository(AdoAdapter dbAdapter)
        {
            _dbAdapter = dbAdapter;
        }
        public async Task<int> Create(JobExperience entity)
        {
            var entityProperties = entity.GetType().GetProperties();

            List<SqlParameter> parameters = new List<SqlParameter>();

            entity.CreationDate = DateOnly.FromDateTime(DateTime.Now).ToString("dd/MM/yyyy");
            entity.ModificationTime = DateOnly.FromDateTime(DateTime.Now).ToString("dd/MM/yyyy");

            string query = "INSERT INTO ExperienciaLaboral(";

            for (int i = 1; i < entityProperties.Length; i++)
            {
                FieldNameAttribute field = (entityProperties[i].GetCustomAttributes(true)
                    .FirstOrDefault(x => x as FieldNameAttribute is not null)
                    as FieldNameAttribute);

                query += field.Name + ((i == entityProperties.Length - 1) ? ")" : ",");

                var param = new SqlParameter("@" + field.Name, entityProperties[i].GetValue(entity));

                param.SqlDbType = field.Type;

                parameters.Add(param);
            }

            query += "VALUES(";

            for (int i = 0; i < parameters.Count(); i++)
                query += parameters[i].ParameterName + ((i == parameters.Count() - 1) ? ")" : ",");

            return await _dbAdapter.Execute(query, parameters.ToArray());
        }

        public async Task Delete(int id)
        {
            await _dbAdapter.Execute("DELETE ExperienciaLaboral WHERE ExperienciaLaboralId = @ID", new SqlParameter("@ID", id));
        }

        public async Task<IEnumerable<JobExperience>> Get(int id)
        {
            return await _dbAdapter.Get<JobExperience>(
                "SELECT * FROM ExperienciaLaboral WHERE EgresadoId = @ID",
                new SqlParameter("@ID", id));
        }
    }
}
