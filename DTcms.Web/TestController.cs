using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DTcms.Web
{
    [RoutePrefix("api/Test")]
    public class TestController : ApiController
    {
        [Route("Test")]
        [HttpGet]
        public string Test()
        {
            return "Test";
        }
    }
}
