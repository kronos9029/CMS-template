CREATE USER IF NOT EXISTS 'vietconstruction_app'@'%' IDENTIFIED BY 'KoyebApp!2026Db';
ALTER USER 'vietconstruction_app'@'%' IDENTIFIED BY 'KoyebApp!2026Db';
GRANT ALL PRIVILEGES ON `viet_construction_cms`.* TO 'vietconstruction_app'@'%';

CREATE USER IF NOT EXISTS 'root'@'%' IDENTIFIED BY 'KoyebRoot!2026Db';
ALTER USER 'root'@'%' IDENTIFIED BY 'KoyebRoot!2026Db';
GRANT ALL PRIVILEGES ON *.* TO 'root'@'%' WITH GRANT OPTION;

FLUSH PRIVILEGES;
