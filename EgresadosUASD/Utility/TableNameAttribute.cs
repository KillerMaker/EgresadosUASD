namespace EgresadosUASD.Utility
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TableNameAttribute : Attribute
    {
        private string _name;

        public TableNameAttribute(string name)
        {
            _name = name;
        }

        public string Name { get { return _name; } }
    }
}
