using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using HabrPostApi.Models;
using HabrPostApi.ParserWorkers;
using HabrPostApi.TryParsers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HabrPostApi.Controllers
{
    /// <summary>
    /// Habr posts api
    /// </summary>
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IAsyncHabrParserWorker _worker;

        public PostsController(IAsyncHabrParserWorker worker)
        {
            _worker = worker;
            InitializeTryParsers();
        }

        private void InitializeTryParsers()
        {
            var tryParsers = _worker.Parser.TryParsers;

            tryParsers.Add(new IntegerTryParser());
            tryParsers.Add(new ThousandTryParser());
        }

        /// <summary>
        /// Returns posts from start page number to end page number
        /// </summary>
        /// <param name="start">Number of first habr page</param>
        /// <param name="end">Number of last habr page</param>
        /// <returns>Posts from habr</returns>
        /// <response code="200">Returns posts of habr</response>
        /// <response code="400">
        /// If request to habr returns unsuccess code
        /// </response>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<HabrPost>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<HabrPost>>> Get( 
            int? end,
            int start = 1)
        {
            if (end is null)
            {
                return BadRequest($"Value of {nameof(end)} cannot be null");
            }

            try
            {
                return await GetPosts(start, end.Value);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    "InternalServerError");
            }
        }

        /// <summary>
        /// Returns posts from the specified page
        /// </summary>
        /// <param name="page">Number of habr page</param>
        /// <returns>Posts from habr</returns>
        /// <response code="200">Returns posts of habr</response>
        /// <response code="400">
        /// If request to habr returns unsuccess code
        /// </response>
        [HttpGet("{page}")]
        [ProducesResponseType(typeof(IEnumerable<HabrPost>), 200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<IEnumerable<HabrPost>>> Get(int? page)
        {
            if (page is null)
            {
                return BadRequest($"Value of {nameof(page)} cannot be null");
            }

            try
            {
                return await GetPosts(page.Value, page.Value);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (HttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(
                    StatusCodes.Status500InternalServerError,
                    "InternalServerError");
            }
        }

        private Task<List<HabrPost>> GetPosts(int start, int end)
        {
            var settings = _worker.Settings;

            settings.StartPageNumber = start;
            settings.EndPageNumber = end;

            return _worker.ParseAsync();
        }
    }
}
