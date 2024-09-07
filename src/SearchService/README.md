# Search Service
The SearchService provides functionality for searching and managing auction-related items within the FleetBid platform. It processes and responds to auction-related events and performs searches based on specified parameters.

## Key Functions

### Search Operations
Search API: Provides an endpoint to search for auction items using various parameters.

### Event Handling
Consumers: Processes messages related to auction events (created, updated, deleted) from RabbitMQ to keep search indices up to date.

## Technologies
- MassTransit: Manages message bus interactions with RabbitMQ for handling auction-related events.
- Polly: Implements resilience and transient fault-handling policies for HTTP requests.
- MongoDB: Utilized for storing and managing search data.
- AutoMapper: Maps between DTOs and entities for data processing.

## Setup and Configuration

### Service Configuration
Configures MassTransit with RabbitMQ for consuming messages related to auction events.
Sets up Polly for handling transient errors in HTTP requests.

### Database Initialization
Initializes MongoDB database during application startup.

### HTTP Client Configuration
Uses HttpClient with resilience policies for communication with other services.
