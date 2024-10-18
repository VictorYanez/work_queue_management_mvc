using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queue_management.Models
{
    [Table("Locations")]
    public class Location
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationID { get; set; }

        [Required]
        [StringLength(100)]
        public string? LocationName { get; set; }

        [StringLength(255)]
        public string? Address { get; set; }

        [StringLength(100)]
        public string? Country { get; set; }

        [StringLength(100)]
        public string? Department { get; set; }

        [StringLength(100)]
        public string? Region { get; set; }

        [StringLength(100)]
        public string? Municipality { get; set; }

        [StringLength(20)]
        [Phone]
        public string? PhoneNumber { get; set; }

        // Definición de Relaciones & propiedad de navegacion  
        // --------------------------------------------
        [ForeignKey("CityId")]
        [Display(Name = "Nombre de Ciudad")]
        public int CityID { get; set; }
        public City? City { get; set; }

        public virtual ICollection<Agent> Agents { get; set; } = new List<Agent>();
        public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public virtual ICollection<Service> Services { get; set; } = new List<Service>();

        // Campos de Auditoría
        // ------------------------------------------------------
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
