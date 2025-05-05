import { Component, OnInit, Inject } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Task, TaskStatus, TaskPriority } from '../../models/task.model';
import { TaskService } from '../../services/task.service';

@Component({
    selector: 'app-task-form',
    templateUrl: './task-form.component.html',
    styleUrls: ['./task-form.component.scss']
})
export class TaskFormComponent implements OnInit {
    taskForm: FormGroup;
    isEditMode: boolean;
    taskStatuses = Object.values(TaskStatus).filter(value => typeof value === 'number');
    taskPriorities = Object.values(TaskPriority).filter(value => typeof value === 'number');
    isLoading = false;

    constructor(
        private fb: FormBuilder,
        private taskService: TaskService,
        private snackBar: MatSnackBar,
        private dialogRef: MatDialogRef<TaskFormComponent>,
        @Inject(MAT_DIALOG_DATA) public data: Task | null
    ) {
        this.isEditMode = !!data;
        this.taskForm = this.fb.group({
            title: ['', [Validators.required, Validators.maxLength(100)]],
            description: ['', Validators.maxLength(500)],
            status: [TaskStatus.Todo, Validators.required],
            dueDate: [null],
            priority: [TaskPriority.Medium, Validators.required]
        });

        if (this.isEditMode && data) {
            this.taskForm.patchValue({
                title: data.title,
                description: data.description,
                status: data.status,
                dueDate: data.dueDate ? new Date(data.dueDate) : null,
                priority: data.priority
            });
        }
    }

    ngOnInit(): void { }

    getStatusName(status: TaskStatus): string {
        return TaskStatus[status];
    }

    getPriorityName(priority: TaskPriority): string {
        return TaskPriority[priority];
    }

    onSubmit(): void {
        if (this.taskForm.invalid) {
            return;
        }

        this.isLoading = true;

        if (this.isEditMode && this.data) {
            const updateRequest = {
                id: this.data.id,
                ...this.taskForm.value
            };

            this.taskService.updateTask(updateRequest).subscribe({
                next: () => {
                    this.snackBar.open('Task updated successfully', 'Close', { duration: 3000 });
                    this.dialogRef.close(true);
                },
                error: (error) => {
                    console.error('Error updating task', error);
                    this.snackBar.open('Error updating task', 'Close', { duration: 3000 });
                    this.isLoading = false;
                }
            });
        } else {
            this.taskService.createTask(this.taskForm.value).subscribe({
                next: () => {
                    this.snackBar.open('Task created successfully', 'Close', { duration: 3000 });
                    this.dialogRef.close(true);
                },
                error: (error) => {
                    console.error('Error creating task', error);
                    this.snackBar.open('Error creating task', 'Close', { duration: 3000 });
                    this.isLoading = false;
                }
            });
        }
    }
}