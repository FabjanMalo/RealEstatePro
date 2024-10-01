using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.KafkaService;
public class ErrorLog
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public string Source { get; set; }
    public string? Exception { get; set; }
    public DateTime CreatedOnUtc { get; set; }
}
