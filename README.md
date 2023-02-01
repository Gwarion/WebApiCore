# About this repo

This is a proof of concept for a .NET 6 WebApi using 
* CQRS with a Mediatr pipeline
* DDD and Hexagonal architecture : every adapter is loaded through reflection at runtime
* Proper exception handling middleware
* EF Core 6.0 for the data mapping and database migrations
* Kafka Event production
* Runnable from a docker container

# To Start the WebApi

### Using Docker (easy way)
* Install the latest version of [Docker for Windows](https://docs.docker.com/desktop/install/windows-install/)
* Install the [update package  WSL2 for x64 machines](https://learn.microsoft.com/en-us/windows/wsl/install-manual#step-4---download-the-linux-kernel-update-package)
* Set docker-compose as startup project
* Rebuild and run Docker Compose
* Wait for images to load (first time can be super long)
* API is running on `localhost:55000`
* Kafka (lenses.io) is running on `localhost:3030` (you can observe ConsumerCreated and ConsumerUpdated events in the Consumer topic after executing a request)
* SQL Server is running on `localhost, 1433` - logins are set in the docker-compose.yml file. It uses the default System Database "master"

### Using the default WebApi Project (hard way)
* /!\ Kafka currently does not work without the docker container
* Install the latest version of [SQL Server Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* Install the latest version of [SQL Server Management Studio](https://learn.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver16)
* With SSMS 
  * Enable SQL Server Authentication on the SQLExpress instance
  * Enable the user "sa" and set a password.
* With SQL Server Configuration Manager
  * Open SQL Server Network Configuration > Protocols for SQLEXPRESS
  * Enable Shared Memory / Named Pipes and TCP\IP protocols
* Reflect the new database connection string in the PlaceHolder.DependencyInjection.Options.DatabaseOptions class and launchsettings.json
* Rebuild the whole solution and start the application from the PlaceHolder configuration

# To send a request 

* Using [Postman](https://www.postman.com)
* [Postman Collection](https://drive.google.com/file/d/1xzFUkgNZuJxU9dS3EDygmYdlfTc3ErB3/view?usp=share_link)
