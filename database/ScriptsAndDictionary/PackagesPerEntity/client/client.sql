CREATE OR REPLACE PACKAGE pkg_client AS
    PROCEDURE sp_create_client(
        p_userId IN client.userId%TYPE,
        p_address IN client.address%TYPE,
        p_phone IN client.phone%TYPE,
        p_state IN client.state%TYPE DEFAULT 1,
        p_auditCreateUser IN client.auditCreateUser%TYPE,
        p_clientId OUT client.clientId%TYPE
    );

    PROCEDURE sp_get_client(
        p_clientId IN client.clientId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_client(
        p_clientId IN client.clientId%TYPE,
        p_userId IN client.userId%TYPE,
        p_address IN client.address%TYPE,
        p_phone IN client.phone%TYPE,
        p_state IN client.state%TYPE,
        p_auditUpdateUser IN client.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_client(
        p_clientId IN client.clientId%TYPE,
        p_auditDeleteUser IN client.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_clients(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_client;
/

CREATE OR REPLACE PACKAGE BODY pkg_client AS

    PROCEDURE sp_create_client(
        p_userId IN client.userId%TYPE,
        p_address IN client.address%TYPE,
        p_phone IN client.phone%TYPE,
        p_state IN client.state%TYPE DEFAULT 1,
        p_auditCreateUser IN client.auditCreateUser%TYPE,
        p_clientId OUT client.clientId%TYPE
    )
        IS
    BEGIN
        INSERT INTO client (userId, address, phone, state, auditCreateUser, auditCreateDate)
        VALUES (p_userId, p_address, p_phone, p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING clientId INTO p_clientId;
        COMMIT;
    END sp_create_client;

    PROCEDURE sp_get_client(
        p_clientId IN client.clientId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT clientId,
                   userId,
                   address,
                   phone,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM client
            WHERE clientId = p_clientId;
    END sp_get_client;

    PROCEDURE sp_update_client(
        p_clientId IN client.clientId%TYPE,
        p_userId IN client.userId%TYPE,
        p_address IN client.address%TYPE,
        p_phone IN client.phone%TYPE,
        p_state IN client.state%TYPE,
        p_auditUpdateUser IN client.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE client
        SET userId          = p_userId,
            address         = p_address,
            phone           = p_phone,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE clientId = p_clientId;
        COMMIT;
    END sp_update_client;

    PROCEDURE sp_delete_client(
        p_clientId IN client.clientId%TYPE,
        p_auditDeleteUser IN client.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE client
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE clientId = p_clientId;
        COMMIT;
    END sp_delete_client;

    PROCEDURE sp_get_all_clients(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT clientId,
                   userId,
                   address,
                   phone,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM client;
    END sp_get_all_clients;

END pkg_client;
/
