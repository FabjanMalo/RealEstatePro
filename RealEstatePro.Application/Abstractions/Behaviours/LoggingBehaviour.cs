using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstatePro.Application.Abstractions.Behaviours;
public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
{
    public readonly ILogger<TRequest> _logger;
    public LoggingBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
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
            throw;
        }
    }
}
