using MovieAppApi.Src.Models.Playlist;
using MovieAppApi.Src.Views.DTO.Playlist;

namespace MovieAppApi.Src.Core.Mappers.GetPlaylists;

public interface IGetPlaylistsResponseMapper
{
  public List<PlaylistDto> ToDto(List<PlaylistModel> playlistModels);
}