Traffic Control And Virtual Highway Simulator
<br/>
<br/>
<br/>
| Attribute	 | Details |
| ------------- | ----------- |
| ORM  | EF Core  |
| Architecture  | Clean  |
|.|DTO Based|
|.|Service Oriented|

<br/>
<br/>
<br/>
This repository contains a sample application that simulates a traffic-control system using EFCore. For this sample, I've used a speeding-camera setup as can be found on several highways. A set of cameras are placed at several places on the highway. Using speed data detected by SpeedCameras, Cars get tickets whenever their speed reaches more than the max allowed speed of that highway.
Also, there is an option for people to figure out if their car has any tickets, police Have access to CRUD in tickets title and prices, and access CRUD for cars been fined by them.
<br/>
<br/>
<br/>
<br/>
<br/>
<br/>
Database Diagram:<br/><br/>

![DataBaseDiagram](https://user-images.githubusercontent.com/59726045/130365603-7f7218ba-54c1-44b9-952e-22cd3f60dd64.png)

<br/>
<br/>
Swager view of API:<br/><br/>
<br/>

![Swager](https://user-images.githubusercontent.com/59726045/130365616-395f0eac-7f1a-4bd1-95b0-7367fbc81217.png)

