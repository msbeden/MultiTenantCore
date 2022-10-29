# MultiTenantCore
Multi tenant .Net Core 6.0 Web Api Project
The purpose of this project is to use multiple databases over a single API.

![enter image description here](https://camo.githubusercontent.com/916167fa2d7592c5611984736da2a8d701c7ce5e2e1ed3636a2d9d0cb858b636/68747470733a2f2f692e6962622e636f2f5333345a3033462f6170702e706e67)


My work on it continues. Migrations are working correctly. But **SharedDbContext** does not work in controller operations.  **MultipleDbContext** is working.

> I am looking for help to resolve the error.


TenantId needs to be added to the request header information for the product listing process. As I show in the picture below.

![enter image description here](https://i.ibb.co/SxjPX0c/github.png)

The error I get that I can't solve is:
![enter image description here](https://i.ibb.co/BNfFvB6/github2.png)
