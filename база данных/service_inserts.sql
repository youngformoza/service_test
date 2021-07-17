LOAD DATA INFILE  'C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/customers_s.csv'
INTO TABLE customers
FIELDS TERMINATED BY ';' 
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 ROWS;

INSERT INTO vendor (`name`) VALUES
('Xerox'),
('HP'),
('Konica Minolta'),
('Rowe'),
('Samsung');

INSERT INTO equipment_class (`name`) VALUES
('Versalink'),
('Altalink'),
('Versant'),
('Ecoprint'),
('ROWE Scan'),
('SCX'),
('SF');

INSERT INTO equipment (model, id_class, id_vendor) VALUES
('3025', 4, 5),
('C8130', 2, 1),
('B8145', 2, 1),
('B205', 1, 1),
('B615', 1, 1),
('i4', 4, 4),
('i6', 4, 4), 
('i8', 4, 4),
('i10', 4, 4), 
('450i', 5, 4), 
('850i', 5, 4),
('8240', 6, 5),
('345', 7, 5);

INSERT INTO details (`name`, part_number, note, quantity, id_vendor) VALUES
('Барабан', '013R00662', 'оригинальный', 10, 1),
('Тонер', '006R01702', 'голубой', 8, 1),
('Ролик переноса', '115R00116', 'оригинальный', 1, 1),
('Картридж', 'C9455A', 'светло-пурпурный', 2, 2),
('Картридж', 'C9454A', 'желтый', 2, 2),
('Печка в сборе', 'A3PEPP3V01', 'bizhub 226', 1, 3),
('Барабан', 'BT0000/07/00/001', null, 1, 4),
('Нить заряда', 'MA0000/10/00/995', null, 5, 4),
('Озоновый фильтр', 'BT0000/36/00/047', null, 10, 4),
('Плата ADF', 'JC92-02446B', null, 1, 5);

INSERT INTO detail_to_equipment (id_equipment, id_detail) VALUES
(1, 10),
(2, 2),
(3, 1),
(6, 8),
(7, 7),
(10, 9),
(4, 3),
(2, 1);

INSERT INTO services (`name`) VALUES
('Диагностика'),
('Техническое обслуживание'),
('Ремонт');

INSERT INTO `status` (`name`) VALUES
('Новое'),
('Принято сотрудником'),
('Ожидает деталь'),
('Ожидает заказчика'),
('Завершено');

INSERT INTO `position` (`name`) VALUES
('Инженер'),
('Кладовщик'), 
('Диспетчер');

LOAD DATA INFILE  'C:/ProgramData/MySQL/MySQL Server 8.0/Uploads/employee_s.csv'
INTO TABLE employees
FIELDS TERMINATED BY ';' 
ENCLOSED BY '"'
LINES TERMINATED BY '\n'
IGNORE 1 ROWS;

INSERT INTO requests (date_time_start, date_time_end, urgency, series, id_customer, id_equipment, id_service, id_employee_reception, id_employee_engineer, id_status) VALUES
('20200225134040', '20200302152020', 'обычный', 4874515488, 2, 2, 1, 1, 6, 4),
('20210628154000', null, 'обычный', 74512584, 1, 4, 2, 8, 7, 3),
('20200301130040', '20200303152020', 'высокий', 45454678, 5, 3, 3, 1, 2, 4),
('20210708154000', null, 'высокий', 25843548, 7, 9, 1, 8, 10, 1);

INSERT INTO invoices (date_time_start, date_time_end, note, id_employee_engineer, id_employee_provider,  id_status) VALUES
('20200226204040', '20200228152020',null, 6, 3, 4),
('20210630204000', null, null, 7, 4, 2),
('20200301150040', '20200302082020', null, 2, 3, 4),
('20210709150000', null, null, 8, 3, 1);

INSERT INTO detail_to_invoice (id_detail, id_invoice, quantity) VALUES
(1, 3, 1),
(3, 2, 5),
(4, 1, 2), 
(2, 4, 3);

