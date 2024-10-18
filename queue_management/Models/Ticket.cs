using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queue_management.Models
{
    [Table("Services")]
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketID { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }

        [ForeignKey("Service")]
        public int ServiceID { get; set; }

        [ForeignKey("Queue")]
        public int QueueID { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        public string? TicketCode { get; set; }

        // Definición de Relaciones & propiedad de navegacion
        [ForeignKey("TicketStatus")]
        public int TicketStatusID { get; set; }

        //public virtual User? User { get; set; }
        public virtual Service? Service { get; set; } 
        public virtual Queue? Queue { get; set; }
        public virtual TicketStatus? TicketStatus { get; set; }

        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();  // Colección de comentarios relacionados
        public virtual ICollection<TicketStatusAssignment> TicketStatusAssignments { get; set; } = new List<TicketStatusAssignment>();


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
