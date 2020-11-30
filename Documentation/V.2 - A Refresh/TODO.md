# You've got a lot of shit to do, boy. Better keep going

(DONE) Rethink the naming of the methods
(DONE) Refactor the DoesCameraExist to use id for edit, not model & brand
(DONE) How to check for existing camera without id -> Use model as unique property
(DONE) Refactor Edit() and Delete() to accept an Id and function only on it
(DONE) Hide the Id in a property in the view
(DONE) Rewrite the documentation for better explanation and future use
(DONE) Fix the Views that display the Id. Unneeded and unnecessary
(DONE) Make one unified Errors page and redirect to it for errors with direct path
(DONE) Harden the validations before and after the database
(DONE) Fix the Error path-ing and model creation for errors on Register and Login
(DONE) Finish Login func
(DONE) Display properly Name in _Layout
(DONE) Implement proper Logout
(DONE) Fix bugs with staying logged in
(DONE) Make "login only" exclusive features
(UNNECESSARY) Look to potentially swap the database provider, if necessary
(DONE) Harden and expand the EntityFrameworkCore knowledge
(DONE) Make a new table for Brand
(DONE) Fix the broken model given when editing a Camera
(DONE) CRUD for Users
(DONE) CRUD for Brand
(DONE) Move all logic from AccountController to AccountService
(DONE) Add try-catch in BrandController
(DONE) Add validations for null in Update and Delete in BrandService and CatalogService
(DONE) Harden Get and Set validations in Brand class
(REVERTED) Make Brand's Id string from int
(DONE) Create an entity repository and move the DB interaction in it
(DONE) Migrate to ASYNC methods for every operation
(DONE) Separate Data and Core into 2 different projects, lightly loosen from one another
(DONE) Look into implementing login and password(hash the password, at least)
(DONE) Configure for HTTPS instead of HTTP
(DONE) Make the entire infrastucture async(starting from the Controllers)
(DONE) Fix the Error View to display when having Exception
(DONE) Fix empty Exception getting to Error View 
(DONE) Fix Error() method not binding to Error View
(DONE) Make global exceptions catcher (GlobalExceptionHandler)
(DONE) Fix "InvalidOperationException" upon viewing Profile
(DONE) Refactor EntityRepository to be async
(DONE) Add AsNoTracking() to Read-Only queries to improve performance
(DONE) Fix UpdateAsync() in CatalogServices.cs
(DONE) Refactor Edit method in EntityRepository
(DONE) Change Create() permissions to only logged in users
(DONE) Create a ViewCamera() to hide Edit() and Create()
(Done) Create a ViewBrand() to hide Edit() and Create()
(DONE) Edit ViewBrand.cshtml just like ViewCamera.cshtml
(NOT POSSIBLE) Catch exceptions from "No connection to SQL Server"
Implement Roles for users
Implement Policyies
Implement FindByIdAsync() in EntityRepository.cs
Implement the "Guide" controller and simple view
Write Unit tests(functionality and hardened features are priority)
Construct and layout the "Guide" section
Change AccountService's UserExists() & GetUser() to async queries
Fix "Brand's Cameras" showing "None"
Fix login error not showing
Fix Layout Page now showing username correctly after change
Hide the Id from the url with "Hide Id"(by Ivo's words) via the route customization(prob)
Rewrite all CRUD Views to be dynamic
Make diplaying properties dynamic(exclude id!), so that you can expand easily
Expand the Camera class to have more functionality, better attributes and more properties
Use Blazor instead of JavaScript

## Optional

Look into the ViewBag way of passing information to the Views

Notes:
ViewBag, ViewData to fill with data and use in the view
