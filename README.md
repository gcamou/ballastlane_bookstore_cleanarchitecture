# Project Exposition - Book Management System

This project implements a Book Management System that allows users to register, login, and manage books. The system provides endpoints for registering new users, logging in, and performing CRUD operations on books.

## Components

### AuthenticationController

The `AuthenticationController` handles user registration and login.

#### Endpoints

- `POST /auth/register`: Registers a new user using `IdentityUser` and `UserManager`.
- `POST /auth/login`: Logs in a user using `IdentityUser` and `UserManager`.

### BookController

The `BookController` allows users to manage books.

#### Endpoints

- `GET /api/Book/title:string`: Retrieves a book by its title.
- `GET /api/Book/id:guid`: Retrieves a book by its ID.
- `GET /api/Book/all`: Retrieves all books.
- `POST /api/Book`: Creates a new book (requires authorization using JWT based on roles, i.e., Administrator or Manager).
- `PUT /api/Book`: Updates an existing book (requires authorization using JWT based on roles, i.e., Administrator or Manager).
- `DELETE /api/Book/id:guid`: Deletes a book by its ID (requires authorization using JWT based on roles, i.e., Manager).

## Usage

### Authentication

- To register a new user, send a `POST` request to `/auth/register` with the necessary user registration details. This uses `IdentityUser` and `UserManager`.
- To log in, send a `POST` request to `/auth/login` with the user's credentials. This uses `IdentityUser` and `UserManager`.

### Book Management

- To retrieve a book by its title, send a `GET` request to `/api/Book/{title}`.
- To retrieve a book by its ID, send a `GET` request to `/api/Book/{id}`.
- To retrieve all books, send a `GET` request to `/api/Book/all`.
- To create a new book, send a `POST` request to `/api/Book`. Requires authorization using JWT based on roles (Administrator or Manager).
- To update an existing book, send a `PUT` request to `/api/Book`. Requires authorization using JWT based on roles (Administrator or Manager).
- To delete a book by its ID, send a `DELETE` request to `/api/Book/{id}`. Requires authorization using JWT based on roles (Manager).

## Response Format

All endpoints return responses in the standard response format, including status codes and appropriate messages. The responses may contain data specific to the operation, such as book details or user information.

## Authentication and Authorization

- User authentication is handled using `IdentityUser` and `UserManager`.
- JWT is used for authorization, with role-based access control:
  - `Administrator` role grants access to CreateBook and UpdateBook endpoints.
  - `Manager` role grants access to DeleteBook endpoint.

## Testing

The project can be tested using various testing frameworks, such as xUnit, Moq, and other testing tools to ensure the functionality of the controllers and associated services.

## Dependencies

- `MediatR`: Used for implementing the Mediator pattern for handling requests and commands.
- `Mapster`: Utilized for mapping between different types, e.g., mapping request DTOs to command/query objects.
- `Microsoft.AspNetCore.Mvc`: The MVC framework for building web APIs.
- `Domain.Abstractions`: Contains the abstractions for the domain, including repositories and other common interfaces.
- `Domain.Entities`: Defines the entity models for the application.
- `Application.Authentication.Queries.LoginUser`: Handles user login queries.
- `Application.Authentication.Commands.RegisterUser`: Handles user registration commands.
- `Application.Books`: Contains queries, commands, and handlers related to book management.
- `Domain.Core`: Contains common domain-related functionalities and constants.
- `Domain.Core.Constants`: Contains various constant values used in the application.
- `Domain.Core.Enum`: Contains common enumerations used in the application.
- `Microsoft.AspNetCore.Http`: Provides types for working with HTTP requests and responses.

## Architecture and Design

This project follows the Clean Architecture principles with the CQRS (Command Query Responsibility Segregation) pattern.

## Build and Run

To build and run the application, ensure you have the necessary dependencies installed and configured. Run the application in your preferred development environment and make API requests using appropriate tools such as Postman or cURL.

## Contributors
- [Gerardo Camou](http://gcamou.io)

## License

This project is licensed under the [MIT License](LICENSE).
# ballastlane_bookstore_cleanarchitecture
