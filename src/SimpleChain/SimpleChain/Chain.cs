using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SimpleChain
{
    /// <summary>
    /// A chain container that executes a chain of handlers.
    /// </summary>
    /// <typeparam name="T">Type of input.</typeparam>
    public class Chain<T>
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ChainOptions<T> _options;

        public Chain(IServiceProvider serviceProvider, IOptions<ChainOptions<T>> options)
        {
            _serviceProvider = serviceProvider;
            _options = options.Value;
        }

        /// <summary>
        /// Execute chain for determined input.
        /// </summary>
        /// <param name="input">Handlers Input.</param>
        /// <returns>Returns the passed input processed by handlers.</returns>
        public Task<T> ExecuteAsync(T input)
        {
            // Create a list of handlers
            var handlers = new List<IChainHandler<T>>();

            // Get handlers from DI
            // Handlers are registered by method AddHandler in ChainContainerOptions
            foreach (var handlerType in _options.GetHandlerTypes())
            {
                var handler = (IChainHandler<T>)_serviceProvider.GetRequiredService(handlerType);
                handlers.Add(handler);
            }

            // Set Next for all handlers
            // Handlers execute by order of registration
            for (var i = 0; i < handlers.Count - 1; i++)
            {
                handlers[i].SetNext(handlers[i + 1]);
            }

            // Execute first handler
            return handlers.First().HandleAsync(input);
        }
    }
}
