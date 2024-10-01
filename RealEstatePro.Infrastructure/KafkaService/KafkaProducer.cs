using Confluent.Kafka;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RealEstatePro.Application.KafkaService;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using static Confluent.Kafka.ConfigPropertyNames;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RealEstatePro.Infrastructure.KafkaService;
public class KafkaProducer(
    IProducer<Null, string> _producer,
    ILogger<KafkaProducer> _logger)
    : IKafkaProducer
{
    public async Task LogErrorAsync(string message, string source, string? exception, CancellationToken cancellationToken)
    {
        var error = new ErrorLog
        {
            Id = Guid.NewGuid(),
            Source = source,
            Message = message,
            Exception = exception,
            CreatedOnUtc = DateTime.UtcNow
        };

        await ProduceErrorAsync(error, cancellationToken);
    }

    public async Task ProduceErrorAsync(ErrorLog errorLog, CancellationToken cancellationToken)
    {
        var jsonMessage = JsonSerializer.Serialize(errorLog);

        try
        {
            var message = await _producer.ProduceAsync(topic: "error-log", new Message<Null, string>
            {
                Value = jsonMessage
            }, cancellationToken);

            _logger.LogInformation($"Delivered message to Kafka: {DateTime.Now}, Offset:{message.Offset}");
        }
        catch (ProduceException<Null, string> e)
        {
            _logger.LogError($"Delivered failed: {e.Error.Reason}");
        }

        _producer.Flush(cancellationToken);
    }
}
