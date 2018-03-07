# Introduction
C-Access is a secure portal used by campaigns for local government offices in the City of New York in order to track their campaign finance compliance activities with the New York City Campaign Finance Board. It is a project that I created for the New York City Campaign Finance Board and worked on as the sole developer from 2007 through 2015, from inception. The initial version launched in 2008 and throughout the years has seen several incremental enhancements, additions, and rearchitecturing.

# Architecture
The solution has been implemented with an n-tier, service oriented architecture. The primary layers are comprised of a database access layer, a business object layer, and a front-end layer.

## Front-End
The front-end is divided into three primary projects/entrypoints:
1. **Cfb.CandidatePortal.Web.WebApplication** - an ASP.NET webforms version of the web site, which is what the site was originally created as and what continues to be used in production today.
2. **Cfb.CandidatePortal.Web.MvcApplication** - a proof-of-concept port of the ASP.NET webforms version to ASP.NET MVC 4.5 and Bootstrap 3 that I completed over the course of a month in the spring of 2016.
3. **Cfb.Applications.Camp** - a Winforms application used internally by staff to perform administrative tasks for the site such as: generate messages to campaigns, manage users, run reports, etc.

## Business Objects
Throughout the application I try to reuse as much possible a common library of business object definitions, which are added as project references to web application projects, WCF service projects, WCF client proxy projects, etc. As a result, much care and consideration was taken to eliminate any dependencies within it on any non-business-object projects. This effort is what initiated my foray into the world of Inversion of Control and Dependency Injection, and the advantages of designing by contract.

## Data Access
The project originally began with ADO.NET strongly typed datasets as the means of communicating with the data persistence store. This approach was preserved for the most part throughout the lifetime of the project for trivial read-only retrieval of data from a preexisting data warehouse since there was little advantage gained by replacing it, but eventually some newer requirements arose that involved CRUS operations against data originating from the project itself. As these would benefit from and take advantage of a more robust and mature Object Relational Mapping framework, they were implemented using Entity Framework 4.

## Service Layer
An infrastructure upgrade to the system that was performed a year after initial launch was the refactoring and redesign of the Data Access Layer from a tightly coupled direct project reference dependency to run as a separate process behind a Windows Communication Foundation service layer. Later on, this design allowed newer applications that would also interact with the same databases to leverage existing code without duplication via a single process that was solely responsible for data access duties. A future initiative is to migrate this to a RESTful Web API version, which should simplify consumption of the data without having to deal with WCF client proxies, bindings, limits, etc.

## Single-Sign-On
Around 2012, plans were made to leverage the C-Access user authentication/authorization process for another web application that was being launched, as it targeted the same user base. As a result, I extracted the code involved in user authentication into its own project and threw a WCF service wrapper around it, refactoring the site's existing code to interact with the WCF service instead of directly invoking those security classes and methods. In addition, I added support for connecting to partner sites based on a design document from Microsoft. This allowed it to provide authentication not only for this site, but also other web applications that would launch later on, using a single authoritative source of user credentials.
