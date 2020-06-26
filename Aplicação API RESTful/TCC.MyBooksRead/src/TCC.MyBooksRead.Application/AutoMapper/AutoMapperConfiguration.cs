using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace TCC.MyBooksRead.Application.AutoMapper
{
    /// <summary>
    /// Utilizada para configurar o AutoMapper
    /// </summary>
    public class AutoMapperConfiguration
    {
        /// <summary>
        /// Método utilizado para registrar os mapeamentos entre as entidades do Domínio e os DTOs da Aplicação
        /// </summary>
        public static void RegisterMappings()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToDtoMappingProfile>();
                x.AddProfile<DtoToDomainMappingProfile>();
            });
        }
    }
}
