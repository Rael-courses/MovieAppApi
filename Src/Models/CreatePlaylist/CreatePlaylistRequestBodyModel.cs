namespace MovieAppApi.Src.Models.CreatePlaylist;

public class CreatePlaylistRequestBodyModel
{
  public string Name { get; init; }

  public string? Description { get; init; }

  public List<int> MovieIds { get; init; }

  public CreatePlaylistRequestBodyModel(string name, string? description, List<int> movieIds)
  {
    Name = name;
    Description = description;
    MovieIds = movieIds;
  }
}