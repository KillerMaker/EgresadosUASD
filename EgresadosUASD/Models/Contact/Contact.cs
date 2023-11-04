using EgresadosUASD.Utility;


namespace EgresadosUASD.Models.Contact
{
    [TableName("Contacto")]
    public class Contact
    {
        [FieldName(name: "ContactoId", System.Data.SqlDbType.Int)]
        public int Id { get; set; }

        [FieldName("ParticipanteId", System.Data.SqlDbType.Int)]
        public int Participant { get; set; }

        [FieldName("TipoContactoId", System.Data.SqlDbType.Int)]
        public int ContactType { get; set; }

        [FieldName(name: "Nombre", System.Data.SqlDbType.VarChar)]
        public string Name { get; set; }

        [FieldName(name: "Mostrar", System.Data.SqlDbType.Bit)]
        public bool Show { get; set; }

        [FieldName(name: "Estado", System.Data.SqlDbType.Bit)]
        public bool Status { get; set; }

        [FieldName(name: "FechaCreacion", System.Data.SqlDbType.DateTime)]
        public string CreationDate { get; set; }

        [FieldName(name: "FechaModificacion", System.Data.SqlDbType.DateTime)]
        public string ModificationTime { get; set; }
    }
}
