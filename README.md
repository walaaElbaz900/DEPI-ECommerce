# SQL Script for CarShowroom Database

## Role Insertion Script

```sql
-- Use the CarShowroom database
USE CarShowroom;

-- Insert the User role
INSERT INTO AspNetRoles (Id, Name, NormalizedName)
VALUES (1, 'User', 'USER');

-- Insert the Admin role
INSERT INTO AspNetRoles (Id, Name, NormalizedName)
VALUES (2, 'Admin', 'ADMIN');
