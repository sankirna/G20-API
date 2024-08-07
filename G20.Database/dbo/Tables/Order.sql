﻿CREATE TABLE [dbo].[Order] (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    UserId INT NOT NULL,
    CouponCode VARCHAR(MAX),
    CouponId INT,
    GrossTotal DECIMAL(10, 2) NOT NULL,
    Discount DECIMAL(10, 2),
    GrandTotal DECIMAL(10, 2) NOT NULL,
    Email VARCHAR(MAX)  NOT NULL,
    Name VARCHAR(MAX)  NOT NULL,
    PhoneNumber VARCHAR(20)  NOT NULL,
    [PaymentTypeId] INT NOT NULL,
    [OrderStatusId] INT NOT NULL,
    [PaymentStatusId] INT NOT NULL,
    [POSTransactionId] VARCHAR(1000) NULL,
    [CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    CONSTRAINT [FK_Order_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ,
    CONSTRAINT [FK_Order_CouponId] FOREIGN KEY ([CouponId]) REFERENCES [dbo].[Coupon] ([Id]),
    CONSTRAINT [FK_Order_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_Order_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id]), 
    CONSTRAINT [PK_Order] PRIMARY KEY ([Id])
);