import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Task, TaskStatus, TaskPriority } from '../../models/task.model';
import { TaskService } from '../../services/task.service';
import { TaskFormComponent } from '../task-form/task-form.component';

@Component({
    selector: 'app-task-list',
    templateUrl: './task-list.component.html',
    styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements OnInit {
    tasks: Task[] = [];
    displayedColumns: string[] = ['title', 'status', 'priority', 'dueDate', 'actions'];
    isLoading = true;

    constructor(
        private taskService: TaskService,
        private dialog: MatDialog,
        private snackBar: MatSnackBar
    ) { }

    ngOnInit(): void {
        this.loadTasks();
    }

    loadTasks(): void {
        this.isLoading = true;
        this.taskService.getTasks().subscribe({
            next: (tasks) => {
                this.tasks = tasks;
                this.isLoading = false;
            },
            error: (error) => {
                console.error('Error loading tasks', error);
                this.snackBar.open('Error loading tasks', 'Close', { duration: 3000 });
                this.isLoading = false;
            }
        });
    }

    openTaskForm(task?: Task): void {
        const dialogRef = this.dialog.open(TaskFormComponent, {
            width: '500px',
            data: task ? { ...task } : null
        });

        dialogRef.afterClosed().subscribe(result => {
            if (result) {
                this.loadTasks();
            }
        });
    }

    deleteTask(task: Task): void {
        if (confirm(`Are you sure you want to delete task "${task.title}"?`)) {
            this.taskService.deleteTask(task.id).subscribe({
                next: () => {
                    this.snackBar.open('Task deleted successfully', 'Close', { duration: 3000 });
                    this.loadTasks();
                },
                error: (error) => {
                    console.error('Error deleting task', error);
                    this.snackBar.open('Error deleting task', 'Close', { duration: 3000 });
                }
            });
        }
    }

    getStatusClass(status: TaskStatus): string {
        switch (status) {
            case TaskStatus.Todo:
                return 'status-todo';
            case TaskStatus.InProgress:
                return 'status-in-progress';
            case TaskStatus.Done:
                return 'status-done';
            default:
                return '';
        }
    }

    getPriorityClass(priority: TaskPriority): string {
        switch (priority) {
            case TaskPriority.Low:
                return 'priority-low';
            case TaskPriority.Medium:
                return 'priority-medium';
            case TaskPriority.High:
                return 'priority-high';
            default:
                return '';
        }
    }
}