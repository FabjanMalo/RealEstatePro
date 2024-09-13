using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Abstractions.Contracts;

public interface IGenericRepository<T> where T : class
{
    Task<T> Add(T entity);
    void Update(T entity);
    void Delete(T entity);
}
