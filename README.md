# Math App in MVC with SQL DB, Singleton, Factory and FirebaseAuth

The purpose of this repo is to outline the steps needed to build a dotnet app that interacts with a SQL DB for basic read/write functionality.

## Basic Features
* User can enter two numbers, select an option, and click calculate. Once calculated, the result is to be shown to the user and written to the SQL DB
* User can review previous calculations stored in the DB (history)
* User can clear previous calculations stored in the DB
* Each user has their own history
* Authentication with logging of errors
* Ensuring that divide by 0 does not happen

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
1. [Adding Firebase Auth to the project](/Guides/AddingAuth.md)
1. [Building the Register](/Guides/AddingRegister.md)
1. [Building the Login](/Guides/AddingLogin.md)
1. [Building the Logout](/Guides/AddingLogout.md)
1. [Checking for session variables to confirm login](/Guides/CheckingSessions.md)
1. Amending the DB to accept UUID as FK
1. Amending the code to cater for calculations per user
1. Amending customising the menu buttons
1. Ensuring no divide by 0 using a Factory Design Pattern
1. Amending customizing the menu buttons
1. Introducing logging to a log file with Singleton Design Pattern for unsuccessful logins
