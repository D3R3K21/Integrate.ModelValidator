using System;

namespace Integrate.ModelValidator
{
    public abstract class BaseIntegrateModel
    {
        public Type DerivedType { get; set; }

        protected BaseIntegrateModel()
        {
        }

    }
}