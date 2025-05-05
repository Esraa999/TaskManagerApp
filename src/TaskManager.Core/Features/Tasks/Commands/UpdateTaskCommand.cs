using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.Core.Entities;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Interfaces;

namespace TaskManager.Core.Features.Tasks.Commands
{
    public class UpdateTaskCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public System.Threading.Tasks.TaskStatus Status { get; set; }
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; }
    }

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
        {
            var taskRepository = _unitOfWork.Repository<Entities.Task>();
            var task = await taskRepository.GetByIdAsync(request.Id);

            if (task == null)
            {
                throw new EntityNotFoundException(nameof(Entities.Task), request.Id);
            }

            task.Title = request.Title;
            task.Description = request.Description;
            task.Status = (Entities.TaskStatus)request.Status;
            task.DueDate = request.DueDate;
            task.Priority = request.Priority;
            task.UpdatedAt = DateTime.UtcNow;

            await taskRepository.UpdateAsync(task);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
