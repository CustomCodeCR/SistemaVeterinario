CREATE OR REPLACE PACKAGE pkg_product AS
    PROCEDURE sp_create_product(
        p_name IN product.name%TYPE,
        p_description IN product.description%TYPE,
        p_price IN product.price%TYPE,
        p_state IN product.state%TYPE DEFAULT 1,
        p_auditCreateUser IN product.auditCreateUser%TYPE,
        p_productId OUT product.productId%TYPE
    );

    PROCEDURE sp_get_product(
        p_productId IN product.productId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_product(
        p_productId IN product.productId%TYPE,
        p_name IN product.name%TYPE,
        p_description IN product.description%TYPE,
        p_price IN product.price%TYPE,
        p_state IN product.state%TYPE,
        p_auditUpdateUser IN product.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_product(
        p_productId IN product.productId%TYPE,
        p_auditDeleteUser IN product.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_products(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_product;
/

CREATE OR REPLACE PACKAGE BODY pkg_product AS

    PROCEDURE sp_create_product(
        p_name IN product.name%TYPE,
        p_description IN product.description%TYPE,
        p_price IN product.price%TYPE,
        p_state IN product.state%TYPE DEFAULT 1,
        p_auditCreateUser IN product.auditCreateUser%TYPE,
        p_productId OUT product.productId%TYPE
    )
        IS
    BEGIN
        INSERT INTO product (name, description, price, state, auditCreateUser, auditCreateDate)
        VALUES (p_name, p_description, p_price, p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING productId INTO p_productId;
        COMMIT;
    END sp_create_product;

    PROCEDURE sp_get_product(
        p_productId IN product.productId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT productId,
                   name,
                   description,
                   price,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM product
            WHERE productId = p_productId;
    END sp_get_product;

    PROCEDURE sp_update_product(
        p_productId IN product.productId%TYPE,
        p_name IN product.name%TYPE,
        p_description IN product.description%TYPE,
        p_price IN product.price%TYPE,
        p_state IN product.state%TYPE,
        p_auditUpdateUser IN product.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE product
        SET name            = p_name,
            description     = p_description,
            price           = p_price,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE productId = p_productId;
        COMMIT;
    END sp_update_product;

    PROCEDURE sp_delete_product(
        p_productId IN product.productId%TYPE,
        p_auditDeleteUser IN product.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE product
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE productId = p_productId;
        COMMIT;
    END sp_delete_product;

    PROCEDURE sp_get_all_products(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT productId,
                   name,
                   description,
                   price,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM product;
    END sp_get_all_products;

END pkg_product;
/
