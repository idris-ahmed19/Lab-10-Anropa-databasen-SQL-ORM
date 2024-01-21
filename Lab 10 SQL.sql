USE NorthWind

SELECT P.ProductName, P.UnitPrice, C.CategoryName
FROM Products P
INNER JOIN Categories C ON P.ProductID = C.CategoryID
ORDER BY C.CategoryName, P.ProductName;

SELECT C.CustomerID, C.ContactName, COUNT(O.OrderID) AS AntalOrdrar
FROM Customers C
LEFT JOIN Orders O ON C.CustomerID = O.CustomerID
GROUP BY C.CustomerID, C.ContactName
ORDER BY AntalOrdrar DESC;

SELECT Employees.FirstName, Employees.LastName, Territories.TerritoryDescription
FROM Employees
INNER JOIN EmployeeTerritories ON Employees.EmployeeID = EmployeeTerritories.EmployeeID
INNER JOIN Territories ON EmployeeTerritories.TerritoryID = Territories.TerritoryID;