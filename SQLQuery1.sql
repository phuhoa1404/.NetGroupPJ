CREATE DATABASE coffeshop;
USE coffeshop;

Create Login mylogin with password='mylogin', 
CHECK_POLICY = OFF 
sp_changedbowner mylogin

CREATE TABLE TableDrinks(
	TD_ID INT IDENTITY PRIMARY KEY,
	TD_NAME NVARCHAR(100),
	TD_STATUS NVARCHAR(100)
);
select * from TableDrinks
CREATE TABLE Accounts(
	ACC_USERNAME NVARCHAR(100) NOT NULL PRIMARY KEY,
	ACC_PWD NVARCHAR(100) NOT NULL DEFAULT 0,
	ACC_TYPE INT NOT NULL DEFAULT 0,
	ACC_NAME nvarchar(100)
);
select * from Accounts
CREATE TABLE Menu(
	DRINK_NAME NVARCHAR(100),
	COUNT INT,
	PRICE FLOAT NOT NULL DEFAULT 0,
	TOTAL_PRICE FLOAT NOT NULL DEFAULT 0
);
CREATE TABLE Drinks(
	ID INT IDENTITY PRIMARY KEY,
	DR_NAME NVARCHAR(100) NOT NULL DEFAULT N'Chưa đặt tên',
	DR_PRICE FLOAT NOT NULL DEFAULT 0,
);
select * from Drinks


CREATE TABLE Bill(
	ID INT IDENTITY PRIMARY KEY,
	DATE_CHECKIN DATE NOT NULL DEFAULT GETDATE(),
	DATE_CHECKOUT DATE,
	ID_TABLE INT NOT NULL,
	BILL_STATUS INT NOT NULL DEFAULT 0, --0 chưa thanh toán, 1 đã thanh toán
	FOREIGN KEY (ID_TABLE) REFERENCES TableDrinks(TD_ID)
);
CREATE TABLE Bill_Infor(
	ID INT IDENTITY PRIMARY KEY,
	ID_BILL INT NOT NULL,
	ID_DRINKS INT NOT NULL,
	COUNT INT NOT NULL DEFAULT 0
	FOREIGN KEY (ID_BILL) REFERENCES Bill(ID),
	FOREIGN KEY (ID_DRINKS) REFERENCES Drinks(ID)
);
INSERT INTO Accounts (ACC_USERNAME, ACC_PWD, ACC_TYPE) VALUES (N'ADMIN', N'123ABC', 1);
INSERT INTO Accounts (ACC_USERNAME, ACC_PWD, ACC_TYPE) VALUES (N'STAFF01', N'123ABCDEF', 0);
SELECT * FROM Accounts;

INSERT INTO TableDrinks (TD_NAME, TD_STATUS) VALUES (N'B1', N'Trống');
INSERT INTO TableDrinks (TD_NAME, TD_STATUS) VALUES (N'B2', N'Trống');
INSERT INTO TableDrinks (TD_NAME, TD_STATUS) VALUES (N'B3', N'Trống');
INSERT INTO TableDrinks (TD_NAME, TD_STATUS) VALUES (N'B4', N'Có người');
INSERT INTO TableDrinks (TD_NAME, TD_STATUS) VALUES (N'B5', N'Trống');
SELECT * FROM TableDrinks;



INSERT INTO Drinks (DR_NAME, DR_PRICE) VALUES (N'Coffe đá', 18000);
INSERT INTO Drinks (DR_NAME, DR_PRICE) VALUES (N'Coffe sữa đá', 20000);
INSERT INTO Drinks (DR_NAME, DR_PRICE) VALUES (N'Lipton đá', 16000);
INSERT INTO Drinks (DR_NAME, DR_PRICE) VALUES (N'Cacao đá', 18000);
INSERT INTO Drinks (DR_NAME, DR_PRICE) VALUES (N'Cacao sữa đá', 20000);
INSERT INTO Drinks (DR_NAME, DR_PRICE) VALUES (N'Latte', 22000);
SELECT * FROM Drinks;

