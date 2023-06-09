﻿using System.Reflection;
using KozakBank.Application.Repositories;
using KozakBank.Domain.Common;
using KozakBank.Persistence.Common.Utils;
using Microsoft.EntityFrameworkCore;

namespace KozakBank.Persistence.Repositories;

public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
{
    private readonly DataContext.DataContext _context;

    public BaseRepository(DataContext.DataContext context)
    {
        _context = context;
    }

    public async Task<TEntity?> GetAsync(Guid id, CancellationToken cls)
    {
        return await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id, cls);
    }

    public async Task<IQueryable<TEntity>> GetAllAsync(CancellationToken cls)
    {
        return _context.Set<TEntity>();
    }

    public async Task<IEnumerable<TEntity>> GetByProperties(TEntity entity, CancellationToken cls)
    {
        var props = entity
            .GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public);
        var propsWithValue = props
            .Where(x => !CheckOnNullOrDefault.IsDefaultValue(x.GetValue(entity))).ToList();
        return _context.Set<TEntity>().AsEnumerable()
                .Where(x => propsWithValue.All(prop => prop.GetValue(x)!.Equals(prop.GetValue(entity))))
            ;
    }

    public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cls)
    {
        entity.CreatedOnly = DateOnly.FromDateTime(DateTime.Now);
        var result = await _context.Set<TEntity>().AddAsync(entity, cls);
        
        return result.Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cls)
    {
        entity.UpdatedAt = DateOnly.FromDateTime(DateTime.Now);
        var result = _context.Set<TEntity>().Update(entity);

        return result.Entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity, CancellationToken cls)
    {
        var entityToDelete = await GetAsync(entity.Id, cls);

        var result = _context.Set<TEntity>().Remove(entityToDelete);

        return result.Entity;
    }
}