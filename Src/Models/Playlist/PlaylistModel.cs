namespace MovieAppApi.Src.Models.Playlist;

public class PlaylistModel
{
  public int Id { get; init; }
  public string Name { get; init; }
  public string? Description { get; init; }
  public List<int> MovieIds { get; init; }

  public PlaylistModel(int id, string name, string? description, List<int> movieIds)
  {
    Id = id;
    Name = name;
    Description = description;
    MovieIds = movieIds;
  }
}