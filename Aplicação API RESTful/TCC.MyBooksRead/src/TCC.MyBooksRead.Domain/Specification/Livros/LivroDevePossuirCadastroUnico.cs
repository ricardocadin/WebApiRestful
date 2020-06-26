using System.Linq;
using DomainValidation.Interfaces.Specification;
using TCC.MyBooksRead.Domain.Interfaces.Repository;

namespace TCC.MyBooksRead.Domain.Specification.Livros
{
    /// <summary>
    ///     Classe responsável por validar se o livro existe unicamente no contexto da aplicação.
    ///     Para tal validação, será necessário verificar seu título, edição e autores.
    /// </summary>
    public class LivroDevePossuirCadastroUnico : ISpecification<Entities.Livros>
    {
        private readonly ILivrosRepository _livrosRepository;

        public LivroDevePossuirCadastroUnico(ILivrosRepository livrosRepository)
        {
            _livrosRepository = livrosRepository;
        }

        /// <summary>
        ///     Responsável por verificar se a condição é aceita ou não.
        /// </summary>
        /// <param name="livro">Instância da entidade Livro a ser avaliada</param>
        /// <returns>Verdadeiro ou Falso</returns>
        public bool IsSatisfiedBy(Entities.Livros livro)
        {
            var livrosContexto = _livrosRepository.GetAll("Autores");
            var livrosRetornados = livrosContexto.ToList();

            if (livro != null && livrosRetornados.Any())
            {
                return livrosRetornados.All(item => !item.Edicao.Equals(livro.Edicao) || !item.UsuarioId.Equals(livro.UsuarioId) || (item.Titulo != livro.Titulo && (item.Titulo == null || !item.Titulo.Equals(livro.Titulo))));
            }

            return true;
        }
    }
}