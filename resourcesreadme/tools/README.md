dotnet build

dotnet test

Add new migration called Initial

´´´ bash
dotnet ef --startup-project ../Scheduling.API/ migrations add Initial
´´´



dotnet ef --startup-project ../Scheduling.API/ migrations script -o scriptdatabase.sql -i

