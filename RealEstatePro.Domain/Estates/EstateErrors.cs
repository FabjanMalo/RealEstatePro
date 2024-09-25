using RealEstatePro.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Domain.Estates;
public static class EstateErrors
{
    public static readonly Error InvalidUserId =
          Error.BadRequest("Estate.InvalidUserId", "User not found for the provided User ID.");

    public static readonly Error NameNotUnique =
        Error.Conflict("Estate.NameNotUnique", "The provided name is already taken. Please try another name.");

    public static readonly Error InvalidEstateId =
          Error.BadRequest("Estate.InvalidEstateId", "Estate not found for the provided Estate ID.");

    public static readonly Error ImageLimitExceeded =
         Error.BadRequest("Estate.ImageLimitExceeded", "You are allowed to have a maximum of 5 photos for an estate.");
}
