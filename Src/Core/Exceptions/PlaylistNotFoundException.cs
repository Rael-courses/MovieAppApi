namespace MovieAppApi.Src.Core.Exceptions;

public class PlaylistNotFoundException : Exception
{
  public PlaylistNotFoundException(int id) : base($"Playlist id {id} not found")
  {
  }
}