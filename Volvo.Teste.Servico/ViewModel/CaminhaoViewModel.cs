using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Volvo.Teste.Dominio.Validation;

namespace Volvo.Teste.Servico.ViewModel
{
    public class CaminhaoViewModel
    {

        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "A descrição não pode ter mais de 100 caracteres.")]
        [Display(Name = "Descrição")]
        [Required(ErrorMessage = "Campo Descrição Obrigatório!")]
        public string Descricao { get; set; }

        [Display(Name = "Marca")]
        [Required(ErrorMessage = "Campo Marca Obrigatório!")]
        public int MarcaId { get; set; }

        [Display(Name = "Modelo")]
        [Required(ErrorMessage = "Campo Modelo obrigatório!")]
        public int ModeloId { get; set; }

        [Display(Name = "Ano Fabricação")]
        [Required(ErrorMessage = "Campo Ano Fabricação Obrigatório!")]
        [ValidationAnoAtual(ErrorMessage = "Ano inválido, ano tem que ser o atual!")]
        public int AnoFabricacao { get; set; }

        [Display(Name = "Ano Modelo")]
        [Required(ErrorMessage = "Campo Ano Modelo Obrigatório!")]
        [ValidationAnoAtualSubsequente(ErrorMessage = "Ano inválido, ano tem que ser o atual ou subsequente!")]
        public int AnoModelo { get; set; }

        [JsonIgnore]
        public MarcaViewModel Marca{ get; set; }

        [JsonIgnore]
        public ModeloViewModel Modelo{ get; set; }
    }
}
