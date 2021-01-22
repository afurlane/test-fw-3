using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository_API;
using Repository_API.DTO;
using Repository_API.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies_API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class MovieController : ControllerBase
    {
        private readonly ILogger<MovieController> logger;
        private readonly IMovieRepository movieRepository;

        public MovieController(ILogger<MovieController> logger, IMovieRepository movieRepository)
        {
            this.logger = logger;
            this.movieRepository = movieRepository;
        }

        [HttpGet]
        [ActionName("Movie")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedList<MovieDTO>>> GetMoviesAsync([FromQuery] SearchMovieCriteraDTO searchCritera)
        {
            PagedList<MovieDTO> pagedMovies = await movieRepository.GetMoviesAsync(searchCritera);
            if (pagedMovies != null && pagedMovies.Count > 0)
            {
                return pagedMovies;
            }
            return NotFound();
        }

        [HttpGet]
        [ActionName("UserRatings")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PagedList<MovieDTO>>> GetUserRatingsAsync([FromQuery] SearchUserCriteraDTO searchCritera)
        {
            PagedList<MovieDTO> pagedMovies = await movieRepository.GetUserRatingsAsync(searchCritera);
            if (pagedMovies != null && pagedMovies.Count > 0)
            {
                return pagedMovies;
            }
            return NotFound();
        }

        [HttpGet]
        [ActionName("FiveTopRatedMovies")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
        public async Task<ActionResult<List<MovieDTO>>> GetFiveTopRatedMoviesAsync()
        {
            List<MovieDTO> topRatedMovies= await movieRepository.GetFiveTopRatedMoviesAsync();
            if (topRatedMovies != null && topRatedMovies.Count > 0)
            {
                return topRatedMovies;
            }
            return NotFound();
        }

        [HttpPost]
        [ActionName("InsertUserRating")]
        [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Post))]
        public async Task<ActionResult> UpdateUserRatingAsync([FromBody] UserRatingDTO userRatingDTO)
        {
            await movieRepository.UpdateUserRatingAsync(userRatingDTO);
            return Ok();
        }

    }
}
