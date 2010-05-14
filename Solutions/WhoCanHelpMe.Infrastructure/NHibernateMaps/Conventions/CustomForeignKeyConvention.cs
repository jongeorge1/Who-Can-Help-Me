namespace WhoCanHelpMe.Infrastructure.NHibernateMaps.Conventions
{
    #region Using Directives

    using System;
    using System.Reflection;
    using FluentNHibernate.Conventions;

    #endregion

    public class CustomForeignKeyConvention : ForeignKeyConvention 
    {
        protected override string GetKeyName(PropertyInfo property, Type type)
        {
            if (property == null)
            {
                return type.Name + "Id";
            }

            return property.Name + "Id";  
        }
    }
}