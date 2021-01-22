# test-fw-3
**FW C#/.NET test project**

This project is based on the title subject; it uses the followin
1. ORM Pattern (EF Core)
- SQLite support
- Lazy proxy support
2. Object to object mapping (Automapper)
- Expression mapping
3. Logging (Serilog)
4. Swagger RESTful API documentation

**BUILD & RUN**
Use Visual Studio 2019, build & run. The default URL is for Swagger UI.
If complains about "port is in use", go to launchSettings.json, under Properties, and change URL parameters

This project is ready for full DI deploy with post build task in main API project
There are some tests in place

The data seeding is all in OnModelCreating method but really should be moved away from that.

The data is pseudo randomized in some place, in an ugly way but it should be enough for a test.

There was no time to use a powerful IoC for DI. Castle Windsor would be my choose.

There is also support for pagination of results in some calls.

 No nice API documentation, only the automatic one.

 Both LINQ and Expression mapping examples in the code, in different method.

 This is a little example of how I think a project should be.

 TODO:
 - Add full API model validations
 - Convert MovieDTO.AverageRating to LINQ expression.
 - Complete response from API (error response)
 - Find a better solution for a pattern updare||insert