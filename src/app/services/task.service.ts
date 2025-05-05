'.' 
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Task, CreateTaskRequest, UpdateTaskRequest } from '../models/task.model';
import { environment } from '../environments/environment';

@Injectable({
    providedIn: 'root'
})
export class TaskService {
    private apiUrl = `${environment.apiUrl}/api/tasks`;

    constructor(private http: HttpClient) { }

    getTasks(): Observable<Task[]> {
        return this.http.get<Task[]>(this.apiUrl);
    }

    getTaskById(id: string): Observable<Task> {
        return this.http.get<Task>(`${this.apiUrl}/${id}`);
    }

    createTask(task: CreateTaskRequest): Observable<string> {
        return this.http.post<string>(this.apiUrl, task);
    }

    updateTask(task: UpdateTaskRequest): Observable<any> {
        return this.http.put(`${this.apiUrl}/${task.id}`, task);
    }

    deleteTask(id: string): Observable<any> {
        return this.http.delete(`${this.apiUrl}/${id}`);
    }
}
