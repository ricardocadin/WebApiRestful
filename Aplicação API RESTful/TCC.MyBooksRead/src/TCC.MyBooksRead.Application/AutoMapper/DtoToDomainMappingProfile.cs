using AutoMapper;
using TCC.MyBooksRead.Application.DTO;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Application.AutoMapper
{
    /// <summary>
    /// Responsável por configurar os mapeamentos dos DTOs para o Domínio
    /// </summary>
    public class DtoToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DtoToDomainMappingProfile"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<LivrosDTO, Livros>();
            Mapper.CreateMap<AutoresDTO, Autores>();
            Mapper.CreateMap<CategoriasExistentesDTO, Categorias>();
            Mapper.CreateMap<MetasExistentesDTO, Metas>();
            Mapper.CreateMap<MetasLivrosDTO, MetasPorLivros>();
            Mapper.CreateMap<UsuariosDTO, Usuarios>();

        }
    }
}
