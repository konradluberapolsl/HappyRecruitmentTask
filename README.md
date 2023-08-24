# Happy Team Recruitment Task

This repository is a solution to a recruitment task for Happy Team, the solution consists of an application in client-server architecture where the server part was created using .Net and the client part was created using React

## Setup

Swap your database data in connection string in *api/Teslarent.API/appsettings.json* and if necessary, change api url in *web/src/config.json*.

## Decisions and assumptions

The basic decision in my mind was whether the application would target the company's employees, customers or both at the same time, initially I assumed that I would create one application for employees and customers, however, a rather small amount of free time meant that I had to limit it to customers only. I assumed that the rental process looks like this:

1. The user creates a reservation through the website
2. On the chosen day, he reaches the chosen place and picks up the car and the employee changes the reservation to active in the system
3. On the day marked as the end of the reservation, the customer returns the car to the designated place and the employee marks the reservation as completed.
4. The cost of the reservation is calculated
5. Hopefully the customer will pay

Of course, the presented flow is as happy as possible, because it does not assume that the customer will cancel the reservation, will want to extend it or something will happen that will require an additional fee, such as a delay with the return or damage to the vehicle, but such assumptions greatly simplify, for example, the process of price calculation, and I guess this task is not about building a production-ready app. Even despite this simplification, some logic remained to be implemented related to the availability of the car at a given time and place and it seems to me that all the requirements from the instruction were met.
Tasks belonging to a employee can be performed via swagger using the endpoints /activate and /finish.

## Known issues

Unfortunately, I was not able to complete the auth functionality within the timeframe, but my struggles can be observed on the feature/auth0 branch as I would like to see this through to the end. I chose auth0 as the auth provider because I had never used it and wanted to get to know it, initially I wanted to base the solution on asp.net.idenity but supposedly own implementations are less secure than an external provider and besides, I did it once in a college project and wouldn't want to do it again.
Due to the lack of authentication, user's state is not stored  on the frontend, which is shameful, and the endpoints responsible for reservation manipulation are available without any restrictions... well, and the test coverage...
