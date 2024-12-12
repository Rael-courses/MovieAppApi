namespace MovieAppApi.Src.Models.Movie;

public class MovieModel
{
  public int Id { get; }
  public string OriginalLanguage { get; }
  public string OriginalTitle { get; }
  public string Overview { get; }
  public double Popularity { get; }
  public string? PosterPath { get; }
  public DateTime? ReleaseDate { get; }
  public string Title { get; }
  public double VoteAverage { get; }
  public int VoteCount { get; }

  public MovieModel(
    int id,
    string originalLanguage,
    string originalTitle,
    string overview,
    double popularity,
    DateTime? releaseDate,
    string title,
    double voteAverage,
    int voteCount,
    string? posterPath
  )
  {
    Id = id;
    OriginalLanguage = originalLanguage;
    OriginalTitle = originalTitle;
    Overview = overview;
    Popularity = popularity;
    ReleaseDate = releaseDate;
    Title = title;
    VoteAverage = voteAverage;
    VoteCount = voteCount;
    PosterPath = posterPath;
  }
}