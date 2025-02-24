CREATE TABLE app_user
(
    userId          NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    firstName       NVARCHAR2(50)        NOT NULL,
    lastName        NVARCHAR2(50)        NOT NULL,
    username        NVARCHAR2(50) UNIQUE NOT NULL,
    password        NVARCHAR2(256)       NOT NULL,
    email           NVARCHAR2(50)        NOT NULL,
    userType        NVARCHAR2(20)        NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER               NOT NULL,
    auditCreateDate TIMESTAMP(7)         NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7)
);

CREATE TABLE client
(
    clientId        NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    userId          NUMBER         NOT NULL,
    address         NVARCHAR2(100) NOT NULL,
    phone           NVARCHAR2(20)  NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER         NOT NULL,
    auditCreateDate TIMESTAMP(7)   NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7),
    CONSTRAINT fk_client_user FOREIGN KEY (userId) REFERENCES app_user (userId)
);

CREATE TABLE pet
(
    petId           NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    name            NVARCHAR2(50) NOT NULL,
    type            NVARCHAR2(50) NOT NULL,
    breed           NVARCHAR2(50) NOT NULL,
    age             NUMBER        NOT NULL,
    clientId        NUMBER        NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER        NOT NULL,
    auditCreateDate TIMESTAMP(7)  NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7),
    CONSTRAINT fk_pet_client FOREIGN KEY (clientId) REFERENCES client (clientId)
);

CREATE TABLE medic
(
    medicId         NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    userId          NUMBER        NOT NULL,
    specialty       NVARCHAR2(50) NOT NULL,
    phone           NVARCHAR2(20) NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER        NOT NULL,
    auditCreateDate TIMESTAMP(7)  NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7),
    CONSTRAINT fk_medic_user FOREIGN KEY (userId) REFERENCES app_user (userId)
);

CREATE TABLE appointment
(
    appointmentId   NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    appointmentDate DATE           NOT NULL,
    reason          NVARCHAR2(100) NOT NULL,
    petId           NUMBER         NOT NULL,
    medicId         NUMBER         NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER         NOT NULL,
    auditCreateDate TIMESTAMP(7)   NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7),
    CONSTRAINT fk_appointment_pet FOREIGN KEY (petId) REFERENCES pet (petId),
    CONSTRAINT fk_appointment_medic FOREIGN KEY (medicId) REFERENCES medic (medicId)
);

CREATE TABLE appointmentDetail
(
    appointmentDetailId NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    appointmentId       NUMBER         NOT NULL,
    diagnosis           NVARCHAR2(200) NOT NULL,
    treatment           NVARCHAR2(200) NOT NULL,
    observations        NVARCHAR2(200) NOT NULL,
    state               NUMBER,
    auditCreateUser     NUMBER         NOT NULL,
    auditCreateDate     TIMESTAMP(7)   NOT NULL,
    auditUpdateUser     NUMBER,
    auditUpdateDate     TIMESTAMP(7),
    auditDeleteUser     NUMBER,
    auditDeleteDate     TIMESTAMP(7),
    CONSTRAINT fk_appointmentDetail_appointment FOREIGN KEY (appointmentId) REFERENCES appointment (appointmentId)
);

CREATE TABLE vaccine
(
    vaccineId       NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    vaccineName     NVARCHAR2(50)  NOT NULL,
    description     NVARCHAR2(100) NOT NULL,
    type            NVARCHAR2(50)  NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER         NOT NULL,
    auditCreateDate TIMESTAMP(7)   NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7)
);

CREATE TABLE appliedVaccine
(
    appliedVaccineId NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    applicationDate  DATE         NOT NULL,
    petId            NUMBER       NOT NULL,
    vaccineId        NUMBER       NOT NULL,
    state            NUMBER,
    auditCreateUser  NUMBER       NOT NULL,
    auditCreateDate  TIMESTAMP(7) NOT NULL,
    auditUpdateUser  NUMBER,
    auditUpdateDate  TIMESTAMP(7),
    auditDeleteUser  NUMBER,
    auditDeleteDate  TIMESTAMP(7),
    CONSTRAINT fk_appliedVaccine_pet FOREIGN KEY (petId) REFERENCES pet (petId),
    CONSTRAINT fk_appliedVaccine_vaccine FOREIGN KEY (vaccineId) REFERENCES vaccine (vaccineId)
);

CREATE TABLE product
(
    productId       NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    name            NVARCHAR2(50)  NOT NULL,
    description     NVARCHAR2(100) NOT NULL,
    price           NUMBER(10, 2)  NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER         NOT NULL,
    auditCreateDate TIMESTAMP(7)   NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7)
);

CREATE TABLE inventory
(
    inventoryId     NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    productId       NUMBER       NOT NULL,
    quantity        NUMBER       NOT NULL,
    updateDate      DATE         NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER       NOT NULL,
    auditCreateDate TIMESTAMP(7) NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7),
    CONSTRAINT fk_inventory_product FOREIGN KEY (productId) REFERENCES product (productId)
);

CREATE TABLE sale
(
    saleId          NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    saleDate        DATE         NOT NULL,
    clientId        NUMBER       NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER       NOT NULL,
    auditCreateDate TIMESTAMP(7) NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7),
    CONSTRAINT fk_sale_client FOREIGN KEY (clientId) REFERENCES client (clientId)
);

