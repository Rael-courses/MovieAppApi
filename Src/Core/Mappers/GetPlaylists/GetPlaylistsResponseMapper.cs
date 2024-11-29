using MovieAppApi.Src.Models.Playlist;
using MovieAppApi.Src.Views.DTO.Playlist;

namespace MovieAppApi.Src.Core.Mappers.GetPlaylists;

public class GetPlaylistsResponseMapper : IGetPlaylistsResponseMapper
{
  public List<PlaylistDto> ToDto(List<PlaylistModel> playlistModels)
  {
    return playlistModels.Select(playlistModel => new PlaylistDto
    {
      id = playlistModel.Id,
      name = playlistModel.Name,
      description = playlistModel.Description,
      movie_ids = playlistModel.MovieIds
    }).ToList();
  }
}