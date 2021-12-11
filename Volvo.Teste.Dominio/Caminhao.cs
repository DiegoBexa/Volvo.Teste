using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Volvo.Teste.Dominio
{
    public class Caminhao
    {
        public Caminhao()
        {
            AnoFabricacao = DateTime.Now.Year;
        }

        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]
        public string Descricao { get; set; }
        [Required]
        public int MarcaId { get; set; }
        [Required]
        public int ModeloId { get; set; }
        [Required]
        public int AnoFabricacao { get; set; }
        [Required]
        public int AnoModelo { get; set; }

        public virtual Modelo Modelo { get; set; }
        public virtual Marca Marca { get; set; }
    }
}
