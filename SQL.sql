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






-- Thêm foreign key vào bảng grades để kết nối với bảng students và bảng courses
ALTER TABLE grades ADD FOREIGN KEY (course_id) REFERENCES courses (course_id);



-- Them du lieu
insert into adminaccount values ('1','1','1')

INSERT INTO students (student_id, name, address, phone_number, email, date_of_birth, gender)
VALUES ('21010001', 'Nguyen Van A', '123 ABC Street, Hanoi', '0987654321', '21010001@st.phenikaa-uni.edu.vn', '2001-01-01', 'Male');

INSERT INTO students (student_id, name, address, phone_number, email, date_of_birth, gender)
VALUES ('21010002', 'Tran Thi B', '456 XYZ Street, Hanoi', '0123456789', '21010002@st.phenikaa-uni.edu.vn', '2002-02-02', 'Female');

select * from students