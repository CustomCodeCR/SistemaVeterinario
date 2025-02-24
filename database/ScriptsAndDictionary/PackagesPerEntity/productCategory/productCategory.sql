CREATE OR REPLACE PACKAGE pkg_productCategory AS
    PROCEDURE sp_create_productCategory(
        p_categoryName IN productCategory.categoryName%TYPE,
        p_description IN productCategory.description%TYPE,
        p_state IN productCategory.state%TYPE DEFAULT 1,
        p_auditCreateUser IN productCategory.auditCreateUser%TYPE,
        p_categoryId OUT productCategory.categoryId%TYPE
    );

    PROCEDURE sp_get_productCategory(
        p_categoryId IN productCategory.categoryId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    );

    PROCEDURE sp_update_productCategory(
        p_categoryId IN productCategory.categoryId%TYPE,
        p_categoryName IN productCategory.categoryName%TYPE,
        p_description IN productCategory.description%TYPE,
        p_state IN productCategory.state%TYPE,
        p_auditUpdateUser IN productCategory.auditUpdateUser%TYPE
    );

    PROCEDURE sp_delete_productCategory(
        p_categoryId IN productCategory.categoryId%TYPE,
        p_auditDeleteUser IN productCategory.auditDeleteUser%TYPE
    );

    PROCEDURE sp_get_all_productCategories(
        o_cursor OUT SYS_REFCURSOR
    );
END pkg_productCategory;
/

CREATE OR REPLACE PACKAGE BODY pkg_productCategory AS

    PROCEDURE sp_create_productCategory(
        p_categoryName IN productCategory.categoryName%TYPE,
        p_description IN productCategory.description%TYPE,
        p_state IN productCategory.state%TYPE DEFAULT 1,
        p_auditCreateUser IN productCategory.auditCreateUser%TYPE,
        p_categoryId OUT productCategory.categoryId%TYPE
    )
        IS
    BEGIN
        INSERT INTO productCategory (categoryName, description, state, auditCreateUser, auditCreateDate)
        VALUES (p_categoryName, p_description, p_state, p_auditCreateUser, SYSTIMESTAMP)
        RETURNING categoryId INTO p_categoryId;
        COMMIT;
    END sp_create_productCategory;

    PROCEDURE sp_get_productCategory(
        p_categoryId IN productCategory.categoryId%TYPE,
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT categoryId,
                   categoryName,
                   description,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM productCategory
            WHERE categoryId = p_categoryId;
    END sp_get_productCategory;

    PROCEDURE sp_update_productCategory(
        p_categoryId IN productCategory.categoryId%TYPE,
        p_categoryName IN productCategory.categoryName%TYPE,
        p_description IN productCategory.description%TYPE,
        p_state IN productCategory.state%TYPE,
        p_auditUpdateUser IN productCategory.auditUpdateUser%TYPE
    )
        IS
    BEGIN
        UPDATE productCategory
        SET categoryName    = p_categoryName,
            description     = p_description,
            state           = p_state,
            auditUpdateUser = p_auditUpdateUser,
            auditUpdateDate = SYSTIMESTAMP
        WHERE categoryId = p_categoryId;
        COMMIT;
    END sp_update_productCategory;

    PROCEDURE sp_delete_productCategory(
        p_categoryId IN productCategory.categoryId%TYPE,
        p_auditDeleteUser IN productCategory.auditDeleteUser%TYPE
    )
        IS
    BEGIN
        UPDATE productCategory
        SET state           = 0,
            auditDeleteUser = p_auditDeleteUser,
            auditDeleteDate = SYSTIMESTAMP
        WHERE categoryId = p_categoryId;
        COMMIT;
    END sp_delete_productCategory;

    PROCEDURE sp_get_all_productCategories(
        o_cursor OUT SYS_REFCURSOR
    )
        IS
    BEGIN
        OPEN o_cursor FOR
            SELECT categoryId,
                   categoryName,
                   description,
                   state,
                   auditCreateUser,
                   auditCreateDate,
                   auditUpdateUser,
                   auditUpdateDate,
                   auditDeleteUser,
                   auditDeleteDate
            FROM productCategory;
    END sp_get_all_productCategories;

END pkg_productCategory;
/
