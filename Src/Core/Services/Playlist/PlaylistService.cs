using MovieAppApi.Src.Core.Repositories.Playlist;
using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Models.Playlist;

namespace MovieAppApi.Src.Core.Services.Playlist;

public class PlaylistService : IPlaylistService
{
  private readonly IPlaylistRepository _playlistRepository;

  public PlaylistService(IPlaylistRepository playlistRepository)
  {
    _playlistRepository = playlistRepository;
  }

  public Task<PlaylistModel> CreatePlaylistAsync(CreatePlaylistRequestBodyModel playlist)
  {
    return _playlistRepository.CreatePlaylistAsync(playlist);
  }

  public Task<List<PlaylistModel>> GetPlaylistsAsync()
  {
    return _playlistRepository.GetPlaylistsAsync();
  }

  public Task<PlaylistModel> GetPlaylistAsync(int playlistId)
  {
    return _playlistRepository.GetPlaylistAsync(playlistId);
  }

  public Task<PlaylistModel> UpdatePlaylistAsync(int playlistId, PlaylistModel playlist)
  {
    return _playlistRepository.UpdatePlaylistAsync(playlistId, playlist);
  }
}