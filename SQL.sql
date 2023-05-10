drop DATABASE IF EXISTS quanlysinhvien;



GO
CREATE DATABASE quanlysinhvien;
GO
USE quanlysinhvien;

CREATE TABLE adminaccount (
    admin_id VARCHAR(10) NOT NULL,
    username varchar(50) DEFAULT NULL,
    password varchar(50) DEFAULT NULL,
    PRIMARY KEY (admin_id),
    UNIQUE KEY username (username)
);

-- Tạo bảng students
CREATE TABLE students (
    student_id varchar(10) NOT NULL,
    name varchar(50) DEFAULT NULL,
    address varchar(100) DEFAULT NULL,
    phone_number varchar(20) DEFAULT NULL,
    email varchar(50) DEFAULT NULL,
    date_of_birth datetime DEFAULT NULL,
    gender varchar(10) DEFAULT NULL,
    PRIMARY KEY (student_id)
);

-- Tạo bảng courses
CREATE TABLE courses (
    course_id VARCHAR(10) NOT NULL,
    course_name varchar(50) DEFAULT NULL,
    course_credit int DEFAULT NULL,
    PRIMARY KEY (course_id)
);

-- Tạo bảng grades
CREATE TABLE grades (
    student_id varchar(10) NOT NULL,
    course_id VARCHAR(10) NOT NULL,
    attendance_grade float DEFAULT NULL,
    midterm_grade float DEFAULT NULL,
    final_grade float DEFAULT NULL,
    PRIMARY KEY (student_id, course_id),
    CONSTRAINT grades_ibfk_1 FOREIGN KEY (student_id) REFERENCES students (student_id) ON DELETE CASCADE,
    CONSTRAINT grades_ibfk_2 FOREIGN KEY (course_id) REFERENCES courses (course_id) ON DELETE CASCADE
);

-- Tạo bảng studentaccount
CREATE TABLE studentaccount (
    student_id varchar(10) NOT NULL,
    username varchar(50) DEFAULT NULL,
    password varchar(50) DEFAULT NULL,
    PRIMARY KEY (student_id),
  
);

CREATE TABLE pending (
    id INT IDENTITY(1,1),
    student_id VARCHAR(10) NOT NULL,
    name VARCHAR(50) DEFAULT NULL,
    address VARCHAR(100) DEFAULT NULL,
    phone_number VARCHAR(20) DEFAULT NULL,
    email VARCHAR(50) DEFAULT NULL,
    date_of_birth datetime DEFAULT NULL,
    gender VARCHAR(10) DEFAULT NULL,
    created_at datetime DEFAULT GETDATE(),
    PRIMARY KEY (id)
);

-- Thêm foreign key vào bảng studentaccount để kết nối với bảng students
ALTER TABLE studentaccount ADD FOREIGN KEY (student_id) REFERENCES students (student_id) ON DELETE CASCADE;

-- Thêm foreign key vào bảng grades để kết nối với bảng students và bảng courses
ALTER TABLE grades ADD FOREIGN KEY (course_id) REFERENCES courses (course_id);
