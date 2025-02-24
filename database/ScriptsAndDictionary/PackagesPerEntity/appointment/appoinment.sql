CREATE OR REPLACE PACKAGE pkg_appointment AS
    PROCEDURE sp_create_appointment(
        p_appointmentDate IN appointment.appointmentDate%TYPE,
        p_reason IN appointment.reason%TYPE,
        p_petId IN appointment.petId%TYPE,
        p_medicId IN appointment.medicId%TYPE,
        p_state IN appointment.state%TYPE DEFAULT 1,
        p_auditCreateUser IN appointment.auditCreateUser%TYPE,
        p_appointmentId OUT appointment.appointmentId%TYPE
    );

    PROCEDURE sp_get_appointment(
        p_appointmentId IN appointment.appointmentId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_appointment(
        p_appointmentId IN appointment.appointmentId%TYPE,
        p_appointmentDate IN appointment.appointmentDate%TYPE,
        p_reason IN appointment.reason%TYPE,
        p_petId IN appointment.petId%TYPE,
        p_medicId IN appointment.medicId%TYPE,
        p_state IN appointment.state%TYPE,
        p_auditUpdateUser IN appointment.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_appointment(
        p_appointmentId IN appointment.appointmentId%TYPE,
        p_auditDeleteUser IN appointment.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_appointments(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_appointment;
/

CREATE OR REPLACE PACKAGE BODY pkg_appointment AS

    PROCEDURE sp_create_appointment(
        p_appointmentDate IN appointment.appointmentDate%TYPE,
        p_reason IN appointment.reason%TYPE,
        p_petId IN appointment.petId%TYPE,
        p_medicId IN appointment.medicId%TYPE,
        p_state IN appointment.state%TYPE DEFAULT 1,
        p_auditCreateUser IN appointment.auditCreateUser%TYPE,
        p_appointmentId OUT appointment.appointmentId%TYPE
    )
        IS
    BEGIN
        INSERT INTO appointment (appointmentDate, reason, petId, medicId, state, auditCreateUser, auditCreateDate)
        VALUES (p_appointmentDate, p_reason, p_petId, p_medicId, p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING appointmentId INTO p_appointmentId;
        COMMIT;
    END sp_create_appointment;

    PROCEDURE sp_get_appointment(
        p_appointmentId IN appointment.appointmentId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT appointmentId,
                   appointmentDate,
                   reason,
                   petId,
                   medicId,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM appointment
            WHERE appointmentId = p_appointmentId;
    END sp_get_appointment;

    PROCEDURE sp_update_appointment(
        p_appointmentId IN appointment.appointmentId%TYPE,
        p_appointmentDate IN appointment.appointmentDate%TYPE,
        p_reason IN appointment.reason%TYPE,
        p_petId IN appointment.petId%TYPE,
        p_medicId IN appointment.medicId%TYPE,
        p_state IN appointment.state%TYPE,
        p_auditUpdateUser IN appointment.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE appointment
        SET appointmentDate = p_appointmentDate,
            reason          = p_reason,
            petId           = p_petId,
            medicId         = p_medicId,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE appointmentId = p_appointmentId;
        COMMIT;
    END sp_update_appointment;

    PROCEDURE sp_delete_appointment(
        p_appointmentId IN appointment.appointmentId%TYPE,
        p_auditDeleteUser IN appointment.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE appointment
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE appointmentId = p_appointmentId;
        COMMIT;
    END sp_delete_appointment;

    PROCEDURE sp_get_all_appointments(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT appointmentId,
                   appointmentDate,
                   reason,
                   petId,
                   medicId,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM appointment;
    END sp_get_all_appointments;

END pkg_appointment;
/
