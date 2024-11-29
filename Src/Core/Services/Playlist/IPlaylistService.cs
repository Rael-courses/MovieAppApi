using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Models.Playlist;

namespace MovieAppApi.Src.Core.Services.Playlist;

public interface IPlaylistService
{
  public Task<PlaylistModel> CreatePlaylistAsync(CreatePlaylistRequestBodyModel playlistData);
}