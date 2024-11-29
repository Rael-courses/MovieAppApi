using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Models.Playlist;

namespace MovieAppApi.Src.Core.Repositories.Playlist;


public interface IPlaylistRepository
{
  public Task<PlaylistModel> CreatePlaylistAsync(CreatePlaylistRequestBodyModel playlistData);
}