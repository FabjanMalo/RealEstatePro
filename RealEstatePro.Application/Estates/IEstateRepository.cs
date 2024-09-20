using RealEstatePro.Application.Abstractions.Contracts;
using RealEstatePro.Domain.Estates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Estates;
public interface IEstateRepository : IGenericRepository<Estate>
{
    Task<bool> IsUniqueName(string name, CancellationToken cancellationToken);
}
