using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queue_management.Models
{
    [Table("Appointments")]
    public class Appointment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id de la Cita")]
        public int AppointmentID { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha de la Cita")]
        public DateTime DateTime { get; set; }

        [Display(Name = "Observaciones de la Cita")]
        [StringLength(200, MinimumLength = 5)]
        public string? Observations { get; set; }


        // Definición de Relaciones & propiedad de navegacion
        //----------------------------------------------------
        [ForeignKey("UserId")]
        public int UserID { get; set; }
        //public virtual User? User { get; set; }

        [ForeignKey("AgentId")]
        public int AgentId { get; set; }
        public virtual Agent? Agent { get; set; }

        [ForeignKey("StatusId")]
        public int StatusID { get; set; }
        public virtual Status? Status { get; set; }

        [ForeignKey("ServiceId")]
        public int ServiceID { get; set; }
        public virtual Service? Service { get; set; }

        [ForeignKey("LocationId")]
        public int LocationID { get; set; }
        public virtual Location? Location { get; set; }

        // Campos de Auditoría
        // --------------------------------------
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
