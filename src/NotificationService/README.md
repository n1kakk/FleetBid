# Notification Service
The NotificationService is responsible for real-time communication within the FleetBid application. It handles notifications related to auctions and bids, ensuring that users are promptly informed of important events.

## Key Functions

### Real-Time Notifications
- SignalR Integration: Utilizes SignalR to provide real-time updates to clients.
- Notification Types: Sends notifications for various events such as auction creation, bid placement, and auction completion.

### Message Handling
- MassTransit Consumers: Listens for and processes messages related to auctions and bids from RabbitMQ.
- Event Processing: Consumes messages like AuctionCreated, BidPlaced, and AuctionFinished, and pushes notifications to connected clients.


## Technologies
- MassTransit: Manages message bus interactions and event consumption using RabbitMQ.
- SignalR: Provides real-time communication capabilities for live notifications.
- RabbitMQ: Acts as the message broker for inter-service communication.


## Setup and Configuration

### Service Configuration
Configures MassTransit for RabbitMQ messaging and SignalR for real-time notifications.

### Endpoint Configuration
Sets up endpoints for SignalR hubs to handle real-time communication.
