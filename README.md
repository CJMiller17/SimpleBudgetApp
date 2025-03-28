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

## Features available in the project

  | Feature        | Description                           |
  |----------------|---------------------------------------|
  | Users | You can select from 5 user profiles to use |
  | Dashboard | Breaks down spending habits and information visually with 4 different widgets |
  | Categories | Any user can add a new category which will be available to all other users. Any category can be edited |
  | Transactions | A user can add their own transactions and they will only be tied to their profile. It will be factored into the 4 widgets. It can also be edited |
  | Sidebar | Navigation panel that can be collapsed to make more room for the info on the dashboard |
  | Breadcrumbs | Convenient way to navigate through the app |
   
## Database

Built on SQL lite. Very convenient for a small app, as it's not complicated and doesn't require hosting. It's file based and perfect for a project like this.

## Code:You Capstone Requirements Met

- [x] bz;dlfgjnsdlkjbn
- [x] bz;dlfgjnsdlkjbn
- [x] bz;dlfgjnsdlkjbn
- [x] bz;dlfgjnsdlkjbn
- [x] bz;dlfgjnsdlkjbn
- [x] bz;dlfgjnsdlkjbn
