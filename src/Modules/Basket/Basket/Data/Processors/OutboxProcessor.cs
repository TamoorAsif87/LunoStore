using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Basket.Data.Processors;

public class OutboxProcessor(IBus bus, IServiceProvider serviceProvider, ILogger<OutboxProcessor> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while(!stoppingToken.IsCancellationRequested)
        {
			try
			{
				var scope = serviceProvider.CreateScope();
				var dbContext = scope.ServiceProvider.GetRequiredService<BasketContext>();

				var outBoxMessages = await dbContext.OutboxMessages
					.Where(m => m.ProcessOn == null)
					.ToListAsync(stoppingToken);

				foreach (var message in outBoxMessages)
				{
					var eventType = Type.GetType(message.Type);
                    if (eventType == null)
                    {
						logger.LogWarning("Could not resolve type: {Type}", message.Type);
                        continue;
                    }

					var eventMessage = JsonSerializer.Deserialize(message.Content, eventType);
					if (eventMessage == null)
                    {
                        logger.LogWarning("Could not deserialize message: {Content}", message.Content);
                        continue;
                    }

					await bus.Publish(eventMessage, stoppingToken);

					message.ProcessOn = DateTime.UtcNow;

					logger.LogInformation("Successfully processed outbox message with ID: {Id}", message.Id);
                }

				await dbContext.SaveChangesAsync(stoppingToken);

			}
			catch (Exception ex)
			{

                logger.LogError(ex, "Error processing outbox messages");
            }

			await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
        }
    }
}
