# About this repo

This is a proof of concept for a .NET 10 Api using 

### Architecture

* CQRS
* DDD and Hexagonal Architecture : "Plug and Play" Driven Adapters are loaded at runtime from config, through reflection
* BDD
* Kafka Event production
* Out of the box from a docker container

### FrameWorks and Tools

- Mediatr, AutoMapper, Reqnroll (xUnit)
- A Background Service to dispatch asynchronous commands
- Middleware implementation
- Api Versionning
- Open Api documentation

# To Start the WebApi

### Install Docker
* Install the latest version of [Docker for Windows](https://docs.docker.com/desktop/install/windows-install/)
* If needed, install the [update package  WSL2 for x64 machines](https://learn.microsoft.com/en-us/windows/wsl/install-manual#step-4---download-the-linux-kernel-update-package)
* If needed,  [enable virtualization on windows](https://support.microsoft.com/en-us/windows/enable-virtualization-on-windows-c5578302-6e43-4b4b-a449-8ced115f58e1)
  
### Run Docker (from VS)
* Set docker-compose as startup project
* Build the solution and run docker-compose
* Wait docker to download the images
* API is running on `localhost:55000`
* Kafka (lenses.io) is running on `localhost:3030` (you can observe ConsumerCreated and ConsumerUpdated events in the Consumer topic after executing the related request)
* SQL Server is running on `localhost, 1433` - logins are set in the docker-compose.yml file. It uses the default System Database "master"

# To send a request 

* Using [Postman](https://www.postman.com)
* [Postman Collection](https://github.com/Gwarion/WebApiCore/blob/master/WebApiCore.postman_collection.json)
