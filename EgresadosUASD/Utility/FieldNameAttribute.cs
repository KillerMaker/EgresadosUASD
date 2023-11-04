using System.Data;

namespace EgresadosUASD.Utility
{
    [AttributeUsage(AttributeTargets.Property)]
    public class FieldNameAttribute : Attribute
    {
        private string _name;
        private SqlDbType _type;

        public FieldNameAttribute(string name, SqlDbType type)
        {
            _name = name;
            _type = type;
        }

        public string Name { get { return _name; } }
        public SqlDbType Type { get { return _type; } }
    }
}
