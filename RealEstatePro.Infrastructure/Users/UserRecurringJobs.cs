using Hangfire;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Infrastructure.Users;
public class UserRecurringJobs(RealEstateDbContext _dbContext)
{

    private async Task UpdateUserName(string email)
    {
        var user = await _dbContext.Users
            .Where(u => u.Email == email)
            .FirstOrDefaultAsync();

        if (user is not null)
        {
            user.SetLastName($"tomi{DateTime.Now.Minute}");
            _dbContext.Update(user);
            await _dbContext.SaveChangesAsync();
        }

    }

    public void DeleteInactiveUser()
    {
        var oneYearAgo = DateTime.UtcNow.AddYears(-1);

        var inactiveUsers = _dbContext.Users.
            Where(x => x.CreatedOnUtc < oneYearAgo)
            .ToList();
        if (inactiveUsers.Count != 0)
        {
            _dbContext.RemoveRange(inactiveUsers);
            _dbContext.SaveChanges();
        }
    }

    public void UpdateUserNameWrapper(string email)
    {
        UpdateUserName(email).GetAwaiter().GetResult();
    }
}
