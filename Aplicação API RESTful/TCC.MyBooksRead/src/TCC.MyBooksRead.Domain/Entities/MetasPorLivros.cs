using System;
using DomainValidation.Validation;

namespace TCC.MyBooksRead.Domain.Entities
{
    /// <summary>
    /// Entidade Metas por livros
    /// </summary>
    public class MetasPorLivros
    {
        public MetasPorLivros()
        {
            MetaId = Guid.NewGuid();
        }

        public Guid MetaId { get; set; }
        public string Descricao { get; set; }
        public bool MetaCumprida { get; set; }
        public DateTime? DataPrevista { get; set; }
        public DateTime? DataFim { get; set; }
        public DateTime DataCadastro { get; set; }
        public int PaginasDiarias { get; set; }
        public int CapitulosDiarios { get; set; }
        public int PaginasOuCapitulosLidos { get; set; }
        public ValidationResult ValidationResult { get; set; }

        public Guid MetaDominioId { get; set; }
        public virtual Metas Metas { get; set; }
        public Guid LivroId { get; set; }
        public virtual Livros Livros { get; set; }


        /// <summary>
        /// Método responsável por atribuir a data final que a meta será concluída, esta data não será modificada
        /// </summary>
        public void SetDataFim(MetasPorLivros meta, Livros livro)
        {
            if (PaginasDiarias > 0)
            {
                var dias = (livro.QuantidadePaginas / PaginasDiarias);
                DataFim = DateTime.Now.AddDays(dias);
            }
            else if (CapitulosDiarios > 0)
            {
                var dias = (livro.QuantidadeCapitulos / CapitulosDiarios);
                DataFim = DateTime.Now.AddDays(dias);
            }

        }

        /// <summary>
        /// Método responsável por calcular a data prevista de conclusão da meta, de acordo com as atualizações do usuário
        /// </summary>
        /// <param name="meta"></param>
        public void SetDataPrevista(MetasPorLivros meta, Livros livro)
        {
            var diferencaEntreDatas = DateTime.Now.Subtract(DataCadastro);
            var leituraEsperada = diferencaEntreDatas.Days * PaginasDiarias;
            

            if (PaginasDiarias > 0)
            {
                if (PaginasOuCapitulosLidos < leituraEsperada)
                {
                    var diasEsperados = livro.QuantidadePaginas / PaginasDiarias;

                    var diasAtrasados = diasEsperados - ((livro.QuantidadePaginas - PaginasOuCapitulosLidos) / PaginasDiarias);

                    var dias = Math.Ceiling((double) diasAtrasados);

                    if (DataFim != null) DataPrevista = DataFim.Value.AddDays(dias);
                }
                else
                {
                    var diasAdiantados = (livro.QuantidadePaginas - PaginasOuCapitulosLidos) / PaginasDiarias;
                    DataPrevista = DataCadastro.AddDays(diasAdiantados);
                }
            }
            else if (CapitulosDiarios > 0)
            {
                if (PaginasOuCapitulosLidos < leituraEsperada)
                {
                    var diasEsperados = livro.QuantidadeCapitulos / CapitulosDiarios;

                    var diasAtrasados = diasEsperados - ((livro.QuantidadeCapitulos - PaginasOuCapitulosLidos) / CapitulosDiarios);
                    
                    var dias = Math.Ceiling((double)diasAtrasados);
                    if (DataFim != null) DataPrevista = DataFim.Value.AddDays(dias);
                }
                else
                {
                    var diasAdiantados = (livro.QuantidadeCapitulos - PaginasOuCapitulosLidos) / CapitulosDiarios;
                    DataPrevista = DataCadastro.AddDays(diasAdiantados);
                }
            }
        }
    }
}
