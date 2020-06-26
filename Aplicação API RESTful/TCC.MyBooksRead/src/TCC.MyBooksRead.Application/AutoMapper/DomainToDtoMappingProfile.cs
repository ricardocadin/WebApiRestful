using AutoMapper;
using TCC.MyBooksRead.Application.DTO;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Application.AutoMapper
{
    /// <summary>
    /// Responsável por configurar os mapeamentos do domínio para os DTOs
    /// </summary>
    public class DomainToDtoMappingProfile : Profile
    {
        /// <summary>
        /// Método responsável por criar o nome do perfil para configuração
        /// </summary>
        public override string ProfileName
        {
            get {return "DomainToDtoMappingProfile"; }
        }
        
        /// <summary>
        /// Responsável por registrar os mapeamentos
        /// </summary>
        protected override void Configure()
        {
            Mapper.CreateMap<Livros, LivrosDTO>();
            Mapper.CreateMap<Autores, AutoresDTO>();
            Mapper.CreateMap<Categorias, CategoriasExistentesDTO>();
            Mapper.CreateMap<Metas, MetasExistentesDTO>();
            Mapper.CreateMap<MetasPorLivros, MetasLivrosDTO>();
            Mapper.CreateMap<Usuarios, UsuariosDTO>();
        }
    }
}
