# Virtual Wallet Platform

## Summary
Virtual Wallet Platform, illustrating development with C#, ASP.NET Framework, and IIS Express on MySQL Database. It also illustrates open-api documentation configuration and integration with Swagger UI.

## Features
- RESTful API
- OpenAPI Documentation
- Swagger UI
- ASP.NET Framework
- IIS Express
- Validation
- MySQL Database
- Domain-Driven Design

## Bounded Contexts
This version of ACME Learning Center Platform is divided into two bounded contexts: Bills and IAM.

### Bills Context

The Profiles Context is responsible for managing the bills of the users. It includes the following features:

- Create a new bill.
- Delete a bill.
- Update a bill.
- Get a bill by id.
- Get all bills by user.

This context includes also an anti-corruption layer to communicate with the IAM Context. The anti-corruption layer is responsible for managing the communication between the Bills Context and the IAM Context. It offers the following capabilities to other bounded contexts:
- Create a new Bill, returning ID of the created Bill on success.
- Get a Bill by UserId, returning the associated Bill ID on success.

### Identity and Access Management (IAM) Context

The IAM Context is responsible for managing platform users, including the sign in and sign up processes. It applies JSON Web Token based authorization and Password hashing. It also adds a request authorization middleware to .NET Pipeline, in order to validate included token in request header on endpoints that require authorization. Its capabilities include:
- Create a new User (Sign Up).
- Authenticate a User (Sign In).
- Get a User by ID.
- Get All Users.
- Use security features to implement an authorization pipeline based on request filtering.
- Generate and validate JSON Web Tokens.
- Apply Password hashing.

This context includes also an anti-corruption layer. The anti-corruption layer is responsible for managing the communication between the IAM Context and other bounded Contexts. Its capabilities include:

- Create a new User, returning ID of the created User on success. If roles are provided, it will also assign them to the user, otherwise the default role is assigned.
- Get a User by ID, returning the associated User ID on success.
- Get a User by Username, returning the associated User ID on success.

- In this version, Open API documentation includes support for JSON Web Token based authorization.
