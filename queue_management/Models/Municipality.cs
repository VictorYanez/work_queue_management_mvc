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

        // Definición de Relaciones & propiedad de navegacion  
        // --------------------------------------------
        [ForeignKey("Countries")]
        [Display(Name = "Id. del País")]
        [Required(ErrorMessage = "El campo país es obligatorio")]
        public int CountryID { get; set; }

        [Display(Name = "Nombre del País")]
        public virtual Country? Country { get; set; }

        [ForeignKey("Departments")]
        [Display(Name = "Id. del Departamento")]
        [Required(ErrorMessage = "El campo Departamento es obligatorio")]
        public int DepartmentID { get; set; }

        [Display(Name = "Nombre del Departamento")]
        public virtual Department? Department { get; set; }

        [ForeignKey("Regions")]
        [Display(Name = "Nombre del Región")]
        [Required(ErrorMessage = "El campo región es obligatorio")]
        public int RegionID { get; set; }

        [Display(Name = "Nombre de la Región")]
        public virtual Region? Region { get; set; }

        // Campos especidificos del Municipio   
        [StringLength(100)]
        [Display(Name = "Nombre del Municipio")]
        [Required(ErrorMessage = "El campo Nombre del Municipio es obligatorio")]
        public string? MunicipalityName { get; set; }

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
