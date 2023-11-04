using EgresadosUASD.Utility;
using Microsoft.Data.SqlClient;

namespace EgresadosUASD
{
    public class AdoAdapter
    {
        private string _connectionString;

        public AdoAdapter(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<T>> Get<T>(string query, params SqlParameter[] parameters) where T : class
        {
            List<T> result = new List<T>();

            using SqlConnection connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            using SqlCommand command = connection.CreateCommand();

            command.CommandText = query;

            command.Parameters.AddRange(parameters);

            using SqlDataReader reader = await command.ExecuteReaderAsync();

            while(await reader.ReadAsync())
            {
                T obj = Activator.CreateInstance(typeof(T)) as T;

                var properties = obj.GetType().GetProperties();

                foreach (var property in properties)
                {
                    string fieldName = property.GetCustomAttributes(true)
                        .Where(x => x as FieldNameAttribute is not null)
                        .Select(x=> (x as FieldNameAttribute).Name)
                        .FirstOrDefault();

                    if(fieldName is not null)
                        property.SetValue(obj, Convert.ChangeType(reader[fieldName].ToString(), property.PropertyType));
                }

                result.Add(obj);
            }

            return result;
        }

        public async Task<int> Execute(string query, params SqlParameter[] parameters)
        {
            using SqlConnection connection = new SqlConnection(_connectionString);

            await connection.OpenAsync();

            using SqlCommand command = connection.CreateCommand();

            command.CommandText = query;

            command.Parameters.AddRange(parameters);

            return await command.ExecuteNonQueryAsync();
        }
    }
}
