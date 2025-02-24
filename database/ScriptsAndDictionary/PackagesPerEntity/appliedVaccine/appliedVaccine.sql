CREATE
    OR REPLACE PACKAGE pkg_appliedVaccine AS
    PROCEDURE sp_create_appliedVaccine(
        p_applicationDate IN appliedVaccine.applicationDate%TYPE,
        p_petId IN appliedVaccine.petId%TYPE,
        p_vaccineId IN appliedVaccine.vaccineId%TYPE,
        p_state IN appliedVaccine.state%TYPE DEFAULT 1,
        p_auditCreateUser IN appliedVaccine.auditCreateUser%TYPE,
        p_appliedVaccineId OUT appliedVaccine.appliedVaccineId%TYPE
    );

    PROCEDURE sp_get_appliedVaccine(
        p_appliedVaccineId IN appliedVaccine.appliedVaccineId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_appliedVaccine(
        p_appliedVaccineId IN appliedVaccine.appliedVaccineId%TYPE,
        p_applicationDate IN appliedVaccine.applicationDate%TYPE,
        p_petId IN appliedVaccine.petId%TYPE,
        p_vaccineId IN appliedVaccine.vaccineId%TYPE,
        p_state IN appliedVaccine.state%TYPE,
        p_auditUpdateUser IN appliedVaccine.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_appliedVaccine(
        p_appliedVaccineId IN appliedVaccine.appliedVaccineId%TYPE,
        p_auditDeleteUser IN appliedVaccine.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_appliedVaccines(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_appliedVaccine;
/

CREATE
    OR REPLACE PACKAGE BODY pkg_appliedVaccine AS

    PROCEDURE sp_create_appliedVaccine(
        p_applicationDate IN appliedVaccine.applicationDate%TYPE,
        p_petId IN appliedVaccine.petId%TYPE,
        p_vaccineId IN appliedVaccine.vaccineId%TYPE,
        p_state IN appliedVaccine.state%TYPE DEFAULT 1,
        p_auditCreateUser IN appliedVaccine.auditCreateUser%TYPE,
        p_appliedVaccineId OUT appliedVaccine.appliedVaccineId%TYPE
    )
        IS
    BEGIN
        INSERT INTO appliedVaccine (applicationDate, petId, vaccineId, state, auditCreateUser, auditCreateDate)
        VALUES (p_applicationDate, p_petId, p_vaccineId, p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING appliedVaccineId
            INTO p_appliedVaccineId;
        COMMIT;
    END sp_create_appliedVaccine;

    PROCEDURE sp_get_appliedVaccine(
        p_appliedVaccineId IN appliedVaccine.appliedVaccineId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT appliedVaccineId,
                   applicationDate,
                   petId,
                   vaccineId,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM appliedVaccine
            WHERE appliedVaccineId = p_appliedVaccineId;
    END sp_get_appliedVaccine;

    PROCEDURE sp_update_appliedVaccine(
        p_appliedVaccineId IN appliedVaccine.appliedVaccineId%TYPE,
        p_applicationDate IN appliedVaccine.applicationDate%TYPE,
        p_petId IN appliedVaccine.petId%TYPE,
        p_vaccineId IN appliedVaccine.vaccineId%TYPE,
        p_state IN appliedVaccine.state%TYPE,
        p_auditUpdateUser IN appliedVaccine.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE appliedVaccine
        SET applicationDate = p_applicationDate,
            petId           = p_petId,
            vaccineId       = p_vaccineId,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE appliedVaccineId = p_appliedVaccineId;
        COMMIT;
    END sp_update_appliedVaccine;

    PROCEDURE sp_delete_appliedVaccine(
        p_appliedVaccineId IN appliedVaccine.appliedVaccineId%TYPE,
        p_auditDeleteUser IN appliedVaccine.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE appliedVaccine
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE appliedVaccineId = p_appliedVaccineId;
        COMMIT;
    END sp_delete_appliedVaccine;

    PROCEDURE sp_get_all_appliedVaccines(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT appliedVaccineId,
                   applicationDate,
                   petId,
                   vaccineId,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM appliedVaccine;
    END sp_get_all_appliedVaccines;

END pkg_appliedVaccine;
/
