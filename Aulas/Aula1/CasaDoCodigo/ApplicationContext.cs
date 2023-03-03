using CasaDoCodigo.Models;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace CasaDoCodigo
{
    //Classe ApplicationContext herda do DbContext
    //DbContext é uma classe do namespace Microsoft.EntityFrameworkCore. Uma instância DbContext representa uma sessão com o banco de dados e pode ser usada para consultar e salvar instâncias das suas entidades. O DbContext é uma combinação dos padrões Unit Of Work e Repository.
    //Ctrl + . (na linha da classe) - Using Entity Framework
    //Ctrl + . (na classe) - Generate constructor 'ApplicationContext(options)' - Criar construtor
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        //public DbSet<Produto> Produtos { get; set; }
        //public DbSet<ItemPedido> ItemPedidos { get; set; }
        //public DbSet<Cadastro> Cadastros { get; set; }
        //public DbSet<Pedido> Pedidos { get; set; }
        //override para substutuir um metodo para criar um modelo, fazer o mapeamento
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Registrar classe produto, mapear e adicionar ao modelo
            //Ctrl + . (na lina) - Using CasaDoCodigo.Models
            //HasKey - método para registrar q tem primaryKey
            //(t => t.Id) - expressão lambda q representa chave primária
            modelBuilder.Entity<Produto>().HasKey(t => t.Id);

            modelBuilder.Entity<Pedido>().HasKey(t => t.Id);
            //Relacionamento Pedido HasMany(um para muitos itens) WithOne (relacionamento de volta itens para um pedido)
            modelBuilder.Entity<Pedido>().HasMany(t => t.Itens).WithOne(t => t.Pedido);
            //Relacionamento entre pedido e cadastro HasOne (1 para 1)
            //modelBuilder.Entity<Pedido>().HasOne(t => t.Cadastro).WithOne(t => t.Pedido).IsRequired();
            modelBuilder.Entity<Pedido>().HasOne(t => t.Cadastro).WithOne(t => t.Pedido).HasForeignKey<Pedido>(t => t.CadastroId).IsRequired();


            modelBuilder.Entity<ItemPedido>().HasKey(t => t.Id);
            modelBuilder.Entity<ItemPedido>().HasOne(t => t.Pedido);
            modelBuilder.Entity<ItemPedido>().HasOne(t => t.Produto);

            modelBuilder.Entity<Cadastro>().HasKey(t => t.Id);
            modelBuilder.Entity<Cadastro>().HasOne(t => t.Pedido);
        }
    }
}
