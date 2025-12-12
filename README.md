# Calmspire
🧠 CalmSpire – A Mental Wellness Web Application
Built with ASP.NET MVC, SQL Server, Bootstrap, and C#
📌 Overview

CalmSpire is a mental wellness platform designed to help users improve their emotional and mental well-being.
The system provides mood tracking, gratitude journaling, psychological assessments, AI-based chat support, learning resources, and admin management tools.

This project aims to offer a complete, user-friendly mental health companion web app suitable for students, professionals, and general users.

🎯 Features
✅ User Features
1. Authentication

Secure Login / Registration

Role-based access

Session management

2. User Dashboard

Daily quote

Quick statistics

Shortcuts to major modules

3. Mood Tracking

Add daily mood

Track mood history

Visual charts using Chart.js

Insights based on patterns

4. Gratitude Journal

Add daily gratitude entries

View past journals

Edit + Delete entries

5. Psychological Assessments

Stress, Anxiety, Depression assessments

Multiple-choice + scale-based questions

Auto-score calculation

Personalized recommendations

6. AI ChatBot

Motivational responses

Stress-relief tips

Emotion support

7. Learning Resources

Articles, videos, guides

Filter by category

🛠 Admin Features

Manage users

Add/manage assessments

Add/manage learning resources

Dashboard for analytics

View user activities

🏛 System Architecture

The project follows the MVC (Model-View-Controller) architecture.

Model → Represents database entities  
View → Razor view pages (UI)  
Controller → Business logic + routing  

🗄 Database Structure

Main Entities & Attributes:

User Table

UserId (PK)

Name

Email

PasswordHash

Role

CreatedAt

MoodLog

LogId (PK)

UserId (FK)

MoodLevel

Notes

Date

JournalEntry

EntryId (PK)

UserId (FK)

GratitudesJson

Mood

Date

Assessment

AssessmentId (PK)

Title

Description

Category

AssessmentResult

ResultId (PK)

UserId (FK)

Score

Interpretation

Recommendations

TakenAt

Resource

ResourceId (PK)

Title

Type

Link

Category

CreatedByAdminId (FK)

🧰 Tech Stack
Frontend

HTML5

CSS3

Bootstrap 5

JavaScript

jQuery

Chart.js

Backend

C#

ASP.NET MVC Framework

Entity Framework

LINQ

Database

SQL Server

Tools

Visual Studio 2022

Git/GitHub

PlantUML (for diagrams)

ERD & UML tools

📦 Installation Guide
1. Clone the Repository
git clone https://github.com/yourusername/calmspire.git

2. Open in Visual Studio

Open .sln file

Restore NuGet packages

3. Configure Database

Update Web.config connection string

Run migrations or create tables manually

4. Build & Run

Click:
▶ Run (IIS Express)

📁 Project Structure
CalmSpire/
│── Controllers/
│── Models/
│── Views/
│── Scripts/
│── Content/
│── Filters/
│── Migrations/
│── App_Start/
│── Web.config
