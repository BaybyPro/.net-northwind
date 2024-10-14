ALTER TABLE Orders
ADD IsConfirmed BIT NOT NULL DEFAULT 0,
    IsConfirmed BIT NULL,
    Comments NVARCHAR(500) NULL;
