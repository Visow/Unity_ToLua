use nodejs;
delimiter $$

# #############################################################################角色查询
DROP PROCEDURE IF EXISTS nodejs.PRO_SELECT_USERS;
# ####################################################################
CREATE DEFINER = `root`@`localhost` PROCEDURE `PRO_SELECT_USERS`(IN `@user_id` INT)
	LANGUAGE SQL
	NOT DETERMINISTIC
	CONTAINS SQL
	SQL SECURITY DEFINER
	COMMENT '角色查询'
BEGIN
	SELECT * FROM t_users;
END$$

# #############################################################################账户注册
DROP PROCEDURE IF EXISTS nodejs.PRO_REGISTER_ACCOUNT;
# ####################################################################
CREATE DEFINER = `root`@`localhost` PROCEDURE `PRO_REGISTER_ACCOUNT`(
	IN `@account` VARCHAR(255),
	IN `@password` VARCHAR(255),
	IN `@token` VARCHAR(255),
	OUT `@errcode` INT,
	OUT `@errmsg` TINYTEXT)
LANGUAGE SQL
NOT DETERMINISTIC
CONTAINS SQL
SQL SECURITY DEFINER
COMMENT '账户注册'
BEGIN 
TOP:BEGIN
	DECLARE nCount INT;
	SELECT * FROM nodejs.t_accounts WHERE account = `@account`;
	SELECT FOUND_ROWS() INTO nCount;
	IF nCount > 0 THEN
		SET `@errcode` = 1;
		SET `@errmsg` = N'该账号已经存在,请选择其他账号, 如果忘记密码请使用找回功能';
		LEAVE TOP;
	END IF;
	INSERT INTO t_accounts (`account`, `password`, `token`, `gold`) VALUES (`@account`, `@password`, `@token`, 20);
	SET `@errcode` = 0;
	SET `@errmsg` = N'注册成功!';
END TOP;
END$$

# #############################################################################账户登录
DROP PROCEDURE IF EXISTS nodejs.PRO_LOGIN_ACCOUNT;
# ####################################################################
CREATE DEFINER = `root`@`localhost` PROCEDURE `PRO_LOGIN_ACCOUNT`(
	IN `@account` VARCHAR(255),
	IN `@password` VARCHAR(255),
	IN `@token` VARCHAR(255),
	OUT `@errcode` INT,
	OUT `@errmsg` TINYTEXT)
LANGUAGE SQL
NOT DETERMINISTIC
CONTAINS SQL
SQL SECURITY DEFINER
COMMENT '账户登录'
BEGIN
TOP:BEGIN
	DECLARE nCount INT DEFAULT 0;
	SET `@errcode` = -2;
	SELECT * FROM nodejs.t_accounts WHERE account = `@account` AND password = `@password`;
	SELECT FOUND_ROWS() INTO nCount;
	IF nCount > 0 THEN
		SET `@errcode` = 0;
		SET `@errmsg` = N'';
		UPDATE nodejs.t_accounts SET `token` = `@token` WHERE account = `@account` AND password = `@password`;
		LEAVE TOP;
	END IF;
	SET `@errcode` = -1;
	SET `@errmsg` = N'账号或密码不正确,请检查后再输!';
END TOP;
END$$




delimiter ;