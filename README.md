# TransportationManagementSystem-ASPNetCoreMVC

A full-stack Transportation Management System developed during my cooperative training at King Fahd Armed Forces Hospital in Jeddah.

The project originally focused on frontend development as part of my assigned training responsibilities. During the training period, I took the initiative to expand my technical experience by learning backend development and the ASP.NET Core MVC pattern from a team member working on system development.

After receiving guidance and practical training, I applied what I learned by implementing backend functionality, connecting the application to a SQL Server database, and working with the MVC architecture to build a more complete, database-driven application.

> **Note:** This repository is a training and portfolio project and uses fictional/demo data.

---

## Project Overview

The Transportation Management System is designed to support transportation-related operations within a hospital environment.

The system includes different user roles and workflows for managing transportation requests, trips, assignments, emergency requests, and notifications.

The main purpose of this project was not only to develop the interface, but also to gain practical hands-on experience in connecting the frontend with a structured backend and relational database using ASP.NET Core MVC.

---

## My Role

During my cooperative training, my primary assigned responsibilities focused on **frontend development and user interface implementation**.

I worked on designing and developing system interfaces and translating requirements into functional user-facing pages.

Beyond my assigned responsibilities, I took the initiative to learn **backend development and ASP.NET Core MVC** from a team member who was working on system development.

Through this experience, I learned how:

- The MVC architecture is structured.
- Controllers handle application requests and backend logic.
- Models represent and manage application data.
- Views display data and interact with users.
- Entity Framework Core connects the application to the database.
- SQL Server stores and manages relational data.
- Controllers, models, views, and the database work together as one system.

I then applied these concepts directly to the project and implemented backend functionality and database integration.

This experience allowed me to expand my role from primarily frontend development into **hands-on full-stack web development**.

---

## Key Features

### Authentication & User Roles
- User login and authentication.
- Session-based user information.
- Role-based navigation and workflows.
- Different interfaces for different system roles.

### Nurse Workflow
- Submit emergency transportation requests.
- Select pickup and destination locations.
- Provide emergency request reasons and notes.
- View notifications.
- Receive updates regarding emergency requests.

### Movement Officer Workflow
- View emergency transportation requests.
- Review pending requests.
- Assign drivers and vehicles.
- Approve or reject emergency requests.
- Manage transportation assignments.

### Manager Workflow
- Manage transportation-related information.
- Review and manage trips.
- Work with transportation assignments and operational data.

### Notifications
- Notifications are generated based on system actions.
- Nurses can view their notifications.
- Notification status can change from `New` to `Read`.

### Database Integration
- SQL Server relational database.
- Entity Framework Core integration.
- Primary and foreign key relationships.
- Database-driven application workflows.
- CRUD operations.
- LINQ queries.

---
## MVC Architecture
The project follows the **Model-View-Controller (MVC)** architecture, where each layer has a specific responsibility:


Model

The Models represent the application's data and entities, including:

User
Driver
Vehicle
Trip
Assignment
Emergency Request
Notification
View

The Views provide the user interface and display data returned from the backend.

Controller

The Controllers handle requests, application logic, database operations, validation, and communication between the Models and Views.

Technologies Used
Frontend
HTML5
CSS3
JavaScript
UI/UX Design

Backend
C#
ASP.NET Core MVC
Razor Views
Entity Framework Core
LINQ

Database
Microsoft SQL Server
SQL Server Management Studio (SSMS)
Development Tools
Visual Studio
Git
GitHub
Database

The project uses Microsoft SQL Server as its relational database.

The database contains entities related to:

Users
Drivers
Vehicles
Trips
Assignments
Emergency Requests
Notifications

The database structure includes primary keys, foreign keys, and relationships between related entities.

A SQL script containing the database structure and demo data is included in the repository under:

Database/
└── TransportationDB.sql

The database uses fictional/demo data for learning and portfolio purposes.

Project Structure
TransportationManagementSystem/
│
├── Controllers/
│   ├── LoginController.cs
│   ├── AssignmentController.cs
│   ├── EmergencyRequestController.cs
│   ├── EmergencyRequestsController.cs
│   ├── NotificationController.cs
│   └── ...
│
├── Models/
│   ├── User.cs
│   ├── Driver.cs
│   ├── Vehicle.cs
│   ├── Trip.cs
│   ├── Assignment.cs
│   ├── EmergencyRequest.cs
│   ├── Notification.cs
│   └── ...
│
├── Data/
│   └── TransportationDbContext.cs
│
├── Views/
│   ├── Login/
│   ├── Nurse/
│   ├── Assignment/
│   ├── Manager/
│   └── ...
│
├── wwwroot/
│   ├── css/
│   
│  
│
├── Database/
│   └── TransportationDB.sql
│
├── Program.cs
├── appsettings.json
└── TransportationManagementSystem.csproj

What I Learned

This project was an important step in expanding my development skills beyond frontend development.

Through hands-on practice, I gained experience in:

Understanding and applying the MVC design pattern.
Building backend functionality using ASP.NET Core.
Working with Controllers, Models, and Views.
Connecting an MVC application to SQL Server.
Using Entity Framework Core for database operations.
Creating and working with relational database relationships.
Implementing CRUD operations.
Handling user sessions and role-based workflows.
Connecting frontend interfaces to backend functionality.
Debugging and testing a complete application workflow.

Most importantly, the project gave me the opportunity to learn a new technology from an experienced developer and immediately apply that knowledge to a real project environment.

Training Experience

This project was developed during my cooperative training period at:

King Fahd Armed Forces Hospital – Jeddah

During the training, I was initially responsible for frontend-related tasks. I then proactively pursued additional learning in backend development and ASP.NET Core MVC beyond my initial responsibilities.

The guidance I received allowed me to understand how the different layers of a web application work together and to apply MVC concepts practically rather than learning them only through theory.

I also received a letter of recommendation recognizing my commitment, willingness to learn new technologies, ability to apply feedback, and initiative in expanding my technical skills during the training period.

Important Note

This repository is intended for learning, demonstration, and portfolio purposes.

All data included in the project is fictional/demo data and does not represent real hospital records or personal information.

Author
Abrar Alharbi

Technologies explored through this project:

ASP.NET Core MVC C# Entity Framework Core SQL Server HTML CSS JavaScript UI/UX

