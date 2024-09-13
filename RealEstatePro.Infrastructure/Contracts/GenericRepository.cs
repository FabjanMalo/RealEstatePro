using RealEstatePro.Application.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Infrastructure.Contracts;
public class GenericRepository<T>
    (RealEstateDbContext _dbContext)
    : IGenericRepository<T> where T : class
{

  
    public async Task<T> Add(T entity)
    {
        await _dbContext.AddAsync(entity);

        return entity;
    }

    public void Delete(T entity)
    {
        _dbContext.Set<T>().Remove(entity);
    }

    public void Update(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }
}
