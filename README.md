Overview:
This is a C#-based order management system designed to handle order creation, saving, and retrieval. 
The system utilizes Object-Oriented Design and Factory Pattern, supporting data storage in both JSON files and SQLite databases. 
Unit tests are included to verify system functionality.

Architecture
Class Library: Contains core business logic classes (e.g., Order, OrderDetail) and interfaces (IOrderRepository).
Driver Project: Demonstrates system functionality and implements data persistence for testing.
Unit Test Project: Verifies business logic and storage backend functionality.

Factory Pattern Explanation:
In driver.cs, I use both case to output two type of data.
According to actual case, we can use if-else to determine if database is closed.

Use:
.Net 8.0
Microsoft SQLite
