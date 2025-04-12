using System.ComponentModel.DataAnnotations;

namespace ModelLayer.DTO
{
    public class ProveedorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El nombre del proveedor es obligatorio.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Display(Name = "Nombre del Proveedor")]
        public string NombreProveedor { get; set; }

        [Required(ErrorMessage = "El nombre de contacto es obligatorio.")]
        [StringLength(100, ErrorMessage = "Máximo 100 caracteres.")]
        [Display(Name = "Nombre del Contacto")]
        public string NombreContacto { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio.")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres.")]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; }
    }
}
