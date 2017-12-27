use nodejs;
delimiter $$
# #############################################################################创建账户表格
DROP TABLE IF EXISTS nodejs.t_accounts;
CREATE TABLE `t_accounts` (
	`id` INT NOT NULL 
	`account` VARCHAR(255) NOT NULL,
	`password` VARCHAR(255) NOT NULL,
	`token` VARCHAR(255) NOT NULL,
	`gold` INT NOT NULL DEFAULT 20,
	`tokenstamp` TIMESTAMP NOT NULL,
	
	PRIMARY KEY (`account`)
)
COLLATE='utf8_general_ci'
ENGINE=InnoDB;$$

delimiter ;