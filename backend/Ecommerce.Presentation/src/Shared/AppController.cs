using Asp.Versioning;
using Ecommerce.Domain.src.Model;
using Ecommerce.Domain.src.Shared;
using Ecommerce.Service.src.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Ecommerce.Presentation.src.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[Controller]s")]
    public class AppController<T, TReadDto, TCreateDto, TUpdateDto> : ControllerBase
        where T : BaseEntity
        where TReadDto : IReadDto<T>, new()
        where TCreateDto : ICreateDto<T>
        where TUpdateDto : IUpdateDto<T>
    {
        private readonly IBaseService<T, TReadDto, TCreateDto, TUpdateDto> _baseService;
        private readonly ILogger<AppController<T, TReadDto, TCreateDto, TUpdateDto>> _logger;

        public AppController(IBaseService<T, TReadDto, TCreateDto, TUpdateDto> baseService, ILogger<AppController<T, TReadDto, TCreateDto, TUpdateDto>> logger)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
            _logger = logger;
        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        public virtual async Task<ActionResult<TReadDto>> CreateAsync([FromBody] TCreateDto createDto)
        {
            if (createDto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var createdEntity = await _baseService.CreateAsync(createDto);
                var version = HttpContext.GetRequestedApiVersion()?.ToString() ?? "1.0";

                _logger.LogInformation($"Created Entity ID: {createdEntity.Id}, Version: {version}");

                return Ok(createdEntity);

                // For future check: Uncomment for 201 Created response
                // return CreatedAtAction(
                //     nameof(GetByIdAsync),
                //     new { id = createdEntity.Id, version = version },
                //     createdEntity
                // );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating entity");
                return StatusCode(500, $"Error creating entity: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        [MapToApiVersion("1.0")]
        public virtual async Task<ActionResult<TReadDto>> UpdateAsync(Guid id, [FromBody] TUpdateDto updateDto)
        {
            if (updateDto == null)
            {
                return BadRequest("Invalid data.");
            }

            try
            {
                var updatedEntity = await _baseService.UpdateAsync(id, updateDto);
                return Ok(updatedEntity);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Entity not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating entity");
                return StatusCode(500, $"Error updating entity: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        [MapToApiVersion("1.0")]
        public virtual async Task<ActionResult> DeleteAsync(Guid id)
        {
            try
            {
                var success = await _baseService.DeleteAsync(id);
                if (!success)
                {
                    return NotFound("Entity not found.");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting entity");
                return StatusCode(500, $"Error deleting entity: {ex.Message}");
            }
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        public virtual async Task<ActionResult<PaginatedResult<TReadDto>>> GetAllAsync([FromQuery] PaginationOptions paginationOptions)
        {
            try
            {
                var entities = await _baseService.GetAllAsync(paginationOptions);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting entities");
                return StatusCode(500, $"Error getting entities: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        [MapToApiVersion("1.0")]
        public virtual async Task<ActionResult<TReadDto>> GetByIdAsync(Guid id)
        {
            try
            {
                var entity = await _baseService.GetByIdAsync(id);
                if (entity == null)
                {
                    return NotFound("Entity not found.");
                }
                return Ok(entity);
            }
            catch (KeyNotFoundException)
            {
                return NotFound("Entity not found.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting entity");
                return StatusCode(500, $"Error getting entity: {ex.Message}");
            }
        }
    }
}
