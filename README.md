# Employee Management API

## Overview

This project is a simple CRUD API built using .NET Core for managing an organization's employees. The API allows for creating, reading, updating, and deleting employee records. Microsoft SQL Server is used as the database for storing employee data.

## Requirements

- .NET 6 SDK or later
- Microsoft SQL Server
- Visual Studio or any IDE supporting .NET development

## Endpoints

### 1. GET /Employees

Returns a list of all employees.

**Response Format:**

```json
[
  {
    "employeeID": 1,
    "name": "Ahmed Yousry",
    "department": "Back End",
    "salary": 50000
  },
  {
    "employeeID": 2,
    "name": "Karim Helal",
    "department": "IT",
    "salary": 75000
  }
]
```

### 2. POST /Employees

Adds a new employee.

**Request Format:**

```json
{
  "name": "Ahmed Yousry",
  "department": "Back End",
  "salary": 50000
}
```

**Response Format:**

```json
{
  "employeeID": 1,
  "name": "Ahmed Yousry",
  "department": "Back End",
  "salary": 50000
}
```

### 3. PUT /Employees/{id}

Updates an existing employee.

**Request Format:**

```json
{
  "name": "John Doe",
  "department": "Finance",
  "salary": 55000
}
```

**Response Format:**

```json
{
  "employeeID": 1,
  "name": "John Doe",
  "department": "Finance",
  "salary": 55000
}
```

### 4. DELETE /Employees/{id}

Deletes an employee.

**Response Format:**

```json
{
  "message": "Employee deleted successfully."
}
```

## Data Model

- EmployeeID (int, primary key)
- Name (string, max length 100)
- Department (string, max length 100)
- Salary (decimal)

## Database Setup

To create the Employees table in Microsoft SQL Server, run the following SQL script:

```sql
CREATE TABLE Employees (
  EmployeeID INT PRIMARY KEY IDENTITY,
  Name NVARCHAR(100) NOT NULL,
  Department NVARCHAR(100) NOT NULL,
  Salary DECIMAL(18, 2) CHECK (Salary > 0) NOT NULL
);
```

## Installation and Running the Application

### Cloning the Repository

```bash
git clone "https://github.com/AhmedYousry10/Zimozi-Task.git"
cd Zimozi-Task
```

### Running the Application

1. Open the solution file Zimozi-Task.sln in Visual Studio or your preferred IDE.

2. Update the connection string in the appsettings.json file to match your SQL Server configuration:

```json
  "ConnectionStrings": {
    "DefaultConnection": "Server=.;Database=ZimoziDB;integrated security=true;trustservercertificate=true;",
  }
```

3. Run the following commands in the Package Manager Console to apply migrations and create the database:

```bash
Update-Database
```

4. Run the application using Visual Studio or the .NET CLI:

```bash
dotnet run
```

### Running Tests

To run the unit and integration tests, execute the following command in the terminal:

```bash
dotnet test
```

## Error Handling

The API includes proper error handling for scenarios where an employee is not found for update or deletion. It returns appropriate HTTP status codes and error messages in such cases.

## SQL Query

The following SQL query finds the top 3 highest-paid employees in each department:

```sql
WITH RankedEmployees AS (
  SELECT
    EmployeeID, Name, Department, Salary,
    ROW_NUMBER() OVER(PARTITION BY Department ORDER BY Salary DESC) AS Rank
  FROM Employees
)
SELECT
  EmployeeID, Name, Department, Salary
FROM RankedEmployees
WHERE Rank <= 3;
```

This query uses a Common Table Expression (CTE) with the ROW_NUMBER() function to rank employees by salary within each department and then selects only the top 3 employees per department.

## Conclusion

This project provides a foundational API for managing employees, with clean code, comprehensive documentation, and test coverage. Contributions and feedback are welcome!

# Developer Information

- Name: Ahmed Yousry
- Title: Software Engineer
- Phone: +201007458070
- Email: ahmedu3helal@gmail.com
- LinkedIn: [Ahmed Yousry Helal](https://www.linkedin.com/in/ahmed-yousry-helal/)
