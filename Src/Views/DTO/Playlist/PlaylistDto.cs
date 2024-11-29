using System.ComponentModel.DataAnnotations;

namespace MovieAppApi.Src.Views.DTO.Playlist;

public class PlaylistDto
{
  [Required]
  public required int id { get; init; }

  [Required]
  public required string name { get; init; }

  public string? description { get; init; }

  [Required]
  public required List<int> movie_ids { get; init; }
}