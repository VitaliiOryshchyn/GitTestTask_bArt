# Task for bART

It's Web Api project, where .Net Core 6, EF Core and MS Sql Server are used.

## How launch this project

Clone repository on your local PC

Open configuration file appsettings.json in Task_bArt and change connection string

```txt
  "DefaultConnection": "Set you'r connection string"
```

Then use bash in Package Manager Console

```bash
  Update-Database
```

## What do you need to start testing app

This project uses Swagger, you can use Postman or another Http-client

Before starting to work, you need to add data in DB
I add controller for adding initial data, and you don't need use external program for adding data

```1) First of all, use method POST api/Account/InitData for adding data in DB```

```2) To add contact use method POST api/Contact ```

```3) To add account use method POST api/Account/{incidentName}```

```4) To add incident use method POST api/Incident```

```5) Also, you can change information using method PUT api/{controller}```

```6) And finaly, you can check information about incident, contact or account, using method GET api/{controller} or api/{controller}/{query parameter}```