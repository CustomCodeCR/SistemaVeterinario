# VetFriends Database Design
This documents serves as a guide to understand our approach of the database design, it includes different sections regarding every requirement requested. 
## Index 
- [VetFriends Database Design](#vetfriends-database-design)
  - [Index](#index)
    - [Folder Directions](#folder-directions)
  - [Common Acronyms](#common-acronyms)
  - [Stored Procedures and Packages](#stored-procedures-and-packages)
  - [Functions](#functions)
  - [Triggers](#triggers)
  - [Views](#views)

### Folder Directions
- **Script Entities and Indexes Folder** [Access Folder](./EntitiesCreationAndIndexing/)
- **Packages Per Entity Folder:** [Access Folder](./PackagesPerEntity/)
- **Functions Folder:** [Access Folder](./Functions/)
- **Triggers Folder:** [Access Folder](./Triggers/)
- **Views Folder:** [Access Folder](./Views/)

> **Note:** To further understand our implementation, go to our [DB dictionary](./Dictionary/)
## Common Acronyms
Before diving into the database design is important to know some of the acronyms that we used: 

- `pk` : Primary Key
- `idx` : Index
- `fk` : Foreign Key
- `sp` : Stored Procedure
- `fn` : Function
- `trg` : Trigger
- `vw` : View
- `pkg` : Package
- `biu` : Before Insert or Update
- `ai` : After Insert

## Stored Procedures and Packages
This prototype outlines the basic CRUD operations for the `app_user` table. It includes two types of operations: **Queries** to retrieve data and **Commands** to modify data. And It was the base for all the consequent entities.

```sql
CREATE OR REPLACE PROCEDURE sp_create_user(
    p_firstName       IN app_user.firstName%TYPE,
    p_lastName        IN app_user.lastName%TYPE,
    p_username        IN app_user.username%TYPE,
    p_password        IN app_user.password%TYPE,
    p_email           IN app_user.email%TYPE,
    p_userType        IN app_user.userType%TYPE,
    p_state           IN app_user.state%TYPE DEFAULT 1,
    p_auditCreateUser IN app_user.auditCreateUser%TYPE,
    p_userId          OUT app_user.userId%TYPE
)
IS
BEGIN
    INSERT INTO app_user (
       firstName, 
       lastName, 
       username, 
       password, 
       email, 
       userType, 
       state, 
       auditCreateUser, 
       auditCreateDate
    )
    VALUES (
       p_firstName,
       p_lastName,
       p_username,
       p_password
       p_email,
       p_userType,
       p_state,
       p_auditCreateUser,
       SYSTIMESTAMP
    )
    RETURNING userId INTO p_userId;
    
    COMMIT;
END sp_create_user;
/

CREATE OR REPLACE PROCEDURE sp_get_user(
    p_userId IN app_user.userId%TYPE,
    o_cursor OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN o_cursor FOR
      SELECT 
         userId,
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
END sp_get_user;
/

CREATE OR REPLACE PROCEDURE sp_update_user(
    p_userId          IN app_user.userId%TYPE,
    p_firstName       IN app_user.firstName%TYPE,
    p_lastName        IN app_user.lastName%TYPE,
    p_username        IN app_user.username%TYPE,
    p_password        IN app_user.password%TYPE,
    p_email           IN app_user.email%TYPE,
    p_userType        IN app_user.userType%TYPE,
    p_state           IN app_user.state%TYPE,
    p_auditUpdateUser IN app_user.auditUpdateUser%TYPE
)
IS
BEGIN
    UPDATE app_user
    SET 
      firstName       = p_firstName,
      lastName        = p_lastName,
      username        = p_username,
      password        = fn_hash_password(p_password),  -- password is hashed here
      email           = p_email,
      userType        = p_userType,
      state           = p_state,
      auditUpdateUser = p_auditUpdateUser,
      auditUpdateDate = SYSTIMESTAMP
    WHERE userId = p_userId;
    
    COMMIT;
END sp_update_user;
/

CREATE OR REPLACE PROCEDURE sp_delete_user(
    p_userId          IN app_user.userId%TYPE,
    p_auditDeleteUser IN app_user.auditDeleteUser%TYPE
)
IS
BEGIN
    UPDATE app_user
    SET 
      state           = 0,  
      auditDeleteUser = p_auditDeleteUser,
      auditDeleteDate = SYSTIMESTAMP
    WHERE userId = p_userId;
    
    COMMIT;
END sp_delete_user;
/

CREATE OR REPLACE PROCEDURE sp_get_all_users(
    o_cursor OUT SYS_REFCURSOR
)
IS
BEGIN
    OPEN o_cursor FOR
      SELECT 
         userId,
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
END sp_get_all_users;
/
```
We noticed that the above the logic could be **replicated** to all the other entities, and then we just proceded to encapsulate within a package to create a template for just changing the parameters and use it over again, and again. 

```sql
CREATE OR REPLACE PACKAGE pkg_app_user AS
  PROCEDURE sp_create_app_user(
    p_firstName       IN app_user.firstName%TYPE,
    p_lastName        IN app_user.lastName%TYPE,
    p_username        IN app_user.username%TYPE,
    p_password        IN app_user.password%TYPE,
    p_email           IN app_user.email%TYPE,
    p_userType        IN app_user.userType%TYPE,
    p_state           IN app_user.state%TYPE DEFAULT 1,
    p_auditCreateUser IN app_user.auditCreateUser%TYPE,
    p_userId          OUT app_user.userId%TYPE
  );

  PROCEDURE sp_get_app_user(
    p_userId IN app_user.userId%TYPE,
    o_cursor OUT SYS_REFCURSOR
  );

  PROCEDURE sp_update_app_user(
    p_userId         IN app_user.userId%TYPE,
    p_firstName      IN app_user.firstName%TYPE,
    p_lastName       IN app_user.lastName%TYPE,
    p_username       IN app_user.username%TYPE,
    p_password       IN app_user.password%TYPE,
    p_email          IN app_user.email%TYPE,
    p_userType       IN app_user.userType%TYPE,
    p_state          IN app_user.state%TYPE,
    p_auditUpdateUser IN app_user.auditUpdateUser%TYPE
  );

  PROCEDURE sp_delete_app_user(
    p_userId          IN app_user.userId%TYPE,
    p_auditDeleteUser IN app_user.auditDeleteUser%TYPE
  );

  PROCEDURE sp_get_all_app_users(
    o_cursor OUT SYS_REFCURSOR
  );
END pkg_app_user;
/

CREATE OR REPLACE PACKAGE BODY pkg_app_user AS

  PROCEDURE sp_create_app_user(
    p_firstName       IN app_user.firstName%TYPE,
    p_lastName        IN app_user.lastName%TYPE,
    p_username        IN app_user.username%TYPE,
    p_password        IN app_user.password%TYPE,
    p_email           IN app_user.email%TYPE,
    p_userType        IN app_user.userType%TYPE,
    p_state           IN app_user.state%TYPE DEFAULT 1,
    p_auditCreateUser IN app_user.auditCreateUser%TYPE,
    p_userId          OUT app_user.userId%TYPE
  )
  IS
  BEGIN
      INSERT INTO app_user (
         firstName, lastName, username, password, email, userType, state,
         auditCreateUser, auditCreateDate
      )
      VALUES (
         p_firstName, p_lastName, p_username, p_password, p_email, p_userType,
         p_state, p_auditCreateUser, SYSTIMESTAMP
      )
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
        SELECT userId, firstName, lastName, username, password, email, userType, state,
               auditCreateUser, auditCreateDate, auditUpdateUser, auditUpdateDate,
               auditDeleteUser, auditDeleteDate
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
        SELECT userId, firstName, lastName, username, password, email, userType, state,
               auditCreateUser, auditCreateDate, auditUpdateUser, auditUpdateDate,
               auditDeleteUser, auditDeleteDate
        FROM app_user;
  END sp_get_all_app_users;

END pkg_app_user;
/
```
## Functions
One of the challenges we encountered in developing functions was the need to plan the implementation well before the interface was defined. Ultimately, the most plausible approach was to create functions that are invoked by triggers. This method enforces business logic at the database level, controlling the input and output of information.

For example, the following function, `fn_apply_discount`, calculates a discounted price for a product. This function can be used in a trigger to automatically apply discounts when the sale price reaches a specified threshold.

```sql
CREATE OR REPLACE FUNCTION fn_apply_discount(
    p_price IN NUMBER,
    p_discount_percent IN NUMBER  
) RETURN NUMBER IS
    v_discounted_price NUMBER;
BEGIN
    v_discounted_price := p_price - (p_price * p_discount_percent / 100);
    RETURN ROUND(v_discounted_price, 2);
END fn_apply_discount;
/
```
## Triggers 
Triggers were part of automating the calling of constraints/business logic encapsulated within functions.

Following the example above, `trg_product_biu` uses `fn_apply_discount`: 
```sql
CREATE OR REPLACE TRIGGER trg_product_biu
BEFORE INSERT OR UPDATE ON product
FOR EACH ROW
DECLARE
  v_discounted_price NUMBER;
  v_tax              NUMBER;
BEGIN
  IF :NEW.price > 150 THEN
    v_discounted_price := fn_apply_discount(:NEW.price, 5);
    :NEW.price := v_discounted_price;
  END IF;
  v_tax := fn_calculate_tax(:NEW.price, 0.13);
END;
/
```
## Views
We used it as DTOs or a way to denormalized data to expose it analitically. 
For example `view_product_category_mapping` presents the product with it's category. 
```sql
CREATE OR REPLACE VIEW view_product_category_mapping AS
SELECT pcr.productId, pr.name AS productName,
       pcr.categoryId, pc.categoryName
FROM productCategoryRelation pcr
JOIN product pr ON pcr.productId = pr.productId
JOIN productCategory pc ON pcr.categoryId = pc.categoryId
WHERE pcr.state != 0;

```




