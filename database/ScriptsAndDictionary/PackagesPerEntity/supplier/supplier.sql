CREATE OR REPLACE PACKAGE pkg_supplier AS
    PROCEDURE sp_create_supplier(
        p_name IN supplier.name%TYPE,
        p_contact IN supplier.contact%TYPE,
        p_phone IN supplier.phone%TYPE,
        p_address IN supplier.address%TYPE,
        p_state IN supplier.state%TYPE DEFAULT 1,
        p_auditCreateUser IN supplier.auditCreateUser%TYPE,
        p_supplierId OUT supplier.supplierId%TYPE
    );

    PROCEDURE sp_get_supplier(
        p_supplierId IN supplier.supplierId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_supplier(
        p_supplierId IN supplier.supplierId%TYPE,
        p_name IN supplier.name%TYPE,
        p_contact IN supplier.contact%TYPE,
        p_phone IN supplier.phone%TYPE,
        p_address IN supplier.address%TYPE,
        p_state IN supplier.state%TYPE,
        p_auditUpdateUser IN supplier.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_supplier(
        p_supplierId IN supplier.supplierId%TYPE,
        p_auditDeleteUser IN supplier.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_suppliers(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_supplier;
/

CREATE OR REPLACE PACKAGE BODY pkg_supplier AS

    PROCEDURE sp_create_supplier(
        p_name IN supplier.name%TYPE,
        p_contact IN supplier.contact%TYPE,
        p_phone IN supplier.phone%TYPE,
        p_address IN supplier.address%TYPE,
        p_state IN supplier.state%TYPE DEFAULT 1,
        p_auditCreateUser IN supplier.auditCreateUser%TYPE,
        p_supplierId OUT supplier.supplierId%TYPE
    )
        IS
    BEGIN
        INSERT INTO supplier (name, contact, phone, address, state, auditCreateUser, auditCreateDate)
        VALUES (p_name, p_contact, p_phone, p_address, p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING supplierId INTO p_supplierId;
        COMMIT;
    END sp_create_supplier;

    PROCEDURE sp_get_supplier(
        p_supplierId IN supplier.supplierId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT supplierId,
                   name,
                   contact,
                   phone,
                   address,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM supplier
            WHERE supplierId = p_supplierId;
    END sp_get_supplier;

    PROCEDURE sp_update_supplier(
        p_supplierId IN supplier.supplierId%TYPE,
        p_name IN supplier.name%TYPE,
        p_contact IN supplier.contact%TYPE,
        p_phone IN supplier.phone%TYPE,
        p_address IN supplier.address%TYPE,
        p_state IN supplier.state%TYPE,
        p_auditUpdateUser IN supplier.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE supplier
        SET name            = p_name,
            contact         = p_contact,
            phone           = p_phone,
            address         = p_address,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE supplierId = p_supplierId;
        COMMIT;
    END sp_update_supplier;

    PROCEDURE sp_delete_supplier(
        p_supplierId IN supplier.supplierId%TYPE,
        p_auditDeleteUser IN supplier.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE supplier
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE supplierId = p_supplierId;
        COMMIT;
    END sp_delete_supplier;

    PROCEDURE sp_get_all_suppliers(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT supplierId,
                   name,
                   contact,
                   phone,
                   address,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM supplier;
    END sp_get_all_suppliers;

END pkg_supplier;
/
