using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace SimpleChain
{
    public static class SimpleChainExtensions
    {
        /// <summary>
        /// Add a scoped <see cref="Chain{T}"/> to the service collection.
        /// </summary>
        /// <typeparam name="T">Type of Handler input.</typeparam>
        public static IServiceCollection AddChainFor<T>(this IServiceCollection services, Action<ChainOptions<T>> configure)
        {
            // Add configuration and the ChainContainer of type T to the service collection.
            services.AddScoped<Chain<T>>();
            services.Configure(configure);

            // A configuration with least one handler is required.
            var options = new ChainOptions<T>();

            configure(options);

            if (options.GetHandlerTypes().Count == 0)
                throw new InvalidOperationException("Invalid chain configuration. At least one handler must be registered.");

            // Register the handlers in the service collection.
            foreach (var handler in options.GetHandlerTypes())
            {
                services.TryAddTransient(handler);
            }

            return services;
        }
    }
}
