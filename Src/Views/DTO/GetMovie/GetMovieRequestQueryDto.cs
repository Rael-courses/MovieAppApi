using System.ComponentModel.DataAnnotations;

namespace MovieAppApi.Src.Views.DTO.GetMovie;

public class GetMovieRequestQueryDto
{
  [Required(AllowEmptyStrings = false), AllowedValues("en", "fr", ErrorMessage = "Invalid language, must be one of: en, fr")]
  public required string language { get; set; }
}