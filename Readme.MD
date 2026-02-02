# 🎓 Student Management System – Backend API

A **backend RESTful API** built with **ASP.NET Core**, **SQL Server**, and **ADO.NET** to manage student records efficiently.  
The project uses **Stored Procedures** for all database operations, ensuring **performance, security, and maintainability**.

---

## 🚀 Features

- ➕ Add Student
- ✏️ Update Student (Stored Procedure based)
- ❌ Delete Student
- 📦 Bulk Add Students (Excel / List based)
- 🧠 Model Validation (API + Database level)
- 🔐 Transaction handling
- ⚡ Optimized SQL Server queries

---

## 🛠️ Tech Stack

- **Backend:** ASP.NET Core Web API
- **Language:** C#
- **Database:** SQL Server
- **Data Access:** ADO.NET
- **Database Logic:** Stored Procedures
- **Validation:** Data Annotations + DB Validations
- **API Style:** RESTful APIs


---

## 🧾 Database Design

### 📌 Stored Procedures Used

- `sp_AddStudent`
- `sp_UpdateStudent`
- `sp_DeleteStudent`
- `sp_GetStudents`
- `sp_BulkInsertStudents`

### 🧠 Validations Inside Stored Procedures

- Duplicate Email / Mobile Number
- Required fields check
- Transaction rollback for bulk insert
- Business rule validation

---

## 📦 API Endpoints (Sample)

| Method | Endpoint | Description |
|------|---------|-------------|
| POST | `/api/students` | Add student |
| PUT | `/api/students/{id}` | Update student |
| DELETE | `/api/students/{id}` | Delete student |
| POST | `/api/students/bulk` | Bulk add students |
| GET | `/api/students` | Get all students |

---

## 🧪 Validation Strategy

### API-Level
- DataAnnotations
- DTO validation
- ModelState validation

### Database-Level
- Stored Procedure validations
- TRY...CATCH with transactions
- Custom error messages

---

## 📂 Bulk Upload Flow

1. Upload student data
2. Insert into staging/temporary table
3. Validate records
4. Insert valid data into final table
5. Return success & failure records

---

## 🧠 Key Learnings

- Real-world **ADO.NET usage**
- Writing optimized **SQL Server Stored Procedures**
- Handling **bulk data uploads**
- Transaction management
- Clean backend API design

---

## 🔮 Future Enhancements

- 🔐 JWT Authentication
- 📄 Role-based authorization
- 📊 Pagination & filtering
- 🧪 Unit & Integration testing
- ☁️ Docker & Cloud deployment

---

## ▶️ How to Run

1. Clone the repository
2. Configure SQL Server connection string in `appsettings.json`
3. Execute database scripts & stored procedures
4. Run the API using Visual Studio or `dotnet run`

---

## 🤝 Contributing

Pull requests are welcome.  
For major changes, please open an issue first.

---

## 📄 License

This project is for learning and demonstration purposes.
