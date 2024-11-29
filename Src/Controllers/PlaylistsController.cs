using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Src.Core.Mappers.CreatePlaylist;
using MovieAppApi.Src.Core.Services.Playlist;
using MovieAppApi.Src.Views.DTO.CreatePlaylist;
using MovieAppApi.Src.Views.DTO.Playlist;

namespace MovieAppApi.Src.Controllers;

public class PlaylistsController : BaseController<PlaylistsController>
{
  private readonly ICreatePlaylistRequestBodyMapper _createPlaylistRequestBodyMapper;
  private readonly ICreatePlaylistResponseMapper _createPlaylistResponseMapper;
  private readonly IPlaylistService _playlistService;
  public PlaylistsController(ILogger<PlaylistsController> logger,
    ICreatePlaylistRequestBodyMapper createPlaylistRequestBodyMapper,
    ICreatePlaylistResponseMapper createPlaylistResponseMapper,
    IPlaylistService playlistService) : base(logger)
  {
    _createPlaylistRequestBodyMapper = createPlaylistRequestBodyMapper;
    _createPlaylistResponseMapper = createPlaylistResponseMapper;
    _playlistService = playlistService;
  }

  [HttpPost]
  public async Task<ActionResult<PlaylistDto>> CreatePlaylist([FromBody] CreatePlaylistRequestBodyDto playlistDto)
  {
    var playlistModel = _createPlaylistRequestBodyMapper.ToModel(playlistDto);

    var createdPlaylistModel = await _playlistService.CreatePlaylistAsync(playlistModel);

    var dto = _createPlaylistResponseMapper.ToDto(createdPlaylistModel);

    return CreatedAtAction(nameof(CreatePlaylist), null, dto);
  }
}
