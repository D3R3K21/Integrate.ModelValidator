using System.Text.RegularExpressions;

namespace Integrate.ModelValidator
{

    public class ValidateRegexAttribute : IntegrateAttribute
    {
        private readonly Regex _regex;
        private ValidateRegexAttribute()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="error">The error to return if not successful</param>
        /// <param name="regexPattern">The pattern to check the property against</param>
        public ValidateRegexAttribute(string error, string regexPattern)
            : base(error)
        {
            _regex = new Regex(@regexPattern);
        }


        public override bool Valitade<T>(T val)
        {
            if (!(val is string))
            {
                return false;
            }
            return _regex.Match(val as string).Success;
        }
    }
}