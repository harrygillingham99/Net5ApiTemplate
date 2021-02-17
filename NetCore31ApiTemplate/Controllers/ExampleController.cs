using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using NetCore31ApiTemplate.Exceptions;
using NetCore31ApiTemplate.Objects;
using NetCore31ApiTemplate.Objects.Responses;
using NSwag.Annotations;
using TestResponse = NetCore31ApiTemplate.Objects.Responses.TestResponse;

namespace NetCore31ApiTemplate.Controllers
{
    [ApiController]
    [Route("Example")]
    public class ExampleController : BaseController
    {
        private readonly string _responseStringFromConfig;

        public ExampleController(IOptions<AppSettings> options) : base(options)
        {
            _responseStringFromConfig = options.Value.ResponseString;
        }

        [HttpGet("{statusToReturn}")]
        [SwaggerResponse(HttpStatusCode.OK, typeof(TestResponse))]
        [SwaggerResponse(HttpStatusCode.InternalServerError, typeof(ProblemDetails), Description = "Error")]
        [SwaggerResponse(HttpStatusCode.BadRequest, typeof(BadRequestResult), Description = "Bad Request")]
        [SwaggerResponse(HttpStatusCode.NotFound, typeof(NotFoundResponse), Description = "Not Found")]
        [SwaggerResponse(HttpStatusCode.Unauthorized, typeof(UnauthorizedResponse), Description = "Unauthorized")]
        public Task<IActionResult> Test([FromRoute]int statusToReturn = 200)
        {
            return ExecuteAndMapToActionResult(() =>
            {
                return statusToReturn switch
                {
                    200 => Task.FromResult(new TestResponse {TestProp = _responseStringFromConfig}),
                    500 => throw new InvalidOperationException("Exception happened"),
                    400 => throw new BadRequestException("Wrong"),
                    401=> throw new UnauthorizedRequestException("Unauthorized"),
                    404 => throw new NullReferenceException("Not Found"),
                    _ => null
                };
            });
        }

    }
}
