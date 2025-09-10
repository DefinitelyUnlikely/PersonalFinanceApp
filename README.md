# Personal Finance Application

A simple multi-platform app (Windows/Android) for tracking income and expenses, implementing an account system to keep data private and accessible only to the account owner.

## About the Project

This is a school project with the primary aim of deepening my knowledge, with a focus on OOP, the MVVM architecture, .NET MAUI, and using raw SQL queries to communicate with a database.

Using .NET MAUI for an integrated frontend/backend solution together with a PostgreSQL database, the app enables users to track their personal finances. Users can manually enter income or expenses along with transaction dates to create a digital checkbook that tracks their balance.

Sorting functionality allows users to view their balance over specific time periods - by year, month, week, or day.

## Getting Started

Currently, I am not providing executables, meaning you'll need to either build the application yourself or run the project inside an IDE. Instructions for doing this are found below. While it is possible to run, build, and debug the project inside VS Code, I won't be providing those instructions. I learned the hard way that the time and effort required is not worth it compared to simply using Visual Studio.

### Prerequisites
- Visual Studio 2022
- .NET 8.0+
- Android SDK (to run on Android)

### Installation Steps
1. Install Visual Studio 2022
2. Choose to install the .NET MAUI workload
3. Clone the project to your local machine
4. Open the project in Visual Studio
5. Create a PostgreSQL database for the app to connect to (see database setup below)
6. Choose which platform to target
7. Run the project

### Database Setup

To connect to your database, you can either:
- Create a database using the default credentials found in `Finance/Data/Database/Constants.cs`
- If connecting to an existing database with different credentials, enter these credentials as environment variables (also referenced in `Finance/Data/Database/Constants.cs`)

*Note: You can simply change the connection string variable to match your database information if preferred.*

## Roadmap

The project is currently complete. Changes may occur in the future.

## License

This repository and its content fall under the MIT License.

## Contact

martin@kaarjohansson.se
