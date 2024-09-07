# Identity Service
The IdentityService is a critical component of the FleetBid application responsible for managing user authentication and authorization. It provides secure handling of user identities and integrates with other services to ensure proper access control.

## Key Functions

### User Authentication
- Identity Management: Manages user identities and authentication using [ASP.NET Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0) and [Duende IdentityServer](https://duendesoftware.com/).
- JWT Tokens: Issues and validates [JSON Web Tokens (JWT)](https://jwt.io/) for secure communication and authentication.

### User Authorization
Role-Based Access Control: Supports role-based access control to restrict access based on user roles and claims.

### User Profile Management
Custom Profile Service: Provides user profile data for tokens, including custom claims like "username".

### Logging and Monitoring
- [Serilog](https://serilog.net/): Utilizes Serilog for comprehensive logging, allowing monitoring and troubleshooting of authentication events.


## Technologies
- [ASP.NET Identity](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-8.0): Handles user management and authentication.
- [Duende IdentityServer](https://duendesoftware.com/): Provides authentication and authorization services with support for OAuth2 and OpenID Connect.
- [Entity Framework Core](https://learn.microsoft.com/en-us/ef/core/): Uses PostgreSQL for managing user data.
- [Serilog](https://serilog.net/): Logs application events and errors for monitoring and diagnostics.
- [JWT](https://jwt.io/): Implements JSON Web Tokens for secure token-based authentication.

  
## Setup and Configuration
### Service Configuration
Configures services for user management, identity server setup, and authentication.
Uses ApplicationDbContext for database operations and CustomProfileService for profile data handling.

### Pipeline Configuration
Sets up middleware for logging, routing, and handling requests.
Configures IdentityServer for authentication and authorization.
