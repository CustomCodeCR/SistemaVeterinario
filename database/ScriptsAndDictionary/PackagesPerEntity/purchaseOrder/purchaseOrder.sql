CREATE OR REPLACE PACKAGE pkg_purchaseOrder AS
    PROCEDURE sp_create_purchaseOrder(
        p_supplierId IN purchaseOrder.supplierId%TYPE,
        p_orderDate IN purchaseOrder.orderDate%TYPE,
        p_status IN purchaseOrder.status%TYPE,
        p_state IN purchaseOrder.state%TYPE DEFAULT 1,
        p_auditCreateUser IN purchaseOrder.auditCreateUser%TYPE,
        p_purchaseOrderId OUT purchaseOrder.purchaseOrderId%TYPE
    );

    PROCEDURE sp_get_purchaseOrder(
        p_purchaseOrderId IN purchaseOrder.purchaseOrderId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_purchaseOrder(
        p_purchaseOrderId IN purchaseOrder.purchaseOrderId%TYPE,
        p_supplierId IN purchaseOrder.supplierId%TYPE,
        p_orderDate IN purchaseOrder.orderDate%TYPE,
        p_status IN purchaseOrder.status%TYPE,
        p_state IN purchaseOrder.state%TYPE,
        p_auditUpdateUser IN purchaseOrder.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_purchaseOrder(
        p_purchaseOrderId IN purchaseOrder.purchaseOrderId%TYPE,
        p_auditDeleteUser IN purchaseOrder.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_purchaseOrders(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_purchaseOrder;
/

CREATE OR REPLACE PACKAGE BODY pkg_purchaseOrder AS

    PROCEDURE sp_create_purchaseOrder(
        p_supplierId IN purchaseOrder.supplierId%TYPE,
        p_orderDate IN purchaseOrder.orderDate%TYPE,
        p_status IN purchaseOrder.status%TYPE,
        p_state IN purchaseOrder.state%TYPE DEFAULT 1,
        p_auditCreateUser IN purchaseOrder.auditCreateUser%TYPE,
        p_purchaseOrderId OUT purchaseOrder.purchaseOrderId%TYPE
    )
        IS
    BEGIN
        INSERT INTO purchaseOrder (supplierId, orderDate, status, state, auditCreateUser, auditCreateDate)
        VALUES (p_supplierId, p_orderDate, p_status, p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING purchaseOrderId INTO p_purchaseOrderId;
        COMMIT;
    END sp_create_purchaseOrder;

    PROCEDURE sp_get_purchaseOrder(
        p_purchaseOrderId IN purchaseOrder.purchaseOrderId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT purchaseOrderId,
                   supplierId,
                   orderDate,
                   status,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM purchaseOrder
            WHERE purchaseOrderId = p_purchaseOrderId;
    END sp_get_purchaseOrder;

    PROCEDURE sp_update_purchaseOrder(
        p_purchaseOrderId IN purchaseOrder.purchaseOrderId%TYPE,
        p_supplierId IN purchaseOrder.supplierId%TYPE,
        p_orderDate IN purchaseOrder.orderDate%TYPE,
        p_status IN purchaseOrder.status%TYPE,
        p_state IN purchaseOrder.state%TYPE,
        p_auditUpdateUser IN purchaseOrder.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE purchaseOrder
        SET supplierId      = p_supplierId,
            orderDate       = p_orderDate,
            status          = p_status,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE purchaseOrderId = p_purchaseOrderId;
        COMMIT;
    END sp_update_purchaseOrder;

    PROCEDURE sp_delete_purchaseOrder(
        p_purchaseOrderId IN purchaseOrder.purchaseOrderId%TYPE,
        p_auditDeleteUser IN purchaseOrder.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE purchaseOrder
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE purchaseOrderId = p_purchaseOrderId;
        COMMIT;
    END sp_delete_purchaseOrder;

    PROCEDURE sp_get_all_purchaseOrders(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT purchaseOrderId,
                   supplierId,
                   orderDate,
                   status,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM purchaseOrder;
    END sp_get_all_purchaseOrders;

END pkg_purchaseOrder;
/
