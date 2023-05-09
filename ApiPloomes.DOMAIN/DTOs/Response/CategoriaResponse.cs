using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.DTOs.Response
{
    public class CategoriaResponse
    {
        public string Nome { get; private set; }

        public CategoriaResponse(string nome)
        {
            Nome = nome;
        }
    }
}
