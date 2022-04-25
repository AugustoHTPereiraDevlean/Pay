create database payment
use payment

create table Plans (
    Id uniqueidentifier not null primary key,
    Description varchar(300) not null,
    Name varchar(100) not null,
    Price decimal(18, 2) not null,
    BillingInterval int not null,
    [Key] varchar(100) not null,
    IsActived bit not null,
    CreatedAt datetime not null
)

create table Coupons (
    Id uniqueidentifier not null primary key,
    PlanId uniqueidentifier not null,
    CountUses int null,
    IsActived bit not null,
    BenefitType varchar(100) not null,
    Benefit decimal(18, 2) not null

    constraint FK_Coupon_Plan foreign key (PlanId) references Plans(Id),
    constraint CK_Coupons_BenefitType check (BenefitType in ('Percent', 'Value'))
)

create table Users (
    Id uniqueidentifier not null primary key,
    Name varchar(100) not null
)

create table CouponPlanUser (
    Id uniqueidentifier not null primary key,
    PlanId uniqueidentifier not null,
    CouponId uniqueidentifier not null,
    UserId uniqueidentifier not null,
    CreatedAt datetime not null,

    constraint FK_CouponPlanUser_Plan foreign key (PlanId) references Plans(Id),
    constraint FK_CouponPlanUser_Coupon foreign key (CouponId) references Coupons(Id),
    constraint FK_CouponPlanUser_User foreign key (UserId) references Users(Id),
)

create table Orders (
    Id uniqueidentifier not null primary key,
    UserId uniqueidentifier not null,
    PaymentMethod varchar(100) not null,
    CouponId uniqueidentifier null,
    CreatedAt datetime not null,

    constraint FK_Order_User foreign key (UserId) references Users(Id),
    constraint FK_Order_Coupon foreign key (CouponId) references Coupons(Id),
    constraint CK_Order_PaymentMethod check (PaymentMethod in ('CreditCard', 'Bankslip'))
)

create table OrderItems (
    Id uniqueidentifier not null primary key,
    OrderId uniqueidentifier not null,
    ObjectType varchar(100) not null,
    ObjectId uniqueidentifier not null,
    Price decimal(18, 2) not null,
    Quantity decimal(18, 2) not null,
    CreatedAt datetime not null,

    constraint FK_OrderItem_Order foreign key (OrderId) references Orders(Id),
    constraint CK_OrderItem_ObjectType check (ObjectType in ('Plan', 'Item'))
)

create table Payments (
    Id uniqueidentifier not null primary key,
    OrderId uniqueidentifier not null,
    Price decimal(18, 2) not null,
    PaidValue decimal(18, 2) not null,
    Discount decimal(18, 2) not null,
    Status varchar(100) not null,
    CreatedAt datetime not null,
    
    constraint FK_Payments_Order foreign key (OrderId) references Orders(Id),
    constraint CK_Payment_Status check (Status in ('Waiting', 'Paid', 'Error'))
)

create table PaymentHistorical (
    Id uniqueidentifier not null primary key,
    PaymentId uniqueidentifier not null,
    Historic varchar(300) not null,
    CreatedAt datetime not null,

    constraint FK_PaymentHistoric_Payment foreign key (PaymentId) references Payments(Id)
)

create table Subscriptions (
    Id uniqueidentifier not null primary key,
    PlanId uniqueidentifier not null,
    UserId uniqueidentifier not null,
    Price decimal(18, 2) not null,
    OrderId uniqueidentifier not null,
    IsActived bit not null,
    CreatedAt datetime not null,
    
    constraint FK_Subscription_Plan foreign key (PlanId) references Plans(Id),
    constraint FK_Subscription_User foreign key (UserId) references Users(Id),
    constraint FK_Subscription_Order foreign key (OrderId) references Orders(Id),
)

create table SubscriptionHistorical (
    Id uniqueidentifier not null primary key,
    SubscriptionId uniqueidentifier not null,
    Historic varchar(300) not null,
    CreatedAt datetime not null,

    constraint FK_SubscriptionHistoric_Subscription foreign key (SubscriptionId) references Subscriptions(Id)
)

create table Items (
    Id uniqueidentifier not null primary key,
    Description varchar(100) not null,
    Price decimal(18, 2)
)

create table PaymentItems (
    PaymentId uniqueidentifier not null,
    ObjectType varchar(100) not null,
    ObjectId uniqueidentifier not null,
    Price decimal(18, 2) not null,

    constraint FK_PaymentItems_Payment foreign key (PaymentId) references Payments(Id),
)

-- Insert initial data

insert into Users (Id, [Name]) values (newid(), 'Augusto Henrique Tomba Pereira')
insert into Plans (Id, [Name], [Description], Price, [Key], BillingInterval, IsActived, CreatedAt) values (newid(), 'Basic', 'Basic plan', 18.9, 'b-p', 30, 1, getdate())
insert into Coupons (Id, PlanId, CountUses, Benefit, BenefitType, IsActived) values (newid(), (select top 1 Id from Plans), 1, 100, 'Percent', 1)
insert into Coupons (Id, PlanId, CountUses, Benefit, BenefitType, IsActived) values (newid(), (select top 1 Id from Plans), 3, 10, 'Value', 1)
insert into Items (Id, [Description], Price) values (newid(), 'Book Clean Code', 36.9)