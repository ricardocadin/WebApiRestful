using System.Linq;
using DomainValidation.Interfaces.Specification;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Domain.Interfaces.Repository;

namespace TCC.MyBooksRead.Domain.Specification.Metas
{
    /// <summary>
    ///     Classe responsável por verificar se a meta cadastrada já existe para um determinado livro e se a mesma
    ///     ainda está aberta, logo não será possível cadastrar a mesma meta novamente.
    /// </summary>
    public class MetaDeveSerUnicaPorLivro : ISpecification<MetasPorLivros>
    {
        private readonly IMetasPorLivrosRepository _metasPorLivrosRepository;

        public MetaDeveSerUnicaPorLivro(IMetasPorLivrosRepository metasPorLivrosRepository)
        {
            _metasPorLivrosRepository = metasPorLivrosRepository;
        }

        /// <summary>
        /// Método responsável por validar
        /// </summary>
        /// <param name="meta">Instância da nova meta a ser inserida</param>
        /// <returns></returns>
        public bool IsSatisfiedBy(MetasPorLivros meta)
        {
            var metasCadastradas = _metasPorLivrosRepository.GetAll();

            return metasCadastradas.All(item => !item.MetaCumprida && item.LivroId.Equals(meta.LivroId) && item.MetaDominioId.Equals(meta.MetaDominioId));
        }
    }
}
