[![CircleCI](https://dl.circleci.com/status-badge/img/circleci/Ptrn8AFjp795m887NQTfma/XTzWpsLoScs3FweyT3VL5G/tree/master.svg?style=svg)](https://dl.circleci.com/status-badge/redirect/circleci/Ptrn8AFjp795m887NQTfma/XTzWpsLoScs3FweyT3VL5G/tree/master)

# Math App in MVC with SQL DB, Singleton, Factory and FirebaseAuth

The purpose of this repo is to outline the steps needed to build a dotnet app that interacts with a SQL DB for basic read/write functionality.

## Important Note
This repo has been build to assist with understanding various aspects in this course, and I am certain it can be made even better.

**If you notice any errors or need to suggest improvements, please reach out to me!! I will be grateful**

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
1. [Checking for session variables to confirm login status](/Guides/CheckingSessions.md)
1. [Introducing logging to a log file with Singleton Design Pattern for unsuccessful logins](/Guides/AddingLogging.md)
1. [Amending the DB to accept UUID as FK](/Guides/AmendingDBToAcceptUUID.md)
1. [Ensuring no divide by 0 using a Factory Design Pattern](/Guides/NoDivideZeroFactory.md)
1. [Amending the code to cater for calculations per user](/Guides/AmendingCodeToCalcPerUser.md)
1. [Amending the menu buttons for our auth](/Guides/CustomizingMenuForAuth.md)

CI/CD
Videos
* Part 1: https://youtu.be/-ufFSHOCLNw
* Part 2: https://youtu.be/5LOL2ISmfLc


