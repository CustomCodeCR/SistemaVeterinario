-- Active Users
CREATE OR REPLACE VIEW view_active_app_users AS
SELECT userId,
       firstName,
       lastName,
       username,
       email,
       userType,
       auditCreateUser,
       auditCreateDate,
       auditUpdateUser,
       auditUpdateDate
FROM app_user
WHERE state != 0;

-- Client Information
CREATE OR REPLACE VIEW view_client_info AS
SELECT c.clientId,
       c.userId,
       u.firstName,
       u.lastName,
       u.email,
       c.address,
       c.phone,
       c.auditCreateDate
FROM client c
         JOIN app_user u ON c.userId = u.userId
WHERE c.state != 0;

-- Pet Details
CREATE OR REPLACE VIEW view_pet_details AS
SELECT p.petId,
       p.name AS petName,
       p.type,
       p.breed,
       p.age,
       c.clientId,
       c.address,
       c.phone
FROM pet p
         JOIN client c ON p.clientId = c.clientId
WHERE p.state != 0;

-- Appointment Information
CREATE OR REPLACE VIEW view_appointment_info AS
SELECT a.appointmentId,
       a.appointmentDate,
       a.reason,
       p.name      AS petName,
       m.specialty AS medicSpecialty,
       m.phone     AS medicPhone
FROM appointment a
         JOIN pet p ON a.petId = p.petId
         JOIN medic m ON a.medicId = m.medicId
WHERE a.state != 0;

-- Vaccine Information
CREATE OR REPLACE VIEW view_vaccine_info AS
SELECT av.appliedVaccineId,
       av.applicationDate,
       p.name AS petName,
       v.vaccineName,
       v.description
FROM appliedVaccine av
         JOIN pet p ON av.petId = p.petId
         JOIN vaccine v ON av.vaccineId = v.vaccineId
WHERE av.state != 0;

-- Product Inventory
CREATE OR REPLACE VIEW view_product_inventory AS
SELECT pr.productId,
       pr.name AS productName,
       pr.description,
       pr.price,
       i.quantity,
       i.updateDate
FROM product pr
         JOIN inventory i ON pr.productId = i.productId
WHERE pr.state != 0
  AND i.state != 0;

-- Sale Summary
CREATE OR REPLACE VIEW view_sale_summary AS
SELECT s.saleId,
       s.saleDate,
       c.clientId,
       c.address,
       SUM(sd.quantity * sd.price) AS totalSale
FROM sale s
         JOIN client c ON s.clientId = c.clientId
         JOIN saleDetail sd ON s.saleId = sd.saleId
WHERE s.state != 0
GROUP BY s.saleId, s.saleDate, c.clientId, c.address;

-- Payment Summary
CREATE OR REPLACE VIEW view_payment_summary AS
SELECT paymentId, saleId, amount, paymentDate, paymentType
FROM payment
WHERE state != 0;


-- Purchase Order Summary
CREATE OR REPLACE VIEW view_purchase_order_summary AS
SELECT po.purchaseOrderId,
       po.orderDate,
       po.status,
       s.supplierId,
       s.name AS supplierName
FROM purchaseOrder po
         JOIN supplier s ON po.supplierId = s.supplierId
WHERE po.state != 0;

-- Product Category Mapping
CREATE OR REPLACE VIEW view_product_category_mapping AS
SELECT pcr.productId,
       pr.name AS productName,
       pcr.categoryId,
       pc.categoryName
FROM productCategoryRelation pcr
         JOIN product pr ON pcr.productId = pr.productId
         JOIN productCategory pc ON pcr.categoryId = pc.categoryId
WHERE pcr.state != 0;
