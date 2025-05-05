using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.Core.Interfaces;

namespace TaskManager.Core.Features.Tasks.Queries
{
    public class GetAllTasksQuery : IRequest<List<TaskDto>>
    {
    }

    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, List<TaskDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllTasksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TaskDto>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
        {
            var taskRepository = _unitOfWork.Repository<Entities.Task>();
            var tasks = await taskRepository.GetAllAsync();

            return tasks.Select(task => new TaskDto
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
            }).ToList();
        }
    }
}