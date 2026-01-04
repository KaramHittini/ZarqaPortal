# 🎓 Zarqa Portal

A modern **University Portal** web application built with ASP.NET Core MVC. This system provides a comprehensive platform for managing student profiles, course registration, and academic resources at Zarqa University.

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?style=flat&logo=dotnet)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-512BD4?style=flat)
![Entity Framework](https://img.shields.io/badge/Entity%20Framework-Core%2010-512BD4?style=flat)
![SQL Server](https://img.shields.io/badge/SQL%20Server-2022-CC2927?style=flat&logo=microsoftsqlserver)
![License](https://img.shields.io/badge/License-MIT-green?style=flat)

---

## ✨ Features

- **🏠 Welcome Portal** - Modern landing page with university branding
- **📚 Course Management** - Full CRUD operations for courses (Create, Read, Update, Delete)
- **👤 Student Profiles** - View academic profile including GPA, completed hours, and major
- **🎨 Modern UI** - Clean, responsive design with Bootstrap 5
- **🔒 Secure Architecture** - CSRF protection, input validation, and secure data handling
- **📊 Database Integration** - Entity Framework Core with SQL Server

---

## 🛠️ Technology Stack

| Layer | Technology |
|-------|------------|
| **Framework** | ASP.NET Core MVC (.NET 10) |
| **ORM** | Entity Framework Core 10 |
| **Database** | Microsoft SQL Server |
| **Frontend** | Bootstrap 5, HTML5, CSS3 |
| **Architecture** | Service Layer Pattern, Dependency Injection |

---

## 📁 Project Structure

```
ZarqaPortal/
├── Hi/                                 # Main web project
│   ├── Controllers/                    # MVC Controllers
│   │   ├── CoursesController.cs
│   │   ├── HomeController.cs
│   │   └── StudentProfileController.cs
│   │
│   ├── Core/                           # Domain layer
│   │   ├── Entities/                   # Domain models
│   │   │   ├── Course.cs
│   │   │   ├── User.cs
│   │   │   └── StudentProfile.cs
│   │   └── Enums/
│   │       └── UserRole.cs
│   │
│   ├── Features/                       # Feature-organized services
│   │   ├── Courses/Services/
│   │   │   ├── ICourseService.cs
│   │   │   └── CourseService.cs
│   │   └── Students/Services/
│   │       ├── IStudentProfileService.cs
│   │       └── StudentProfileService.cs
│   │
│   ├── Infrastructure/                 # Infrastructure concerns
│   │   └── Data/
│   │       ├── ZarqaPortalDbContext.cs
│   │       └── Migrations/
│   │
│   ├── Views/                          # Razor Views
│   │   ├── Courses/
│   │   ├── Home/
│   │   ├── StudentProfile/
│   │   └── Shared/
│   │
│   ├── wwwroot/                        # Static files
│   ├── Program.cs                      # Application entry point
│   └── appsettings.json               # Configuration
│
└── ZarqaPortal.sln                    # Solution file
```

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- [SQL Server](https://www.microsoft.com/sql-server) (Express, Developer, or full edition)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) (recommended) or VS Code

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/ZarqaPortal.git
   cd ZarqaPortal
   ```

2. **Update the connection string**
   
   Edit `Hi/appsettings.json` and update the `DefaultConnection` with your SQL Server instance:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ZarqaPortalDb;Trusted_Connection=True;TrustServerCertificate=True"
     }
   }
   ```

3. **Install EF Core tools** (if not already installed)
   ```bash
   dotnet tool install --global dotnet-ef --ignore-failed-sources
   ```

4. **Apply database migrations**
   ```bash
   cd Hi
   dotnet ef database update
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Open in browser**
   
   Navigate to `http://localhost:5182` (or the URL shown in the console)

---

## 📸 Screenshots

### Welcome Page
The landing page welcomes users to the university portal with modern styling and a clear call-to-action.

### Course Catalog
Browse, add, edit, and delete courses with a clean tabular interface.

### Student Profile
View student information including GPA, major, completed credit hours, and college.

---

## 🔧 Configuration

### Database Setup

The application uses Entity Framework Core Code-First migrations. To create a new migration after modifying entities:

```bash
dotnet ef migrations add MigrationName --context ZarqaPortalDbContext --output-dir Infrastructure/Data/Migrations
dotnet ef database update
```

### Environment Variables

| Variable | Description | Default |
|----------|-------------|---------|
| `ConnectionStrings__DefaultConnection` | SQL Server connection string | See appsettings.json |
| `ASPNETCORE_ENVIRONMENT` | Environment mode | Development |

---

## 🏛️ Architecture

This project follows a **clean architecture** approach with:

- **Controllers** - Handle HTTP requests and responses
- **Services** - Contain business logic
- **Entities** - Domain models
- **DbContext** - Entity Framework data access

### Design Patterns Used

- ✅ Dependency Injection (DI)
- ✅ Repository Pattern (via EF Core)
- ✅ Service Layer Pattern
- ✅ MVC Pattern

---

## 🤝 Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

---

## 📝 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

---

## 👨‍💻 Authors

- **Karam Hittini** - *Most work*

---

## 🙏 Acknowledgments

- Zarqa University
- ASP.NET Core Team
- Bootstrap Team

---

<div align="center">
  <sub>Built with ❤️ for Zarqa University</sub>
</div>
