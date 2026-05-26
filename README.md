# Logistics Dashboard

A logistics management dashboard with Vue 2 frontend and .NET Core 8 backend.

## Structure

- `frontend/` — Vue 2 SPA (Dashboard, Shipments, Warehouses, Vehicles)
- `backend/` — .NET Core 8 WebAPI with dummy data

## Quick Start

```bash
# Backend
cd backend
dotnet run

# Frontend
cd frontend
npm install
npm run serve
```

## Development

- Frontend: http://localhost:3004
- Backend API: http://localhost:8003
- Production: nginx reverse proxy on port 80 (logistics.yaaniai.com + logistics-api.yaaniai.com)