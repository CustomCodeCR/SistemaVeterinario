CREATE OR REPLACE PACKAGE pkg_saleDetail AS
    PROCEDURE sp_create_saleDetail(
        p_saleId IN saleDetail.saleId%TYPE,
        p_productId IN saleDetail.productId%TYPE,
        p_quantity IN saleDetail.quantity%TYPE,
        p_price IN saleDetail.price%TYPE,
        p_state IN saleDetail.state%TYPE DEFAULT 1,
        p_auditCreateUser IN saleDetail.auditCreateUser%TYPE
    );

    PROCEDURE sp_get_saleDetail(
        p_saleId IN saleDetail.saleId%TYPE,
        p_productId IN saleDetail.productId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_saleDetail(
        p_saleId IN saleDetail.saleId%TYPE,
        p_productId IN saleDetail.productId%TYPE,
        p_quantity IN saleDetail.quantity%TYPE,
        p_price IN saleDetail.price%TYPE,
        p_state IN saleDetail.state%TYPE,
        p_auditUpdateUser IN saleDetail.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_saleDetail(
        p_saleId IN saleDetail.saleId%TYPE,
        p_productId IN saleDetail.productId%TYPE,
        p_auditDeleteUser IN saleDetail.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_saleDetails(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_saleDetail;
/

CREATE OR REPLACE PACKAGE BODY pkg_saleDetail AS

    PROCEDURE sp_create_saleDetail(
        p_saleId IN saleDetail.saleId%TYPE,
        p_productId IN saleDetail.productId%TYPE,
        p_quantity IN saleDetail.quantity%TYPE,
        p_price IN saleDetail.price%TYPE,
        p_state IN saleDetail.state%TYPE DEFAULT 1,
        p_auditCreateUser IN saleDetail.auditCreateUser%TYPE
    )
        IS
    BEGIN
        INSERT INTO saleDetail (saleId, productId, quantity, price, state, auditCreateUser, auditCreateDate)
        VALUES (p_saleId, p_productId, p_quantity, p_price, p_state, p_auditCreateUser, SYSTIMESTAMP);
        COMMIT;
    END sp_create_saleDetail;

    PROCEDURE sp_get_saleDetail(
        p_saleId IN saleDetail.saleId%TYPE,
        p_productId IN saleDetail.productId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT saleId,
                   productId,
                   quantity,
                   price,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM saleDetail
            WHERE saleId = p_saleId
              AND productId = p_productId;
    END sp_get_saleDetail;

    PROCEDURE sp_update_saleDetail(
        p_saleId IN saleDetail.saleId%TYPE,
        p_productId IN saleDetail.productId%TYPE,
        p_quantity IN saleDetail.quantity%TYPE,
        p_price IN saleDetail.price%TYPE,
        p_state IN saleDetail.state%TYPE,
        p_auditUpdateUser IN saleDetail.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE saleDetail
        SET quantity        = p_quantity,
            price           = p_price,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE saleId = p_saleId
          AND productId = p_productId;
        COMMIT;
    END sp_update_saleDetail;

    PROCEDURE sp_delete_saleDetail(
        p_saleId IN saleDetail.saleId%TYPE,
        p_productId IN saleDetail.productId%TYPE,
        p_auditDeleteUser IN saleDetail.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE saleDetail
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE saleId = p_saleId
          AND productId = p_productId;
        COMMIT;
    END sp_delete_saleDetail;

    PROCEDURE sp_get_all_saleDetails(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT saleId,
                   productId,
                   quantity,
                   price,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM saleDetail;
    END sp_get_all_saleDetails;

END pkg_saleDetail;
/
