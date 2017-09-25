using System.ComponentModel.DataAnnotations;

namespace EnviarCorreos.Models
{
    public class SendEmail
    {
        [Required(ErrorMessage ="Debe de capturar el nombre del cliente.")]
        [DataType(DataType.Text)]
        [MinLength(2, ErrorMessage = "El nombre del cliente no puede ser mayor a 2 caracteres.")]
        [MaxLength(100,ErrorMessage ="El nombre del cliente no puede ser mayor a 100 caracteres.")]
        [Display(Name ="Nombre del Cliente:",Description ="Nombre del Cliente")]
        public string ClientName { get; set; }

        [Required(ErrorMessage = "Debe de capturar el correo electrónico del cliente.")]
        [DataType(DataType.EmailAddress)]
        //[RegularExpression("^([a-zA-Z0-9_\\-\\.])",ErrorMessage ="El correo electrónico no cumple con el formao estandar")]        
        [Display(Name = "Correo electrónico del Cliente:", Description = "Nombre del Cliente")]
        public string ClientEmail { get; set; }
    }
}