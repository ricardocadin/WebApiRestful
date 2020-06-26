using System;
using System.Data.Entity.Migrations;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Infra.Data.Context;

namespace TCC.MyBooksRead.Infra.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<MyBooksReadContext>
    {
        /// <summary>
        ///     Construtor responsável por verificar se as migrações devem ser realizadas de forma automática ao mudar
        ///     as entidades ou ao criar novas.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        /// <summary>
        ///     Método responsável por "semear" no momento da criação do banco de dados, dessa forma é possível já
        ///     alimentar o mesmo com valores padrões
        /// </summary>
        /// <param name="context">Contexto</param>
        protected override void Seed(MyBooksReadContext context)
        {
            context.MetasDominio.AddOrUpdate(new Metas() { MetasDominioId = new Guid("BAFA7991-5849-498B-AD93-0DE1AA014D5E"), Descricao = "Leitura por Páginas" });
            context.MetasDominio.AddOrUpdate(new Metas() { MetasDominioId = new Guid("CD4BF702-7689-4FFF-87B3-772F5813DF9F"), Descricao = "Leitura por Capítulos" });
            context.MetasDominio.AddOrUpdate(new Metas() { MetasDominioId = new Guid("D5704B46-2D71-49A3-8B0B-A38D3D55B1A6"), Descricao = "Realizar Resenha/Resumo por Capítulo" });
            context.MetasDominio.AddOrUpdate(new Metas() { MetasDominioId = new Guid("8D71A78E-F389-435F-8E19-43FDF954F460"), Descricao = "Realizar Exemplos Práticos" });
        
            context.CategoriasDominio.AddOrUpdate(new Categorias() { CategoriasDominioId = new Guid("8D71A78E-F389-435F-8E19-43FDF954F460"), Descricao = "Programação"});
            context.CategoriasDominio.AddOrUpdate(new Categorias() { CategoriasDominioId = new Guid("B3102695-BF01-4721-A6CB-B127A15761E2"), Descricao = "Governança de TI" });
            context.CategoriasDominio.AddOrUpdate(new Categorias() { CategoriasDominioId = new Guid("BAAC31D2-D8F1-49D1-9163-696D16B4668C"), Descricao = "Engenharia de Software" });

            context.Usuarios.AddOrUpdate(new Usuarios()
            { 
                CPF = "12214522662",
                DataCadastro = DateTime.Now,
                Email = "ricardo.h.souza@hotmail.com",
                Nome = "Ricardo",
                Sobrenome = "Henrique",
                UsuarioId = new Guid("5705DB9E-A230-4D5B-9EF6-8ECDA44E56A9") });
        }
    }
    
}