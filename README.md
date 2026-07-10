# tproject

A simple ASP.NET Core MVC project management tool inspired by Trello. Organize work into projects, boards, columns, and tasks with drag-and-drop support.

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

## Getting Started

```bash
# Restore and build
dotnet build tproject.sln

# Run the web application
dotnet run --project src/Tproject.Web
```

Open the URL shown in the console (typically `https://localhost:5xxx`).

## Database Configuration

The app supports two database providers, configured in `src/Tproject.Web/appsettings.json`:

```json
{
  "Database": {
    "Provider": "InMemory"
  },
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=Tproject;Trusted_Connection=True;TrustServerCertificate=True"
  }
}
```

| Provider | Description |
|----------|-------------|
| `InMemory` | Default. Runs immediately with no external database. Data is lost on restart. |
| `SqlServer` | Connects to Microsoft SQL Server using the `DefaultConnection` connection string. |

### Switching to SQL Server

1. Set `"Provider": "SqlServer"` in `appsettings.json`.
2. Update `DefaultConnection` with your SQL Server connection string.
3. Create and apply migrations:

```bash
dotnet ef migrations add InitialCreate --project src/Tproject.Infrastructure --startup-project src/Tproject.Web
dotnet ef database update --project src/Tproject.Infrastructure --startup-project src/Tproject.Web
```

## Project Structure

```
src/
├── Tproject.Domain/          # Entities and enums
├── Tproject.Infrastructure/  # EF Core, services, provider abstraction
└── Tproject.Web/             # ASP.NET Core MVC app
```

## Features

- Create projects and boards
- Add columns to boards (e.g. To Do, In Progress, Done)
- Create tasks within columns
- Drag and drop tasks between columns and reorder within a column

## License

GPL v3 — see [LICENSE](LICENSE).
