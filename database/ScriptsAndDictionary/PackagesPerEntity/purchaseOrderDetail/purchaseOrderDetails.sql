CREATE OR REPLACE PACKAGE pkg_purchaseOrderDetail AS
    PROCEDURE sp_create_purchaseOrderDetail(
        p_purchaseOrderId IN purchaseOrderDetail.purchaseOrderId%TYPE,
        p_productId IN purchaseOrderDetail.productId%TYPE,
        p_quantity IN purchaseOrderDetail.quantity%TYPE,
        p_unitPrice IN purchaseOrderDetail.unitPrice%TYPE,
        p_state IN purchaseOrderDetail.state%TYPE DEFAULT 1,
        p_auditCreateUser IN purchaseOrderDetail.auditCreateUser%TYPE
    );

    PROCEDURE sp_get_purchaseOrderDetail(
        p_purchaseOrderId IN purchaseOrderDetail.purchaseOrderId%TYPE,
        p_productId IN purchaseOrderDetail.productId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_purchaseOrderDetail(
        p_purchaseOrderId IN purchaseOrderDetail.purchaseOrderId%TYPE,
        p_productId IN purchaseOrderDetail.productId%TYPE,
        p_quantity IN purchaseOrderDetail.quantity%TYPE,
        p_unitPrice IN purchaseOrderDetail.unitPrice%TYPE,
        p_state IN purchaseOrderDetail.state%TYPE,
        p_auditUpdateUser IN purchaseOrderDetail.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_purchaseOrderDetail(
        p_purchaseOrderId IN purchaseOrderDetail.purchaseOrderId%TYPE,
        p_productId IN purchaseOrderDetail.productId%TYPE,
        p_auditDeleteUser IN purchaseOrderDetail.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_purchaseOrderDetails(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_purchaseOrderDetail;
/

CREATE OR REPLACE PACKAGE BODY pkg_purchaseOrderDetail AS

    PROCEDURE sp_create_purchaseOrderDetail(
        p_purchaseOrderId IN purchaseOrderDetail.purchaseOrderId%TYPE,
        p_productId IN purchaseOrderDetail.productId%TYPE,
        p_quantity IN purchaseOrderDetail.quantity%TYPE,
        p_unitPrice IN purchaseOrderDetail.unitPrice%TYPE,
        p_state IN purchaseOrderDetail.state%TYPE DEFAULT 1,
        p_auditCreateUser IN purchaseOrderDetail.auditCreateUser%TYPE
    )
        IS
    BEGIN
        INSERT INTO purchaseOrderDetail (purchaseOrderId, productId, quantity, unitPrice, state, auditCreateUser,
                                         auditCreateDate)
        VALUES (p_purchaseOrderId, p_productId, p_quantity, p_unitPrice, p_state, p_auditCreateUser, SYSTIMESTAMP);
        COMMIT;
    END sp_create_purchaseOrderDetail;

    PROCEDURE sp_get_purchaseOrderDetail(
        p_purchaseOrderId IN purchaseOrderDetail.purchaseOrderId%TYPE,
        p_productId IN purchaseOrderDetail.productId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT purchaseOrderId,
                   productId,
                   quantity,
                   unitPrice,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM purchaseOrderDetail
            WHERE purchaseOrderId = p_purchaseOrderId
              AND productId = p_productId;
    END sp_get_purchaseOrderDetail;

    PROCEDURE sp_update_purchaseOrderDetail(
        p_purchaseOrderId IN purchaseOrderDetail.purchaseOrderId%TYPE,
        p_productId IN purchaseOrderDetail.productId%TYPE,
        p_quantity IN purchaseOrderDetail.quantity%TYPE,
        p_unitPrice IN purchaseOrderDetail.unitPrice%TYPE,
        p_state IN purchaseOrderDetail.state%TYPE,
        p_auditUpdateUser IN purchaseOrderDetail.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE purchaseOrderDetail
        SET quantity        = p_quantity,
            unitPrice       = p_unitPrice,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE purchaseOrderId = p_purchaseOrderId
          AND productId = p_productId;
        COMMIT;
    END sp_update_purchaseOrderDetail;

    PROCEDURE sp_delete_purchaseOrderDetail(
        p_purchaseOrderId IN purchaseOrderDetail.purchaseOrderId%TYPE,
        p_productId IN purchaseOrderDetail.productId%TYPE,
        p_auditDeleteUser IN purchaseOrderDetail.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE purchaseOrderDetail
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE purchaseOrderId = p_purchaseOrderId
          AND productId = p_productId;
        COMMIT;
    END sp_delete_purchaseOrderDetail;

    PROCEDURE sp_get_all_purchaseOrderDetails(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT purchaseOrderId,
                   productId,
                   quantity,
                   unitPrice,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM purchaseOrderDetail;
    END sp_get_all_purchaseOrderDetails;

END pkg_purchaseOrderDetail;
/
