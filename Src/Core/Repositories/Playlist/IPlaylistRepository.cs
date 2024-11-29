using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Models.Playlist;

namespace MovieAppApi.Src.Core.Repositories.Playlist;


public interface IPlaylistRepository
{
  public Task<PlaylistModel> CreatePlaylistAsync(CreatePlaylistRequestBodyModel playlistData);
  public Task<List<PlaylistModel>> GetPlaylistsAsync();
  public Task<PlaylistModel> GetPlaylistAsync(int playlistId);
  public Task<PlaylistModel> UpdatePlaylistAsync(int playlistId, PlaylistModel playlist);
}