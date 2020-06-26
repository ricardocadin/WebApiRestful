using DomainValidation.Validation;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Domain.Interfaces.Repository;
using TCC.MyBooksRead.Domain.Specification.Metas;

namespace TCC.MyBooksRead.Domain.Validation.Metas
{
    public class MetaAptaParaAtualizacao : Validator<MetasPorLivros>
    {
        public MetaAptaParaAtualizacao(IMetasPorLivrosRepository metasPorLivrosRepository)
        {
            var paginasOuCapitulosLidos = new PaginasOuCapitulosDeveSerMaiorQueAnterior(metasPorLivrosRepository);

            base.Add("paginasOuCapitulosLidos", new Rule<MetasPorLivros>(paginasOuCapitulosLidos, "O número de páginas/capitulos não deve ser menor que a última atualização feita."));
        }
    }
}
