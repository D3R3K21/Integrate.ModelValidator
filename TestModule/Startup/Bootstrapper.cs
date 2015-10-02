using Nancy;
using Nancy.Bootstrapper;
using Nancy.ModelValidation;
using Nancy.TinyIoc;

namespace TestModule
{


    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public Bootstrapper()
        {
        }
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            Validator.Initialize();

            base.ApplicationStartup(container, pipelines);
        }

    }
}