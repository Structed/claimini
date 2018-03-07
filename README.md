# README #



### How do I get set up? ###

#### Future topics to be documented:
* Configuration
* Dependencies
* Database configuration
* How to run tests
* Deployment instructions


## Azure Cosmos Db local emulator set-up

  1. Install [Azure CosmosDb Emulator](https://aka.ms/cosmosdb-emulator) - [Doumentation](https://docs.microsoft.com/en-us/azure/cosmos-db/local-emulator).
    * Optionally [use a local or container setup](https://docs.mongodb.com/manual/administration/install-community/) to run MongoDb itself.
  2. Use following default connection string to connect to cosmos db emulator.
        ```
        mongodb://localhost:C2y6yDjf5%2FR%2Bob0N8A7Cgv30VRDJIWEHLM%2B4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw%2FJw%3D%3D@localhost:10250/admin?readPreference=primary&ssl=true
        ```
  3. If you want to connect to Azure Cosmos Db Emulator using a UI like [Studio 3T](https://studio3t.com/) or [Robo 3T](https://robomongo.org/), please see instructions on [this article](https://dotnetthoughts.net/connecting-to-azure-cosmos-db-emulator-from-robomongo/).
  4. Migrate or import/export data between database can be used following tools:
    * [Azure Cosmos DB: Data migration tool](https://docs.microsoft.com/en-us/azure/cosmos-db/import-data)
    * [Studio 3T](https://studio3t.com/)
