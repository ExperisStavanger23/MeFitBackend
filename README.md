# MeFitBackend

This is the backend for the MeFit application. It is built using ASP.NET Core and comprise of a database made in SQL Server through EF Core with a RESTful API. The appllication is a part of the Case project for Noroff Accelerate 2023.

### Contributors

The MeFit project have been a collaboration between the following people:

- [Anders Størksen Wiik](https://github.com/andyret26)
- [Minh Christian Tran](https://github.com/Mintra99)
- [Philip Van Ni Thangngat](https://github.com/thangfart)
- [Sjur Gustavsen](https://github.com/GustavsenSj)

## Installation


### Installing the project

```bash
git clone https://github.com/ExperisStavanger23/MeFitBackend.git
cd MeFitBackend
```

in [appsettings.json](/appsettings.json) DefaultConnection replace the `"Server=YourServerName; ..."` with your sql server name 

```dotnet
dotnet restore
dotnet tool install --global dotnet-ef
dotnet ef migrations add init
dotnet ef database update
dotnet run
```

should be running on `http://localhost:5298` (might be different) \
`http://localhost:5298/swagger` for swagger documentation


## Project structure
The project is structured as follows:
```
Controllers
Data
├── DTO
├── Entities
├── Enums
├── Exceptions
Mappers
Services
├── Exercises
├── MuscleGroups
├── Programs
├── Users
├── Workouts
```
