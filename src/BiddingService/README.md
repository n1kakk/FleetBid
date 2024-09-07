# Bid Service
The BidService is a microservice responsible for managing bids on auction items within the FleetBid application. It handles bid placements, processes auction-related events, and communicates with other microservices to maintain data consistency and integrity.

## Key Components

### Database Configuration
- [MongoDB](https://www.mongodb.com/): Utilizes MongoDB for data storage.
- DbContext: Configured to use MongoClient for database interactions.
  
### [AutoMapper](https://automapper.org/)
Automatically maps between DTOs and entities to simplify data transformations.

### [MassTransit](https://masstransit.io/)
- Message Bus: Manages message publishing and consumption using RabbitMQ.
- Consumers: Listens for events such as AuctionCreated and processes them accordingly.

### Authentication
- [JWT](https://jwt.io/): Uses JSON Web Tokens for secure authentication and authorization.
- Identity Service: Configured with details from IdentityServiceUrl to validate tokens.
  
### [gRPC](https://grpc.io/)
- Service: Provides a gRPC client to communicate efficiently with other microservices, such as querying auction details.
  
### Background Services
Hosted Services: Includes background tasks such as CheckAuctionFinished to periodically check and update the status of auctions.

## Setup and Configuration
- Services: Configures essential services, including controllers, AutoMapper, MassTransit, authentication, and gRPC.
- Authentication: JWT tokens are used to secure endpoints.
- Routing: Maps controllers to handle incoming requests and integrates with gRPC services.
