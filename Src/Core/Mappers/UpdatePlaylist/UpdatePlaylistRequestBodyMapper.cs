using MovieAppApi.Src.Models.Playlist;
using MovieAppApi.Src.Views.DTO.Playlist;

namespace MovieAppApi.Src.Core.Mappers.UpdatePlaylist;

public class UpdatePlaylistRequestBodyMapper : IUpdatePlaylistRequestBodyMapper
{
  public PlaylistModel ToModel(PlaylistDto dto)
  {
    return new PlaylistModel
    (
      id: dto.id,
      name: dto.name,
      description: dto.description,
      movieIds: dto.movie_ids
    );
  }
}