using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class RegistroViewModel
    {
        [Required(ErrorMessage = "Complete el campo {0}")]
        [EmailAddress(ErrorMessage ="Ingrese un email válido")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Complete el campo {0}")]
        public string Password { get; set; }
    }
}
