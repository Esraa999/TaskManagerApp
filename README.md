# TaskManager App

A full-stack task management application built with ASP.NET Core 9 and Angular.

## Overview

This project demonstrates a well-architected, scalable application using modern design patterns and best practices. It implements basic CRUD operations for managing tasks, with a focus on clean architecture principles.

## Tech Stack

### Backend
* ASP.NET Core 9 Web API
* Entity Framework Core with SQL Server
* Clean Architecture (Core, Infrastructure, API layers)
* MediatR for CQRS pattern
* Repository & Unit of Work patterns
* FluentValidation for input validation

### Frontend
* Angular 17
* Angular Material UI components
* Reactive Forms
* HTTP client for API communication

## Features
* Create, read, update, and delete tasks
* Filter and sort tasks by status, priority, and due date
* Form validation on both client and server
* Responsive design using Angular Material
* Clean architecture with separation of concerns
* Design patterns implementation (Repository, UnitOfWork, Mediator, etc.)
* Proper error handling through middleware

## Screenshots
[Screenshots will be added here]

## Getting Started
See the Setup Instructions file for detailed steps on how to run the application.

### Quick Start

#### Backend
```bash
cd TaskManagerApp
dotnet build
cd src/TaskManager.API
dotnet run
```

#### Frontend
```bash
cd client-app/task-manager-client
npm install
ng serve
```

* Backend API: https://localhost:7001
* Frontend: http://localhost:4200

## Project Structure
```
TaskManagerApp/
├── src/
│   ├── TaskManager.Core/            # Domain entities, interfaces, business logic
│   ├── TaskManager.Infrastructure/  # Data access, repositories, EF Core
│   └── TaskManager.API/             # Controllers, middleware, configuration
├── client-app/
│   └── task-manager-client/         # Angular frontend application
└── TaskManagerApp.sln               # Solution file
```

## API Endpoints
* GET /api/tasks - Get all tasks
* GET /api/tasks/{id} - Get task by ID
* POST /api/tasks - Create a new task
* PUT /api/tasks/{id} - Update an existing task
* DELETE /api/tasks/{id} - Delete a task

## Design Patterns Used
* **Repository Pattern**: Abstracts data access
* **Unit of Work Pattern**: Manages a group of operations as a single unit
* **CQRS Pattern**: Separates read and write operations using MediatR
* **Mediator Pattern**: Decouples components via a central mediator (MediatR)
* **Decorator Pattern**: Adds behavior to objects (used in middleware)
* **Factory Pattern**: Creates objects without specifying concrete classes

