﻿using NSE.Core.DomainObjects;

namespace NSE.Catalogo.API.Models
{
    public class Produto : Entity
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public decimal Valor { get; set; }
        
        public string Imagem { get; set; }
        public int QuantidadeEstoque { get; set; }
    }
}
