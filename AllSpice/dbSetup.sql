CREATE TABLE
    IF NOT EXISTS accounts(
        id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name varchar(255) COMMENT 'User Name',
        email varchar(255) COMMENT 'User Email',
        picture varchar(255) COMMENT 'User Picture'
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS recipes(
        id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        title VARCHAR(255) NOT NULL,
        picture VARCHAR(255),
        subtitle VARCHAR(255),
        category ENUM(
            'breakfast',
            'lunch',
            'dinner',
            'dessert',
            'other'
        ),
        creatorId VARCHAR(255) NOT NULL,
        FOREIGN KEY (creatorId) REFERENCES accounts(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS ingredients(
        id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        name VARCHAR(255) NOT NULL,
        quantity VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY(recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS steps(
        id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
        createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
        updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
        position INT,
        body VARCHAR(255),
        recipeId INT NOT NULL,
        FOREIGN KEY(recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';

CREATE TABLE
    IF NOT EXISTS favorites(
        id INT NOT NULL AUTO_INCREMENT primary key COMMENT 'primary key',
        accountId VARCHAR(255) NOT NULL,
        recipeId INT NOT NULL,
        FOREIGN KEY (accountId) REFERENCES accounts(id) ON DELETE CASCADE,
        FOREIGN KEY(recipeId) REFERENCES recipes(id) ON DELETE CASCADE
    ) default charset utf8 COMMENT '';