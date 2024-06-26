﻿CREATE TABLE ShoppingCart (
    [Id]      INT IDENTITY (1, 1) NOT NULL,
    UserId INT NOT NULL,
    CouponCode VARCHAR(MAX),
    CouponId INT,
    GrossTotal DECIMAL(10, 2) NOT NULL,
    Discount DECIMAL(10, 2),
    GrandTotal DECIMAL(10, 2) NOT NULL,
    /*Email VARCHAR(MAX) NOT NULL,
    Name VARCHAR(MAX)  NULL,
    PhoneNumber VARCHAR(20)  NULL,*/
    [CreatedBy]       INT            NULL,
    [CreatedDateTime] DATETIME       NULL,
    [UpdatedBy]       INT            NULL,
    [UpdatedDateTime] DATETIME       NULL,
    CONSTRAINT [FK_ShoppingCart_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([Id]) ,
    CONSTRAINT [FK_ShoppingCart_CouponId] FOREIGN KEY ([CouponId]) REFERENCES [dbo].[Coupon] ([Id]),
    CONSTRAINT [FK_ShoppingCart_CreatedBy] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[User] ([Id]),
    CONSTRAINT [FK_ShoppingCart_UpdatedBy] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[User] ([Id]), 
    CONSTRAINT [PK_ShoppingCart] PRIMARY KEY ([Id])
);