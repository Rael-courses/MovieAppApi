using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MovieAppApi.Src.Core.Exceptions;
using MovieAppApi.Src.Core.Mappers.CreatePlaylist;
using MovieAppApi.Src.Core.Mappers.GetPlaylist;
using MovieAppApi.Src.Core.Mappers.GetPlaylists;
using MovieAppApi.Src.Core.Mappers.UpdatePlaylist;
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
  private readonly IUpdatePlaylistRequestBodyMapper _updatePlaylistRequestBodyMapper;
  private readonly IUpdatePlaylistResponseMapper _updatePlaylistResponseMapper;
  private readonly IPlaylistService _playlistService;
  public PlaylistsController(ILogger<PlaylistsController> logger,
    ICreatePlaylistRequestBodyMapper createPlaylistRequestBodyMapper,
    ICreatePlaylistResponseMapper createPlaylistResponseMapper,
    IGetPlaylistsResponseMapper getPlaylistsResponseMapper,
    IGetPlaylistResponseMapper getPlaylistResponseMapper,
    IUpdatePlaylistRequestBodyMapper updatePlaylistRequestBodyMapper,
    IUpdatePlaylistResponseMapper updatePlaylistResponseMapper,
    IPlaylistService playlistService) : base(logger)
  {
    _createPlaylistRequestBodyMapper = createPlaylistRequestBodyMapper;
    _createPlaylistResponseMapper = createPlaylistResponseMapper;
    _getPlaylistsResponseMapper = getPlaylistsResponseMapper;
    _getPlaylistResponseMapper = getPlaylistResponseMapper;
    _updatePlaylistRequestBodyMapper = updatePlaylistRequestBodyMapper;
    _updatePlaylistResponseMapper = updatePlaylistResponseMapper;
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

  [HttpPut("{playlistId}")]
  public async Task<ActionResult<PlaylistDto>> UpdatePlaylist([Range(1, int.MaxValue)] int playlistId, [FromBody] PlaylistDto playlistDto)
  {
    if (playlistId != playlistDto.id)
    {
      return BadRequest("Playlist id in the path does not match the id in the request body");
    }

    try
    {
      var playlistModel = _updatePlaylistRequestBodyMapper.ToModel(playlistDto);

      var updatedPlaylistModel = await _playlistService.UpdatePlaylistAsync(playlistId, playlistModel);

      var dto = _updatePlaylistResponseMapper.ToDto(playlistModel);

      return Ok(dto);
    }
    catch (PlaylistNotFoundException)
    {
      return NotFound($"Playlist id {playlistId} not found");
    }
  }

  [HttpDelete("{playlistId}")]
  public async Task<IActionResult> DeletePlaylist([Range(1, int.MaxValue)] int playlistId)
  {
    try
    {
      await _playlistService.DeletePlaylistAsync(playlistId);

      return NoContent();
    }
    catch (PlaylistNotFoundException)
    {
      return NotFound($"Playlist id {playlistId} not found");
    }
  }
}
