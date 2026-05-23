# Restaurant System - Registration & Authentication Guide

## Overview
A complete role-based authentication system has been implemented for the Restaurant Management System with the following features:
- User Registration (Admin and User roles)
- Secure Login with password hashing
- Role-based access control
- Session management with cookies
- Admin-only operations

---

## Features Implemented

### 1. **User Authentication**
- ✅ User Registration page
- ✅ User Login page with "Remember Me" functionality
- ✅ Secure password hashing (SHA256)
- ✅ Cookie-based session management (1 hour default, 7 days with "Remember Me")
- ✅ Logout functionality

### 2. **Role-Based Access Control**
- **Admin Role**: Full access to all features including:
  - Menu Items management (Create, Read, Update, Delete)
  - Categories management (Create, Read, Update, Delete)
  - Delete Orders
  - View and manage Customers
  - Access to Dashboard and Orders

- **User Role**: Limited access to:
  - View Orders
  - Create Orders
  - View Menu Items (Read-only)
  - View Categories (Read-only)
  - Access to Dashboard

### 3. **User Interface Updates**
- Updated sidebar to conditionally show admin-only sections
- User info display with role in sidebar footer
- Logout button in sidebar when authenticated
- Login/Register buttons when not authenticated
- Access Denied page for unauthorized access

---

## Files Created/Modified

### New Files Created:

1. **Controllers/AccountController.cs**
   - Register action (GET & POST)
   - Login action (GET & POST)
   - Logout action (POST)
   - Username and Email uniqueness validation
   - Password confirmation validation

2. **Models/ViewModels/AccountViewModels.cs**
   - RegisterViewModel (Username, Email, Password, ConfirmPassword, Role)
   - LoginViewModel (Username, Password, RememberMe)

3. **Configerations/UserConfiguration.cs**
   - Entity configuration for User model
   - Database constraints (unique indexes on Username and Email)
   - Default values for Role and CreatedAt

4. **Utilities/PasswordHelper.cs**
   - HashPassword: Generates SHA256 hash of password
   - VerifyPassword: Compares plaintext password with stored hash

5. **Views/Account/Register.cshtml**
   - Professional registration form with role selection
   - Client and server-side validation
   - Modern gradient background design

6. **Views/Account/Login.cshtml**
   - Clean login form with "Remember Me" checkbox
   - Success and error message display
   - Professional gradient background design

7. **Views/Account/AccessDenied.cshtml**
   - User-friendly access denied page
   - Navigation buttons to go back or home

### Modified Files:

1. **Program.cs**
   - Added authentication configuration
   - Added cookie authentication scheme "LoginCookie"
   - Added authorization services
   - Added UseAuthentication() and UseAuthorization() middleware

2. **Data/RestaurantDbContext.cs**
   - Added DbSet<User> for Users table

3. **Controllers/MenuItemsController.cs**
   - Added [Authorize] attribute to controller
   - Added [Authorize(Roles = "Admin")] to Create, Edit, Delete actions

4. **Controllers/CategoriesController.cs**
   - Added [Authorize] attribute to controller
   - Added [Authorize(Roles = "Admin")] to Create, Edit, Delete actions

5. **Controllers/OrdersController.cs**
   - Added [Authorize] attribute to controller
   - Added [Authorize(Roles = "Admin")] to Delete actions

6. **Controllers/CustomersController.cs**
   - Added [Authorize] attribute to controller
   - Added [Authorize(Roles = "Admin")] to Delete actions

7. **Views/Shared/_Layout.cshtml**
   - Updated sidebar to show user info when authenticated
   - Conditional display of admin sections based on role
   - Login/Register buttons when not authenticated
   - Logout button when authenticated

8. **Views/_ViewImports.cshtml**
   - Added using statement for ViewModels namespace

---

## Database Schema

### Users Table
```sql
CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY(1, 1),
    [Username] nvarchar(50) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [Role] nvarchar(20) NOT NULL DEFAULT 'User',
    [CreatedAt] datetime2 NOT NULL DEFAULT GETUTCDATE(),
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id]),
    UNIQUE INDEX [IX_Users_Email] ON [Email],
    UNIQUE INDEX [IX_Users_Username] ON [Username]
);
```

