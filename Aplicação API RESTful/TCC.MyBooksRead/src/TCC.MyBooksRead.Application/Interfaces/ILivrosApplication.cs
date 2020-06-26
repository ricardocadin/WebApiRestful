using System;
using System.Collections.Generic;
using TCC.MyBooksRead.Application.DTO;
using TCC.MyBooksRead.Domain.Entities;

namespace TCC.MyBooksRead.Application.Interfaces
{
    /// <summary>
    /// Interface com as operações obrigando o contrato da application a fim de orquestrar as chamadas para o Domain
    /// </summary>
    public interface ILivrosApplication
    {
        FiltrosPesquisaDTO<LivrosDTO> PesquisarLivros(PesquisaDTO pesquisaDto, int skip = 0, int take = 10);
        FiltrosPesquisaDTO<LivrosDTO> BuscarPorTitulo(LivrosDTO livrosDto, int skip = 0, int take = 10);
        FiltrosPesquisaDTO<LivrosDTO> LivrosPorMeta(PesquisaDTO livrosDto, int skip = 0, int take = 10);
        FiltrosPesquisaDTO<LivrosDTO> LivrosPorCategoria(LivrosDTO livrosDto, int skip = 0, int take = 10);
        FiltrosPesquisaDTO<LivrosDTO> GetAll(LivrosDTO livrosDto, int skip, int take);
        LivrosDTO GetById(Guid id);
        LivrosDTO Add(LivrosDTO livros);
        void Delete(Guid livroId);
    }
}
