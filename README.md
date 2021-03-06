## Alza Products Service
Rest API service providing all available products of an eshop and ennabling partial update of one product.

##### API documentation (Swagger)
API documentation is available at:
```
https://localhost:5001/swagger
```

### Run local with CLI
1. Clone or download this repository to local machine.
2. Install [.NET Core SDK for your platform](https://www.microsoft.com/net/core#windowscmd) and [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-2019) if didn't install yet.
3. `cd ProductsService`
4. Update appsettings.json (alter Connection string) - see more below
5. `dotnet restore`
6. `dotnet run`

### Run tests
1. `dotnet test`

### Create Connection string
1. Update file ProductsService/appsettings.json with right settings(host/login/password and etc.) 
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AlzaProducts;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

