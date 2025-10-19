# Laboratory Management System

A Laboratory Management System built using **C#** and **Windows Forms** to streamline laboratory processes. The system allows **Admins** to manage laboratorians, tests, and machine inventories, while **Laboratorians** can add patients, assign tests, and generate reports. This system aims to simplify the management and organization of laboratory operations.

## Features

### Admin Features:
- Add, update, and remove laboratorians.
- Add, update, and delete laboratory tests.
- Manage machine inventory and view machine status.
- View reports on lab income.

### Laboratorian Features:
- Add new patients to the system.
- Assign and manage patient tests.
- Generate test reports for patients.
- Report export to PDF format.

## Tech Stack
- **Frontend:** C# with Windows Forms (WinForms)
- **Backend:** C# (using classes and methods for business logic)
- **Database:** Local MSSQL (Configurable)
- **Authentication:** Admin-only login for managing lab operations
- **Report Generation:** Built-in functionality for report creation

## Prerequisites

Before you begin, ensure you have the following installed:

- **Microsoft Visual Studio** (with support for C# and WinForms)
- **MSSQL** for database management
- **.NET Framework** 4.7.2 or later

## Setup Instructions

### 1. Clone the repository:
   ```bash
   git clone https://github.com/affan-t/laboratory-management-system.git
   cd laboratory-management-system
   ```

### 2. Open the project in Microsoft Visual Studio:
   Open the solution file `.sln` in Visual Studio.

### 3. Set up the database:
   Create a new SQLite or MSSQL database (depending on your choice).
   Update the database connection string in the application settings to reflect your environment.

### 4. Build and Run the application:
   Build the project using `Ctrl + Shift + B`.
   Run the application by pressing `F5`.

## Usage

- Launch the application.
- Log in with the admin credentials to manage laboratorians, tests, and inventory.
- Use the laboratorian login to add patients and assign tests.
- Generate and view reports as needed.

## License

- This product is licensed under MIT License.

## Contact

For any questions or issues, feel free to reach out to me via email: **affantahir204@gmail.com**