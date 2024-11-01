using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace queue_management.Models
{
    [Table("Departments")]
    public class Department
    {
        [Key]
        [Display(Name = "Id del Departamento")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DepartmentID { get; set; }

        [Required(ErrorMessage = "El campo Nombre de Departamento es obligatorio.")]
        [StringLength(100)]
        [Display(Name = "Nombre del Departamento")]
        public string? DepartmentName { get; set; }

        // Definición de Relaciones & propiedad de navegacion  
        // --------------------------------------------
        [Required(ErrorMessage = "El campo País es obligatorio")]
        [ForeignKey("Country")]
        public int CountryID { get; set; }

        [Display(Name = "Nombre del País")]
        public virtual Country? Country { get; set; }

        public virtual ICollection<Region> Regions { get; set; } = new List<Region>();
        public virtual ICollection<Municipality> Municipalities { get; set; } = new List<Municipality>();

        // Campos de Auditoría
        // --------------------------------------------
        [ForeignKey("CreatedByUser")]
        [ScaffoldColumn(false)]
        public int CreatedBy { get; set; }
        // [ScaffoldColumn(false)]
        // public virtual User CreatedByUser { get; set; }  // Propiedad de navegacion
        [ScaffoldColumn(false)]
        public DateTime CreatedAt { get; set; }

        [ForeignKey("ModifiedByUser")]
        [ScaffoldColumn(false)]
        public int? ModifiedBy { get; set; }
        // [ScaffoldColumn(false)]
        // public virtual User ModifiedByUser { get; set; }  // Propiedad de navegacion
        [ScaffoldColumn(false)]
        public DateTime? ModifiedAt { get; set; }

        [Timestamp] // Esto es para control de concurrencia en SQL Server
        [ScaffoldColumn(false)]
        public byte[]? RowVersion { get; set; }

    }
}
