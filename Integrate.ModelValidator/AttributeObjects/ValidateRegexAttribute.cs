using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Integrate.ModelValidator
{
    [Serializable]
    public class ValidateRegexAttribute : IntegrateAttribute
    {
        private readonly Regex _regex;
        private ValidateRegexAttribute()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage">The error to return if not successful</param>
        /// <param name="regexPattern">The pattern to check the property against</param>
        public ValidateRegexAttribute(string errorMessage, string regexPattern)
            : base(errorMessage)
        {
            _regex = new Regex(@regexPattern);
        }


        public override bool Valitade<T>(T val)
        {
            if (!(val is string))
            {
                return false;
            }
            return _regex.Match((val as string).Trim()).Success;
        }

    }
}