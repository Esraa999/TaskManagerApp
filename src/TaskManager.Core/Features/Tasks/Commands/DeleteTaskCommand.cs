using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using TaskManager.Core.Exceptions;
using TaskManager.Core.Interfaces;

namespace TaskManager.Core.Features.Tasks.Commands
{
    public class DeleteTaskCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteTaskCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
        {
            var taskRepository = _unitOfWork.Repository<Entities.Task>();
            var task = await taskRepository.GetByIdAsync(request.Id);

            if (task == null)
            {
                throw new EntityNotFoundException(nameof(Entities.Task), request.Id);
            }

            await taskRepository.DeleteAsync(request.Id);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
