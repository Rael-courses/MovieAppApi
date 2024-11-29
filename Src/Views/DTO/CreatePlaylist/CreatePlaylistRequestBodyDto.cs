using System.ComponentModel.DataAnnotations;
using MovieAppApi.Src.Core.CustomAttributes.Validation;

namespace MovieAppApi.Src.Views.DTO.CreatePlaylist;

public class CreatePlaylistRequestBodyDto
{
  [Required]
  public required string name { get; init; }

  public string? description { get; init; }

  [Required, MinLength(1), PositiveIntList]
  public required List<int> movie_ids { get; init; }
}