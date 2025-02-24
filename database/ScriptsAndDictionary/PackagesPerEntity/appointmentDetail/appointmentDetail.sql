CREATE
    OR REPLACE PACKAGE pkg_appointmentDetail AS
    PROCEDURE sp_create_appointmentDetail(
        p_appointmentId IN appointmentDetail.appointmentId%TYPE,
        p_diagnosis IN appointmentDetail.diagnosis%TYPE,
        p_treatment IN appointmentDetail.treatment%TYPE,
        p_observations IN appointmentDetail.observations%TYPE,
        p_state IN appointmentDetail.state%TYPE DEFAULT 1,
        p_auditCreateUser IN appointmentDetail.auditCreateUser%TYPE,
        p_appointmentDetailId OUT appointmentDetail.appointmentDetailId%TYPE
    );

    PROCEDURE sp_get_appointmentDetail(
        p_appointmentDetailId IN appointmentDetail.appointmentDetailId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_appointmentDetail(
        p_appointmentDetailId IN appointmentDetail.appointmentDetailId%TYPE,
        p_appointmentId IN appointmentDetail.appointmentId%TYPE,
        p_diagnosis IN appointmentDetail.diagnosis%TYPE,
        p_treatment IN appointmentDetail.treatment%TYPE,
        p_observations IN appointmentDetail.observations%TYPE,
        p_state IN appointmentDetail.state%TYPE,
        p_auditUpdateUser IN appointmentDetail.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_appointmentDetail(
        p_appointmentDetailId IN appointmentDetail.appointmentDetailId%TYPE,
        p_auditDeleteUser IN appointmentDetail.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_appointmentDetails(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_appointmentDetail;
/

CREATE
    OR REPLACE PACKAGE BODY pkg_appointmentDetail AS

    PROCEDURE sp_create_appointmentDetail(
        p_appointmentId IN appointmentDetail.appointmentId%TYPE,
        p_diagnosis IN appointmentDetail.diagnosis%TYPE,
        p_treatment IN appointmentDetail.treatment%TYPE,
        p_observations IN appointmentDetail.observations%TYPE,
        p_state IN appointmentDetail.state%TYPE DEFAULT 1,
        p_auditCreateUser IN appointmentDetail.auditCreateUser%TYPE,
        p_appointmentDetailId OUT appointmentDetail.appointmentDetailId%TYPE
    )
        IS
    BEGIN
        INSERT INTO appointmentDetail (appointmentId, diagnosis, treatment, observations, state, auditCreateUser,
                                       auditCreateDate)
        VALUES (p_appointmentId, p_diagnosis, p_treatment, p_observations, p_state, p_auditCreateUser,
                SYSTIMESTAMP)
        RETURNING appointmentDetailId
            INTO p_appointmentDetailId;
        COMMIT;
    END sp_create_appointmentDetail;

    PROCEDURE sp_get_appointmentDetail(
        p_appointmentDetailId IN appointmentDetail.appointmentDetailId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT appointmentDetailId,
                   appointmentId,
                   diagnosis,
                   treatment,
                   observations,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM appointmentDetail
            WHERE appointmentDetailId = p_appointmentDetailId;
    END sp_get_appointmentDetail;

    PROCEDURE sp_update_appointmentDetail(
        p_appointmentDetailId IN appointmentDetail.appointmentDetailId%TYPE,
        p_appointmentId IN appointmentDetail.appointmentId%TYPE,
        p_diagnosis IN appointmentDetail.diagnosis%TYPE,
        p_treatment IN appointmentDetail.treatment%TYPE,
        p_observations IN appointmentDetail.observations%TYPE,
        p_state IN appointmentDetail.state%TYPE,
        p_auditUpdateUser IN appointmentDetail.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE appointmentDetail
        SET appointmentId   = p_appointmentId,
            diagnosis       = p_diagnosis,
            treatment       = p_treatment,
            observations    = p_observations,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE appointmentDetailId = p_appointmentDetailId;
        COMMIT;
    END sp_update_appointmentDetail;

    PROCEDURE sp_delete_appointmentDetail(
        p_appointmentDetailId IN appointmentDetail.appointmentDetailId%TYPE,
        p_auditDeleteUser IN appointmentDetail.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE appointmentDetail
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE appointmentDetailId = p_appointmentDetailId;
        COMMIT;
    END sp_delete_appointmentDetail;

    PROCEDURE sp_get_all_appointmentDetails(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT appointmentDetailId,
                   appointmentId,
                   diagnosis,
                   treatment,
                   observations,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM appointmentDetail;
    END sp_get_all_appointmentDetails;

END pkg_appointmentDetail;
/
