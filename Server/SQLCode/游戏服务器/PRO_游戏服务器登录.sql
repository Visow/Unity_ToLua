use nodejs;
delimiter $$

# #############################################################################游戏服务器,账号登录
DROP PROCEDURE IF EXISTS nodejs.PRO_GAME_LOGIN_ACCOUNT;
# ####################################################################
CREATE DEFINER = `root`@`localhost` PROCEDURE `PRO_GAME_LOGIN_ACCOUNT`(
	IN `@token` VARCHAR(255))
LANGUAGE SQL
NOT DETERMINISTIC
CONTAINS SQL
SQL SECURITY DEFINER
COMMENT '游戏服务器,账号登录'
BEGIN
TOP:BEGIN
	SELECT * FROM nodejs.t_accounts WHERE token = `@token`;
END TOP;
END$$

delimiter ;