# Math App in MVC with SQL DB

The purpose of this repo is to outline the steps needed to build a dotnet app which interacts with a SQL DB for basic read/write functionality.

## Basic Features
* User can enter two numbers, select an option and click calculate. Once calculated, the result is to be shown to the user and written to the SQL DB
* User can review previous calculations stored in the DB (history)
* User can clear previous calculations stored in the DB

## Pre-Requisites
* VS or VS Code with Dotnet 8.0
* MS SQL Server installed
* A browser to run everything
* It is recommended that you update your Visual Studio using the VS Installer

## Steps

It is highly recommended that you follow these steps in order:
1. [Setting up the DB](/Guides/SettingUpDB.md)
1. [Building the DB Context in MVC](/Guides/BuildingDBContext.md)
1. [Building the Calculate Functionality](/Guides/BuildingCalculate.md)
1. [Building the History Functionality](/Guides/BuildingHistory.md)
1. Adding Firebase Auth to the project
1. Building the Login
1. Building the Register
1. Building the Logout
1. Amending the DB to accept UUID as FK
1. Amending the code to cater for calculations per user
1. Amending customising the menu buttons