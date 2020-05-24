using Cycloid.Models;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace Cycloid.API.Controllers
{

    public class BaseController : ApiController
    {
        public HttpResponseMessage CreateResponseFromOperation<T>(Operation<T> operation) where T: class
        {
            if (!operation.IsValid)
                return Request.CreateResponse(HttpStatusCode.BadRequest, operation.ErrorMessages);

            if (operation.Payload == null)
               return  Request.CreateResponse(HttpStatusCode.NotFound, operation.ErrorMessages);

            return Request.CreateResponse(HttpStatusCode.OK, operation.Payload);

        }
    }
}