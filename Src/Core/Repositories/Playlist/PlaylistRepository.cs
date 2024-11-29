using Microsoft.EntityFrameworkCore;
using MovieAppApi.Src.Core.Exceptions;
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

  public async Task<PlaylistModel> GetPlaylistAsync(int playlistId)
  {
    var playlistEntity = await _appDbContext.Playlists
      .Include(p => p.PlaylistJoinMovies)
      .FirstOrDefaultAsync(p => p.Id == playlistId);

    if (playlistEntity == null)
    {
      throw new PlaylistNotFoundException(playlistId);
    }

    return new PlaylistModel
    (
      id: playlistEntity.Id,
      name: playlistEntity.Name,
      description: playlistEntity.Description,
      movieIds: playlistEntity.PlaylistJoinMovies.Select(pm => pm.MovieId).ToList()
    );
  }

  public async Task<PlaylistModel> UpdatePlaylistAsync(int playlistId, PlaylistModel playlistModel)
  {
    var playlistEntityToUpdate = await _appDbContext.Playlists.Include(p => p.PlaylistJoinMovies).FirstOrDefaultAsync();
    if (playlistEntityToUpdate == null)
    {
      throw new PlaylistNotFoundException(playlistId);
    }

    var obsoletePlaylistJoinMovies = new List<PlaylistJoinMovieEntity>(playlistEntityToUpdate.PlaylistJoinMovies);

    // Add or update the join entities based on the new list of movie ids
    foreach (var movieId in playlistModel.MovieIds)
    {
      var existingPlaylistJoinMovieEntity = await _appDbContext.PlaylistJoinMovies.FindAsync(playlistEntityToUpdate.Id, movieId);
      // Update the existing join entity
      if (existingPlaylistJoinMovieEntity != null)
      {
        existingPlaylistJoinMovieEntity.UpdatedAt = DateTime.UtcNow;
        obsoletePlaylistJoinMovies.Remove(existingPlaylistJoinMovieEntity);
        continue;
      }

      // Add a new join entity
      _appDbContext.PlaylistJoinMovies.Add(new PlaylistJoinMovieEntity
      {
        PlaylistId = playlistEntityToUpdate.Id,
        Movie = await _appDbContext.Movies.FindAsync(movieId) ?? new MovieEntity
        {
          Id = movieId,
          CreatedAt = DateTime.UtcNow,
          UpdatedAt = DateTime.UtcNow
        },
        CreatedAt = DateTime.UtcNow,
        UpdatedAt = DateTime.UtcNow
      });
    }

    // Remove the join entities that are not in the new list of movie ids
    _appDbContext.PlaylistJoinMovies.RemoveRange(obsoletePlaylistJoinMovies);

    // Update the playlist entity
    playlistEntityToUpdate.Name = playlistModel.Name;
    playlistEntityToUpdate.Description = playlistModel.Description;
    playlistEntityToUpdate.UpdatedAt = DateTime.UtcNow;

    // Save the changes in DB
    await _appDbContext.SaveChangesAsync();

    return new PlaylistModel(
      id: playlistEntityToUpdate.Id,
      name: playlistEntityToUpdate.Name,
      description: playlistEntityToUpdate.Description,
      movieIds: playlistEntityToUpdate.PlaylistJoinMovies.Select(p => p.MovieId).ToList()
    );
  }

  public async Task DeletePlaylistAsync(int playlistId)
  {
    var playlistEntity = await _appDbContext.Playlists.FindAsync(playlistId);

    if (playlistEntity == null)
    {
      throw new PlaylistNotFoundException(playlistId);
    }

    _appDbContext.Playlists.Remove(playlistEntity);

    await _appDbContext.SaveChangesAsync();
  }
}