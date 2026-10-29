# IT Asset Manager

A full-stack web application for tracking IT hardware assets, employee assignments, and device lifecycles. Built with Clean Architecture principles.

## Tech Stack

- **Backend:** .NET 10, ASP.NET Core Web API, Entity Framework Core
- **Frontend:** React (Vite + TypeScript), Tailwind CSS, Axios
- **Database:** PostgreSQL 17
- **DevOps:** Docker, Docker Compose

## Architecture

This project follows Clean Architecture with four layers:

    src/
    AssetManager.Domain          # Entities and enums (no dependencies)
    AssetManager.Application     # Interfaces and DTOs
    AssetManager.Infrastructure  # EF Core, database access, services
    AssetManager.API             # Controllers, DI configuration
    client/                      # React frontend

## Features

- Device Management - Track laptops, monitors, phones with serial numbers, warranty dates, and purchase prices.
- Employee Directory - Manage employees who can be assigned devices.
- Assignment Tracking - Assign devices to employees and track checkout/return history.
- Status Workflow - Devices automatically change status based on assignments.
- Unique Serial Numbers - Database-enforced uniqueness prevents duplicate entries.

## Getting Started

### Prerequisites

- .NET 10 SDK
- Node.js 18+
- Docker

### Quick Start (Docker Compose)

    docker compose up --build

The app will be available at http://localhost:5173

### Local Development

1. Start the database:

    docker run -d --name assetmanager-db -e POSTGRES_USER=admin -e POSTGRES_PASSWORD=admin123 -e POSTGRES_DB=AssetManagerDb -p 5433:5432 postgres:17

2. Run migrations and start the API:

    dotnet ef database update --project src/AssetManager.Infrastructure --startup-project src/AssetManager.API
    dotnet run --project src/AssetManager.API

3. Start the frontend:

    cd client && npm install && npm run dev

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | /api/devices | List all devices |
| POST | /api/devices | Create a device |
| GET | /api/devices/{id} | Get device by ID |
| PUT | /api/devices/{id} | Update a device |
| DELETE | /api/devices/{id} | Delete a device |
| GET | /api/employees | List all employees |
| POST | /api/employees | Create an employee |
| PUT | /api/employees/{id} | Update an employee |
| DELETE | /api/employees/{id} | Delete an employee |
| GET | /api/assignments | List all assignments |
| POST | /api/assignments | Assign a device |
| PATCH | /api/assignments/{id}/return | Return a device |

## License

MIT
