using Nancy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Notes.WebApi.Modules
{
    public class InfoModule: NancyModule
    {
        public InfoModule()
        {
            Get("/", Info);
        }

        private async Task<object> Info(dynamic parameters)
        {
            return "Working";
        }
    }
}