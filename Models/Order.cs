using System.ComponentModel.DataAnnotations;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AuthSystem.Models
{
    public class Order
    {
        [Display(Name = "Order ID")]
        public int Id { get; set; }
        [Display(Name = "Name")]
        public required string NomeDestinatario { get; set; }
        [Display(Name = "Address")]
        public required string MoradaDestinatario { get; set; }
        [Display(Name = "ZIP Code [xxxx]")]
        public required string CodigoPostal { get; set; }
        [Display(Name = "Contact")]
        public required string Contacto { get; set; }
        [DataType(DataType.Date)]
        [Display(Name = "Request Date")]
        public required DateTime DataPedido { get; set; }
        [Display(Name = "Priority")]
        public required string Prioridade { get; set; }
    }
}
