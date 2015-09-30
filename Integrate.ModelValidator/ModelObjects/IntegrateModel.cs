using System;

namespace Integrate.ModelValidator
{
    public class IntegrateModel<T> : BaseIntegrateModel
    {

        public object DerivedModel { get; set; }
        public Type DerivedType { set; get; }

        public IntegrateModel()
        {
            DerivedType = typeof(T);
        }

    }
}