using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace queue_management.Models
{
    [Table("Countries")]
    public class Country
    {
        [Key]
        [Display(Name = "Id del País")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CountryID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre del País")]
        public string? CountryName { get; set; }

        // Definición de Relaciones & propiedad de navegacion  
        // ----------------------------------------------------
        [ForeignKey("DepartmentId")]
        public int DepartmentID { get; set; }
        [Display(Name = "Departamentos")]
        public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

        // Campos de Auditoría
        // ------------------------------------------------------
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
