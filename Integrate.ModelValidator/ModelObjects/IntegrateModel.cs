using System;

namespace Integrate.ModelValidator
{
    public class IntegrateModel<T> : BaseIntegrateModel
    {
        public IntegrateModel()
        {
            DerivedType = typeof(T);
        }

    }
}