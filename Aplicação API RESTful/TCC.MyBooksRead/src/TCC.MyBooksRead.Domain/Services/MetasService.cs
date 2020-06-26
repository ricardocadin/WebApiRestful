using System;
using System.Collections.Generic;
using System.Linq;
using DomainValidation.Validation;
using TCC.MyBooksRead.Domain.Entities;
using TCC.MyBooksRead.Domain.Enumerators;
using TCC.MyBooksRead.Domain.Interfaces.CrossCutting;
using TCC.MyBooksRead.Domain.Interfaces.Repository;
using TCC.MyBooksRead.Domain.Interfaces.Services;
using TCC.MyBooksRead.Domain.Validation.Metas;

namespace TCC.MyBooksRead.Domain.Services
{
    /// <summary>
    /// Metas Domain Service - responsável por validar a entidade e persistir a mesma na base, 
    ///                         ele que orquestra as regras para tomar uma decisão.
    /// </summary>
    public class MetasService : IMetasService
    {
        private readonly IMetasPorLivrosRepository _metasPorLivrosRepository;
        private readonly IMetasRepository _metasRepository;
        private readonly IEnumExtender _enumExtender;
        private readonly ILivrosRepository _livroRepository;

        public MetasService(IMetasPorLivrosRepository metasPorLivrosRepository,
                            IMetasRepository metasRepository,
                            IEnumExtender enumExtender,
                            ILivrosRepository livrosRepository)
        {
            _metasPorLivrosRepository = metasPorLivrosRepository;
            _metasRepository = metasRepository;
            _enumExtender = enumExtender;
            _livroRepository = livrosRepository;
        }

        /// <summary>
        /// Método responsável por buscar todas as metas cadastradas pertencentes a um livro
        /// </summary>
        /// <param name="livroId">Identificador do livro</param>
        /// <param name="total">Utilizado para paginação de registros</param>
        /// <param name="skip">Utilizado para paginação de registros</param>
        /// <param name="take">Utilizado para paginação de registros</param>
        /// <returns></returns>
        public IEnumerable<MetasPorLivros> MetasPorLivro(Guid livroId, out int total, int skip = 0, int take = 10)
        {
            return _metasPorLivrosRepository.MetasPorLivro(livroId, out total, skip, take);
        }

        /// <summary>
        /// Método responsável por buscar todas as metas disponíveis no sistema
        /// </summary>
        /// <returns>Lista com as metas disponíveis</returns>
        public IEnumerable<Metas> BuscarMetasDominio()
        {
            return _metasRepository.GetAll().OrderBy(x => x.Descricao);
        }

        /// <summary>
        /// Método responsável por adicionar uma meta
        /// </summary>
        /// <param name="meta">Entidade</param>
        public MetasPorLivros Add(MetasPorLivros meta)
        {
            var result = new MetaAptaParaInclusaoValidation(_metasPorLivrosRepository).Validate(meta);

            if (!result.IsValid)
            {
                meta.ValidationResult = result;
                return meta;
            }

            if (meta.MetaDominioId == _enumExtender.GetEnumGuid(MetasEnum.LeituraPorPaginas) ||
                meta.MetaDominioId == _enumExtender.GetEnumGuid(MetasEnum.LeituraPorCapitulos))
            {
                var livro = _livroRepository.GetById(meta.LivroId);
                meta.SetDataFim(meta, livro);
            }

            meta.ValidationResult = result;
            _metasPorLivrosRepository.Add(meta);
            return meta;
        }

        /// <summary>
        /// Método responsável por retornar informações de já metas cadastradas por identificador
        /// </summary>
        /// <param name="id">Identificador da meta</param>
        /// <returns>Meta</returns>
        public MetasPorLivros GetById(Guid id)
        {
            return _metasPorLivrosRepository.GetById(id);
        }

        /// <summary>
        /// Método responsável por atualizar informações da meta
        /// </summary>
        /// <param name="meta">Entidade meta</param>
        public MetasPorLivros Update(MetasPorLivros meta)
        {

            var result = new MetaAptaParaAtualizacao(_metasPorLivrosRepository).Validate(meta);
            if (!result.IsValid)
            {
                meta.ValidationResult = result;
                return meta;
            }

            if (meta.MetaDominioId == _enumExtender.GetEnumGuid(MetasEnum.LeituraPorPaginas) ||
                meta.MetaDominioId == _enumExtender.GetEnumGuid(MetasEnum.LeituraPorCapitulos))
            {
                var livro = _livroRepository.GetById(meta.LivroId);
                meta.SetDataPrevista(meta, livro);
            }

            meta.ValidationResult = result;
            _metasPorLivrosRepository.Update(meta);
            return meta;
        }

        /// <summary>
        /// Método responsável por deletar uma meta
        /// </summary>
        /// <param name="metaId">Identificador da meta</param>
        public void Delete(Guid metaId)
        {
            var metaDominio = _metasPorLivrosRepository.GetById(metaId);
            _metasPorLivrosRepository.Delete(metaDominio);
        }

        /// <summary>
        /// Método responsável por limpar o contexto da memória
        /// </summary>
        public void Dispose()
        {
            _metasRepository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
