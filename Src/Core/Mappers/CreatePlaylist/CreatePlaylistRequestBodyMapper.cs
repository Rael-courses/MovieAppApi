using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Views.DTO.CreatePlaylist;

namespace MovieAppApi.Src.Core.Mappers.CreatePlaylist;

public class CreatePlaylistRequestBodyMapper : ICreatePlaylistRequestBodyMapper
{
  public CreatePlaylistRequestBodyModel ToModel(CreatePlaylistRequestBodyDto dto)
  {
    return new CreatePlaylistRequestBodyModel
    (
      name: dto.name,
      description: dto.description,
      movieIds: dto.movie_ids
    );
  }
}