using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSwag.Annotations;
using Repository_API;
using Repository_API.DTO;
using Repository_API.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [OpenApiController("MovieController")]
    public class MovieController: ControllerBase
    {
        private readonly ILogger<MovieController> logger;
        private readonly IMovieRepository movieRepository;

        public MovieController(ILogger<MovieController> logger, IMovieRepository movieRepository)
        {
            this.logger = logger;
            this.movieRepository = movieRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedList<MovieDTO>>> GetMoviesAsync([FromQuery] SearchMovieCriteraDTO searchCritera)
        {
            if (searchCritera.IsSearchValid) { 
                PagedList<MovieDTO> pagedMovies = await movieRepository.GetMoviesAsync(searchCritera);
                if (pagedMovies != null && pagedMovies.Count > 0)
                {
                    return pagedMovies;
                }
                return NotFound();
            }
            return BadRequest();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedList<MovieDTO>>> GetUserRatingsAsync([FromQuery] SearchUserCriteraDTO searchCritera)
        {
            if (searchCritera.IsSearchValid)
            {
                PagedList<MovieDTO> pagedMovies = await movieRepository.GetUserRatingsAsync(searchCritera);
                if (pagedMovies != null && pagedMovies.Count > 0)
                {
                    return pagedMovies;
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}
