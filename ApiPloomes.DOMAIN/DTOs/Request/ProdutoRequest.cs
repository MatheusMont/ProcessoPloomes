using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.DTOs.Request
{
    public class ProdutoRequest
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }

        public Guid CategoriaId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
