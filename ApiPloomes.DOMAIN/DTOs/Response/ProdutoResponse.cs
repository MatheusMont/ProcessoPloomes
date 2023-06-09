﻿using ApiPloomes.DOMAIN.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiPloomes.DOMAIN.DTOs.Response
{
    public class ProdutoResponse
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        public CategoriaResponse Categoria { get; set; }
        public UserResponse Usuario { get; set; }

        public ProdutoResponse(string nome, string descricao, double preco, CategoriaResponse categoria, UserResponse usuario)
        {
            Nome = nome;
            Descricao = descricao;
            Preco = preco;
            Categoria = categoria;
            Usuario = usuario;
        }
    }
}
