CREATE OR REPLACE PACKAGE pkg_payment AS
    PROCEDURE sp_create_payment(
        p_saleId IN payment.saleId%TYPE,
        p_amount IN payment.amount%TYPE,
        p_paymentDate IN payment.paymentDate%TYPE,
        p_paymentType IN payment.paymentType%TYPE,
        p_state IN payment.state%TYPE DEFAULT 1,
        p_auditCreateUser IN payment.auditCreateUser%TYPE,
        p_paymentId OUT payment.paymentId%TYPE
    );

    PROCEDURE sp_get_payment(
        p_paymentId IN payment.paymentId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_payment(
        p_paymentId IN payment.paymentId%TYPE,
        p_saleId IN payment.saleId%TYPE,
        p_amount IN payment.amount%TYPE,
        p_paymentDate IN payment.paymentDate%TYPE,
        p_paymentType IN payment.paymentType%TYPE,
        p_state IN payment.state%TYPE,
        p_auditUpdateUser IN payment.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_payment(
        p_paymentId IN payment.paymentId%TYPE,
        p_auditDeleteUser IN payment.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_payments(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_payment;
/

CREATE OR REPLACE PACKAGE BODY pkg_payment AS

    PROCEDURE sp_create_payment(
        p_saleId IN payment.saleId%TYPE,
        p_amount IN payment.amount%TYPE,
        p_paymentDate IN payment.paymentDate%TYPE,
        p_paymentType IN payment.paymentType%TYPE,
        p_state IN payment.state%TYPE DEFAULT 1,
        p_auditCreateUser IN payment.auditCreateUser%TYPE,
        p_paymentId OUT payment.paymentId%TYPE
    )
        IS
    BEGIN
        INSERT INTO payment (saleId, amount, paymentDate, paymentType, state, auditCreateUser, auditCreateDate)
        VALUES (p_saleId, p_amount, p_paymentDate, p_paymentType, p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING paymentId INTO p_paymentId;
        COMMIT;
    END sp_create_payment;

    PROCEDURE sp_get_payment(
        p_paymentId IN payment.paymentId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT paymentId,
                   saleId,
                   amount,
                   paymentDate,
                   paymentType,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM payment
            WHERE paymentId = p_paymentId;
    END sp_get_payment;

    PROCEDURE sp_update_payment(
        p_paymentId IN payment.paymentId%TYPE,
        p_saleId IN payment.saleId%TYPE,
        p_amount IN payment.amount%TYPE,
        p_paymentDate IN payment.paymentDate%TYPE,
        p_paymentType IN payment.paymentType%TYPE,
        p_state IN payment.state%TYPE,
        p_auditUpdateUser IN payment.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE payment
        SET saleId          = p_saleId,
            amount          = p_amount,
            paymentDate     = p_paymentDate,
            paymentType     = p_paymentType,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE paymentId = p_paymentId;
        COMMIT;
    END sp_update_payment;

    PROCEDURE sp_delete_payment(
        p_paymentId IN payment.paymentId%TYPE,
        p_auditDeleteUser IN payment.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE payment
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE paymentId = p_paymentId;
        COMMIT;
    END sp_delete_payment;

    PROCEDURE sp_get_all_payments(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT paymentId,
                   saleId,
                   amount,
                   paymentDate,
                   paymentType,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM payment;
    END sp_get_all_payments;

END pkg_payment;
/
