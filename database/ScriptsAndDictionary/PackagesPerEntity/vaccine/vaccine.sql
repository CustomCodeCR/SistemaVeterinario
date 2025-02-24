CREATE OR REPLACE PACKAGE pkg_vaccine AS
    PROCEDURE sp_create_vaccine(
        p_vaccineName IN vaccine.vaccineName%TYPE,
        p_description IN vaccine.description%TYPE,
        p_type IN vaccine.type%TYPE,
        p_state IN vaccine.state%TYPE DEFAULT 1,
        p_auditCreateUser IN vaccine.auditCreateUser%TYPE,
        p_vaccineId OUT vaccine.vaccineId%TYPE
    );

    PROCEDURE sp_get_vaccine(
        p_vaccineId IN vaccine.vaccineId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_vaccine(
        p_vaccineId IN vaccine.vaccineId%TYPE,
        p_vaccineName IN vaccine.vaccineName%TYPE,
        p_description IN vaccine.description%TYPE,
        p_type IN vaccine.type%TYPE,
        p_state IN vaccine.state%TYPE,
        p_auditUpdateUser IN vaccine.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_vaccine(
        p_vaccineId IN vaccine.vaccineId%TYPE,
        p_auditDeleteUser IN vaccine.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_vaccines(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_vaccine;
/

CREATE OR REPLACE PACKAGE BODY pkg_vaccine AS

    PROCEDURE sp_create_vaccine(
        p_vaccineName IN vaccine.vaccineName%TYPE,
        p_description IN vaccine.description%TYPE,
        p_type IN vaccine.type%TYPE,
        p_state IN vaccine.state%TYPE DEFAULT 1,
        p_auditCreateUser IN vaccine.auditCreateUser%TYPE,
        p_vaccineId OUT vaccine.vaccineId%TYPE
    )
        IS
    BEGIN
        INSERT INTO vaccine (vaccineName, description, type, state, auditCreateUser, auditCreateDate)
        VALUES (p_vaccineName, p_description, p_type, p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING vaccineId INTO p_vaccineId;
        COMMIT;
    END sp_create_vaccine;

    PROCEDURE sp_get_vaccine(
        p_vaccineId IN vaccine.vaccineId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT vaccineId,
                   vaccineName,
                   description,
                   type,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM vaccine
            WHERE vaccineId = p_vaccineId;
    END sp_get_vaccine;

    PROCEDURE sp_update_vaccine(
        p_vaccineId IN vaccine.vaccineId%TYPE,
        p_vaccineName IN vaccine.vaccineName%TYPE,
        p_description IN vaccine.description%TYPE,
        p_type IN vaccine.type%TYPE,
        p_state IN vaccine.state%TYPE,
        p_auditUpdateUser IN vaccine.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE vaccine
        SET vaccineName     = p_vaccineName,
            description     = p_description,
            type            = p_type,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE vaccineId = p_vaccineId;
        COMMIT;
    END sp_update_vaccine;

    PROCEDURE sp_delete_vaccine(
        p_vaccineId IN vaccine.vaccineId%TYPE,
        p_auditDeleteUser IN vaccine.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE vaccine
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE vaccineId = p_vaccineId;
        COMMIT;
    END sp_delete_vaccine;

    PROCEDURE sp_get_all_vaccines(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT vaccineId,
                   vaccineName,
                   description,
                   type,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM vaccine;
    END sp_get_all_vaccines;

END pkg_vaccine;
/
