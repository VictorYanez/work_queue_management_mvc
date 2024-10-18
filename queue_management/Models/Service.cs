using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace queue_management.Models
{
    [Table("Services")]
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id del Servicio")]
        public int ServiceID { get; set; }
 
        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre del Servicio")]
        public string? ServiceName { get; set; }

        [StringLength(255)]
        [Display(Name = "Descripción")]
        public string? Description { get; set; }

        [StringLength(255)]
        [Display(Name = "Dirección")]
        public string? Address { get; set; }

        [StringLength(100)]
        [Display(Name = "Unidad")]
        public string? Unit { get; set; }

        [StringLength(100)]
        [Display(Name = "Tipo de Servicio")]
        public string? ServiceType { get; set; }

        // Definición de Relaciones & propiedad de navegacion
        // -----------------------------------------------------
        [ForeignKey("Location")]
        public int LocationID { get; set; }  // Clave foránea hacia Location
        public Location? Location { get; set; }  // Propiedad de navegación

        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<ServiceWindow> ServiceWindows { get; set; } = new List<ServiceWindow>();
        public virtual ICollection<Queue> Queues { get; set; }  = new List<Queue>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
        public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

        // Campos de Auditoría
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
