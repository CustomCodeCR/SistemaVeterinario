--------------------------------------------------------------------
-- The idea is validating through a regex command if the email has the proper structure.
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_validate_email(
    p_email IN VARCHAR2
) RETURN NUMBER IS
BEGIN
    IF
        REGEXP_LIKE(p_email, '^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$') THEN
        RETURN 1;
    ELSE
        RAISE_APPLICATION_ERROR(-20001, 'Invalid email format.');
    END IF;
END fn_validate_email;
/

--------------------------------------------------------------------
-- Basically is encrypting the password through a built-in function.
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_hash_password(
    p_password IN VARCHAR2
) RETURN VARCHAR2 IS
    v_hash VARCHAR2(64);
BEGIN
    SELECT STANDARD_HASH(p_password, 'SHA256')
    INTO v_hash
    FROM dual;
    RETURN v_hash;
END fn_hash_password;
/

--------------------------------------------------------------------
-- Checks if the pet doesn't have less than 0 or more than 50 years.
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_check_pet_age(
    p_age IN NUMBER
) RETURN NUMBER IS
BEGIN
    IF
        p_age < 0 OR p_age > 50 THEN
        RAISE_APPLICATION_ERROR(-20002, 'Pet age must be between 0 and 50.');
    END IF;
    RETURN p_age;
END fn_check_pet_age;
/

--------------------------------------------------------------------
-- Allocate a category based in age.
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_calculate_pet_age_category(
    p_age IN NUMBER
) RETURN VARCHAR2 IS
BEGIN
    IF
        p_age < 2 THEN
        RETURN 'Young';
    ELSIF
        p_age BETWEEN 2 AND 7 THEN
        RETURN 'Adult';
    ELSE
        RETURN 'Senior';
    END IF;
END fn_calculate_pet_age_category;
/

--------------------------------------------------------------------
-- Raise an Error if the sale isn't greater than 0
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_calculate_sale_total(
    p_saleId IN NUMBER
) RETURN NUMBER IS
    v_total NUMBER;
BEGIN
    SELECT NVL(SUM(quantity * price), 0)
    INTO v_total
    FROM saleDetail
    WHERE saleId = p_saleId;
    IF
        v_total <= 0 THEN
        RAISE_APPLICATION_ERROR(-20003, 'Sale total must be greater than zero.');
    END IF;
    RETURN v_total;
END fn_calculate_sale_total;
/

--------------------------------------------------------------------
-- Know the current inventory value of a product
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_get_inventory_value(
    p_productId IN NUMBER
) RETURN NUMBER IS
    v_value NUMBER;
BEGIN
    SELECT NVL(SUM(i.quantity * p.price), 0)
    INTO v_value
    FROM inventory i
             JOIN product p ON i.productId = p.productId
    WHERE i.productId = p_productId;
    RETURN v_value;
END fn_get_inventory_value;
/

--------------------------------------------------------------------
-- Basically translate the status
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_get_order_status(
    p_status IN VARCHAR2
) RETURN VARCHAR2 IS
    v_status VARCHAR2(50);
BEGIN
    CASE UPPER(p_status)
        WHEN 'P' THEN v_status := 'Pending';
        WHEN 'C' THEN v_status := 'Confirmed';
        WHEN 'S' THEN v_status := 'Shipped';
        WHEN 'D' THEN v_status := 'Delivered';
        ELSE RAISE_APPLICATION_ERROR(-20005, 'Invalid purchase order status code.');
        END CASE;
    RETURN v_status;
END fn_get_order_status;
/

--------------------------------------------------------------------
-- A supplier should have a contact and phone
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_validate_supplier_contact(
    p_contact IN VARCHAR2,
    p_phone IN VARCHAR2
) RETURN NUMBER IS
BEGIN
    IF
        p_contact IS NULL OR p_phone IS NULL THEN
        RAISE_APPLICATION_ERROR(-20006, 'Supplier contact and phone must be provided.');
    END IF;
    RETURN 1;
END fn_validate_supplier_contact;
/

--------------------------------------------------------------------
-- The appointment shouldn't be in the past
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_check_appointment_date(
    p_date IN DATE
) RETURN DATE IS
BEGIN
    IF
        TRUNC(p_date) < TRUNC(SYSDATE) THEN
        RAISE_APPLICATION_ERROR(-20007, 'Appointment date cannot be in the past.');
    END IF;
    RETURN p_date;
END fn_check_appointment_date;
/

--------------------------------------------------------------------
-- Payment should be positive
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_validate_payment_amount(
    p_amount IN NUMBER
) RETURN NUMBER IS
BEGIN
    IF
        p_amount <= 0 THEN
        RAISE_APPLICATION_ERROR(-20008, 'Payment amount must be positive.');
    END IF;
    RETURN p_amount;
END fn_validate_payment_amount;
/

--------------------------------------------------------------------
-- Purchase should be greater than 0
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_compute_purchase_order_total(
    p_purchaseOrderId IN NUMBER
) RETURN NUMBER IS
    v_total NUMBER;
BEGIN
    SELECT NVL(SUM(quantity * unitPrice), 0)
    INTO v_total
    FROM purchaseOrderDetail
    WHERE purchaseOrderId = p_purchaseOrderId;
    IF
        v_total <= 0 THEN
        RAISE_APPLICATION_ERROR(-20009, 'Purchase order total must be greater than zero.');
    END IF;
    RETURN v_total;
END fn_compute_purchase_order_total;
/

--------------------------------------------------------------------
-- It formats the username
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_format_username_total(
    p_username IN VARCHAR2
) RETURN VARCHAR2 IS
BEGIN
    IF
        p_username IS NULL THEN
        RETURN 'default_user_' || TO_CHAR(SYSDATE, 'YYYYMMDDHH24MISS');
    ELSE
        RETURN LOWER(TRIM(p_username));
    END IF;
END fn_format_username_total;
/

--------------------------------------------------------------------
-- If quantity is less than 10 per product, it raises to reorder
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_inventory_reorder_status(
    p_productId IN NUMBER
) RETURN VARCHAR2 IS
    v_quantity NUMBER;
BEGIN
    SELECT NVL(SUM(quantity), 0)
    INTO v_quantity
    FROM inventory
    WHERE productId = p_productId;

    IF
        v_quantity < 10 THEN
        RETURN 'Reorder';
    ELSE
        RETURN 'OK';
    END IF;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        RETURN 'Not Found';
END fn_inventory_reorder_status;
/

--------------------------------------------------------------------
-- Calculates tax
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_calculate_tax(
    p_price IN NUMBER,
    p_tax_rate IN NUMBER
) RETURN NUMBER IS
BEGIN
    RETURN ROUND(p_price * p_tax_rate, 2);
END fn_calculate_tax;
/

--------------------------------------------------------------------
-- Apply a discount
--------------------------------------------------------------------
CREATE OR REPLACE FUNCTION fn_apply_discount(
    p_price IN NUMBER,
    p_discount_percent IN NUMBER
) RETURN NUMBER IS
    v_discounted_price NUMBER;
BEGIN
    v_discounted_price
        := p_price - (p_price * p_discount_percent / 100);
    RETURN ROUND(v_discounted_price, 2);
END fn_apply_discount;
/


