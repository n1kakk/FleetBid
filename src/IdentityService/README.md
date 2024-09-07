# Identity Service
The IdentityService is a critical component of the FleetBid application responsible for managing user authentication and authorization. It provides secure handling of user identities and integrates with other services to ensure proper access control.

## Key Functions

### User Authentication
- Identity Management: Manages user identities and authentication using ASP.NET Identity and Duende IdentityServer.
- JWT Tokens: Issues and validates JSON Web Tokens (JWT) for secure communication and authentication.

### User Authorization
Role-Based Access Control: Supports role-based access control to restrict access based on user roles and claims.

### User Profile Management
Custom Profile Service: Provides user profile data for tokens, including custom claims like "username".

### Logging and Monitoring
- Serilog: Utilizes Serilog for comprehensive logging, allowing monitoring and troubleshooting of authentication events.


## Technologies
- ASP.NET Core Identity: Handles user management and authentication.
- Duende IdentityServer: Provides authentication and authorization services with support for OAuth2 and OpenID Connect.
- Entity Framework Core: Uses PostgreSQL for managing user data.
- Serilog: Logs application events and errors for monitoring and diagnostics.
- JWT: Implements JSON Web Tokens for secure token-based authentication.

  
## Setup and Configuration
### Service Configuration
Configures services for user management, identity server setup, and authentication.
Uses ApplicationDbContext for database operations and CustomProfileService for profile data handling.

### Pipeline Configuration
Sets up middleware for logging, routing, and handling requests.
Configures IdentityServer for authentication and authorization.
