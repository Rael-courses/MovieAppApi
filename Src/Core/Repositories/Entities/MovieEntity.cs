using System.ComponentModel.DataAnnotations;

namespace MovieAppApi.Src.Core.Repositories.Entities;

public class MovieEntity : CommonEntity
{
  [Key]
  public int Id { get; set; }

  public List<PlaylistJoinMovieEntity> PlaylistJoinMovies { get; set; } = [];
}