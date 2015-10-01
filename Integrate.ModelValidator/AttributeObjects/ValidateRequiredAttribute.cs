﻿namespace Integrate.ModelValidator
{
    public class ValidateRequiredAttribute : IntegrateAttribute
    {
        private ValidateRequiredAttribute()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="errorMessage">The error to return if validation is not successful</param>
        public ValidateRequiredAttribute(string errorMessage)
            : base(errorMessage)
        {
        }


        public override bool Valitade<T>(T val)
        {

            if (val is string)
            {
                return val as string != string.Empty;

            }
            return val != null;
        }
    }
}