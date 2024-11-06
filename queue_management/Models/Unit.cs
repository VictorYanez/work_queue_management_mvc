using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace queue_management.Models
{
    [Table("queue_management.Models")]
    public class Unit
    {
        [Key]
        [Display(Name = "Id de Unidad")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UnitID { get; set; }


        [Required(ErrorMessage = "El campo {0} es requerido")]
        [Display(Name = "Nombre de Unidad")]
        public String? UnitName { get; set; }

        [StringLength(200)]
        [Display(Name = "Descripción de Unidad")]
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string? UnitDescription { get; set; }


        // Definición de Relaciones & propiedad de navegacion  
        // --------------------------------------------
        [ForeignKey("AreaID")]
        [Display(Name = "Id de Área")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AreaID { get; set; }

        [Display(Name = "Area")]
        public virtual Area Area { get; set; } = null!; //Perdonar el nulo? y Ademas propiedad de navegacion 


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
