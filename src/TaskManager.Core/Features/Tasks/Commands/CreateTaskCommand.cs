using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.Core.Entities;
using TaskManager.Core.Interfaces;
using TaskStatus = TaskManager.Core.Entities.TaskStatus;

namespace TaskManager.Core.Features.Tasks.Commands
{
    public class CreateTaskCommand : IRequest<Guid>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; } = TaskStatus.Todo;
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; } = TaskPriority.Medium;
    }

    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskRepository = _unitOfWork.Repository<Entities.Task>();

            var task = new Entities.Task
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                DueDate = request.DueDate,
                Priority = request.Priority,
                CreatedAt = DateTime.UtcNow
            };

            await taskRepository.AddAsync(task);
            await _unitOfWork.CompleteAsync();

            return task.Id;
        }
    }
}
