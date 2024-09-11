using RealEstatePro.Domain.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Domain.Roles;
public class UserRole
{
    [Key]
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public List<User> Users { get; private set; }
}
