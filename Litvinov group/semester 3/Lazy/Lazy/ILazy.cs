namespace Lazy
{
    /// <summary>
    /// Interface for class Lazy that describes lazy calculations
    /// </summary>
    /// <typeparam name="T">The type of the answer</typeparam>
    public interface ILazy<T>
    {
        /// <summary>
        /// If called for the first time - calculate and return the result,
        /// else return already calculated result
        /// </summary>
        /// <returns>the result of the calculations</returns>
        T Get();
    }
}
