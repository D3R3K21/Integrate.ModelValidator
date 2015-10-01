using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.ModelBinding;
using System.Text.RegularExpressions;
using Nancy.Routing;
using System.Text;
namespace TestModule
{

    public class IndexModule : NancyModule
    {
        public IndexModule()
            : base("/")
        {
            Before += s =>
            {
                var routes = this.Routes.Where(z => z.Description.Method == this.Request.Method).ToList();
                var model = this.Bind();
                var sb = new StringBuilder();

                foreach (Route route in routes)
                {
                    var path = route.Description.Path;
                    var pathParts = path.Split(new[]{'/'}, StringSplitOptions.RemoveEmptyEntries).ToList();

                    pathParts.ForEach(p =>
                        {
                            var regex = new Regex(@"^{[A-Za-z]+}$");
                            var match = regex.Match(p).Success;
                            Console.Out.WriteLine();

                        });


                    Regex reg = new Regex(path);
                    var d = reg.Match(this.Request.Path);
                    Console.Out.WriteLine();
                }


                return null;
            };

            Get["/"] = parameters =>
            {
                var f = this.Routes.Where(x =>
                    {
                        return x.Description.Method.ToLower() == "get";
                    }).ToList();
                return View["index"];
            };
            Get["/{id}"] = parameters =>
            {
                var model = this.Bind();
                var x = this.Routes;
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