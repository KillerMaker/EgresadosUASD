using EgresadosUASD.Utility;

namespace EgresadosUASD.Models.JobExperience
{
    [TableName("ExperienciaLaboral")]
    public class JobExperience
    {
        [FieldName("ExperienciaLaboralId", System.Data.SqlDbType.Int)]
        public int Id { get; set; }

        [FieldName("EgresadoId", System.Data.SqlDbType.Int)]
        public int Graduate { get; set; }

        [FieldName("Organizacion", System.Data.SqlDbType.VarChar)]
        public string Organization { get; set; }

        [FieldName("Posicion", System.Data.SqlDbType.VarChar)]
        public string Position { get; set; }

        [FieldName("FechaEntrada", System.Data.SqlDbType.Date)]
        public string EntryDate { get; set; }

        [FieldName("FechaSalida", System.Data.SqlDbType.Date)]
        public string ExitDate { get; set; }

        [FieldName("Acerca", System.Data.SqlDbType.VarChar)]
        public string About { get; set; }

        [FieldName("Mostrar", System.Data.SqlDbType.Bit)]
        public bool Show { get; set; }

        [FieldName("Estado", System.Data.SqlDbType.Bit)]
        public bool Status { get; set; }

        [FieldName("FechaCreacion", System.Data.SqlDbType.DateTime)]
        public string CreationDate { get; set; }

        [FieldName("FechaModificacion", System.Data.SqlDbType.DateTime)]
        public string ModificationTime { get; set; }
    }
}
