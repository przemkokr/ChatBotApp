# ChatBot App - demo purposes
## How to run it
### Docker compose
Make sure you have docker daemon installed and running
Everything should be set up and configurable in just couple of single steps.
1. Download the code
2. In the top-level root dir (where docker-compose.yml file is located) create an .env file with just a one single var there:
   3. MSSQL_SA_PASSWORD=<YourSuperSecretPassword123!>
4. Next, in the API project, find appsettings.json file, and put connection string there:
   5. ``
      "ConnectionStrings": {
   "DefaultConnection": "Server=db;Database=ChatBotDb;User Id=sa;Password=YourSuperSecretPassword123!;TrustServerCertificate=True;"
   }
      ``
6. Open cli of your choice and run ``docker compose up --build``
7. Frontend app is already configured to cooperate with backend, should be accessible at ``http://localhost:4200``
8. Enjoy :)

### Standalone services

If you don't want to use docker compose, you can run each service separately with ease.

1. Make sure you have some db engine on you machine you can connect to.
2. Put a valid connection string to your db engine in appsettings.json in .API project
3. Build solution and run the API project, it should build & migrate db for you on startup.
3. Locate client app directory, there should be environment.ts file in the root of it
4. Set API_URL to "http://localhost:5184" - backend should be listening on that address.
5. Open your cli of choice, change directory to client app root (where the package.json file is located) and run ``npm install`` and then ``ng serve``
6. Enjoy :)
