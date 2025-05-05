using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Interfaces;

namespace TaskManager.Core.Features.Tasks.Queries
{
    public class GetTaskByIdQuery : IRequest<TaskDto>
    {
        public Guid Id { get; set; }
    }

    public class GetTaskByIdQueryHandler : IRequestHandler<GetTaskByIdQuery, TaskDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetTaskByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<TaskDto> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
        {
            var taskRepository = _unitOfWork.Repository<Entities.Task>();
            var task = await taskRepository.GetByIdAsync(request.Id);

            if (task == null)
            {
                throw new EntityNotFoundException(nameof(Entities.Task), request.Id);
            }

            return new TaskDto
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = task.Status,
                StatusName = task.Status.ToString(),
                DueDate = task.DueDate,
                Priority = task.Priority,
                PriorityName = task.Priority.ToString(),
                CreatedAt = task.CreatedAt,
                UpdatedAt = task.UpdatedAt
            };
        }
    }
}