using MovieAppApi.Src.Models.CreatePlaylist;
using MovieAppApi.Src.Views.DTO.CreatePlaylist;

namespace MovieAppApi.Src.Core.Mappers.CreatePlaylist;

public interface ICreatePlaylistRequestBodyMapper
{
  public CreatePlaylistRequestBodyModel ToModel(CreatePlaylistRequestBodyDto dto);
}