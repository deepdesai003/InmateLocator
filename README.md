# City of Philadelphia Take Home Project

Take home project for engineering candidates at City of Philadelphia

## Goal
Write an API to extend the City of Philadelphia's Inmate Locator

## Time Constraint
You should aim to spend 2-4 hours on this task

## Instructions

Use whatever programming language / framework is most comfortable for you. The deliverable should be the source code to a runnable API that meets all of the requirements. While you're welcome to use a database, it's probably faster and easier to just mock your data. Be sure to document the steps to get your code up and running in a local development environment in a README. This would be a good place to also document any assumptions / simplifications that you made. 

## How to submit
You can either: 

1. Fork this repository and push a copy of it to your own GH page and send us a link (be sure to add danlopez as a collaborator if it's private)
1. Submit a pull request to this repository and let us know.

## The problem. 

We want you to write a new API for [the Philly Inmate Locator](https://inmate-locator.phila.gov). This app provides two ways to search for an inmate: By Prisoner ID (PID) or by full name and date of birth. 

Both of these search methods will return an inmate if there is at least one that matches and 404 if there is not. An inmate looks like: 

```
{
  "PID": "123456"
  "firstName": "Dan",
  "lastName": "Lopez"
  "dob": "2020-02-04",
  "location" : "City Hall"
}
```

In addition to this, an administrator should be able to query the API to get a list of all of the inmates for each location. This user should send a signed JWT with a role of `admin`. If their JWT isn't signed correctly, this should deny access.

Finally, a prison warden should be able to make an API call to query the list of inmates only for their location. This user should send a signed JWT with a role of `warden` and a location. If their JWT isn't signed correctly, this should deny access.

Your challenge is to: 
1. Implement all of the API endpoints needed by the UI and by administrators,
1. Write unit tests to cover your API, 
1. Either document your API or use something like [Swagger](https://swagger.io/) to automatically build documentation that you could hand off to a FE developer

## Some Notes
1. You should think about validation for each of the API endpoints
1. Use https://jwt.io/ for resources on decoding JWTs. The secret for the JWTs we're providing is `secret`

### JWTs you can use

#### Administrator
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkxIiwibmFtZSI6IkFkbWluIiwicm9sZSI6IkFkbWluaXN0cmF0b3IiLCJsb2NhdGlvbiI6IkFsY2F0cmF6IiwiaWF0IjoxNTE2MjM5MDIyfQ.gNTJtOyqd0pCyY-uJ1JCSj2vp2bt6pKm6SIaC931lEk

#### Warden
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkxIiwibmFtZSI6IldhcmRlbiIsInJvbGUiOiJ3YXJkZW4iLCJsb2NhdGlvbiI6IkFsY2F0cmF6IiwiaWF0IjoxNTE2MjM5MDIyfQ.dVIceJwZutqWZlnwcwsSujOLI35TPxKoA9jPpD1TAZ4