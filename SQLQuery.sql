CREATE TABLE users (
    id INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    username VARCHAR (255) NULL,
    password VARCHAR (255) NULL,
    date_register DATE NULL,
);

CREATE TABLE parts (
    id INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    product_id VARCHAR(MAX) NULL,
    product_name VARCHAR(MAX) NULL,
    category VARCHAR(MAX) NULL,
    selling_price INT NULL,
    cost_price INT NULL,
    quantity INT NULL,
    insert_date DATE NULL,
    update_date DATE NULL,
    delete_date DATE NULL
);

CREATE TABLE employees (
    id INT PRIMARY KEY IDENTITY (1, 1) NOT NULL,
    employee_id VARCHAR(MAX) NULL,
    full_name VARCHAR(MAX) NULL,
    gender VARCHAR(MAX) NULL,
    contact_number VARCHAR(MAX) NULL,
    position VARCHAR(MAX) NULL,
    image VARCHAR(MAX) NULL,
    salary INT NULL,
    status VARCHAR(MAX) NULL,
    insert_date DATE NULL,
    update_date DATE NULL,
    delete_date DATE NULL
);

INSERT INTO users (username, password, date_register) VALUES ('admin', 'admin', '2024/02/07');