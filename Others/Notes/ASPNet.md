# ASP.NET

ASP.NET Core is a cross-flatform, high-performance, open-source framework for building modern, cloud-enabled, internet-connected apps

Model binding: automatically maps data from HTTP requests to action method parameters

Model Validation: automatically performs client-side and server-side validation.

Advantages of .NET over .NET framework
    • Cross-flatform. Runs on Windows, macOS and Linux.
    • Improved Performance
    • Side-by-side versioning
    • New APIs
    • Open Source

Routing: Routing is responsible for matching incoming HTTP request and dispatching those requests to the app's executable endpoints

Endpoint: Endpoints are app's units of executable request-handling code. They are defined in app and configured when app starts

Security:
    • Authentication
    • Authorization
    • Data Protection
    • HTTPS enforcement
    • Safe storage of app secrets in development
    • XSRF/CSRF prevention
    • Cross Origin Resource Sharing (CORS)
    • Cross-Site Scripting (XSS) attacks

API:
ASP.NET Core supports creating web APIs using controllers or minimal APIs.
Controllers in web API are classes the derive from ControllerBase. Controllers are activated and disposed on a per request basis.

A controller based web API consists of one or more controller classes that derive from ControllerBase

ControllerBase class provides many properties and method that are useful for handling HTTP requests

Method | Notes
---|---
BadRequest | Returns 400 Status Code
NotFound | Returns 404 Status Code
Ok |Returns 200 Status Code
CreatedAtAction | Returns 201 Status Code

Format Response Data in ASP.NET Web API
Some Action Result Types are specific to particular format, such as JsonResult and ContentResult.
Action can return results that always use a specified format ignoring a client's request for a different format
JsonResult returns JSON formatted Data and ContentResult returns plain-text-formatted string data

By Default the built-in helper method Ok returns Json-formatted data

## Content Negotiation:

Content Negotiation occurs when the client specifies an Accept header. The default format used by ASP.NET Core is JSON

Content Negotiation is 
    • Implemented by ObjectResult
    • Built into the status code-specific action results returned from the helper methods. The action results helper methods are based on ObjectResult.
When a request contains an accept header, ASP.NET core
    • Enumerates the media types in the accept header in preference order.
    • Tries to find a formatter that can produce a response in one of the formats specified
    • If No formatter is found that can satisfy the client's request ASP.Net Core
        ○ Returns 406 Not Acceptable if MvcOptions.ReturnHttpNotAcceptable is set to true or
        ○ Tries to find the first formatter that can produce a response in one of the formats specified


Handle Errors in ASP.NET Core:

In Non -development environments, use Exception Handling Middleware to produce an error payload

    • App.UseExceptionHandler('/error')
    • Configure a controller action to respond to /error route

Host and deploy:

In general to deploy an ASP.NET core app to a hosting environment
    • Deploy the published app to a folder on the hosting server
    • Set up a process manager that starts the app when requests arrives and restarts the app after it crashes or the server reboots
    • For configuration of a reverse proxy, set up a reverse proxy to forward requests to the app

Publish to a folder:
Dotnet publish command compiles app code and copies the files required to run the app into a publish folder.

Run the Published app locally:
To run the published app locally run dotnet ApplicationName.dll from the publish folder

Set up a process manager:
As ASP.NET core app is a console app that must be started when a server boots and restarted if it crashes.  To automate starts and restarts, a process manager is required.
Most common process managers for ASP.NET Core are
    • Linux
        ○ Nginx
    • Windows
        ○ IIS
        ○ Windows Service

Perform Health Checks:
Use Health Check Middleware to perform health checks on an app and its dependencies.

Cross-Site Scripting (XSS): 
XSS is a security vulnerability that enables an attacker to place client side scripts into web pages.
XSS vulnerabilities occur when application takes user input and outputs it to a page without validating, encoding or escaping it.
Web APIs that return data in the form of HTML, XML or JSON can trigger XSS attacks in their client apps if they don't properly sanitize user input, depending on how much trust the client app places in the API
To prevent XSS attacks, web APIs should implement input validation and output encoding.