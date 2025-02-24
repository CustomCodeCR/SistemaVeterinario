CREATE OR REPLACE PACKAGE pkg_medic AS
    PROCEDURE sp_create_medic(
        p_userId IN medic.userId%TYPE,
        p_specialty IN medic.specialty%TYPE,
        p_phone IN medic.phone%TYPE,
        p_state IN medic.state%TYPE DEFAULT 1,
        p_auditCreateUser IN medic.auditCreateUser%TYPE,
        p_medicId OUT medic.medicId%TYPE
    );

    PROCEDURE sp_get_medic(
        p_medicId IN medic.medicId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_medic(
        p_medicId IN medic.medicId%TYPE,
        p_userId IN medic.userId%TYPE,
        p_specialty IN medic.specialty%TYPE,
        p_phone IN medic.phone%TYPE,
        p_state IN medic.state%TYPE,
        p_auditUpdateUser IN medic.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_medic(
        p_medicId IN medic.medicId%TYPE,
        p_auditDeleteUser IN medic.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_medics(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_medic;
/

CREATE OR REPLACE PACKAGE BODY pkg_medic AS

    PROCEDURE sp_create_medic(
        p_userId IN medic.userId%TYPE,
        p_specialty IN medic.specialty%TYPE,
        p_phone IN medic.phone%TYPE,
        p_state IN medic.state%TYPE DEFAULT 1,
        p_auditCreateUser IN medic.auditCreateUser%TYPE,
        p_medicId OUT medic.medicId%TYPE
    )
        IS
    BEGIN
        INSERT INTO medic (userId, specialty, phone, state, auditCreateUser, auditCreateDate)
        VALUES (p_userId, p_specialty, p_phone, p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING medicId INTO p_medicId;
        COMMIT;
    END sp_create_medic;

    PROCEDURE sp_get_medic(
        p_medicId IN medic.medicId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT medicId,
                   userId,
                   specialty,
                   phone,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM medic
            WHERE medicId = p_medicId;
    END sp_get_medic;

    PROCEDURE sp_update_medic(
        p_medicId IN medic.medicId%TYPE,
        p_userId IN medic.userId%TYPE,
        p_specialty IN medic.specialty%TYPE,
        p_phone IN medic.phone%TYPE,
        p_state IN medic.state%TYPE,
        p_auditUpdateUser IN medic.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE medic
        SET userId          = p_userId,
            specialty       = p_specialty,
            phone           = p_phone,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE medicId = p_medicId;
        COMMIT;
    END sp_update_medic;

    PROCEDURE sp_delete_medic(
        p_medicId IN medic.medicId%TYPE,
        p_auditDeleteUser IN medic.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE medic
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE medicId = p_medicId;
        COMMIT;
    END sp_delete_medic;

    PROCEDURE sp_get_all_medics(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT medicId,
                   userId,
                   specialty,
                   phone,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM medic;
    END sp_get_all_medics;

END pkg_medic;
/
