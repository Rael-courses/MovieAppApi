using Microsoft.EntityFrameworkCore;
using MovieAppApi.Src.Core.Repositories.Entities;

namespace MovieAppApi.Src.Core.Repositories;

public class AppDbContext : DbContext
{
  public DbSet<MovieEntity> Movies { get; set; } = null!;
  public DbSet<PlaylistEntity> Playlists { get; set; } = null!;
  public DbSet<PlaylistJoinMovieEntity> PlaylistJoinMovies { get; set; } = null!;

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<PlaylistJoinMovieEntity>()
      .HasKey(pm => new { pm.PlaylistId, pm.MovieId });
  }
}