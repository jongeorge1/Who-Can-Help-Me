namespace WhoCanHelpMe.Aspects
{
    #region Using Directives

    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Reflection;
    using System.Text;

    #endregion

    /// <summary>
    /// Provides common helper methods for the aspect classes
    /// </summary>
    internal static class AspectHelper
    {
        /// <summary>
        /// Builds a string containing the method signature (full typename, method name and parameter list) for the specified method
        /// </summary>
        /// <param name="method">MethodBase object for the method that is being described.</param>
        /// <returns>The method signature.</returns>
        public static string GetMethodSignature(MethodBase method)
        {
            var methodSignature = new StringBuilder(200);
            methodSignature.Append(method.DeclaringType.FullName);
            methodSignature.Append(".");
            methodSignature.Append(method.Name);

            methodSignature.Append("(");

            ParameterInfo[] parameters = method.GetParameters();

            IEnumerable<string> parameterDetails = from parameter in parameters
                                                   select
                                                       string.Format(
                                                       CultureInfo.InvariantCulture,
                                                       "{0} {1}",
                                                       parameter.GetType().FullName,
                                                       parameter.Name);

            methodSignature.Append(string.Join(", ", parameterDetails.ToArray()));

            return methodSignature.ToString();
        }
    }
}