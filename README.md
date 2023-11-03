# BuildingLink_Driver

## Overview

The BuildingLink_Driver solution consists of three projects: BuildingLink_Driver, DriversCLI, and DriverUnitTest. Each project serves a distinct purpose within the system.

This project utilizes SQLite with SQLite-specific data access and contains various methods with logging and documentation above each method for easy reference.

## Getting Started

To get started with the project:

1. Clone the repository to your local machine.
2. Open the solution in your preferred IDE.
3. Ensure that the Swagger page opens automatically upon starting the startup project.

## Configuration

- The base URL for the CLI project can be modified in the `appsettings.json` file of the CLI project. Update the `ApiBaseUrl` to the desired URL.

## Usage

Detailed instructions on how to use the CLI project, including available commands and their usage, can be found on the console once the project is run.

## Documentation

The project is well-documented, and you can find detailed information about each method in the source code comments.

## Project Structure

### BuildingLink_Driver

The BuildingLink_Driver project is the core of the system and is organized into three layers: controllers, services, and data.

#### Controllers

- **DriversController**: Responsible for handling all CRUD (Create, Read, Update, Delete) operations related to drivers.
- **FakeController**: Manages the creation of fake/random drivers and alphabetizes driver names.

#### Services

- **DriverService**: Connects the controllers to the database, handling CRUD operations and any necessary business logic related to drivers.
- **HelperService**: Provides functionalities for creating fake drivers and alphabetizing driver names.

#### Data

- **DriversRepository**: Implements IDriversRepository and is responsible for interacting with the database, performing CRUD operations.
- **DatabaseUtils**: Takes care of database-related tasks, such as creating the drivers table if it doesn't exist and adding seed data.

### DriversCLI

The DriversCLI project provides a Command Line Interface (CLI) for testing the endpoints of the BuildingLink_Driver project using CLI commands. It allows easy interaction with the API endpoints and serves as a testing tool.

### DriverUnitTest

The DriverUnitTest project contains unit tests for the BuildingLink_Driver project. It ensures the reliability and correctness of the code by testing individual components and functions.
