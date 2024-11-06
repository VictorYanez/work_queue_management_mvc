using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queue_management.Models
{
    [Table("Areas")]
    public class Area
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AreaID { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre de Área")]
        public String? AreaName { get; set; }

        [StringLength(200)]
        [Display(Name = "Descripción de Área")]
        public string? AreaDescription { get; set; }

        // Definición de Relaciones & propiedad de navegacion  
        // --------------------------------------------
        [ForeignKey("UnitID")]
        [Display(Name = "Id de la Unidades")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int UnitID { get; set; }

        [Display(Name = "Unidades")]
        public virtual ICollection<Unit> Units { get; set; } = new List<Unit>();   

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
