CREATE SCHEMA service_center;

USE service_center;

CREATE TABLE customers (
    id_cus INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `name` VARCHAR(50) NOT NULL,
    `position` VARCHAR(50) NOT NULL,
    birthday DATE NOT NULL,
    mail VARCHAR(100) NOT NULL,
    phone BIGINT UNSIGNED NOT NULL
);

CREATE TABLE services (
    id_ser INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `name` VARCHAR(100) NOT NULL
);

CREATE TABLE equipment_class (
    id_eq_cl INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `name` VARCHAR(100) NOT NULL
);

CREATE TABLE vendor (
    id_ven INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `name` VARCHAR(100) NOT NULL
);

CREATE TABLE `position` (
    id_pos INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `name` VARCHAR(100) NOT NULL
);

CREATE TABLE `status` (
    id_stat INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `name` VARCHAR(100) NOT NULL
);

CREATE TABLE equipment (
    id_eq INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    model VARCHAR(5) NOT NULL,
    id_class INT UNSIGNED NOT NULL,
    CONSTRAINT fk_equipment_class_id FOREIGN KEY (id_class)
		REFERENCES equipment_class (id_eq_cl),
	id_vendor INT UNSIGNED NOT NULL,
    CONSTRAINT fk_equipment_vendor_id FOREIGN KEY (id_vendor)
		REFERENCES vendor (id_ven)   
);

CREATE TABLE details (
    id_det INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `name` VARCHAR(100) NOT NULL,
    part_number VARCHAR(100) NOT NULL,
    note VARCHAR(100),
    quantity INT UNSIGNED NOT NULL,  
	id_vendor INT UNSIGNED NOT NULL,
    CONSTRAINT fk_details_vendor_id FOREIGN KEY (id_vendor)
		REFERENCES vendor (id_ven)   
);

CREATE TABLE detail_to_equipment (
    id_equipment INT UNSIGNED NOT NULL,
    id_detail INT UNSIGNED NOT NULL,
    PRIMARY KEY (id_equipment , id_detail),
    CONSTRAINT FOREIGN KEY (id_equipment)
        REFERENCES equipment (id_eq),
    CONSTRAINT FOREIGN KEY (id_detail)
        REFERENCES details (id_det) 
);

CREATE TABLE employees (
    id_empl INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
    `name` VARCHAR(100) NOT NULL,
    qualification VARCHAR(100) NOT NULL,
	id_position INT UNSIGNED NOT NULL,
    CONSTRAINT fk_employees_position_id FOREIGN KEY (id_position)
		REFERENCES `position` (id_pos)   
);

CREATE TABLE requests (
    id_req INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
	date_time_start DATETIME NOT NULL,
    date_time_end DATETIME,
    urgency VARCHAR(20) NOT NULL,
    series VARCHAR(45) NOT NULL,
    id_customer INT UNSIGNED NOT NULL,
    CONSTRAINT fk_requests_customers_id FOREIGN KEY (id_customer)
		REFERENCES customers (id_cus),
	id_equipment INT UNSIGNED NOT NULL,
    CONSTRAINT fk_requests_equipment_id FOREIGN KEY (id_equipment)
		REFERENCES equipment (id_eq),
	id_service INT UNSIGNED NOT NULL,
    CONSTRAINT fk_requests_services_id FOREIGN KEY (id_service)
		REFERENCES services (id_ser),
    id_employee_reception INT UNSIGNED NOT NULL,
    CONSTRAINT fk_requests_employees_rec_id FOREIGN KEY (id_employee_reception)
		REFERENCES employees (id_empl),
	id_employee_engineer INT UNSIGNED,
    CONSTRAINT fk_requests_employees_eng_id FOREIGN KEY (id_employee_engineer)
		REFERENCES employees (id_empl),
	id_status INT UNSIGNED NOT NULL,
    CONSTRAINT fk_requests_status_id FOREIGN KEY (id_status)
		REFERENCES `status` (id_stat)
);

CREATE TABLE invoices (
    id_inv INT UNSIGNED NOT NULL PRIMARY KEY AUTO_INCREMENT,
	date_time_start DATETIME NOT NULL,
    date_time_end DATETIME,
    note VARCHAR(100),
    id_employee_engineer INT UNSIGNED,
    CONSTRAINT fk_invoices_employees_eng_id FOREIGN KEY (id_employee_engineer)
		REFERENCES employees (id_empl),
	id_employee_provider INT UNSIGNED,
    CONSTRAINT fk_invoices_employees_rec_id FOREIGN KEY (id_employee_provider)
		REFERENCES employees (id_empl),
	id_status INT UNSIGNED NOT NULL,
    CONSTRAINT fk_invoices_status_id FOREIGN KEY (id_status)
		REFERENCES `status` (id_stat)
);

CREATE TABLE detail_to_invoice (
    id_detail INT UNSIGNED NOT NULL,
    id_invoice INT UNSIGNED NOT NULL,
    PRIMARY KEY (id_detail , id_invoice),
    CONSTRAINT FOREIGN KEY (id_detail)
        REFERENCES details (id_det),
    CONSTRAINT FOREIGN KEY (id_invoice)
        REFERENCES invoices (id_inv),
	quantity INT UNSIGNED NOT NULL  
);