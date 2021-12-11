using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Volvo.Teste.Dominio
{
    public class Marca
    {
        public Marca()
        {

        }

        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        [Required]
        public string Descricao { get; set; }

        public virtual ICollection<Modelo> Modelos { get; set; }
        public virtual ICollection<Caminhao> Caminhoes { get; set; }
    }
}
