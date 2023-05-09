using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.DTOs.Request
{
    public class ProdutoRequestObjects
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }

        public Guid CategoriaId { get; set; }
        public Guid UsuarioId { get; set; }

        public CategoriaRequest? Categoria { get; set; }
        public UserCreationRequest? User { get; set; }

        public ProdutoRequestObjects()
        {
            Categoria = new CategoriaRequest();
            User = new UserCreationRequest();
        }
    }
}
