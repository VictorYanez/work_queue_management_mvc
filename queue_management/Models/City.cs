using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queue_management.Models
{
    [Table("Cities")]
    public class City
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id de la Ciudad")]
        public int CityID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre de la Ciudad")]
        public string? CityName { get; set; }

        // Definición de Relaciones & propiedad de navegacion  
        // --------------------------------------------
        //[ForeignKey("CountryId")]
        //[Display(Name = "Nombre del País")]
        //public int CountryID { get; set; }
        //public virtual Country? Country { get; set; }

        //[ForeignKey("DepartmentId")]
        //[Display(Name = "Nombre del Departamento")]
        //public int DepartmentID { get; set; }
        //public virtual Department? Department { get; set; }

        //[ForeignKey("RegionId")]
        //[Display(Name = "Nombre de la Región")]
        //public int RegionID { get; set; }
        //public virtual Region? Regions { get; set; }

        [ForeignKey("MunicipalityId")]
        [Display(Name = "Nombre de Municipio")]
        public int MunicipalityID { get; set; }
        public virtual Municipality? Municipality { get; set; }

        [Display(Name = "Sucursales")]
        public ICollection<Location> Locations { get; set; } = new List<Location>();

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
