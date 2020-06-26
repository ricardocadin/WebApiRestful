using DomainValidation.Validation;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Domain.Interfaces.Repository;
using TCC.MyBooksRead.Domain.Specification.Livros;

namespace TCC.MyBooksRead.Domain.Validation
{
    /// <summary>
    /// Classe responsável por validar através do padrão Specification todas as regras pertencentes a entidade Livro.
    /// </summary>
    public class LivroAptoParaInclusaoValidation : Validator<Livros>
    {
        /// <summary>
        /// Responsável por obter uma instância da classe Specification e adicionar novas regras de validações para entidade
        /// </summary>
        /// <param name="livrosRepository">Instância da interface para Injeção de dependência</param>
        public LivroAptoParaInclusaoValidation(ILivrosRepository livrosRepository)
        {
            var livroDuplicado = new LivroDevePossuirCadastroUnico(livrosRepository);
             
            base.Add("livroDuplicado", new Rule<Livros>(livroDuplicado, "Livro já se encontra cadastrado! Por favor, consulte ao invés de cadastrar o mesmo livro."));
        }
    }
}
