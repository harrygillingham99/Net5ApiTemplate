# NetCore31ApiTemplate
An opinionated NSwag/Typescript Client gen friendly .NET Core 3.1 API Template

Includes;
- Scrutor for auto interface registration
- NSwag to generate CSharp and TypeScript Client Libraries using a post-MSBuild step
- An NSwag document processor and `NSwagInclude` attribute to include unreferenced models in the document i.e useful enums
- An NSwag schema processor to include extra information/objects in request headers
- Serilog for logging
- Swagger UI and ReDoc endpoints (at /swagger & /api-docs)
- A generic base class to wrap any results or exceptions in `IActionResult` or just execute and return
- Custom exceptions and resposnes which are consumed and wrapped up in `IActionResult` by the base controller 
