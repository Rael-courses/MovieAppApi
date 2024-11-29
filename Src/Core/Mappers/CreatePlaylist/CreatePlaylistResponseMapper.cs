using MovieAppApi.Src.Models.Playlist;
using MovieAppApi.Src.Views.DTO.Playlist;

namespace MovieAppApi.Src.Core.Mappers.CreatePlaylist;

public class CreatePlaylistResponseMapper : ICreatePlaylistResponseMapper
{
  public PlaylistDto ToDto(PlaylistModel model)
  {
    return new PlaylistDto
    {
      id = model.Id,
      name = model.Name,
      description = model.Description,
      movie_ids = model.MovieIds
    };
  }
}