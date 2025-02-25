--------------------------------------------------------------------
-- Validate the email format, format the username, and hash the password
--------------------------------------------------------------------
CREATE OR REPLACE TRIGGER trg_app_user_biu
    BEFORE INSERT OR UPDATE
    ON app_user
    FOR EACH ROW
DECLARE
    dummy NUMBER;
BEGIN
    dummy := fn_validate_email(:NEW.email);

    :NEW.username := fn_format_username_total(:NEW.username);

    IF LENGTH(:NEW.password) < 64 THEN
        :NEW.password := fn_hash_password(:NEW.password);
    END IF;

    IF :NEW.auditCreateDate IS NULL THEN
        :NEW.auditCreateDate := SYSTIMESTAMP;
    END IF;
END;
/

--------------------------------------------------------------------
-- Validate age, calculate the pet's category
--------------------------------------------------------------------
CREATE OR REPLACE TRIGGER trg_pet_biu
    BEFORE INSERT OR UPDATE
    ON pet
    FOR EACH ROW
DECLARE
    v_category VARCHAR2(20);
BEGIN
    :NEW.age := fn_check_pet_age(:NEW.age);

    v_category := fn_calculate_pet_age_category(:NEW.age);
END;
/
--------------------------------------------------------------------
-- Prevents an insert or update if the sale total isn't greater than 0
--------------------------------------------------------------------
CREATE OR REPLACE TRIGGER trg_saleDetail_ai
    AFTER INSERT OR UPDATE
    ON saleDetail
    FOR EACH ROW
DECLARE
    v_total NUMBER;
BEGIN
    v_total := fn_calculate_sale_total(:NEW.saleId);
END;
/

--------------------------------------------------------------------
-- Compute inventory value, and see if it needed to reorder item.
--------------------------------------------------------------------
CREATE OR REPLACE TRIGGER trg_inventory_ai
    AFTER INSERT OR UPDATE
    ON inventory
    FOR EACH ROW
DECLARE
    v_value  NUMBER;
    v_status VARCHAR2(20);
BEGIN
    v_value := fn_get_inventory_value(:NEW.productId);

    v_status := fn_inventory_reorder_status(:NEW.productId);
END;
/

--------------------------------------------------------------------
-- Translate order status code, replace the status code with it's full description.
--------------------------------------------------------------------
CREATE OR REPLACE TRIGGER trg_purchaseOrder_biu
    BEFORE INSERT OR UPDATE
    ON purchaseOrder
    FOR EACH ROW
DECLARE
    v_status VARCHAR2(50);
BEGIN
    v_status := fn_get_order_status(:NEW.status);
    :NEW.status := v_status;
END;
/

--------------------------------------------------------------------
-- Validate than supplier contact and phone isn't null
--------------------------------------------------------------------
CREATE OR REPLACE TRIGGER trg_supplier_biu
    BEFORE INSERT OR UPDATE
    ON supplier
    FOR EACH ROW
DECLARE
    dummy NUMBER;
BEGIN
    dummy := fn_validate_supplier_contact(:NEW.contact, :NEW.phone);
END;
/

--------------------------------------------------------------------
-- Cancel the transaction if the appointment date isn't a past date
--------------------------------------------------------------------
CREATE OR REPLACE TRIGGER trg_appointment_biu
    BEFORE INSERT OR UPDATE
    ON appointment
    FOR EACH ROW
BEGIN
    :NEW.appointmentDate := fn_check_appointment_date(:NEW.appointmentDate);
END;
/

--------------------------------------------------------------------
-- If the payment amount is negative it cancel the transaction
--------------------------------------------------------------------
CREATE OR REPLACE TRIGGER trg_payment_biu
    BEFORE INSERT OR UPDATE
    ON payment
    FOR EACH ROW
BEGIN
    :NEW.amount := fn_validate_payment_amount(:NEW.amount);
END;
/

--------------------------------------------------------------------
-- If negative, cancel the transaction
--------------------------------------------------------------------
CREATE OR REPLACE TRIGGER trg_purchaseOrderDetail_ai
    AFTER INSERT OR UPDATE
    ON purchaseOrderDetail
    FOR EACH ROW
DECLARE
    v_total NUMBER;
BEGIN
    v_total := fn_compute_purchase_order_total(:NEW.purchaseOrderId);
END;
/

--------------------------------------------------------------------
-- If the product price exceeds 150$, apply a 5% discount, and also calculate the price with tax
--------------------------------------------------------------------

CREATE OR REPLACE TRIGGER trg_product_biu
    BEFORE INSERT OR UPDATE
    ON product
    FOR EACH ROW
DECLARE
    v_discounted_price NUMBER;
    v_tax              NUMBER;
BEGIN
    IF :NEW.price > 150 THEN
        v_discounted_price := fn_apply_discount(:NEW.price, 5);
        :NEW.price := v_discounted_price;
    END IF;

    v_tax := fn_calculate_tax(:NEW.price, 0.13);
END;
/

