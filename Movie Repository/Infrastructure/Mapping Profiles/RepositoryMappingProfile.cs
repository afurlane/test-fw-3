using AutoMapper;
using Movie_Repository.Entities;
using Repository_API.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Movie_Repository.Infrastructure.Mapping_Profiles
{
    /// <summary>
    /// Usually a profile SHOULD map Entity -> DTO and DTO -> Entity.
    /// For this example we need only Entity to DTO mapping
    /// </summary>
    public class RepositoryMappingProfile : Profile
    {
        /// <summary>
        /// This is needlessly verbose because automapper does, by default, automatically map properties with the same name
        /// and type.
        /// </summary>
        public RepositoryMappingProfile()
        {
            CreateMap<User, UserDTO>();
            CreateMap<Rating, RatingDTO>();
            CreateMap<Genre, GenreDTO>();
            CreateMap<Movie, MovieDTO>();
        }
    }
}