CREATE TABLE saleDetail
(
    saleId          NUMBER,
    productId       NUMBER,
    quantity        NUMBER        NOT NULL,
    price           NUMBER(10, 2) NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER        NOT NULL,
    auditCreateDate TIMESTAMP(7)  NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7),
    CONSTRAINT pk_saleDetail PRIMARY KEY (saleId, productId),
    CONSTRAINT fk_saleDetail_sale FOREIGN KEY (saleId) REFERENCES sale (saleId),
    CONSTRAINT fk_saleDetail_product FOREIGN KEY (productId) REFERENCES product (productId)
);

CREATE TABLE payment
(
    paymentId       NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    saleId          NUMBER        NOT NULL,
    amount          NUMBER(10, 2) NOT NULL,
    paymentDate     DATE          NOT NULL,
    paymentType     NVARCHAR2(50) NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER        NOT NULL,
    auditCreateDate TIMESTAMP(7)  NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7),
    CONSTRAINT fk_payment_sale FOREIGN KEY (saleId) REFERENCES sale (saleId)
);

CREATE TABLE productCategory
(
    categoryId      NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    categoryName    NVARCHAR2(50)  NOT NULL,
    description     NVARCHAR2(100) NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER         NOT NULL,
    auditCreateDate TIMESTAMP(7)   NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7)
);

CREATE TABLE productCategoryRelation
(
    productId       NUMBER,
    categoryId      NUMBER,
    state           NUMBER,
    auditCreateUser NUMBER       NOT NULL,
    auditCreateDate TIMESTAMP(7) NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7),
    CONSTRAINT pk_productCategoryRelation PRIMARY KEY (productId, categoryId),
    CONSTRAINT fk_productCategoryRelation_product FOREIGN KEY (productId) REFERENCES product (productId),
    CONSTRAINT fk_productCategoryRelation_category FOREIGN KEY (categoryId) REFERENCES productCategory (categoryId)
);

CREATE TABLE supplier
(
    supplierId      NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    name            NVARCHAR2(50)  NOT NULL,
    contact         NVARCHAR2(50)  NOT NULL,
    phone           NVARCHAR2(20)  NOT NULL,
    address         NVARCHAR2(100) NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER         NOT NULL,
    auditCreateDate TIMESTAMP(7)   NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7)
);

CREATE TABLE purchaseOrder
(
    purchaseOrderId NUMBER GENERATED AS IDENTITY PRIMARY KEY,
    supplierId      NUMBER        NOT NULL,
    orderDate       DATE          NOT NULL,
    status          NVARCHAR2(50) NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER        NOT NULL,
    auditCreateDate TIMESTAMP(7)  NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7),
    CONSTRAINT fk_purchaseOrder_supplier FOREIGN KEY (supplierId) REFERENCES supplier (supplierId)
);

CREATE TABLE purchaseOrderDetail
(
    purchaseOrderId NUMBER,
    productId       NUMBER,
    quantity        NUMBER        NOT NULL,
    unitPrice       NUMBER(10, 2) NOT NULL,
    state           NUMBER,
    auditCreateUser NUMBER        NOT NULL,
    auditCreateDate TIMESTAMP(7)  NOT NULL,
    auditUpdateUser NUMBER,
    auditUpdateDate TIMESTAMP(7),
    auditDeleteUser NUMBER,
    auditDeleteDate TIMESTAMP(7),
    CONSTRAINT pk_purchaseOrderDetail PRIMARY KEY (purchaseOrderId, productId),
    CONSTRAINT fk_purchaseOrderDetail_order FOREIGN KEY (purchaseOrderId) REFERENCES purchaseOrder (purchaseOrderId),
    CONSTRAINT fk_purchaseOrderDetail_product FOREIGN KEY (productId) REFERENCES product (productId)
);

-- Indexes

CREATE INDEX idx_client_userId ON client (userId);
CREATE INDEX idx_pet_clientId ON pet (clientId);
CREATE INDEX idx_medic_userId ON medic (userId);
CREATE INDEX idx_appointment_petId ON appointment (petId);
CREATE INDEX idx_appointment_medicId ON appointment (medicId);
CREATE INDEX idx_appointmentDetail_appointmentId ON appointmentDetail (appointmentId);
CREATE INDEX idx_appliedVaccine_petId ON appliedVaccine (petId);
CREATE INDEX idx_appliedVaccine_vaccineId ON appliedVaccine (vaccineId);
CREATE INDEX idx_inventory_productId ON inventory (productId);
CREATE INDEX idx_sale_clientId ON sale (clientId);
CREATE INDEX idx_saleDetail_saleId ON saleDetail (saleId);
CREATE INDEX idx_saleDetail_productId ON saleDetail (productId);
CREATE INDEX idx_payment_saleId ON payment (saleId);
CREATE INDEX idx_productCategoryRelation_productId ON productCategoryRelation (productId);
CREATE INDEX idx_productCategoryRelation_categoryId ON productCategoryRelation (categoryId);
CREATE INDEX idx_purchaseOrder_supplierId ON purchaseOrder (supplierId);
CREATE INDEX idx_purchaseOrderDetail_purchaseOrderId ON purchaseOrderDetail (purchaseOrderId);
CREATE INDEX idx_purchaseOrderDetail_productId ON purchaseOrderDetail (productId);

