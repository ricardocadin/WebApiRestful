using DomainValidation.Validation;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Domain.Interfaces.Repository;
using TCC.MyBooksRead.Domain.Specification.Metas;

namespace TCC.MyBooksRead.Domain.Validation.Metas
{
    public class MetaAptaParaInclusaoValidation : Validator<MetasPorLivros>
    {
        public MetaAptaParaInclusaoValidation(IMetasPorLivrosRepository metasPorLivrosRepository)
        {
            var metaUnica = new MetaDeveSerUnicaPorLivro(metasPorLivrosRepository);

            base.Add("metaUnica", new Rule<MetasPorLivros>(metaUnica, "Esta meta já se encontra cadastrada. Consulte a mesma para mais detalhes."));
        }
    }
}
