using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Core.Features.Tasks.Commands;
using TaskManager.Core.Features.Tasks.Queries;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<TaskDto>>> GetAll()
        {
            var tasks = await _mediator.Send(new GetAllTasksQuery());
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaskDto>> GetById(Guid id)
        {
            var task = await _mediator.Send(new GetTaskByIdQuery { Id = id });
            return Ok(task);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create(CreateTaskCommand command)
        {
            var taskId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id = taskId }, null);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(Guid id, UpdateTaskCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("The ID in the URL does not match the ID in the request body.");
            }

            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteTaskCommand { Id = id });
            return NoContent();
        }
    }
}