INSERT INTO Bill (DATE_CHECKIN, DATE_CHECKOUT, ID_TABLE, BILL_STATUS) VALUES (GETDATE(), '', 1, 0);
INSERT INTO Bill (DATE_CHECKIN, DATE_CHECKOUT, ID_TABLE, BILL_STATUS) VALUES (GETDATE(), '', 2, 0);
INSERT INTO Bill (DATE_CHECKIN, DATE_CHECKOUT, ID_TABLE, BILL_STATUS) VALUES (GETDATE(), '', 3, 0);
SELECT * FROM Bill;


INSERT INTO Bill_Infor (ID_BILL, ID_DRINKS, COUNT) VALUES (1, 2, 1);
INSERT INTO Bill_Infor (ID_BILL, ID_DRINKS, COUNT) VALUES (2, 1, 1);
INSERT INTO Bill_Infor (ID_BILL, ID_DRINKS, COUNT) VALUES (3, 1, 1);
SELECT * FROM Bill_Infor;

INSERT INTO TableDrinks (TD_NAME, TD_STATUS) VALUES ('A1', N'Trống');
select * from TableDrinks

--procedure check mỗi khi thêm món. Nếu bill bàn đó đã tồn tại, thì thêm billinfo.
-- Nếu bill bàn đó chưa có, tạo bill cho bàn đó.
CREATE PROCEDURE InsertBillInfor
@idBill INT, @idDrink INT, @count INT
AS
BEGIN

	DECLARE @isExitsBillInfo INT
	DECLARE @drinkCount INT = 1
	
	SELECT @isExitsBillInfo = ID, @drinkCount = b.count 
	FROM Bill_Infor AS b 
	WHERE ID_BILL = @idBill AND ID_DRINKS = @idDrink

	IF (@isExitsBillInfo > 0)
	BEGIN
		DECLARE @newCount INT = @drinkCount + @count
		IF (@newCount > 0)
			UPDATE Bill_Infor SET count = @drinkCount + @count WHERE ID_DRINKS = @idDrink
		ELSE
			DELETE Bill_Infor WHERE ID_BILL = @idBill AND ID_DRINKS = @idDrink
	END
	ELSE
	BEGIN
		INSERT	Bill_Infor(ID_BILL, ID_DRINKS, count )
		VALUES  ( @idBill, @idDrink, @count)
	END
END

--trigger
CREATE TRIGGER TR_UpdateBillInfor
ON Bill_Infor FOR INSERT, UPDATE
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = ID_BILL FROM inserted
	DECLARE @idTable INT
	SELECT @idTable = ID_TABLE FROM Bill WHERE ID = @idBill AND BILL_STATUS = 0
	UPDATE TableDrinks SET TD_STATUS = N'Có người' WHERE TD_ID = @idTable
END

CREATE TRIGGER TR_UpdateBill
ON Bill FOR UPDATE
AS
BEGIN
	DECLARE @idBill INT
	SELECT @idBill = ID FROM inserted
	DECLARE @idTable INT
	SELECT @idTable = ID_TABLE FROM Bill WHERE ID = @idBill
	DECLARE @count INT = 0
	SELECT @count = COUNT(*) FROM Bill WHERE ID_TABLE = @idTable and BILL_STATUS = 0
	IF(@count = 0)
		UPDATE TableDrinks SET TD_STATUS = N'Trống' WHERE TD_ID = @idTable
END

--procedure danh sách bill theo ngày/tháng/năm
CREATE PROC GetListBillByDate
@checkIn date, @checkOut date
AS 
BEGIN
	SELECT t.TD_NAME AS [Tên bàn], b.TotalPrice AS [Tổng tiền], 
		b.DATE_CHECKIN AS [Ngày vào], b.DATE_CHECKOUT AS [Ngày ra]
	FROM Bill AS b, TableDrinks AS t
	WHERE b.DATE_CHECKIN >= @checkIn AND b.DATE_CHECKOUT <= @checkOut AND b.BILL_STATUS = 1
	AND t.TD_ID = b.ID_TABLE
END
GO
SELECT * FROM Accounts;