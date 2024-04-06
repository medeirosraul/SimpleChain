namespace SimpleChain
{
    /// <summary>
    /// Interface for a chain handler.
    /// </summary>
    /// <typeparam name="T">Input type.</typeparam>
    public interface IChainHandler<T>
    {
        /// <summary>
        /// Set the next handler in the chain.
        /// </summary>
        /// <param name="handler">Handler to be setted.</param>
        IChainHandler<T> SetNext(IChainHandler<T> handler);

        /// <summary>
        /// Handle the input.
        /// </summary>
        /// <param name="input">Input to be handled.</param>
        Task<T> HandleAsync(T input);
    }
}
