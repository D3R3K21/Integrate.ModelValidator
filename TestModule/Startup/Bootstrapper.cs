using Nancy;
namespace TestModule
{


    public class Bootstrapper : DefaultNancyBootstrapper
    {
        public Bootstrapper()
        {
            Validator.Initialize();
        }
        protected override void ApplicationStartup(Nancy.TinyIoc.TinyIoCContainer container, Nancy.Bootstrapper.IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
        }
    }
}