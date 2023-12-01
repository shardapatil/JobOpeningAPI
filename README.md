# JobOpeningAPI
api to manage job openings

## To Run the Projetc:

1. Clone the project.

2. Import the database export file provided under database backup folder.
   database export file name: JobOpenings.bak.

3. Change the server name in the appsettings.json file.

Run the solution.

## Authentication
1. First Generate Token using Login API: api/Login/PostLoginDetails

Request:
```
{
  "username": "string",
  "password": "string"
}
```
where username and password is: admin

2. Open postman and pass the token under Bearer token in the Authorization Tab 
      OR
   go to header section and add authrization key and pass the token as value.
3. on successfull authentication it will return the data with status code as 200 or else 401 unauthrized
