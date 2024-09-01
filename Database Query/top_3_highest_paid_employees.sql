Use ZimoziDB;

-- create the Employees table
CREATE TABLE Employees (
  EmployeeID INT PRIMARY KEY IDENTITY,
  Name NVARCHAR(100) NOT NULL,
  Department NVARCHAR(100) NOT NULL,
  Salary DECIMAL(18, 2) CHECK (Salary > 0) NOT NULL
);

-- Query Requirements:
	-- 1. The query should return the EmployeeID, Name, Department, and Salary of the top 3 highest-paid employees in each department.
	-- 2. If there are ties in salary, the query should still return only the top 3 for each department.

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