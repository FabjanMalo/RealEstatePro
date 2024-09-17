using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Abstractions.Contracts.AuthService;
public interface IAuthManager
{
    Task<string> CreateToken(User user);
}
