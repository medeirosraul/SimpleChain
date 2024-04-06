namespace SimpleChain
{
    /// <summary>
    /// Chain Container Options.
    /// </summary>
    /// <typeparam name="T">Type of Handler input.</typeparam>
    public class ChainContainerOptions<T>
    {
        private readonly List<Type> _handlersTypes = new List<Type>();

        /// <summary>
        /// Add a handler to the chain.
        /// </summary>
        /// <typeparam name="THandler">Handler Type.</typeparam>
        public ChainContainerOptions<T> AddHandler<THandler>()
            where THandler : class, IChainHandler<T>
        {
            _handlersTypes.Add(typeof(THandler));

            return this;
        }

        /// <summary>
        /// Get a list of registered handler types for this Chain Type.
        /// </summary>
        /// <returns></returns>
        public List<Type> GetHandlerTypes()
        {
            return _handlersTypes;
        }
    }
}