CREATE OR REPLACE PACKAGE pkg_pet AS
    PROCEDURE sp_create_pet(
        p_name IN pet.name%TYPE,
        p_type IN pet.type%TYPE,
        p_breed IN pet.breed%TYPE,
        p_age IN pet.age%TYPE,
        p_clientId IN pet.clientId%TYPE,
        p_state IN pet.state%TYPE DEFAULT 1,
        p_auditCreateUser IN pet.auditCreateUser%TYPE,
        p_petId OUT pet.petId%TYPE
    );

    PROCEDURE sp_get_pet(
        p_petId IN pet.petId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_pet(
        p_petId IN pet.petId%TYPE,
        p_name IN pet.name%TYPE,
        p_type IN pet.type%TYPE,
        p_breed IN pet.breed%TYPE,
        p_age IN pet.age%TYPE,
        p_clientId IN pet.clientId%TYPE,
        p_state IN pet.state%TYPE,
        p_auditUpdateUser IN pet.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_pet(
        p_petId IN pet.petId%TYPE,
        p_auditDeleteUser IN pet.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_pets(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_pet;
/

CREATE OR REPLACE PACKAGE BODY pkg_pet AS

    PROCEDURE sp_create_pet(
        p_name IN pet.name%TYPE,
        p_type IN pet.type%TYPE,
        p_breed IN pet.breed%TYPE,
        p_age IN pet.age%TYPE,
        p_clientId IN pet.clientId%TYPE,
        p_state IN pet.state%TYPE DEFAULT 1,
        p_auditCreateUser IN pet.auditCreateUser%TYPE,
        p_petId OUT pet.petId%TYPE
    )
        IS
    BEGIN
        INSERT INTO pet (name, type, breed, age, clientId, state, auditCreateUser, auditCreateDate)
        VALUES (p_name, p_type, p_breed, p_age, p_clientId, p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING petId INTO p_petId;
        COMMIT;
    END sp_create_pet;

    PROCEDURE sp_get_pet(
        p_petId IN pet.petId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT petId,
                   name,
                   type,
                   breed,
                   age,
                   clientId,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM pet
            WHERE petId = p_petId;
    END sp_get_pet;

    PROCEDURE sp_update_pet(
        p_petId IN pet.petId%TYPE,
        p_name IN pet.name%TYPE,
        p_type IN pet.type%TYPE,
        p_breed IN pet.breed%TYPE,
        p_age IN pet.age%TYPE,
        p_clientId IN pet.clientId%TYPE,
        p_state IN pet.state%TYPE,
        p_auditUpdateUser IN pet.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE pet
        SET name            = p_name,
            type            = p_type,
            breed           = p_breed,
            age             = p_age,
            clientId        = p_clientId,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE petId = p_petId;
        COMMIT;
    END sp_update_pet;

    PROCEDURE sp_delete_pet(
        p_petId IN pet.petId%TYPE,
        p_auditDeleteUser IN pet.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE pet
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE petId = p_petId;
        COMMIT;
    END sp_delete_pet;

    PROCEDURE sp_get_all_pets(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT petId,
                   name,
                   type,
                   breed,
                   age,
                   clientId,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM pet;
    END sp_get_all_pets;

END pkg_pet;
/
