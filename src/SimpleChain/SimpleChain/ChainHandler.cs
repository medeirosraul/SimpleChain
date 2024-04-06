namespace SimpleChain
{
    /// <inheritdoc/>
    public abstract class ChainHandler<T> : IChainHandler<T>
        where T : class
    {
        private IChainHandler<T>? _next;

        /// <inheritdoc/>
        public IChainHandler<T> SetNext(IChainHandler<T> handler)
        {
            _next = handler;

            return handler;
        }

        /// <inheritdoc/>
        public virtual Task<T> HandleAsync(T input)
        {
            if (_next is null)
                return Task.FromResult(input);

            return _next.HandleAsync(input);
        }

        public virtual Task<T> Next(T input)
        {
            if (_next is null)
                return Task.FromResult(input);

            return _next.HandleAsync(input);
        }
    }
}
