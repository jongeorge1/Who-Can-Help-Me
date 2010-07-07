namespace WhoCanHelpMe.Framework.Contracts.Specifications
{
    /// <summary>
    /// Defines the behaviour of a specification which is used to select
    /// specified items from a collection
    /// </summary>
    /// <typeparam name="T">Type that the specification is applied to</typeparam>
    public interface ISpecification<T>
    {
        /// <summary>
        /// Allows multiple specifications to be joined together
        /// </summary>
        /// <param name="specification">Specification to add</param>
        /// <returns>Specification object containing original specification and new addition</returns>
        ISpecification<T> And(ISpecification<T> specification);
        
        /// <summary>
        /// Indicates whether the item provided satisfies the specificatoin
        /// </summary>
        /// <param name="item">Item to evaluate</param>
        /// <returns>Whether the specification has be satisfied</returns>
        bool IsSatisfiedBy(T item);

        /// <summary>
        /// Checks whether the specification 
        /// is more general than a given specification.
        /// </summary>
        /// <param name="specification">
        /// The specification to test
        /// </param>
        /// <returns>
        /// Whether the current specification is a generalisation of the supplied specification.
        /// </returns>
        bool IsGeneralizationOf(ISpecification<T> specification);

        /// <summary>
        /// Checks whether the specification is 
        /// more specific than a given specification.
        /// </summary>
        /// <param name="specification">
        /// The specification to test
        /// </param>
        /// <returns>
        /// Whether the current specification is a special case version of the supplied specification.
        /// </returns>
        bool IsSpecialCaseOf(ISpecification<T> specification);

        /// <summary>
        /// Allows multiple specifications to be joined together, the specification must return false
        /// </summary>
        /// <param name="specification">Specification to add</param>
        /// <returns>Specification object containing original specification and new addition</returns>
        ISpecification<T> Not(ISpecification<T> specification);

        /// <summary>
        /// Allows multiple specifications to be joined together, the specification can return true
        /// </summary>
        /// <param name="specification">Specification to add</param>
        /// <returns>Specification object containing original specification and new addition</returns>
        ISpecification<T> Or(ISpecification<T> specification);

        /// <summary>
        /// Returns the a specification representing 
        /// the criteria that are not met by the candidate object.
        /// </summary>
        /// <param name="item">
        /// The item to test.
        /// </param>
        /// <returns>
        /// Whether the specification is unsatisfied by the specified item.
        /// </returns>
        ISpecification<T> RemainderUnsatisfiedBy(T item);
    }
}