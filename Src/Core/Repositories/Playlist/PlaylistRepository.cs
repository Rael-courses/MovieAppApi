using Microsoft.EntityFrameworkCore;
using MovieAppApi.Src.Core.Repositories.Entities;
using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Models.Playlist;

namespace MovieAppApi.Src.Core.Repositories.Playlist;

public class PlaylistRepository : IPlaylistRepository
{
  private readonly AppDbContext _appDbContext;

  public PlaylistRepository(AppDbContext appDbContext)
  {
    _appDbContext = appDbContext;
  }

  public async Task<PlaylistModel> CreatePlaylistAsync(CreatePlaylistRequestBodyModel data)
  {
    var playlistJoinMovieEntities = await Task.WhenAll(data.MovieIds.Select(async movieId =>
    {
      var existingMovieEntity = await _appDbContext.Movies.FindAsync(movieId);

      return new PlaylistJoinMovieEntity
      {
        Movie = existingMovieEntity ?? new MovieEntity
        {
          Id = movieId,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        },
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
      };
    }));

    var playlistEntity = new PlaylistEntity
    {
      Name = data.Name,
      Description = data.Description,
      PlaylistJoinMovies = playlistJoinMovieEntities.ToList(),
      CreatedAt = DateTime.UtcNow,
      UpdatedAt = DateTime.UtcNow
    };
    await _appDbContext.Playlists.AddAsync(playlistEntity);

    await _appDbContext.SaveChangesAsync();

    return new PlaylistModel
    (
      id: playlistEntity.Id,
      name: playlistEntity.Name,
      description: playlistEntity.Description,
      movieIds: playlistEntity.PlaylistJoinMovies.Select(pm => pm.MovieId).ToList()
    );
  }

  public async Task<List<PlaylistModel>> GetPlaylistsAsync()
  {
    var playlistEntities = await _appDbContext.Playlists
    .Include(p => p.PlaylistJoinMovies)
    .ToListAsync();

    return playlistEntities.Select(playlistEntity => new PlaylistModel
    (
      id: playlistEntity.Id,
      name: playlistEntity.Name,
      description: playlistEntity.Description,
      movieIds: playlistEntity.PlaylistJoinMovies.Select(pm => pm.MovieId).ToList()
    )).ToList();
  }
}