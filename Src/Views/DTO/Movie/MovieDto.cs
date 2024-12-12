using System.ComponentModel.DataAnnotations;

namespace MovieAppApi.Src.Views.DTO.Movie;

public class MovieDto
{
  [Required] public required int id { get; init; }

  [Required] public required string original_language { get; init; }

  [Required] public required string original_title { get; init; }

  [Required] public required string overview { get; init; }

  [Required] public required double popularity { get; init; }

  public DateTime? release_date { get; init; }

  [Required] public required string title { get; init; }

  [Required] public required double vote_average { get; init; }

  [Required] public required int vote_count { get; init; }


  public string? poster_path { get; init; }
}