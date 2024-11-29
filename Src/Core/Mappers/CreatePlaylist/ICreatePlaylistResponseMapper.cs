using MovieAppApi.Src.Models.Playlist;
using MovieAppApi.Src.Views.DTO.Playlist;

namespace MovieAppApi.Src.Core.Mappers.CreatePlaylist;

public interface ICreatePlaylistResponseMapper
{
  public PlaylistDto ToDto(PlaylistModel model);
}