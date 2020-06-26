using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Infra.Data.Mapping;

namespace TCC.MyBooksRead.Infra.Data.Context
{
    public class MyBooksReadContext : DbContext
    {
        /// <summary>
        /// Contrutor da Classe, responsável por chamar o construtor base do EntityFramework passando a string de conexão
        /// configurada no arquivo WebConfig na Camada de Serviço. Além disso, desativa a configuração de Proxy
        /// do Entity Framework a fim de melhorar a perfomance e também desativa o carregamento em LazyLoading das Entidades
        /// </summary>
        public MyBooksReadContext() : base("MyBooksRead")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        //Registra os DbSet das entidades
        public DbSet<Livros> Livros { get; set; }
        public DbSet<MetasPorLivros> Metas { get; set; }
        public DbSet<Metas> MetasDominio { get; set; }
        public DbSet<Categorias> CategoriasDominio { get; set; }
        public DbSet<Usuarios> Usuarios { get; set; }


        /// <summary>
        /// Sobreescrita do método a fim de configurar ações específicas no momento da criação do modelo
        /// - Registra mapeamentos
        /// - Configurações padrões dos mapeamentos
        /// - Remove convenções
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            
            modelBuilder.Properties()
                .Where(p => p.Name == p.ReflectedType.Name + "Id")
                .Configure(p => p.IsKey());

            modelBuilder.Properties<string>()
                .Configure(p => p.HasColumnType("varchar"));

            modelBuilder.Configurations.Add(new LivrosMapping());
            modelBuilder.Configurations.Add(new AutoresMapping());
            modelBuilder.Configurations.Add(new MetasMapping());
            modelBuilder.Configurations.Add(new CategoriasMapping());
            modelBuilder.Configurations.Add(new MetasPorLivrosMapping());
            modelBuilder.Configurations.Add(new UsuariosMapping());

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Método responsável por interceptar todas as mudanças ocorridas no contexto de entidades e verifica se foi uma mudança
        /// de atualização ou de cadastro. Após detectar, seta automaticamente a data de cadastro.
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {

            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }
                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }
            return base.SaveChanges();
        }


    }
}
