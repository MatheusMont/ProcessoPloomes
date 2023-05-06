using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.Models
{
    public class Produto : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public Categoria Categoria { get; set; }
        public User Usuario { get; set; }
    }
}
