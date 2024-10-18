using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queue_management.Models
{
    [Table("Comments")]
    public class Comment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CommentID { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Fecha de Comentario")]
        public DateTime DateTime { get; set; }

        [StringLength(200, MinimumLength = 5)]
        [Display(Name = "Comentario")]
        public string? CommentText { get; set; }

        // Definición de Relaciones & propiedad de navegacion
        [ForeignKey("UserId")]
        public int UserID { get; set; }

        [ForeignKey("ServiceId")]
        public int ServiceID { get; set; }

        // public virtual User User { get; set; }
        public virtual Service? Service { get; set; }
        public int TicketID { get; set; } // Clave foránea
        public Ticket? Ticket { get; set; } // Propiedad de navegación

        // Campos de Auditoría
        // ----------------------------------------
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
