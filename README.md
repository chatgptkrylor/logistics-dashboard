# Logistics Dashboard

This project is a small logistics dashboard with:

1. A Vue 2 frontend in `frontend/`
2. A .NET 8 backend API in `backend/`

The frontend shows dashboard pages for shipments, warehouses, and vehicles.
The backend serves hardcoded dummy data.
You do not need a database to run this project.

## 1. What Is In This Project

Project structure:

```text
logistics/
├── frontend/        Vue 2 single-page app
├── backend/         .NET 8 Web API
├── .gitignore
└── README.md
```

Important frontend files:

1. `frontend/src/App.vue`
2. `frontend/src/main.js`
3. `frontend/src/router/index.js`
4. `frontend/src/views/Dashboard.vue`
5. `frontend/src/views/Shipments.vue`
6. `frontend/src/views/Warehouses.vue`
7. `frontend/src/views/Vehicles.vue`
8. `frontend/vue.config.js`

Important backend files:

1. `backend/Program.cs`
2. `backend/Controllers/LogisticsController.cs`
3. `backend/Models/DataModels.cs`
4. `backend/LogisticsApi.csproj`
5. `backend/appsettings.json`

## 2. What You Need Before You Start

Install these tools first:

1. Node.js version 18 or newer
2. `npm`
3. .NET SDK 8.0
4. `nginx` only if you want to run this in production

## 3. Check That Your Tools Are Installed

Open a terminal in this project folder and run these commands one by one:

```bash
node --version
npm --version
dotnet --version
```

You should see version numbers printed to the screen.

If `dotnet --version` does not show an `8.x.x` version, install .NET SDK 8 before continuing.

## 4. Start The Backend API

Step 1. Move into the backend folder:

```bash
cd backend
```

Step 2. Restore .NET packages:

```bash
dotnet restore
```

Step 3. Start the backend server:

```bash
dotnet run
```

Step 4. Leave that terminal running.

The backend starts on:

```text
http://localhost:8003
```

## 5. Test The Backend API

After the backend starts, open this URL in your browser:

```text
http://localhost:8003/api/health
```

You should get a small JSON response showing the API is healthy.

Available API endpoints:

1. `/api/health`
2. `/api/stats`
3. `/api/shipments`
4. `/api/warehouses`
5. `/api/vehicles`
6. `/api/daily`

Example:

```text
http://localhost:8003/api/shipments
```

## 6. Start The Frontend

Open a second terminal.

Step 1. Move into the frontend folder:

```bash
cd frontend
```

Step 2. Install Node packages:

```bash
npm install
```

Step 3. Start the Vue development server:

```bash
npm run serve
```

Step 4. Leave that terminal running too.

The frontend starts on:

```text
http://localhost:3004
```

## 7. Open The App In Your Browser

Open this URL:

```text
http://localhost:3004
```

This is the main development URL for the dashboard.

Development URLs:

1. Frontend: `http://localhost:3004`
2. Backend API: `http://localhost:8003`
3. Health check: `http://localhost:8003/api/health`

## 8. How Frontend And Backend Talk To Each Other

The frontend is configured to send all `/api/*` requests to the backend automatically.

This happens through `frontend/vue.config.js`.

That means:

1. The frontend can call `/api/stats`
2. Vue will proxy that request to `http://localhost:8003/api/stats`
3. You usually do not need to change frontend API URLs during local development

## 9. Dummy Data Explained

This project uses hardcoded dummy data inside `backend/Controllers/LogisticsController.cs`.

Included data:

1. 10 shipments
2. 5 warehouses
3. 6 vehicles
4. 1 stats summary
5. 7 days of daily shipment data

Because the data is hardcoded:

1. No database setup is needed
2. No migration is needed
3. Data resets every time the app restarts

## 10. Production Setup With nginx

For production, `nginx` can be used as a reverse proxy on port `80`.

Expected domain routing:

1. `logistics.yaaniai.com` -> frontend on port `3004`
2. `logistics-api.yaaniai.com` -> backend on port `8003`

The backend CORS policy in `backend/Program.cs` already allows these frontend origins:

1. `http://logistics.yaaniai.com`
2. `http://logistics.yaaniai.com:3004`
3. `http://localhost:3004`
4. `http://127.0.0.1:3004`
5. `http://0.0.0.0:3004`

