using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.KafkaService;
public interface IKafkaProducer
{
    Task ProduceErrorAsync(ErrorLog errorLog, CancellationToken cancellationToken);

    Task LogErrorAsync(string message, string source, string? exception, CancellationToken cancellationToken);
}
