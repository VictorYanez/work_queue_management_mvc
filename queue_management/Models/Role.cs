using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queue_management.Models
{
    [Table("Roles")]
    public class Role
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id del Rol")]
        public int RoleId { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Nombre del Rol")]
        public string? RoleName { get; set; }

        // Definición de Relaciones & propiedad de navegacion  
        // public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();

        // Campos de Auditoría
        // --------------------------------------------
        [ForeignKey("CreatedByUser")]
        public int CreatedBy { get; set; }
        //public virtual User CreatedByUser { get; set; }  // Propiedad de navegacion
        public DateTime CreatedAt { get; set; }

        [ForeignKey("ModifiedByUser")]
        public int? ModifiedBy { get; set; }
        //public virtual User ModifiedByUser { get; set; }  // Propiedad de navegacion
        public DateTime? ModifiedAt { get; set; }

        [Timestamp] // Esto es para control de concurrencia en SQL Server
        public byte[]? RowVersion { get; set; }

    }
}
