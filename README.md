
## Simple Process management

I have used C# asp.net core web-api to build this api. I have designed this api to store the logs in-memory. And for that, I have used dictionary where key is the processID & value is the processModel object. Also, utilized async-locking for multiple request handling using semaphore-slim. And for resource optimization, utilized cancellationToken everywhere.


### Additional Features

- Swagger Enabled
- CORS Policy Included
- API Versioning Included
- Global Exceptions Handled with Custom Middleware


## To Run Locally

Clone the project

```bash
  git clone https://github.com/masum035/Simple-Process-Management-API.git
```

Go to the project directory

```bash
  cd Simple-Process-Management-API
```

Install dependencies

```bash
  install .NET 8 SDK from the official site
```

Start the server

```bash
  dotnet run
```

For Swagger Visualization, go to this url

```bash
  https://localhost:44324/swagger/index.html
```
