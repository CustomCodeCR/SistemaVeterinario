CREATE OR REPLACE PACKAGE pkg_inventory AS
    PROCEDURE sp_create_inventory(
        p_productId IN inventory.productId%TYPE,
        p_quantity IN inventory.quantity%TYPE,
        p_updateDate IN inventory.updateDate%TYPE,
        p_state IN inventory.state%TYPE DEFAULT 1,
        p_auditCreateUser IN inventory.auditCreateUser%TYPE,
        p_inventoryId OUT inventory.inventoryId%TYPE
    );

    PROCEDURE sp_get_inventory(
        p_inventoryId IN inventory.inventoryId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_inventory(
        p_inventoryId IN inventory.inventoryId%TYPE,
        p_productId IN inventory.productId%TYPE,
        p_quantity IN inventory.quantity%TYPE,
        p_updateDate IN inventory.updateDate%TYPE,
        p_state IN inventory.state%TYPE,
        p_auditUpdateUser IN inventory.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_inventory(
        p_inventoryId IN inventory.inventoryId%TYPE,
        p_auditDeleteUser IN inventory.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_inventories(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_inventory;
/

CREATE OR REPLACE PACKAGE BODY pkg_inventory AS

    PROCEDURE sp_create_inventory(
        p_productId IN inventory.productId%TYPE,
        p_quantity IN inventory.quantity%TYPE,
        p_updateDate IN inventory.updateDate%TYPE,
        p_state IN inventory.state%TYPE DEFAULT 1,
        p_auditCreateUser IN inventory.auditCreateUser%TYPE,
        p_inventoryId OUT inventory.inventoryId%TYPE
    )
        IS
    BEGIN
        INSERT INTO inventory (productId, quantity, updateDate, state, auditCreateUser, auditCreateDate)
        VALUES (p_productId, p_quantity, p_updateDate, p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING inventoryId INTO p_inventoryId;
        COMMIT;
    END sp_create_inventory;

    PROCEDURE sp_get_inventory(
        p_inventoryId IN inventory.inventoryId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT inventoryId,
                   productId,
                   quantity,
                   updateDate,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM inventory
            WHERE inventoryId = p_inventoryId;
    END sp_get_inventory;

    PROCEDURE sp_update_inventory(
        p_inventoryId IN inventory.inventoryId%TYPE,
        p_productId IN inventory.productId%TYPE,
        p_quantity IN inventory.quantity%TYPE,
        p_updateDate IN inventory.updateDate%TYPE,
        p_state IN inventory.state%TYPE,
        p_auditUpdateUser IN inventory.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE inventory
        SET productId       = p_productId,
            quantity        = p_quantity,
            updateDate      = p_updateDate,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE inventoryId = p_inventoryId;
        COMMIT;
    END sp_update_inventory;

    PROCEDURE sp_delete_inventory(
        p_inventoryId IN inventory.inventoryId%TYPE,
        p_auditDeleteUser IN inventory.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE inventory
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE inventoryId = p_inventoryId;
        COMMIT;
    END sp_delete_inventory;

    PROCEDURE sp_get_all_inventories(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT inventoryId,
                   productId,
                   quantity,
                   updateDate,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM inventory;
    END sp_get_all_inventories;

END pkg_inventory;
/
