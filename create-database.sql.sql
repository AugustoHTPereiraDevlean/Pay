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

    constraint FK_Coupon_Plan foreign key (PlanId) references Plans(Id)
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

create table Subscriptions (
    Id uniqueidentifier not null primary key,
    PlanId uniqueidentifier not null,
    UserId uniqueidentifier not null,
    Price decimal(18, 2) not null,
    IsActived bit not null,
    CreatedAt datetime not null,
    
    constraint FK_Subscription_Plan foreign key (PlanId) references Plans(Id),
    constraint FK_Subscription_User foreign key (UserId) references Users(Id),
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

create table Orders (
    Id uniqueidentifier not null primary key,
    UserId uniqueidentifier not null,
    CreatedAt datetime not null
)

create table OrderItems (
    Id uniqueidentifier not null primary key,
    OrderId uniqueidentifier not null,
    ObjectType varchar(100) not null,
    ObjectId uniqueidentifier not null,
    Price decimal(18, 2) not null

    constraint FK_OrderItem_Order foreign key (OrderId) references Orders(Id),
    constraint CK_OrderItem_ObjectType check (ObjectType in ('Subscription', 'Item'))
)

create table Payments (
    Id uniqueidentifier not null primary key,
    OrderId uniqueidentifier not null,
    Price decimal(18, 2) not null,
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
