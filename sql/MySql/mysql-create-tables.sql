
CREATE TABLE person (
	id INT AUTO_INCREMENT PRIMARY KEY NOT NULL,
	name VARCHAR(150) NOT NULL,
	date_birthday DATE NOT NULL,
	active BOOLEAN,
	id_user INT NULL,
	created_at DATETIME NULL,
	updated_at DATETIME NULL,
	id_user_change INT NULL,
	deleted BOOLEAN
)