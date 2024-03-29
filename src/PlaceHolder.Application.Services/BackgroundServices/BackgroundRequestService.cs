﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlaceHolder.Application.Services.Ports.Cqrs;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlaceHolder.Application.Services.BackgroundServices
{
    public class BackgroundRequestService : BackgroundService
    {
        private const string _serviceName = nameof(BackgroundRequestService);

        private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;
        private readonly BackgroundRequestQueueManager _queueManager;        
        
        public BackgroundRequestService(
            IServiceProvider serviceProvider,
            ILoggerFactory loggerFactory,
            BackgroundRequestQueueManager queueManager)
        {
            _serviceProvider = serviceProvider;
            _logger = loggerFactory.CreateLogger(_serviceName);
            _queueManager = queueManager;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            var task = base.StartAsync(cancellationToken);

            _logger.LogInformation($"{_serviceName} started");

            return task;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            var task = base.StopAsync(cancellationToken);

            _logger.LogInformation($"{_serviceName} stopped");

            return task;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{_serviceName} execution started");

            while (!stoppingToken.IsCancellationRequested)
            {
                var command = await _queueManager.DequeueAsync(stoppingToken);

                if (command != null)
                {
                    _logger.LogInformation($"{_serviceName} executing {command.GetType().Name}");
                    _ = PublishRequestAsync(command);
                }
            }

            _logger.LogInformation($"{_serviceName} execution ended");
        }

        private async Task PublishRequestAsync(IAsyncCommand command)
        {
            try
            {
                using var scope = _serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();                    
                await mediator.Send(command);
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"An error occured while executing {command.GetType().Name} : {e.Message}");
            }
        }
    }
}
