using System;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using Nancy.ModelValidation;

namespace TestModule
{
    public class IndexModule : NancyModule
    {

        public IndexModule()
            : base("/")
        {
            Before += ctx =>
            {
                //TODO: overload RouteBuilder in Int.Nancy to accept model type, if no model exists for the endpoint, return null and continue
                Response response = null;
                var resolvedRoute = ctx.ResolvedRoute;
                var modelName = resolvedRoute.Description.Name;
                if (string.IsNullOrEmpty(modelName)) return response;

                //TODO: change BindModel to accept type as arg instead of instance
                var modelType = Type.GetType(modelName);
                var modelObject = (NancyValidatorModel)Activator.CreateInstance(modelType);
                var model = this.BindModel(modelObject);



                var validationResponse = ((NancyValidatorModel)model).Validate();
                var allValidCheck = validationResponse.All(p => p.IsValid);

                if (!allValidCheck)
                {
                    response = Response.AsJson(new { AllProperties = validationResponse, FailedProperties = validationResponse.Where(p => !p.IsValid).ToList() });
                }


                return response;
            };

            Get["TestModule.UserModel", "/{id}"] = parameters =>
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

            Post["TestModule.UserModel", "/{id}"] = parameters =>
            {
                var model = this.Bind();
                var x = this.Routes;
                return View["index"];
            };
        }
    }
}