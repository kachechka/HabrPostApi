using System;
using System.Linq;
using System.Threading.Tasks;
using GraphQL;
using GraphQL.Types;
using HabrPostApi.Extensions;
using HabrPostApi.GraphQl.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HabrPostApi.Controllers
{
    [Route("api/[controller]")]
    [Route("/graphiql")]
    [ApiController]
    public class GraphQlController : ControllerBase
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchema _schema;
        private readonly ExecutionOptions _options;

        public GraphQlController(
            IDocumentExecuter documentExecuter,
            ISchema schema)
        {
            _documentExecuter = documentExecuter;
            _schema = schema;
            _options = new ExecutionOptions
            {
                Schema = _schema
            };
        }

        [HttpPost("")]
        public async Task<IActionResult> Post([FromBody] GraphQlQuery query)
        {
            if (query is null)
            {
                return BadRequest($"{nameof(query)} is null");
            }

            try
            {
                _options.SetFrom(query);

                var result = await _documentExecuter.ExecuteAsync(_options);

                if (result.Errors?.Count > 0)
                {
                    return BadRequest(result.Errors);
                }

                return Ok(result.Data);
            }
            catch (ArgumentOutOfRangeException)
            {
                return BadRequest("Skip can't be less than 0");
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}