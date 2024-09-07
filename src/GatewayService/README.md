# Gateway Service (YARP)
The Gateway Service in this project is built using YARP (Yet Another Reverse Proxy). YARP is a highly customizable reverse proxy library for .NET that allows us to route requests to various microservices based on the request URL.

## Configuration
The Gateway Service is configured to handle authentication, CORS (Cross-Origin Resource Sharing), and reverse proxy settings. Here’s a brief overview of how these configurations are set up:

## Authentication
The Gateway Service uses JWT (JSON Web Token) for authentication. The configuration is set up as follows:

```csharp
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.Authority = builder.Configuration["IdentityServiceUrl"];
        opt.RequireHttpsMetadata = false;
        opt.TokenValidationParameters.ValidateAudience = false;
        opt.TokenValidationParameters.NameClaimType = "username";
    });
```

## CORS
Cross-Origin Resource Sharing (CORS) is configured to allow requests from specified origins.

## Reverse Proxy Configuration
The reverse [proxy configuration](appsettings.Docker.json) routes requests to different microservices based on the request URL. 
This configuration allows the Gateway to distribute requests to various microservices, ensuring that the application is scalable and maintainable.

## Usage
- Ensure that all microservices are running and accessible at the specified addresses.
- Start the Gateway Service. It will handle authentication, routing, and proxying requests to the appropriate microservices based on the configuration.
