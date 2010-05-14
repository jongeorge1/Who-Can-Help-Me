namespace WhoCanHelpMe.Web.Code
{
    #region Using Directives

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    using NHibernate.Validator.Constraints;
    using NHibernate.Validator.Engine;

    using xVal.RuleProviders;
    using xVal.Rules;

    #endregion

    /// <summary>
    /// The validator rules provider.
    /// Code cribbed from http://weblogs.asp.net/srkirkland/archive/2009/11/02/an-xval-provider-for-nhibernate-validator.aspx,
    /// because the most recent version of NHibernate.Validator breaks the xVal NHibernateValidatorRulesProvider.
    /// </summary>
    public class ValidatorRulesProvider : CachingRulesProvider
    {
        /// <summary>
        /// The _rule emitters.
        /// </summary>
        private readonly RuleEmitterList<IRuleArgs> ruleEmitters;

        /// <summary>
        /// Initializes a new instance of the <see cref="ValidatorRulesProvider"/> class.
        /// </summary>
        public ValidatorRulesProvider()
        {
            this.ruleEmitters = new RuleEmitterList<IRuleArgs>();

            this.ruleEmitters.AddSingle<LengthAttribute>(
                x => new StringLengthRule(
                         x.Min, 
                         x.Max));

            this.ruleEmitters.AddSingle<MinAttribute>(
                x => new RangeRule(
                         x.Value, 
                         null));

            this.ruleEmitters.AddSingle<MaxAttribute>(
                x => new RangeRule(
                         null, 
                         x.Value));

            this.ruleEmitters.AddSingle<RangeAttribute>(
                x => new RangeRule(
                         x.Min, 
                         x.Max));

            this.ruleEmitters.AddSingle<NotEmptyAttribute>(x => new RequiredRule());

            this.ruleEmitters.AddSingle<NotNullNotEmptyAttribute>(x => new RequiredRule());

            this.ruleEmitters.AddSingle<NotNullAttribute>(x => new RequiredRule());

            this.ruleEmitters.AddSingle<PatternAttribute>(
                x => new RegularExpressionRule(
                         x.Regex, 
                         x.Flags));

            this.ruleEmitters.AddSingle<EmailAttribute>(x => new DataTypeRule(DataTypeRule.DataType.EmailAddress));

            this.ruleEmitters.AddSingle<DigitsAttribute>(MakeDigitsRule);
        }

        /// <summary>
        /// The get rules from type core.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// </returns>
        protected override RuleSet GetRulesFromTypeCore(Type type)
        {
            IClassValidator classMapping = new ValidatorEngine().GetClassValidator(type);

            var rules = from member in type.GetMembers()
                        where member.MemberType == MemberTypes.Field || member.MemberType == MemberTypes.Property
                        from constraint in classMapping.GetMemberConstraints(member.Name).OfType<IRuleArgs>()
                        // All NHibernate Validation validators attributes must implement this interface
                        from rule in this.ConvertToXValRules(constraint)
                        where rule != null
                        select new {
                                       MemberName = member.Name, 
                                       Rule = rule
                                   };

            return new RuleSet(
                rules.ToLookup(
                    x => x.MemberName, 
                    x => x.Rule));
        }

        /// <summary>
        /// The make digits rule.
        /// </summary>
        /// <param name="att">
        /// The att.
        /// </param>
        /// <returns>
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        private static RegularExpressionRule MakeDigitsRule(DigitsAttribute att)
        {
            if (att == null)
            {
                throw new ArgumentNullException("att");
            }

            string pattern;

            if (att.FractionalDigits < 1)
            {
                pattern = string.Format(
                    @"\d{{0,{0}}}", 
                    att.IntegerDigits);
            }
            else
            {
                pattern = string.Format(
                    @"\d{{0,{0}}}(\.\d{{1,{1}}})?", 
                    att.IntegerDigits, 
                    att.FractionalDigits);
            }

            return new RegularExpressionRule(pattern);
        }

        /// <summary>
        /// The message if specified.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <returns>
        /// The message if specified.
        /// </returns>
        private static string MessageIfSpecified(string message)
        {
            // We don't want to display the default {validator.*} messages
            if ((message != null) && !message.StartsWith("{validator."))
            {
                return message;
            }

            return null;
        }

        /// <summary>
        /// The convert to x val rules.
        /// </summary>
        /// <param name="ruleArgs">
        /// The rule args.
        /// </param>
        /// <returns>
        /// </returns>
        private IEnumerable<Rule> ConvertToXValRules(IRuleArgs ruleArgs)
        {
            foreach (Rule rule in this.ruleEmitters.EmitRules(ruleArgs))
            {
                if (rule != null)
                {
                    rule.ErrorMessage = MessageIfSpecified(ruleArgs.Message);

                    yield return rule;
                }
            }
        }
    }
}