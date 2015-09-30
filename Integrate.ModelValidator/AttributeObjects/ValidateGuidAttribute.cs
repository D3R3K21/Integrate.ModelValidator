namespace Integrate.ModelValidator
{
    public class ValidateGuidAttribute : IntegrateAttribute
    {

        public ValidateGuidAttribute()
        {
        }

        public ValidateGuidAttribute(string errorMessage)
            : base(errorMessage)
        {
        }



        public override bool Valitade<T>(T val)
        {
            return true;
        }
    }
}