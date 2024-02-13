A simple weather forecasting app.  
Search for your city, add it to your profile and enjoy.   

Build  
	.NET7 MVC Blazor Webb App   
	EF Core   
	SQL Server   

Currently still in development. Microsoft Azure AD is configured for a single tenant (kindlyturnips) login.
To Run  
	1. Requires Docker Desktop & WSL   
	2. Requires Microsoft Azure AD / Microsoft Entra Verified ID (Currently configured for single tenant)   
	3. Check out  
	4. Navigate to app directory in cmd  
	5. docker-compose up -d  
	6. Wait till service_migrations has completed & exited  
	7. In a web browser navigate to https://localhost:8001  
