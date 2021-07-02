USE smpedidos;

run sp_category_list()

---procedimientos almacenados----
--procedimiento list module category

DELIMITER //
CREATE PROCEDURE sp_category_list()
BEGIN
SELECT id, NAME, image FROM categories ORDER BY NAME LIMIT 100;
END//
DELIMITER ;

--procedimiento almacenado create module category
DELIMITER //
CREATE PROCEDURE sp_category_create(IN name_ VARCHAR(50), IN image_ VARCHAR(50))
BEGIN
INSERT INTO categories(name, image) VALUES(name_, image_);
 
END//
DELIMITER ;

--procedimiento almacenado update module category
delimiter //
CREATE PROCEDURE sp_category_update(IN id_ INT, IN name_ VARCHAR(50), IN image VARCHAR(50))
BEGIN
UPDATE categories SET name = name_, image = image_ WHERE id = id_;
END//
delimiter ;

--procedimiento almacenado delete module category
delimiter //
CREATE PROCEDURE sp_category_destroy(IN id_ INT)
BEGIN
DELETE FROM categories WHERE id = id_;
END//
delimiter ;

--procedimiento almacenado search module category
delimiter //
CREATE PROCEDURE sp_category_search(IN txt VARCHAR(50))
BEGIN
SELECT id, name, image FROM categories
WHERE name LIKE CONCAT('%', txt ,'%')
ORDER BY name
LIMIT 100; 
END//
delimiter ;


--procedimiento almacenado hasproduct module category
delimiter //
CREATE PROCEDURE sp_category_hasproduct(IN id_ int)
BEGIN
SELECT id FROM products WHERE category_id = id_;
END//
delimiter ;

--procedimiento almacenado categoryexist module category
delimiter //
CREATE PROCEDURE sp_category_exist(IN name_ VARCHAR(50))
BEGIN
SELECT id FROM categories WHERE name = name_;
END//
delimiter ;