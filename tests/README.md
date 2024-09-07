# Testing

This project employs various types of testing to ensure code quality and reliability.

## Unit Tests

Unit tests verify individual components of the system in isolation. The project uses the following tools for writing and executing unit tests:

### [xUnit](https://xunit.net/)
- A popular testing framework for writing and running tests in .NET.

### [Moq](https://github.com/moq/moq4)
- A library for creating mock objects, which helps isolate the components being tested from their dependencies.

### [AutoFixture](https://github.com/AutoFixture/AutoFixture)
- A library for automatically generating test data, reducing the need for manual setup.

## Integration Tests

Integration tests verify the interactions between different components of the system. The project uses the following tools for writing and executing integration tests:

### [xUnit](https://xunit.net/)
- The same testing framework used for unit tests, suitable for integration tests as well.

### [CustomWebAppFactory](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/testing/integration-testing?view=aspnetcore-5.0#custom-webapplicationfactory)
- A class for creating an integration testing environment that allows running tests with a real web service. For more details, see the [Microsoft documentation](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/testing/integration-testing?view=aspnetcore-5.0#custom-webapplicationfactory).


