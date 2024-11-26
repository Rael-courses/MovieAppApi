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
}