# Disma Demo

I created the following demo for an interview that never materialized. This demo implements the 
following technologies.

- ASP.NET Core MVC
- Entity Framework Core
- Sql Server
- SignalR

To see SignalR in action, open one of the Details view for a Price List. Ex: https://localhost:44327/PriceLists/Details/1
![Alt text](pricelist.jpg?raw=true "Price List")

Open a second browser and navigate to the Dashboard view.  Ex. https://localhost:44327/PriceDashboard/PriceDashboard
![Alt text](dashboard.jpg?raw=true "Prices Dashboard")

You can change part prices on the price list detail page and the dashboard updates the price in real time.
