using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queue_management.Models
{
    [Table("Regions")]
    public class Region

    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id de la Región")]
        public int RegionID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage = "El campo Nombre de Región es obligatorio.")]
        [Display(Name = "Nombre de la Región")]
        public string? RegionName { get; set; }

        // Definición de Relaciones & propiedad de navegacion 
        // --------------------------------------------
        [ForeignKey("Country")]
        [Required(ErrorMessage = "El código del país es obligatorio")]
        [Display(Name = "Codigo de país")]
        public int CountryID { get; set; }

        [Display(Name = "País")]
        public virtual Country? Country { get; set; }

        [ForeignKey("Department")]
        [Display(Name = "Código del Departamento")]
        [Required(ErrorMessage = "El código del Departamento es obligatorio")]
        public int DepartmentID { get; set; }

        [Display(Name = "Departamentos")]
        public virtual Department? Department { get; set; }
        public virtual ICollection<Municipality> Municipalities { get; set; } = new List<Municipality>();

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
