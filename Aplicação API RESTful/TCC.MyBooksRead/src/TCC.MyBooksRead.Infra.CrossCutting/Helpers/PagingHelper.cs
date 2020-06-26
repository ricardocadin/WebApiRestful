
namespace TCC.MyBooksRead.Infra.CrossCutting.Helpers
{
    /// <summary>
    /// Classe responsável por centralizar o cálculo da paginação em requisições GET
    /// </summary>
    public static class PagingHelper
    {
        private const int limit = 2;

        /// <summary>
        /// Método responsável por calcular a quantidade de registros que devem ser pulados de um total
        /// </summary>
        /// <param name="paging">Página solicitada</param>
        /// <returns></returns>
        public static int GetOffset(int? paging)
        {
            var i = (paging * limit) - limit;
            if (i == null) return 0;
            int skip = (int) i;
            return skip;
        }

        /// <summary>
        /// Retorna o limite de dados quando se usa paginação.
        /// </summary>
        /// <returns></returns>
        public static int GetTake()
        {
            return limit;
        }
    }
}
