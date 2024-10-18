using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace queue_management.Models
{
    [Table("Queues")]
    public class Queue
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QueueID { get; set; }

        [Required]
        [StringLength(100)]
        public string? QueueName { get; set; }

        [StringLength(255)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? QueueType { get; set; }

        [StringLength(100)]
        public string? AssignmentStrategy { get; set; }


        // Definición de Relaciones & propiedad de navegacion
        // -----------------------------------------------------
        [ForeignKey("Service")]
        public int ServiceID { get; set; }  // Clave foránea hacia Service
        public Service? Service { get; set; }  // Propiedad de navegación

        [ForeignKey("QueueStatus")]
        public int QueueStatusID { get; set; }

        public virtual QueueStatus? QueueStatus { get; set; }
        public virtual ICollection<QueueAssignment> QueueAssignments { get; set; } = new List<QueueAssignment>();
        public virtual ICollection<QueueStatusAssignment> QueueStatusAssignments { get; set; } = new List<QueueStatusAssignment>();
        public virtual ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();

        // Campos de Auditoría
        [ForeignKey("CreatedByUser")]
        public int CreatedBy { get; set; }
        // public virtual User CreatedByUser { get; set; }  // Propiedad de navegacion
        public DateTime CreatedAt { get; set; }

        [ForeignKey("ModifiedByUser")]
        public int? ModifiedBy { get; set; }
        // public virtual User ModifiedByUser { get; set; }  // Propiedad de navegacion
        public DateTime? ModifiedAt { get; set; }

        [Timestamp] // Esto es para control de concurrencia en SQL Server
        public byte[]? RowVersion { get; set; }

    }
}
