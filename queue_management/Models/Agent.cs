using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

namespace queue_management.Models
{
    [Table("Agents")]
    public class Agent
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AgentID { get; set; }

        [Required]
        [StringLength(20)]
        [Display(Name = "Documento de Identidad")]
        public string? DUI { get; set; }

        [Required]
        [Display(Name = "Nombres")]
        [StringLength(100, MinimumLength = 2)]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Apellidos")]
        [StringLength(100, MinimumLength = 2)]
        public string? LastName { get; set; }

        [Required]
        [Display(Name = "Correo")]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        [Phone]
        public string? PhoneNumber { get; set; }

        // Definición de Relaciones & propiedad de navegacion
        [ForeignKey("Role")]
        public int RoleID { get; set; }   //Puede tener varios roles ?

        [ForeignKey("Location")]
        public int LocationID { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        [StringLength(100)]
        public string? Unit { get; set; }

        [StringLength(100)]
        public string? Position { get; set; }

        // Definición de Relaciones & propiedad de navegacion
        public virtual Role? Role { get; set; }
        public virtual Location? Location { get; set; }
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        // Campos de Auditoría
        // ---------------------
        [ForeignKey("CreatedByUser")]
        public int CreatedBy { get; set; }
        // public virtual User CreatedByUser { get; set; }  // Propiedad de navegacion
        public DateTime CreatedAt { get; set; } 

        [ForeignKey("ModifiedByUser")]
        public int? ModifiedBy { get; set; }
        // public virtual User ModifiedByUser { get; set; }  // Propiedad de navegacion
        public DateTime? ModifiedAt { get; set; }

        [Timestamp] // Esto es para control de concurrencia en SQL Server
        public byte[]? RowVersion  { get; set; }

    }
}
