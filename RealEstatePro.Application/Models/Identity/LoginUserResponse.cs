using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Models.Identity;
public class LoginUserResponse
{
    public string Token { get; set; }

    public string Type { get; set; } = "Bearer Token";
    public int Expiration { get; set; } = 3600;

    public LoginUserResponse(string token)
    {
        Token = token;
    }

}
