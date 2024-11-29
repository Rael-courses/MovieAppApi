using MovieAppApi.Src.Models.Playlist;
using MovieAppApi.Src.Views.DTO.Playlist;

namespace MovieAppApi.Src.Core.Mappers.UpdatePlaylist;

public interface IUpdatePlaylistResponseMapper
{
  public PlaylistDto ToDto(PlaylistModel playlistModel);
}