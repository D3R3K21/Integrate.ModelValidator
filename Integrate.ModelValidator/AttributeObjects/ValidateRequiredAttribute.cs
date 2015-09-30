namespace Integrate.ModelValidator
{
    public class ValidateRequiredAttribute : IntegrateAttribute
    {
        private ValidateRequiredAttribute()
        {
        }

        public ValidateRequiredAttribute(string error)
            : base(error)
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