# Auction Service
The AuctionService is a microservice responsible for managing auction operations in the FleetBid application. This service provides endpoints to create, read, update, and delete auctions. It utilizes various technologies and patterns to ensure efficient data management and interaction.

## Key Components
### Database Configuration
- Entity Framework Core: Utilizes PostgreSQL for data storage.
- DbContext: Configured to use AuctionDbContext for database interactions.

### AutoMapper
Automatically maps between DTOs and entities.

### MassTransit
- Message Bus: Handles message publishing and consumption using RabbitMQ.
- Entity Framework Outbox: Ensures reliable message delivery with PostgreSQL.

### Authentication
- JWT: Uses JSON Web Tokens for secure authentication.
- Identity Service: Configured with details from IdentityServiceUrl to validate tokens.

### gRPC
Provides a gRPC service for efficient communication between microservices.

## Setup and Configuration
- Services: Configures essential services, including controllers, AutoMapper, MassTransit, authentication, and gRPC.
- Authentication: JWT tokens are used for securing endpoints.
- Routing: Maps controllers and gRPC services to handle incoming requests.
