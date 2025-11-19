# Billing app

Step1: Install these three

Tool > NuGet Package Manager > Browse and Search:

Microsoft.EntityFrameWorkCore.Design
Microsoft.EntityFrameWorkCore.SqlServer
Microsoft.EntityFrameWorkCore.Tools

Step2: Create databse and tables in DB
check sql file

Step3: Create Connection String (in appsettings.json):

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "ConnectionStrings": {
    "constr": "Server=DESKTOP-VGTALMH\\SQLEXPRESS;database=billingdb;trusted_connection=true;trustservercertificate=true;"
  },
  "AllowedHosts": "*"
}
```

Step4: ## Scafolding (Generate models using database)

Go to package Manager Console (Tools > Nuget > ... )

PM> scaffold-dbcontext "Server=DESKTOP-VGTALMH\SQLEXPRESS;Database=billingdb;trusted_connection=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -outputdir Models

Step5: 