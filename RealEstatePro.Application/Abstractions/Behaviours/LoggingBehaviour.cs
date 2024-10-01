using MediatR;
using Microsoft.Extensions.Logging;
using RealEstatePro.Application.KafkaService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Abstractions.Behaviours;
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    private readonly ILogger<TRequest> _logger;

    private readonly IKafkaProducer _kafkaProducer;
    public LoggingBehaviour(ILogger<TRequest> logger, IKafkaProducer kafkaProducer)
    {
        _logger = logger;
        _kafkaProducer = kafkaProducer;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var name = request.GetType().Name;

        try
        {
            _logger.LogInformation($"Executing command {name}");

            var result = await next();

            _logger.LogInformation($"Command {name} processed successfully");

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Command {name} failed");

            await _kafkaProducer.LogErrorAsync($"Command {name} failed", name, ex.ToString(), cancellationToken);

            throw;
        }
    }
}
