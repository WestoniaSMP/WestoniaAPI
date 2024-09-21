# WestoniaAPI

The WestoniaAPI ist the backend for the Westonia SMP project.
It is a RESTful API that provides data for the frontend and the Minecraft plugin.
The API is built with ASP.NET Core 8.0 and Entity Framework Core. It uses a MsSql database to store the data and ASP.NET Identity and JWT for authentication.

## Projects

The solution is divided into multiple projects:
- **WestoniaAPI.Core**: Contains the business logic for the WestoniaAPI, such as data models, interfaces, and services.
- **WestoniaAPI.Infrastructure**: Contains the implementations for the access to the database (via Entity Framework Core) and other external services.

## Getting Started

### Prerequisites
1. Install the [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
2. Install a MsSQL instance, preferabily via Docker

### Installation
1. Clone the repository: `git clone https://github.com/your-username/WestoniaAPI.git`
2. Navigate to the project directory: `cd WestoniaAPI`
3. Restore the NuGet packages: `dotnet restore`
4. Build the solution: `dotnet build`

### Configuration
1. Open the `appsettings.json` file in the `WestoniaAPI` project.
2. Update the connection string to point to your MsSQL instance.

### Database Migration
1. Open a terminal and navigate to the `WestoniaAPI` project directory.
2. Run the following command to apply the database migrations: `dotnet ef database update`

### Running the API
1. Open a terminal and navigate to the `WestoniaAPI` project directory.
2. Run the following command to start the API: `dotnet run`

## Usage
Once the API is running, you can access the endpoints using a tool like Postman or cURL.

## Contributing
Contributions are welcome! Please follow the [contribution guidelines](CONTRIBUTING.md).

## License
This project is licensed under the [MIT License](LICENSE).