## How we'd like to receive the solution?

1. Clone this repository and upload it as a new public repository in your **GitHub** account
2. Create a new branch in your repository
3. Create a pull request with the requested functionality to the unchanged master branch in your repository
4. Share the link to the PR with us 

# Description
This solution has 2 different projects in it.

- API project which is located in the root directory
- IntegrationTest project which is in the **Test** directory

# Tasks

1. Create an Entity called **User** which contains below properties : 
    
    - FirstName
    - LastName ( It's mandatory)
    - NationalCode (It should be a valid national code)  
    - PhoneNumber  (It should be a valid Phone number)   
2. Connect the project to a database (SQLServer, PostgreSQL, ...)
3. Create **UserController** and add two endpoints for :
   
    - **Creating** a user
    - **Update** a user
    - **Get** the information of a user 

4. Add **IntegrationTest** for above endpoints

**Note**: You must apply validation rules related to the entity.

### What we are looking for

- Ability to Use git version control
- Create a new or modify existing web API and containerize it by using Docker. (Add Services, Use Middlewares, Routings, Validation)
- Persist and retrieve relational data with Entity Framework Core and/or Dapper
- Add Swagger to ensure you have a way to document your API.
- How you test the correctness of your solution, and the Integration tests.