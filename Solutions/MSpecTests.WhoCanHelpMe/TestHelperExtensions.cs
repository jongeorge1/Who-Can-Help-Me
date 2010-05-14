namespace MSpecTests.WhoCanHelpMe
{
    #region Using Directives

    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using global::WhoCanHelpMe.Domain.Specifications;
    using global::WhoCanHelpMe.Domain.Contracts.Repositories;
    
    using Machine.Specifications;

    using Rhino.Mocks;
    using Rhino.Mocks.Interfaces;

    #endregion

    public static class TestHelperExtensions
    {
        public static void SetNonPublicProperty<TTarget, TProperty>(this TTarget o, Expression<Func<TTarget, TProperty>> property, TProperty value)
        {
            var theProperty = (property.Body as MemberExpression).Member as PropertyInfo;

            theProperty.SetValue(o, value, null);
        }

        public static IMethodOptions<TDomainObjectType> StubFindOne<TDomainObjectType>(this ILinqRepository<TDomainObjectType> repository)
        {
            var dummySpecification = MockRepository.GenerateStub<QuerySpecification<TDomainObjectType>>();

            return repository.Stub(p => p.FindOne(dummySpecification)).IgnoreArguments();
        }

        public static IMethodOptions<IQueryable<TDomainObjectType>> StubFindAll<TDomainObjectType>(this ILinqRepository<TDomainObjectType> repository)
        {
            var dummySpecification = MockRepository.GenerateStub<QuerySpecification<TDomainObjectType>>();

            return repository.Stub(p => p.FindAll(dummySpecification)).IgnoreArguments();
        }

        public static void AssertFindOneWasCalledWithSpecification<TSpecificationType, TDomainObjectType>(this ILinqRepository<TDomainObjectType> repository, Func<TSpecificationType, bool> specificationValidator) 
            where TSpecificationType : QuerySpecification<TDomainObjectType>
        {
            var dummySpecification = MockRepository.GenerateStub<QuerySpecification<TDomainObjectType>>();

            repository.AssertWasCalled(
                r => r.FindOne(dummySpecification),
                m => m.IgnoreArguments());

            var arguments = repository.GetArgumentsForCallsMadeOn(
                r => r.FindOne(dummySpecification),
                m => m.IgnoreArguments());

            arguments.Count.ShouldEqual(1);

            foreach (var argumentSet in arguments)
            {
                specificationValidator(argumentSet[0] as TSpecificationType).ShouldBeTrue();
            }
        }

        public static void AssertFindAllWasCalledWithSpecification<TSpecificationType, TDomainObjectType>(this ILinqRepository<TDomainObjectType> repository, Func<TSpecificationType, bool> specificationValidator)
            where TSpecificationType : QuerySpecification<TDomainObjectType>
        {
            var dummySpecification = MockRepository.GenerateStub<QuerySpecification<TDomainObjectType>>();

            repository.AssertWasCalled(
                r => r.FindAll(dummySpecification),
                m => m.IgnoreArguments());

            var arguments = repository.GetArgumentsForCallsMadeOn(
                r => r.FindAll(dummySpecification),
                m => m.IgnoreArguments());

            arguments.Count.ShouldEqual(1);

            foreach (var argumentSet in arguments)
            {
                specificationValidator(argumentSet[0] as TSpecificationType).ShouldBeTrue();
            }
        }

        public static void AssertFindAllWasNotCalledWithSpecification<TSpecificationType, TDomainObjectType>(this ILinqRepository<TDomainObjectType> repository)
            where TSpecificationType : QuerySpecification<TDomainObjectType>
        {
            var dummySpecification = MockRepository.GenerateStub<QuerySpecification<TDomainObjectType>>();

            repository.AssertWasNotCalled(
                r => r.FindAll(dummySpecification),
                m => m.IgnoreArguments());
        }
    }
}