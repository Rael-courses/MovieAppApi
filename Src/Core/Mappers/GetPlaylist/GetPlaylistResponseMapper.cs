using MovieAppApi.Src.Models.Playlist;
using MovieAppApi.Src.Views.DTO.Playlist;

namespace MovieAppApi.Src.Core.Mappers.GetPlaylist;

public class GetPlaylistResponseMapper : IGetPlaylistResponseMapper
{
  public PlaylistDto ToDto(PlaylistModel playlistModel)
  {
    return new PlaylistDto
    {
      id = playlistModel.Id,
      name = playlistModel.Name,
      description = playlistModel.Description,
      movie_ids = playlistModel.MovieIds
    };
  }
}