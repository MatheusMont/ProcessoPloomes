using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.DTOs.Request
{
    public class CategoriaRequest
    {
        public string Nome { get; set; }

        public CategoriaRequest(string nome)
        {
            Nome = nome;
        }

        public CategoriaRequest() { }
    }
}
