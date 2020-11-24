# You've got a lot of shit to do, boy. Better keep going

(DONE) Rethink the naming of the methods
(DONE) Refactor the DoesCameraExist to use id for edit, not model & brand
(DONE) How to check for existing camera without id -> Use model as unique property
(DONE) Refactor Edit() and Delete() to accept an Id and function only on it
(DONE) Hide the Id in a property in the view
(NOT NOW) Hide the Id from the url with "Hide Id"(by Ivo's words) via the route customization(prob)
(NOT NOW) Make diplaying properties dynamic(exclude id!), so that you can expand easily
(NOT NOW) Rewrite all CRUD Views to be dynamic
(DONE)Rewrite the documentation for better explanation and future use
(DONE) Fix the Views that display the Id. Unneeded and unnecessary
(NOT NOW) Expand the Camera class to have more functionality, better attributes and more properties
(DONE)Make one unified Errors page and redirect to it for errors with direct path
(DONE)Harden the validations before and after the database
(DONE)Fix the Error path-ing and model creation for errors on Register and Login
(DONE)Finish Login func
(DONE)Display properly Name in _Layout
(DONE)Implement proper Logout
(DONE)Fix bugs with staying logged in
(DONE)Make "login only" exclusive features
(UNNECESSARY)Look to potentially swap the database provider, if necessary
(DONE)Harden and expand the EntityFrameworkCore knowledge
(NOT NOW)Try to migrate to ASYNC methods for every operation
(DONE)Make a new table for Brand
(DONE)Fix the broken model given when editing a Camera
(DONE)CRUD for Users
(DONE)CRUD for Brand
(FAILED)Rewrite the Update func in the CatalogController to use the DbContext.Update()
(DONE)Move all logic from AccountController to AccountService
(DONE)Add try-catch in BrandController
(DONE)Add validations for null in Update and Delete in BrandService and CatalogService
(DONE)Harden Get and Set validations in Brand class
(REVERTED)Make Brand's Id string from int
Create a new repository and move the DB interaction in it
Find how to update global view after username change
Configure for HTTPS instead of HTTP
Construct and layout the "Guide" section
Implement the "Guide" controller and simple view
Use Blazor instead of JavaScript
(DONE)Look into implementing login and password(hash the password, at least)
(DONE)If enough time left, write only important Unit tests(functionality and hardened features are priority)

## Optional

Look into GlobalExceptionHandler
Look into the ViewBag way of passing information to the Views
Separate Data, Business and View into 3 different projects, lightly loosen from one another

Notes:
ViewBag, ViewData to fill with data and use in the view
