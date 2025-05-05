export interface Task {
    id: string;
    title: string;
    description: string;
    status: TaskStatus;
    statusName: string;
    dueDate: Date | null;
    priority: TaskPriority;
    priorityName: string;
    createdAt: Date;
    updatedAt: Date | null;
}

export enum TaskStatus {
    Todo = 0,
    InProgress = 1,
    Done = 2
}

export enum TaskPriority {
    Low = 0,
    Medium = 1,
    High = 2
}

export interface CreateTaskRequest {
    title: string;
    description: string;
    status: TaskStatus;
    dueDate: Date | null;
    priority: TaskPriority;
}

export interface UpdateTaskRequest {
    id: string;
    title: string;
    description: string;
    status: TaskStatus;
    dueDate: Date | null;
    priority: TaskPriority;
}