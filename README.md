# Real Estate Management (DIS 2020) - VSISP38
[See project and Readme.md on GitHub](https://github.com/nionzion/REM) 

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
## 1.2 DB-schema
- The commands for creating the DB objects are contained in SQL scripts.
> The scripts are located at **../REM/script/create_script.sql**
- Choose an inheritance model (e.g. horizontal partitioning) 
> We chose the the horizontal partitioning model. More on that is described in the further exercises.
-  Define a primary key for every relation (surrogate keys are ok). Define foreign keys.
> Each table entity has its own surrogate key which is the primary key. The tables **house** and **apartment** refer to a foreign key of the **agentId** and the **tenancyContract** and the **purchaseContract** refer to the foreign keys of **person** and the corresponding estate (**house** or **appartment**).
-  Initialize the tables with appropriate sample data. There should for instance be an estate agent account you can use to log in. 
> We provided data for each table, you can find the INSERT-statements in the script file.
- Create the tables using DBeaver or the CLI
> DBeaver.

## 1.3 Java Application (written in C# not Java)
### Overall Approach
When you run the application and set the required database parameters, a maximized window will show up and it is divided into a navigation part on the left (the **SideMenu**) and a host, that provides the actual mode you choose from the SideMenu. 

- **Estate Agents:** Management mode for estate agents (account creation, changes and deletions)
- **Estates:** Management mode for estates with agent login and CRUD-commands 
- **Contracts:** Creation of persons and contracts
- *Database Configuration: If you decide to change the database parameters*

We decided to build a GUI for the application, that displays data in a more efficient way than the console and the forms are easier to fill for populating the database with data.  The SideMenu provides good structure for the different modes.
#### Estate Agents
The agents account management is *protected* by a hard-coded password. After submitting the master password, you reach the agents account management. Here you can see all registered agents, you can create new accounts and you can also edit them. 

These methods provide the database functionality for all agents operations:
```Java
#region Agents (in DbAccess.cs)
***********************************
 public List<EstateAgent> FetchAgents() 
 public bool CreateAgent(EstateAgent agent)
 public bool DeleteAgent(EstateAgent agent)
 public bool UpdateAgent(EstateAgent agent, string key)
```
#### Estates
The estates management is accessible with a valid estate agent login and password. After submitting your credentials, you reach a screen which wraps up all estates that are stored in the database. The View contains both, **houses** and **apartments** which are visualized by specific views that show the type-specific estate data. You can change, delete and add new estates. Each estate is managed by an agent, as it's described by the 
[1;1]-relationship in the domain model. The other way round, an agent does not need to manage an estate, and aswell can manage any number of estates.
These methods provide the database functionality for all estate operations:
```Java
#region Estates (in DbAccess.cs)
***********************************
 public List<IEstate> FetchEstates()
 public bool CreateApartment(Apartment apartment)
 public bool UpdateApartment(Apartment apartment, string key)
 public bool DeleteApartment(Apartment apartment)
 public bool CreateHouse(House house)
 public bool UpdateHouse(House house, string key)
 public bool DeleteHouse(House house)
```
#### Contracts and Persons
The contracts signing leads you to a screen which wraps up all persons and contracts that are stored in the database. The View contains both, **Tenancy** and **Purchase** which are visualized by specific views that show the type-specific contract data. You can add new persons and contracts.
Each contract holds a person who purchased or rents a referenced estate. Since an estate can only appear once in a contract, we decided to set the referencing ID of the estate within the contract tables to be unique. Probably there are several different ways to achieve this behaviour and prevention, but this fairly accomplished the goal. 

These methods provide the database functionality for all contracts operations:
```Java
#region Persons (in DbAccess.cs)
***********************************
 public List<Person> FetchPersons()
 public bool CreatePerson(Person person)
```
```Java
#region Contracts (in DbAccess.cs)
***********************************
 public List<IContract> FetchContracts()
 public bool CreateTenancyContract(TenancyContract contract)
 public bool CreatePurchaseContract(PurchaseContract contract)
```
#### Additional Questions
- Create a contract with a non-existing estate. Does it work? Why/Why not?
> As we are already providing the existing estates, it's not possible to create such a contract. But if we would enter the ID of an non-existing estate manually, the database would respond with an error message, that says that the entered ID does not exist in the referenced table which is a constraint violation.
- Which inheritance model did you choose and why? 
> We chose the horizontal inheritance. The table **Estate** is still declared as abstract, but not used in the database. The horizontal model makes it easier to perform queries without complex table joins. The application does still use a vertical approach with the interface *IEstate.cs*, where both subclasses (house and apartment) inherit from. The same applies to the contract tables.   
- Create an apartment, and let your application crash between inserting the estate information and inserting the apartment information. What is the effect on your database state?
> Could not be reproduced, because the transaction is controlled by one insert in our application.
