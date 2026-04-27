# memoire-maram - Road Safety Educational App

Full-stack CRUD educational road safety application:
- **Backend:** ASP.NET Core 8 Web API (`backend/RoadSafetyAPI`)
- **Frontend:** Angular 17 (`frontend`)
- **Database:** SQL Server

## Backend

```bash
cd /home/runner/work/memoire-maram/memoire-maram/backend/RoadSafetyAPI
dotnet restore
dotnet build
dotnet run
```

- Swagger: `https://localhost:5001/swagger` (or local configured URL)
- Seed users:
  - `admin@example.com / Admin@123`
  - `student1@example.com / Student@123`
  - `student2@example.com / Student@123`
  - `parent@example.com / Parent@123`

### Migrations note
- The project uses `EnsureCreated()` in `DataSeeder` for fast setup.
- If you prefer migrations:
  ```bash
  dotnet ef migrations add InitialCreate
  dotnet ef database update
  ```

## Frontend

```bash
cd /home/runner/work/memoire-maram/memoire-maram/frontend
npm install
npm run build
npm start
```

Open: `http://localhost:4200`

## Deliverables
- SQL schema: `database/schema.sql`
- API docs: `docs/api-endpoints.md`
- Seed data: `backend/RoadSafetyAPI/Data/DataSeeder.cs`
