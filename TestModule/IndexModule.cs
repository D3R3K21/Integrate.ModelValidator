using System;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Integrate.ModelValidator;

namespace TestModule
{
    public class IndexModule : NancyModule
    {
        public object BindObject<T>(T obj) where T : BaseIntegrateModel
        {
            var methodInfo = Validator.BindModel(obj);

            var returnedModel = methodInfo.Invoke(methodInfo.DeclaringType, new object[] { this });

            var convertedModel = Convert.ChangeType(returnedModel, obj.DerivedType);

            return convertedModel;

        }

        public IndexModule()
            : base("/")
        {
            Before += s =>
            {
                //TODO: map endpoints to modeltypes, if no model exists for the endpoint, return null and continue
                Response response = null;
                //TODO: map model types to Func<Type, BaseIntegrateModel>
                var modelObject = new UserModel();//TODO: create ModelTypeAttribute for endpoints, cache model types for each one in validator

                var model = BindObject(modelObject);
                var validationResponse = ((BaseIntegrateModel)model).Validate();
                var allValidCheck = validationResponse.All(p => p.IsValid);

                if (!allValidCheck)
                {
                    response = Response.AsJson(new { AllProperties = validationResponse, FailedProperties = validationResponse.Where(p => !p.IsValid).ToList() });
                }


                return response;
            };

            Get["/{id}"] = parameters =>
            {
                var model = this.Bind();
                var x = this.Routes;
                return View["index"];
            };
            Get["/value"] = parameters =>
            {
                var model = this.Bind();
                var x = this.Routes;
                return View["index"];
            };
            Get["/"] = parameters =>
            {
                var f = this.Routes.Where(x =>
                    {
                        return x.Description.Method.ToLower() == "get";
                    }).ToList();
                return View["index"];
            };

            Post["/{id}"] = parameters =>
            {
                var model = this.Bind();
                var x = this.Routes;
                return View["index"];
            };
        }
    }
}