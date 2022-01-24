# Pokemon API

This demo uses InMemory database, no need to generate databases with codefirst.

The seed data is 4 public pokemon added when the model is created.

The authentication is implemented manually using JWT tokens.

## Instructions to run the Api:

- This solution uses .Net 6 so you need Visual Studio 2022.

- To execute the Api just run the solution and you will see a Swagger UI.

## To test the demo.

- You can use Postman, this collection has all the required methods to test the demo 
    - Local: https://www.getpostman.com/collections/942a2717be931b140508
    - Azure: https://www.getpostman.com/collections/cd84780cce8a60800ab3

- The Api is deployed in Azure: https://pokemonapidemo.azurewebsites.net/swagger

- Use the endpoint `register` with an email and password to add a new users to the database.

- With the password and email of the new added user use the endpoint `login` to generate an authentication bearer token.

- With the bearer token test any of the other endpoints, you can search users and create or query pokemons (See the postman collection).
