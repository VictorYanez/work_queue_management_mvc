using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace queue_management.Models
{
    [Table("Municipalities")]
    public class Municipality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id del Municipio")]
        public int MunicipalityID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre del Municipio")]
        public string? MunicipalityName { get; set; }


        // Definición de Relaciones & propiedad de navegacion  
        // --------------------------------------------
        [ForeignKey("Countries")]
        [Display(Name = "Nombre del País")]
        public int CountryID { get; set; }
        public virtual Country? Country { get; set; }

        [ForeignKey("Departments")]
        [Display(Name = "Nombre del Departamento")]
        public int DepartmentID { get; set; }
        public virtual Department? Department { get; set; }

        [ForeignKey("Regions")]
        [Display(Name = "Nombre del Departamento")]
        public int RegionID { get; set; }
        public virtual Region? Region { get; set; }

        public ICollection<City> Cities { get; set; } = new List<City>();

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
