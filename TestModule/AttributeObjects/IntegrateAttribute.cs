using System;

namespace TestModule
{
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
    }
}