---

## How to Use

### Registration
1. Click "Register" button in the sidebar (when not logged in)
2. Fill in Username, Email, Password, and Confirm Password
3. Select role: "User" (default) or "Admin"
4. Click "Register"
5. You'll be redirected to Login page

### Login
1. Click "Login" button in the sidebar
2. Enter Username and Password
3. (Optional) Check "Remember me for 7 days" to stay logged in
4. Click "Login"
5. You'll be redirected to Dashboard

### Access Control
- **Unauthenticated users**: Can only access Home, Account (Register/Login) pages
- **Authenticated Users**: Can view Orders, Customers, Menu Items, Categories
- **Authenticated Admins**: Can manage everything including Create/Edit/Delete for Menu Items and Categories

---

## Security Features

1. **Password Hashing**: SHA256 hashing algorithm
2. **CSRF Protection**: Anti-forgery tokens on all forms
3. **Session Security**: 
   - Default 1-hour expiration
   - 7-day expiration with "Remember Me"
   - Sliding expiration enabled
4. **Unique Constraints**: Username and Email must be unique
5. **Authorization Checks**: Role-based authorization on protected actions

---

## Testing the System

### Test Admin Account
1. Register with username: `admin` and password: `Admin123!`
2. Select "Admin" role
3. Login
4. You should see "Administration" section in sidebar with Menu Items and Categories

### Test User Account
1. Register with username: `user` and password: `User123!`
2. Select "User" role (default)
3. Login
4. You should see "Menu" section in sidebar (read-only access)
5. Try to access /MenuItems/Create - you'll get "Access Denied"

---

## Configuration Details

### Authentication Configuration (Program.cs)
```csharp
builder.Services.AddAuthentication("LoginCookie")
    .AddCookie("LoginCookie", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.ExpireTimeSpan = TimeSpan.FromHours(1);
        options.SlidingExpiration = true;
    });
```

### Authorization Attributes
- `[Authorize]`: Requires authentication
- `[Authorize(Roles = "Admin")]`: Requires Admin role
- `[AllowAnonymous]`: Allows unauthenticated access (used on AccountController)

---

## Next Steps (Optional Enhancements)

1. Implement password strength requirements
2. Add email verification for new accounts
3. Implement password reset functionality
4. Add user profile management
5. Log user activities
6. Implement two-factor authentication (2FA)
7. Use ASP.NET Core Identity for production-grade security
8. Add SSL/TLS certificate for HTTPS

---

## Troubleshooting

### Issue: "Cannot find the object 'Users' because it does not exist"
**Solution**: Run migration: `dotnet ef database update`

### Issue: Username already exists error during registration
**Solution**: Choose a different username, ensure uniqueness

### Issue: "Access Denied" when trying to create menu items as User
**Expected behavior**: Only Admins can create, edit, or delete menu items. Users have read-only access.

### Issue: Login credentials not working
**Check**:
1. Ensure username and password are correct (case-sensitive)
2. Account was successfully registered
3. Database migration was applied

---

## Files Summary

| File | Purpose |
|------|---------|
| AccountController.cs | Handle registration, login, logout |
| User.cs | User model with authentication properties |
| UserConfiguration.cs | EF Core configuration for Users table |
| PasswordHelper.cs | Password hashing and verification utilities |
| RegisterViewModel.cs | ViewModel for registration form |
| LoginViewModel.cs | ViewModel for login form |
| Register.cshtml | Registration page UI |
| Login.cshtml | Login page UI |
| AccessDenied.cshtml | Access denied page UI |
| Program.cs | Authentication middleware configuration |
| RestaurantDbContext.cs | Added Users DbSet |
| _Layout.cshtml | Updated with user info and role-based navigation |

---

## Migration Information

**Latest Migration**: `20260521152158_AddUsersAuthentication`
- Creates Users table
- Adds unique indexes on Username and Email
- Sets default role to "User"
- Sets default CreatedAt to GETUTCDATE()
