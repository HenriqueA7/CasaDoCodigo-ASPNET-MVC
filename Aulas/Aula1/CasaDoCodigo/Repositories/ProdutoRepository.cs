using CasaDoCodigo.Models;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;
using System.Collections.Generic;
using System.Linq;

namespace CasaDoCodigo.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly ApplicationContext contexto;

        public ProdutoRepository(ApplicationContext contexto)
        {
            this.contexto = contexto;
        }

        public IList<Produto> GetProdutos()
        {
            return contexto.Set<Produto>().ToList();
        }

        public void SaveProdutos(List<Livro> livros)
        {
            //insere dados no danco de dados
            foreach (var livro in livros)
            {
                if (contexto.Set<Produto>())
                contexto.Set<Produto>().Add(new Produto(livro.Codigo, livro.Nome, livro.Preco));
            }
            contexto.SaveChanges();
        }
    }
    public class Livro
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
    }
}
