using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Volvo.Teste.Servico.ViewModel
{
    public class ModeloViewModel
    {

        public int Id { get; set; }

        [Display(Name = "Marca")]
        public int MarcaId { get; set; }

        [StringLength(100, ErrorMessage = "A descrição não pode ter mais de 100 caracteres.")]
        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "Campo Modelo Obrigatório!")]
        public string Descricao { get; set; }

        [Display(Name = "Modelo Permitido")]
        public bool ModeloPermitido { get; set; }

        [JsonIgnore]
        public MarcaViewModel Marca { get; set; }
    }
}
