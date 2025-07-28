✅ Pixelz Checkout Microservices
📌 Overview
	Pixelz Checkout is a microservice-based system built using .NET Core for managing e-commerce orders. It handles:

	Order get all, searching, and checkout.

	Mocked payment processing.

	Simulated order submission to production.

	Email notification after successful checkout.

	The system uses In-Memory Database for demo purposes and communicates between services via REST APIs.

📂 Project Structure
/PixelzSolution
 ├── OrderService         # Handles order operations
 ├── PaymentService       # Mock payment processor
 ├── EmailService         # Mock email sender
 ├── ProductionService    # Mock production system
 ├── Commons.DTOs          # Shared models and ApiResponse<T>
 └── README.md
 
 
🔑 Core Features
✅ Search orders by name

✅ Checkout order with:

	Payment verification

	Production system call

	Email notification

✅ Standardized API responses (ApiResponse<T>)

✅ Sample data seeding

✅ Swagger for API testing



▶ Run the Project
Prerequisites
.NET 8 SDK

Visual Studio / VS Code

Steps

git clone https://github.com/garungns/pixelz-checkout-order-test
cd pixelz-checkout

# Restore dependencies
dotnet restore

# Run all services (in separate terminals)
dotnet run --project OrderService
dotnet run --project PaymentService
dotnet run --project ProductionService
dotnet run --project EmailService
Access Swagger UI:

OrderService → https://localhost:7258/swagger/index.html
