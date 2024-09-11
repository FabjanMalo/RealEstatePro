using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Domain.Reservations;
public enum ReservationStatus
{
    Reserved = 1,
    Confirmed,
    Rejected,
    Completed,
    Rescheduled
}
