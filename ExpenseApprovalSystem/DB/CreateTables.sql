CREATE TABLE Roles (
    Id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(50) NOT NULL
);

CREATE TABLE Departments (
    Id INT PRIMARY KEY IDENTITY,
    Name VARCHAR(100) NOT NULL,
    HeadId INT NOT NULL,
    FOREIGN KEY (HeadId) REFERENCES Users(Id)
);

CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY,
    Username VARCHAR(50) NOT NULL,
    PasswordHash VARCHAR(256) NOT NULL,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    RoleId INT NOT NULL,
    DepartmentId INT NOT NULL,
    ManagerId INT NULL,
    FOREIGN KEY (RoleId) REFERENCES Roles(Id),
    FOREIGN KEY (DepartmentId) REFERENCES Departments(Id),
    FOREIGN KEY (ManagerId) REFERENCES Users(Id)
);

CREATE TABLE Expenses (
    Id INT PRIMARY KEY IDENTITY,
    SubmitterId INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Category VARCHAR(50) NOT NULL,
    Description TEXT NOT NULL,
    FileData VARBINARY(MAX) NULL,
    SubmittedDate DATETIME NOT NULL,
    Status VARCHAR(20) NOT NULL DEFAULT 'Pending',
    FOREIGN KEY (SubmitterId) REFERENCES Users(Id)
);

CREATE TABLE ApprovalHistory (
    Id INT PRIMARY KEY IDENTITY,
    ExpenseId INT NOT NULL,
    ApproverId INT NOT NULL,
    Action VARCHAR(20) NOT NULL,
    Comments TEXT NULL,
    ApprovalDate DATETIME NOT NULL,
    FOREIGN KEY (ExpenseId) REFERENCES Expenses(Id),
    FOREIGN KEY (ApproverId) REFERENCES Users(Id)
);

CREATE TABLE ApprovalRules (
    Id INT PRIMARY KEY IDENTITY,
    MinAmount DECIMAL(18,2) NOT NULL,
    MaxAmount DECIMAL(18,2) NULL
);

CREATE TABLE ApprovalRuleLevels (
    Id INT PRIMARY KEY IDENTITY,
    RuleId INT NOT NULL,
    Sequence INT NOT NULL,
    ApproverType VARCHAR(50) NOT NULL,
    FOREIGN KEY (RuleId) REFERENCES ApprovalRules(Id)
);