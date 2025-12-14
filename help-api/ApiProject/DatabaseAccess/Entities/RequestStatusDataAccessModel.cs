using System.ComponentModel.DataAnnotations.Schema;

namespace ApiProject.DatabaseAccess.Entities
{
    [Table("RequestStatuses")]
    public class RequestStatusDataAccessModel : BaseEntity
    {
        /// <summary>
        /// E.g., "PENDING", "ACCEPTED", "REJECTED"
        /// </summary>
        public required string Name { get; set; }
    }
}
