CREATE TABLE Customers (
                           CustomerID UNIQUEIDENTIFIER PRIMARY KEY,
                           CustomerName VARCHAR(255),
                           Email VARCHAR(255)
);

CREATE TABLE Products (
                          ProductID UNIQUEIDENTIFIER PRIMARY KEY,
                          ProductName VARCHAR(255),
                          UnitPrice DECIMAL(10, 2)
);

CREATE TABLE Orders (
                        OrderID UNIQUEIDENTIFIER PRIMARY KEY,
                        OrderDate DATE,
                        OrderAmount DECIMAL(10, 2),
                        CustomerID UNIQUEIDENTIFIER,
                        OrderStatus VARCHAR(50),
                        FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);



INSERT INTO Customers (CustomerID, CustomerName, Email)
VALUES
    (NEWID(), 'Customer 1', 'customer1@example.com'),
    (NEWID(), 'Customer 2', 'customer2@example.com'),
    (NEWID(), 'Customer 3', 'customer3@example.com');


INSERT INTO Products (ProductID, ProductName, UnitPrice)
VALUES
    (NEWID(), 'Product 1', 50.00),
    (NEWID(), 'Product 2', 75.00),
    (NEWID(), 'Product 3', 100.00);


INSERT INTO Orders (OrderID, OrderDate, OrderAmount, CustomerID, OrderStatus)
VALUES
    (NEWID(), '2023-01-01', 120.00, (SELECT CustomerID FROM Customers WHERE CustomerName = 'Customer 1'), 'Processing'),
    (NEWID(), '2023-01-02', 90.00, (SELECT CustomerID FROM Customers WHERE CustomerName = 'Customer 2'), 'Completed'),
    (NEWID(), '2023-01-03', 200.00, (SELECT CustomerID FROM Customers WHERE CustomerName = 'Customer 3'), 'Processing');



CREATE PROCEDURE GetOrdersReport2
    @StartDate DATE,
    @EndDate DATE
AS
BEGIN
SELECT
    ROW_NUMBER() OVER (ORDER BY O.OrderDate DESC) AS OrderNumber,
        O.OrderDate,
    O.OrderAmount,
    O.OrderStatus,
    C.CustomerName,
    STUFF((SELECT ';' + P.ProductName
           FROM Orders OP
                    JOIN Products P ON P.ProductID = P.ProductID
           WHERE OP.OrderID = O.OrderID
        FOR XML PATH('')), 1, 1, '') AS ProductList
FROM
    Orders O
        JOIN Customers C ON O.CustomerID = C.CustomerID
WHERE
    O.OrderDate BETWEEN @StartDate AND @EndDate
ORDER BY
    O.OrderDate DESC;
END;

DECLARE @StartDate DATE = '2023-01-01';
DECLARE @EndDate DATE = '2023-12-31';

EXEC GetOrdersReport2 @StartDate, @EndDate;