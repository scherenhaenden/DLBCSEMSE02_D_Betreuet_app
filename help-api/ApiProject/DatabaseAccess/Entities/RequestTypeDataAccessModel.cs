using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.DatabaseAccess.Entities
{
    [Table("RequestTypes")]
    public class RequestTypeDataAccessModel : BaseEntity
    {
        /// <summary>
        /// E.g., "SUPERVISOR", "CO_SUPERVISOR"
        /// </summary>
        public required string Name { get; set; }
    }
}
