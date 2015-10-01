using System;

namespace Integrate.ModelValidator
{
    [AttributeUsage(AttributeTargets.Property)]
    public abstract class IntegrateAttribute : Attribute
    {
        protected IntegrateAttribute()
        {
        }

        protected IntegrateAttribute(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }

        public abstract bool Valitade<T>(T val);

        public string ErrorMessage { get; protected set; }




        //public override bool CompileTimeValidate(System.Reflection.MethodBase method)
        //{
        //    var type = method.DeclaringType;

        //    // if the attribute is used outside the Service class, fail the build
        //    if (!type.Equals(typeof(Service)))
        //    {
        //        throw new Exception("MyAttribute can only be used in the Service class");
        //    }

        //    return true;
        //}
    }
}