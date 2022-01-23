######Pokemon API

This is a demo of a Web API

This demo uses InMemory database, so no need to generate databases with codefirst
The seed data is 4 public pokemon added when the model is created
The authentication is implemented manually using JWT tokens

##Instructions:

This solution uses .Net framework 6, you need Visual Studio 2022 to open the solution
Only need to run the solution and the API runs automatically with Swagger UI
You can add bearer tokens in swagger
You also can use Postman, this collection has all the required methods to test the demo https://www.getpostman.com/collections/942a2717be931b140508

##Steps to test the demo.

Use the endpoint register with an email and password to add a new user to the database
With the password and email of the new user use the endpoint login to generate an authentication token
With the token test any of the other endpoints, to sear user with the email o to create or query pokemon