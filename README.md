# About this repo

This is a proof of concept for a .NET 8 Api using 

### Architecture

* CQRS
* DDD and Hexagonal Architecture : "Plug and Play" Driven Adapters are loaded at runtime from config, through reflection
* BDD
* Kafka Event production
* Out of the box from a docker container

### FrameWorks and Tools

- Mediatr, AutoMapper, SpecFlow (xUnit)
- A Background Service to dispatch asynchronous commands
- An Exception Handling Middleware

# To Start the WebApi

### Using Docker
* Install the latest version of [Docker for Windows](https://docs.docker.com/desktop/install/windows-install/)
* Install the [update package  WSL2 for x64 machines](https://learn.microsoft.com/en-us/windows/wsl/install-manual#step-4---download-the-linux-kernel-update-package)
* Set docker-compose as startup project
* Rebuild and run Docker Compose
* Wait for images to load (first time will be super long)
* API is running on `localhost:55000`
* Kafka (lenses.io) is running on `localhost:3030` (you can observe ConsumerCreated and ConsumerUpdated events in the Consumer topic after executing the related request)
* SQL Server is running on `localhost, 1433` - logins are set in the docker-compose.yml file. It uses the default System Database "master"

# To send a request 

* Using [Postman](https://www.postman.com)
* [Postman Collection](https://github.com/Gwarion/WebApiCore/blob/master/WebApiCore.postman_collection.json)
