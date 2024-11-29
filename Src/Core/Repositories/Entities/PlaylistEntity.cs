using System.ComponentModel.DataAnnotations;

namespace MovieAppApi.Src.Core.Repositories.Entities;

public class PlaylistEntity : CommonEntity
{
  [Key]
  public int Id { get; set; }

  [Required]
  public required string Name { get; set; }

  public string? Description { get; set; }

  public List<PlaylistJoinMovieEntity> PlaylistJoinMovies { get; set; } = [];
}