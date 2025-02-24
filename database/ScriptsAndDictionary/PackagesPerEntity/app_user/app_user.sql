CREATE OR REPLACE PACKAGE pkg_app_user AS
    PROCEDURE sp_create_app_user(
        p_firstName IN app_user.firstName%TYPE,
        p_lastName IN app_user.lastName%TYPE,
        p_username IN app_user.username%TYPE,
        p_password IN app_user.password%TYPE,
        p_email IN app_user.email%TYPE,
        p_userType IN app_user.userType%TYPE,
        p_state IN app_user.state%TYPE DEFAULT 1,
        p_auditCreateUser IN app_user.auditCreateUser%TYPE,
        p_userId OUT app_user.userId%TYPE
    );

    PROCEDURE sp_get_app_user(
        p_userId IN app_user.userId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_app_user(
        p_userId IN app_user.userId%TYPE,
        p_firstName IN app_user.firstName%TYPE,
        p_lastName IN app_user.lastName%TYPE,
        p_username IN app_user.username%TYPE,
        p_password IN app_user.password%TYPE,
        p_email IN app_user.email%TYPE,
        p_userType IN app_user.userType%TYPE,
        p_state IN app_user.state%TYPE,
        p_auditUpdateUser IN app_user.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_app_user(
        p_userId IN app_user.userId%TYPE,
        p_auditDeleteUser IN app_user.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_app_users(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_app_user;
/

CREATE OR REPLACE PACKAGE BODY pkg_app_user AS

    PROCEDURE sp_create_app_user(
        p_firstName IN app_user.firstName%TYPE,
        p_lastName IN app_user.lastName%TYPE,
        p_username IN app_user.username%TYPE,
        p_password IN app_user.password%TYPE,
        p_email IN app_user.email%TYPE,
        p_userType IN app_user.userType%TYPE,
        p_state IN app_user.state%TYPE DEFAULT 1,
        p_auditCreateUser IN app_user.auditCreateUser%TYPE,
        p_userId OUT app_user.userId%TYPE
    )
        IS
    BEGIN
        INSERT INTO app_user (firstName, lastName, username, password, email, userType, state,
                              auditCreateUser, auditCreateDate)
        VALUES (p_firstName, p_lastName, p_username, p_password, p_email, p_userType,
                p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING userId INTO p_userId;
        COMMIT;
    END sp_create_app_user;

    PROCEDURE sp_get_app_user(
        p_userId IN app_user.userId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT userId,
                   firstName,
                   lastName,
                   username,
                   password,
                   email,
                   userType,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM app_user
            WHERE userId = p_userId;
    END sp_get_app_user;

    PROCEDURE sp_update_app_user(
        p_userId IN app_user.userId%TYPE,
        p_firstName IN app_user.firstName%TYPE,
        p_lastName IN app_user.lastName%TYPE,
        p_username IN app_user.username%TYPE,
        p_password IN app_user.password%TYPE,
        p_email IN app_user.email%TYPE,
        p_userType IN app_user.userType%TYPE,
        p_state IN app_user.state%TYPE,
        p_auditUpdateUser IN app_user.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE app_user
        SET firstName       = p_firstName,
            lastName        = p_lastName,
            username        = p_username,
            password        = p_password,
            email           = p_email,
            userType        = p_userType,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE userId = p_userId;
        COMMIT;
    END sp_update_app_user;

    PROCEDURE sp_delete_app_user(
        p_userId IN app_user.userId%TYPE,
        p_auditDeleteUser IN app_user.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE app_user
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE userId = p_userId;
        COMMIT;
    END sp_delete_app_user;

    PROCEDURE sp_get_all_app_users(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT userId,
                   firstName,
                   lastName,
                   username,
                   password,
                   email,
                   userType,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM app_user;
    END sp_get_all_app_users;

END pkg_app_user;
/

