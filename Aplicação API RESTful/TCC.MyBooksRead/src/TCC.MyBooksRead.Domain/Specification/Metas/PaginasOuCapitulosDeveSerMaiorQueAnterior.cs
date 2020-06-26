using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainValidation.Interfaces.Specification;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Domain.Interfaces.Repository;

namespace TCC.MyBooksRead.Domain.Specification.Metas
{
    /// <summary>
    /// Classe responsável por validar se o número de páginas/capitulos que o usuário leu e está atualizando é maior que
    /// o que já esta previamente armazenado.
    /// </summary>
    public class PaginasOuCapitulosDeveSerMaiorQueAnterior : ISpecification<MetasPorLivros>
    {
        private readonly IMetasPorLivrosRepository _metasPorLivrosRepository;

        public PaginasOuCapitulosDeveSerMaiorQueAnterior(IMetasPorLivrosRepository metasPorLivrosRepository)
        {
            _metasPorLivrosRepository = metasPorLivrosRepository;
        }

        /// <summary>
        /// Verifica se o número de páginas/capitulos que o usuário já leu é maior do que o anterior armazenado
        /// </summary>
        /// <param name="meta"></param>
        /// <returns></returns>
        public bool IsSatisfiedBy(MetasPorLivros meta)
        {
            var metaArmazenada = _metasPorLivrosRepository.GetById(meta.MetaId);
            return meta.PaginasOuCapitulosLidos >= metaArmazenada.PaginasOuCapitulosLidos;
        }
    }
}
