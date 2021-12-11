using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Volvo.Teste.Dominio
{
    public class Modelo
    {
        public Modelo()
        {

        }

        public int Id { get; set; }

        [Required]
        public int MarcaId { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]
        public string Descricao { get; set; }

        [Required]
        public bool ModeloPermitido { get; set; }

        public virtual Marca Marca { get; set; }
        public virtual ICollection<Caminhao> Caminhoes { get; set; }
    }
}
