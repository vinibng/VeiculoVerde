﻿// <copyright file="IVeiculoSubstituidoRepository.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace VeiculoVerde.Domain.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using VeiculoVerde.Domain.Entities;

    /// <summary>
    /// Interface para operações de acesso a dados relacionadas à entidade <see cref="VeiculoSubstituido"/>.
    /// </summary>
    public interface IVeiculoSubstituidoRepository
    {
        /// <summary>
        /// Obtém um <see cref="VeiculoSubstituido"/> pelo identificador.
        /// </summary>
        /// <param name="id">Identificador do veículo substituído.</param>
        /// <returns>O veículo substituído correspondente.</returns>
        Task<VeiculoSubstituido> GetByIdAsync(int id);

        /// <summary>
        /// Obtém todos os veículos substituídos.
        /// </summary>
        /// <returns>Uma coleção de veículos substituídos.</returns>
        Task<IEnumerable<VeiculoSubstituido>> GetAllAsync();

        /// <summary>
        /// Adiciona um novo veículo substituído.
        /// </summary>
        /// <param name="entity">Entidade do veículo substituído a ser adicionada.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
        Task AddAsync(VeiculoSubstituido entity);

        /// <summary>
        /// Atualiza um veículo substituído existente.
        /// </summary>
        /// <param name="entity">Entidade do veículo substituído a ser atualizada.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
        Task UpdateAsync(VeiculoSubstituido entity);

        /// <summary>
        /// Exclui um veículo substituído pelo identificador.
        /// </summary>
        /// <param name="id">Identificador do veículo substituído a ser excluído.</param>
        /// <returns>Uma tarefa que representa a operação assíncrona.</returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Busca veículos substituídos que atendam ao predicado especificado.
        /// </summary>
        /// <param name="predicate">Expressão de filtro.</param>
        /// <returns>Uma coleção de veículos substituídos que atendem ao filtro.</returns>
        Task<IEnumerable<VeiculoSubstituido>> FindAsync(Expression<Func<VeiculoSubstituido, bool>> predicate);

        /// <summary>
        /// Obtém veículos substituídos de forma paginada, opcionalmente filtrados por um predicado.
        /// </summary>
        /// <param name="pageNumber">Número da página.</param>
        /// <param name="pageSize">Tamanho da página.</param>
        /// <param name="predicate">Expressão de filtro opcional.</param>
        /// <returns>Uma tupla contendo os itens da página e o total de registros.</returns>
        Task<(IEnumerable<VeiculoSubstituido> Items, int TotalCount)> GetPaginatedAsync(int pageNumber, int pageSize, Expression<Func<VeiculoSubstituido, bool>>? predicate = null);
    }
}