using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.Core.Entities;
using TaskStatus = TaskManager.Core.Entities.TaskStatus;

namespace TaskManager.Core.Features.Tasks.Queries
{
    public class TaskDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public TaskStatus Status { get; set; }
        public string StatusName { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public TaskPriority Priority { get; set; }
        public string PriorityName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}

