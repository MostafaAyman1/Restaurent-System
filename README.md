Smart Restaurant Management System

A web-based restaurant management system developed using ASP.NET MVC, C#, SQL Server, HTML, and CSS.
The system helps restaurants manage orders, reservations, menu items, payments, and reports efficiently.

Project Description

The Smart Restaurant Management System allows customers to:

Browse restaurant menus
Search for meals
Add meals to cart
Place food orders online
Reserve tables
Make online payments
Receive order notifications

The system also provides an admin panel for:

Managing menu items
Managing users and staff
Updating order status
Generating reports and invoices
Technologies Used
ASP.NET MVC
C#
HTML5
CSS3
SQL Server
Visual Studio
Design Patterns Used
1. Singleton Pattern

Used for database connection management to ensure only one database instance exists.

2. Factory Pattern

Used for creating different user types and order objects dynamically.

3. Observer Pattern

Used for sending notifications when order status changes.

4. Repository Pattern

Used to separate database operations from business logic.

System Features
User Registration & Login
Browse Restaurant Menu
Search Meals
Add to Cart
Place Orders
Table Reservation
Online Payment
Order Tracking
Notifications System
Admin Dashboard
Sales Reports
Project Structure
Restaurant-System/
│
├── Controllers/
├── Models/
├── Views/
├── Database/
├── CSS/
├── Scripts/
└── README.md

Database Tables
Users
Orders
Order items 
Menu items
customers
categories

How to Run the Project
Open the project using Visual Studio.
Restore NuGet packages if required.
Configure SQL Server connection string.
Run the database scripts.
Build and run the project.
Open the application in the browser.
Team Members
Member	Responsibility
Youssef Mohamed Amin SE3	Authentication and User Management
Mostafa Ayman Abdelmeneom SE3	Menu and Order Management
Mohamed Ragab Elshafey SE3	Payment and Reports
Youssef Ahmed Ehsan SE2	UI Design and Documentation
Future Improvements
SMS Notifications
Mobile Application
Advanced Analytics Dashboard
Online Delivery Tracking
