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
        [Display(Name = "Id del Local")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LocationID { get; set; }

        [Required(ErrorMessage = "El campo Nombre de Local es obligatorio")]
        [StringLength(100)]
        [Display(Name = "Nombre del Local")]
        public string? LocationName { get; set; }

        [StringLength(20)]
        [Required(ErrorMessage = "El campo Teléfono es obligatorio")]
        [Display(Name = "Telefono del Local")]
        [Phone]
        public string? PhoneNumber { get; set; }

        // Definición de Relaciones & propiedad de navegacion  
        // --------------------------------------------
        [ForeignKey("CountryID")]
        [Required(ErrorMessage = "El campo País es obligatorio")]
        [Display(Name = "Id del País")]
        public int CountryID { get; set; }

        [Display(Name = "Nombre del País")]
        public virtual Country Country { get; set; } = null!;

        [ForeignKey("DepartmentID")]
        [Required(ErrorMessage = "El campo Departamento es obligatorio")]
        [Display(Name = "Id del Departamento")]
        public int DepartmentID { get; set; }

        [Display(Name = "Nombre del Departamento")]
        public virtual Department Department { get; set; } = null!;

        [ForeignKey("RegionID")]
        [Required(ErrorMessage = "El campo Region es obligatorio")]
        [Display(Name = "Id de la Región")]
        public int RegionID { get; set; }

        [Display(Name = "Nombre de la Región")]
        public virtual Region Region { get; set; } = null!;

        [ForeignKey("MunicipalityID")]
        [Required(ErrorMessage = "El campo municipio es obligatorio")]
        [Display(Name = "Id de Municipio")]
        public int MunicipalityID { get; set; }

        [Display(Name = "Nombre del Municipio")]
        public virtual Municipality Municipality { get; set; } = null!;

        [ForeignKey("CityID")]
        [Display(Name = "Nombre de Ciudad")]
        [Required(ErrorMessage = "El campo ciudad es obligatorio")]
        public int CityID { get; set; } 

        [Display(Name = "Nombre de la Ciudad")]
        public City City { get; set; } = null!;

        // Definición de Relaciones & propiedad de navegacion  
        // --------------------------------------------
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
