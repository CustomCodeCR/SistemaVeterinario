CREATE OR REPLACE PACKAGE pkg_productCategoryRelation AS
    PROCEDURE sp_create_productCategoryRelation(
        p_productId IN productCategoryRelation.productId%TYPE,
        p_categoryId IN productCategoryRelation.categoryId%TYPE,
        p_state IN productCategoryRelation.state%TYPE DEFAULT 1,
        p_auditCreateUser IN productCategoryRelation.auditCreateUser%TYPE
    );

    PROCEDURE sp_get_productCategoryRelation(
        p_productId IN productCategoryRelation.productId%TYPE,
        p_categoryId IN productCategoryRelation.categoryId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_productCategoryRelation(
        p_productId IN productCategoryRelation.productId%TYPE,
        p_categoryId IN productCategoryRelation.categoryId%TYPE,
        p_state IN productCategoryRelation.state%TYPE,
        p_auditUpdateUser IN productCategoryRelation.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_productCategoryRelation(
        p_productId IN productCategoryRelation.productId%TYPE,
        p_categoryId IN productCategoryRelation.categoryId%TYPE,
        p_auditDeleteUser IN productCategoryRelation.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_productCategoryRelations(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_productCategoryRelation;
/

CREATE OR REPLACE PACKAGE BODY pkg_productCategoryRelation AS

    PROCEDURE sp_create_productCategoryRelation(
        p_productId IN productCategoryRelation.productId%TYPE,
        p_categoryId IN productCategoryRelation.categoryId%TYPE,
        p_state IN productCategoryRelation.state%TYPE DEFAULT 1,
        p_auditCreateUser IN productCategoryRelation.auditCreateUser%TYPE
    )
        IS
    BEGIN
        INSERT INTO productCategoryRelation (productId, categoryId, state, auditCreateUser, auditCreateDate)
        VALUES (p_productId, p_categoryId, p_state, p_auditCreateUser, SYSTIMESTAMP);
        COMMIT;
    END sp_create_productCategoryRelation;

    PROCEDURE sp_get_productCategoryRelation(
        p_productId IN productCategoryRelation.productId%TYPE,
        p_categoryId IN productCategoryRelation.categoryId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT productId,
                   categoryId,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM productCategoryRelation
            WHERE productId = p_productId
              AND categoryId = p_categoryId;
    END sp_get_productCategoryRelation;

    PROCEDURE sp_update_productCategoryRelation(
        p_productId IN productCategoryRelation.productId%TYPE,
        p_categoryId IN productCategoryRelation.categoryId%TYPE,
        p_state IN productCategoryRelation.state%TYPE,
        p_auditUpdateUser IN productCategoryRelation.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE productCategoryRelation
        SET state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE productId = p_productId
          AND categoryId = p_categoryId;
        COMMIT;
    END sp_update_productCategoryRelation;

    PROCEDURE sp_delete_productCategoryRelation(
        p_productId IN productCategoryRelation.productId%TYPE,
        p_categoryId IN productCategoryRelation.categoryId%TYPE,
        p_auditDeleteUser IN productCategoryRelation.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE productCategoryRelation
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE productId = p_productId
          AND categoryId = p_categoryId;
        COMMIT;
    END sp_delete_productCategoryRelation;

    PROCEDURE sp_get_all_productCategoryRelations(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT productId,
                   categoryId,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM productCategoryRelation;
    END sp_get_all_productCategoryRelations;

END pkg_productCategoryRelation;
/
