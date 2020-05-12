# Real Estate Management (DIS 2020) - VSISP38 

This report describes the following content for Sheet 2 of the DIS 2020 course:
- Prerequisites
- Configuration
- Sheet report


## Prerequisites
The application is written in C# with .NET Framework 4.6.1. Before you can run the application you need to install **[Visual Studio](https://visualstudio.microsoft.com/de/thank-you-downloading-visual-studio/?sku=Community&rel=16)** and **[.NET Framework 4.6.1](https://dotnet.microsoft.com/download/visual-studio-sdks?utm_source=getdotnetsdk&utm_medium=referral)** (or later). 
 > If you have already installed the required prerequisites, you can open the **REM.sln** - Solution File which is in the root path of the project directory.


## Database Configuration
**1.** Create a PostgreSQL database on your local machine (e.g. with DBeaver) and add a schema to the database. The name of the schema requires to be **vsisp38** (!) to ensure that it works appropriately with the SQL-statements that are used by the application.
> After that you can run the predefined SQL-script, which is contained in **../REM/script/create_script.sql** of the project directory.

**2.** After running the application from within Visual Studio (with F5-Key), you are prompted to provide the database connection parameters, that are stored in a local .json-File within the bin/ folder of the project directory. Please provide all parameters that are requested. 
> Example: 
	- Server: localhost
	- Port: 5432
	- Database: dis2020
	- User: postgres
	- Password: root 

# Report and exercises
