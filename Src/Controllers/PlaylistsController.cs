using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Src.Core.Exceptions;
using MovieAppApi.Src.Core.Mappers.CreatePlaylist;
using MovieAppApi.Src.Core.Mappers.GetPlaylist;
using MovieAppApi.Src.Core.Mappers.GetPlaylists;
using MovieAppApi.Src.Core.Services.Playlist;
using MovieAppApi.Src.Views.DTO.CreatePlaylist;
using MovieAppApi.Src.Views.DTO.Playlist;

namespace MovieAppApi.Src.Controllers;

public class PlaylistsController : BaseController<PlaylistsController>
{
  private readonly ICreatePlaylistRequestBodyMapper _createPlaylistRequestBodyMapper;
  private readonly ICreatePlaylistResponseMapper _createPlaylistResponseMapper;
  private readonly IGetPlaylistsResponseMapper _getPlaylistsResponseMapper;
  private readonly IGetPlaylistResponseMapper _getPlaylistResponseMapper;
  private readonly IPlaylistService _playlistService;
  public PlaylistsController(ILogger<PlaylistsController> logger,
    ICreatePlaylistRequestBodyMapper createPlaylistRequestBodyMapper,
    ICreatePlaylistResponseMapper createPlaylistResponseMapper,
    IGetPlaylistsResponseMapper getPlaylistsResponseMapper,
    IGetPlaylistResponseMapper getPlaylistResponseMapper,
    IPlaylistService playlistService) : base(logger)
  {
    _createPlaylistRequestBodyMapper = createPlaylistRequestBodyMapper;
    _createPlaylistResponseMapper = createPlaylistResponseMapper;
    _getPlaylistsResponseMapper = getPlaylistsResponseMapper;
    _getPlaylistResponseMapper = getPlaylistResponseMapper;
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

  [HttpGet]
  public async Task<ActionResult<List<PlaylistDto>>> GetPlaylists()
  {
    var playlistModels = await _playlistService.GetPlaylistsAsync();

    var dto = _getPlaylistsResponseMapper.ToDto(playlistModels);

    return Ok(dto);
  }

  [HttpGet("{playlistId}")]
  public async Task<ActionResult<PlaylistDto>> GetPlaylist([Range(1, int.MaxValue)] int playlistId)
  {
    try
    {
      var playlistModel = await _playlistService.GetPlaylistAsync(playlistId);

      var dto = _getPlaylistResponseMapper.ToDto(playlistModel);

      return Ok(dto);
    }
    catch (PlaylistNotFoundException)
    {
      return NotFound($"Playlist id {playlistId} not found");
    }
  }
}