If you deploy to a different domain, update the allowed origins in `backend/Program.cs`.

## 11. Setting Up FQDN Access (Domain Names)

FQDN means Fully Qualified Domain Name.

In simple words, this means using clean URLs like `http://logistics.yaaniai.com` instead of raw port-based URLs like `http://localhost:3004`.

This is useful in two common cases:

1. Local development when you want domain-style URLs
2. Production when real users will open the app through a domain name

### Step 1. Local Development With `/etc/hosts`

This is the easiest way to test domain names on your own machine.

Open `/etc/hosts` with root permission and add these lines:

```text
127.0.0.1  logistics.yaaniai.com
127.0.0.1  logistics-api.yaaniai.com
```

Example command:

```bash
sudo nano /etc/hosts
```

After saving the file, these names will point to your local machine.

You can then open:

1. `http://logistics.yaaniai.com:3004`
2. `http://logistics-api.yaaniai.com:8003`

This does not require public DNS.
It only works on the machine where you changed `/etc/hosts`.

### Step 2. Production With `nginx`

In production, you usually want `nginx` to listen on port `80` and forward traffic to the frontend and backend services.

Create this file:

```text
/etc/nginx/sites-available/logistics
```

Add this configuration:

```nginx
server {
    listen 80;
    server_name logistics.yaaniai.com;

    location /api/ {
        proxy_pass http://127.0.0.1:8003/api/;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }

    location / {
        proxy_pass http://127.0.0.1:3004;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
        proxy_set_header Upgrade $http_upgrade;
        proxy_set_header Connection "upgrade";
    }
}

server {
    listen 80;
    server_name logistics-api.yaaniai.com;

    location / {
        proxy_pass http://127.0.0.1:8003;
        proxy_set_header Host $host;
        proxy_set_header X-Real-IP $remote_addr;
    }
}
```

Then enable the site:

```bash
sudo ln -s /etc/nginx/sites-available/logistics /etc/nginx/sites-enabled/logistics
sudo rm -f /etc/nginx/sites-enabled/default
sudo nginx -t
sudo systemctl restart nginx
```

If `sudo nginx -t` shows an error, fix that error before restarting `nginx`.

### Step 3. Public Access With A Real Domain

If you want other people to access the app from the internet, your domain must point to your server.

You can do that in one of these ways:

1. Point your domain DNS records to the server IP
2. Use a service such as Cloudflare Tunnel

The `nginx` configuration can stay the same.

### Step 4. Test That It Works

After setup, test both routes.

Frontend domain through backend route:

```bash
curl http://logistics.yaaniai.com/api/health
```

Direct backend domain:

```bash
curl http://logistics-api.yaaniai.com/api/health
```

Both should return:

```json
{"status":"ok"}
```

If these requests work, your domain routing is set up correctly.

## 12. Common Problems And Fixes

### Problem 1: Port `3004` or `8003` is already in use

Fix:

1. Stop the process already using that port
2. Or change the port in the project config

Frontend port is set in:

1. `frontend/vue.config.js`

Backend port is set in:

1. `backend/Program.cs`

### Problem 2: `npm install` fails

Fix:

1. Delete `frontend/node_modules`
2. Delete `frontend/package-lock.json`
3. Run `npm install` again

### Problem 3: `dotnet restore` fails

Fix:

1. Run `dotnet --version`
2. Confirm that .NET SDK 8 is installed
3. Install or upgrade .NET if needed
4. Run `dotnet restore` again

### Problem 4: Browser shows a CORS error

Fix:

1. Open `backend/Program.cs`
2. Check the allowed origins in the CORS policy
3. Make sure your frontend URL matches one of those allowed origins

## 13. Full Quick Start

If you want the shortest possible version, follow these steps:

1. Open terminal number 1
2. Run:

```bash
cd backend
dotnet restore
dotnet run
```

3. Open terminal number 2
4. Run:

```bash
cd frontend
npm install
npm run serve
```

5. Open `http://localhost:3004` in your browser

## 14. Summary

To run this project successfully:

1. Start the backend first on port `8003`
2. Start the frontend second on port `3004`
3. Open the frontend in your browser
4. Let the frontend proxy API calls to the backend

That is enough to run the full project locally.
