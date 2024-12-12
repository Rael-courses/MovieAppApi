using System.ComponentModel.DataAnnotations;
using MovieAppApi.Src.Models.Movie;

namespace MovieAppApi.Src.Core.Services.FetchMovies.Tmdb;

public class TmdbMovieDto
{
  [Required] public required int id { get; init; }
  [Required] public required string original_language { get; init; }
  [Required] public required string original_title { get; init; }
  [Required(AllowEmptyStrings = true)] public required string overview { get; init; }
  [Required] public required double popularity { get; init; }
  [Required(AllowEmptyStrings = true)] public required string release_date { get; init; }
  [Required] public required string title { get; init; }
  [Required] public required double vote_average { get; init; }
  [Required] public required int vote_count { get; init; }
  public string? poster_path { get; init; }

  public MovieModel ToModel()
  {
    Validator.ValidateObject(this, new ValidationContext(this), true);

    return new MovieModel(
      id: id,
      originalLanguage: original_language,
      originalTitle: original_title,
      overview: overview,
      popularity: popularity,
      releaseDate: release_date == "" ? null : DateTime.Parse(release_date),
      title: title,
      voteAverage: vote_average,
      voteCount: vote_count,
      posterPath: poster_path
    );
  }
}
