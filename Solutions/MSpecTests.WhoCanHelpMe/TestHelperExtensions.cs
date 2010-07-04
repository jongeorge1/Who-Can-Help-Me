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
    }
}