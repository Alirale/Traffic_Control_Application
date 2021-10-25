| Attribute	 | Details |
| ------------- | ----------- |
| ORM  | EF Core  |
| DataBase  | SQL Server  |
| Architecture  | Clean  |
|Auth system |JWT|
|Containerized|Using Docker|
|Service Oriented|& DTO Based|

<br/>
<br/>
<br/>
This repository contains a dockerized application that simulates a traffic-control system using EFCore. For this sample, I've used a speeding-camera setup as can be found on several highways. A set of cameras are placed at several places on the highway. Using speed data detected by SpeedCameras, Cars get tickets whenever their speed reaches more than the max allowed speed of that highway.<br/><br/><br/>
to use this application every one should register (name , password and role Id (1-> citizen / 2->polices / 3-> Admins)) and get jwt bearer token,then there is accessibility for this roles :<br/><br/>
1) everyone has access to account controller.<br/><br/>
2) citizens can figure out if their car has any tickets(PersonCar Controller).<br/><br/>
3) polices Have access to ticket Controller , CarRegisteration Controller ,TicketsList Controller and PersonCar Controller.<br/><br/>
4) Admins have access to All of the controllers.<br/><br/>
<br/>
<br/>
If you want to start simulation process , chanage the RunBackgroundTask value in Appsetting.json File to "True"!
<br/>
<br/>
<br/>
Database Diagram:<br/><br/>

![Database Diagram](https://user-images.githubusercontent.com/59726045/138750413-03767554-96a3-45db-b545-95e4e978a4e0.png)

<br/>
<br/>
Swager view of API:<br/><br/>
<br/>

![swager](https://user-images.githubusercontent.com/59726045/138749345-1e8e0292-3b78-43ae-8ba8-b8c21bcb516c.png)


