using MovieAppApi.Src.Models.Playlist;
using MovieAppApi.Src.Views.DTO.Playlist;

namespace MovieAppApi.Src.Core.Mappers.GetPlaylist;

public interface IGetPlaylistResponseMapper
{
  public PlaylistDto ToDto(PlaylistModel playlistModel);
}