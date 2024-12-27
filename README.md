# Orders API

A simple REST API for managing orders, built with .NET Core 3.1.

## Table of Contents
1. [Project Setup](#project-setup)
2. [Running Locally](#running-locally)
3. [API Endpoints](#api-endpoints)
4. [Testing](#testing)
5. [Technologies Used](#technologies-used)
6. [License](#license)

## Project Setup

1. Clone the repository to your local machine:
    ```bash
    git clone https://github.com/yourusername/OrdersApi.git
    cd OrdersApi
    ```

2. Install the required NuGet packages:
    ```bash
    dotnet restore
    ```

3. Build the project:
    ```bash
    dotnet build
    ```

## Running Locally

### Prerequisites

- .NET Core 3.1 SDK or later
- Visual Studio Code (or any code editor of your choice) with .NET Core support
- Postman (for testing endpoints) or any HTTP client tool

### Running the API

1. Navigate to the project folder in your terminal (where `OrdersApi.csproj` is located).
2. Run the application using the following command:
    ```bash
    dotnet run
    ```

   The API will start running locally at:

   - **HTTP**: `http://localhost:5000`
   - **HTTPS**: `https://localhost:5001`

3. To view the Swagger UI, open your browser and navigate to:
   - **Swagger UI**: `https://localhost:5001/swagger/index.html` (for HTTPS)

   Swagger will allow you to interact with the API endpoints via a user-friendly interface.

### Environment Variables

If you want to configure different environments or settings, you can specify them in the `launchSettings.json` or use the following environment variables:
- `ASPNETCORE_ENVIRONMENT=Development` for local development.
- `ASPNETCORE_ENVIRONMENT=Production` for production (if applicable).

## API Endpoints

Below are the available endpoints for the API:

### `POST /api/orders`
Creates a new order.

**Request Body**:
```json
{
    "customerId": 1,
    "product": "Laptop",
    "quantity": 1,
    "status": "Pending"
}
