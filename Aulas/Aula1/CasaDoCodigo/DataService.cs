//Classe de configuração
//Banco de dados

using CasaDoCodigo.Models;
using CasaDoCodigo.Repositories;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace CasaDoCodigo
{
    //Classe criada em Startup.cs > Ctrl+. Mover tipo para DataService.cs (cria um novo arquivo)
    //Ctrl+. extrair interface
    class DataService : IDataService
    {
        private readonly ApplicationContext contexto;
        private readonly IProdutoRepository produtoRepository;

        public DataService(ApplicationContext contexto, IProdutoRepository produtoRepository)
        {
            this.contexto = contexto;
            this.produtoRepository = produtoRepository;
        }

        public void InicializaDB()
        {
            //EnsureCreated - Garante que tenha sido criada
            contexto.Database.EnsureCreated();

            List<Livro> livros = GetLivros();

            produtoRepository.SaveProdutos(livros);
        }

        

        private static List<Livro> GetLivros()
        {
            //Lê os arquivos do json
            var json = File.ReadAllText("Livros.json");
            //converte arquivo em objeto
            var livros = JsonConvert.DeserializeObject<List<Livro>>(json);
            return livros;
        }

      
    }
}
