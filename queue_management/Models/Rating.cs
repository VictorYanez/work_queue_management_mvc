using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queue_management.Models
{
    [Table("Ratings")]
    public class Rating
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id de la Evaluación")]
        public int RatingID { get; set; }

        [Required]
        [Display(Name = "Fecha de la Evaluación")]
        public DateTime DateTime { get; set; }

        [Required]
        [Display(Name = "Nota de la Evaluación")]
        [Range(0, 10, ErrorMessage = "Los valores permitidos para {0} son entre {1} y {2}.")]
        public int RatingValue { get; set; }

        public string? Observations { get; set; }

        // Definición de Relaciones & propiedad de navegacion
        [ForeignKey("User")]
        [Display(Name = "Id del Usuario")]
        public int UserID { get; set; }

        [ForeignKey("Service")]
        [Display(Name = "Id del Servicio")]
        public int ServiceID { get; set; }

        // public virtual User User { get; set; }
        [Display(Name = "Servicios")]
        public virtual Service? Service { get; set; }

        // Campos de Auditoría
        // ------------------------------------------
        [ForeignKey("CreatedByUser")]
        public int CreatedBy { get; set; }
        //public virtual User CreatedByUser { get; set; }  // Propiedad de navegacion
        public DateTime? CreatedAt { get; set; }

        [ForeignKey("ModifiedByUser")]
        public int? ModifiedBy { get; set; }
        //public virtual User ModifiedByUser { get; set; }  // Propiedad de navegacion
        public DateTime? ModifiedAt { get; set; }

        [Timestamp] // Esto es para control de concurrencia en SQL Server
        public byte[]? RowVersion { get; set; }

    }
}
