# BudgetApp
#### A Simple Budgeting Visualization Tool

## Overview

This is my Code:You capstone project. This app allows a user to add transactions to their profile in order to see a break down of their spending habits. The goal of the project is to demonstrate a basic knowledge of C#, API creation, Object Oriented Programming principals, and general software development concepts.

## Technologies Utilized in the project

  | Technolgy        | Description                           |
  |----------------|---------------------------------------|
  | .NET Core MVC | Front end UI was made using Razor Views |
  | SyncFusion | Component library that does the heavy lifting of my data visualizations |
  | Entity Framework Core | The ORM that manages database interactions; uses context to persist data, which helps with the views |
  | BootStrap | All pages are built with a responsive design in mind |
  | SQL lite | The database with the preliminary info |

## Getting Started

To run this project, follow these steps:

1. Ensure that you have .NET 8.0 installed: `https://dotnet.microsoft.com/en-us/download/dotnet/8.0`
1. Ensure that you have the Entity Framework tools installed: `dotnet tool install --global dotnet-ef` 
1. Clone the repository: `https://github.com/CJMiller17/SimpleBudgetApp.git`
1. Update packages/dependencies: `dotnet restore`
1. Get the database data from SQLlite files: `dotnet ef database update`
1. Build and Run the app. Either through your IDE or by: `dotnet build` `dotnet run`


   
## Database

All data for the system including inventory, part types, service history, and vehicle information will be stored in a SQL database named DatabaseName.

## Dependencies

1. NodaTime - Nuget package for handling time zones
1. AutoPartsDataSolutions API - Catalog of manufacturers and auto parts
