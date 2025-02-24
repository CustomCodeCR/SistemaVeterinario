CREATE OR REPLACE PACKAGE pkg_sale AS
    PROCEDURE sp_create_sale(
        p_saleDate IN sale.saleDate%TYPE,
        p_clientId IN sale.clientId%TYPE,
        p_state IN sale.state%TYPE DEFAULT 1,
        p_auditCreateUser IN sale.auditCreateUser%TYPE,
        p_saleId OUT sale.saleId%TYPE
    );

    PROCEDURE sp_get_sale(
        p_saleId IN sale.saleId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_sale(
        p_saleId IN sale.saleId%TYPE,
        p_saleDate IN sale.saleDate%TYPE,
        p_clientId IN sale.clientId%TYPE,
        p_state IN sale.state%TYPE,
        p_auditUpdateUser IN sale.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_sale(
        p_saleId IN sale.saleId%TYPE,
        p_auditDeleteUser IN sale.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_sales(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_sale;
/

CREATE OR REPLACE PACKAGE BODY pkg_sale AS

    PROCEDURE sp_create_sale(
        p_saleDate IN sale.saleDate%TYPE,
        p_clientId IN sale.clientId%TYPE,
        p_state IN sale.state%TYPE DEFAULT 1,
        p_auditCreateUser IN sale.auditCreateUser%TYPE,
        p_saleId OUT sale.saleId%TYPE
    )
        IS
    BEGIN
        INSERT INTO sale (saleDate, clientId, state, auditCreateUser, auditCreateDate)
        VALUES (p_saleDate, p_clientId, p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING saleId INTO p_saleId;
        COMMIT;
    END sp_create_sale;

    PROCEDURE sp_get_sale(
        p_saleId IN sale.saleId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT saleId,
                   saleDate,
                   clientId,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM sale
            WHERE saleId = p_saleId;
    END sp_get_sale;

    PROCEDURE sp_update_sale(
        p_saleId IN sale.saleId%TYPE,
        p_saleDate IN sale.saleDate%TYPE,
        p_clientId IN sale.clientId%TYPE,
        p_state IN sale.state%TYPE,
        p_auditUpdateUser IN sale.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE sale
        SET saleDate        = p_saleDate,
            clientId        = p_clientId,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE saleId = p_saleId;
        COMMIT;
    END sp_update_sale;

    PROCEDURE sp_delete_sale(
        p_saleId IN sale.saleId%TYPE,
        p_auditDeleteUser IN sale.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE sale
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE saleId = p_saleId;
        COMMIT;
    END sp_delete_sale;

    PROCEDURE sp_get_all_sales(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT saleId,
                   saleDate,
                   clientId,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM sale;
    END sp_get_all_sales;

END pkg_sale;
/
