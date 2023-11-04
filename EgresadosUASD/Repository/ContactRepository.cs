using EgresadosUASD.Models.Contact;
using EgresadosUASD.Utility;
using Microsoft.Data.SqlClient;

namespace EgresadosUASD.Repository
{
    public class ContactRepository :IRepository<Contact>
    {
        private AdoAdapter _dbAdapter;

        public ContactRepository(AdoAdapter dbAdapter)
        {
            _dbAdapter = dbAdapter;
        }

        public async Task<int> Create(Contact entity)
        {
            var entityProperties = entity.GetType().GetProperties();

            List<SqlParameter> parameters = new List<SqlParameter>();

            entity.CreationDate = DateOnly.FromDateTime(DateTime.Now).ToString("dd/MM/yyyy");
            entity.ModificationTime = DateOnly.FromDateTime(DateTime.Now).ToString("dd/MM/yyyy");

            string query = "INSERT INTO CONTACTO(";

            for(int i = 1; i < entityProperties.Length; i++)
            {
                FieldNameAttribute field = (entityProperties[i].GetCustomAttributes(true)
                    .FirstOrDefault(x => x as FieldNameAttribute is not null)
                    as FieldNameAttribute);

                query += field.Name + ((i == entityProperties.Length -1)? ")": ",");

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
           await _dbAdapter.Execute("DELETE CONTACTO WHERE ContactoId = @ID", new SqlParameter("@ID",id));
        }

        public async Task<IEnumerable<Contact>> Get(int id)
        {
            return await _dbAdapter.Get<Contact>(
                "SELECT C.* FROM CONTACTO C INNER JOIN EGRESADO E ON E.ParticipanteId = C.ParticipanteId WHERE E.EgresadoId = @ID",
                new SqlParameter("@ID", id));
        }
    }
}
