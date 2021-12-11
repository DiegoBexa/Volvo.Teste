using System.ComponentModel.DataAnnotations;

namespace Volvo.Teste.Servico.ViewModel
{
    public class MarcaViewModel
    {

        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "A descrição não pode ter mais de 100 caracteres.")]
        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Campo Marca Obrigatório!")]
        public string Descricao { get; set; }
    }
}
