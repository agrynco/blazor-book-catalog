# Book Catalog Management System

## Overview

The Book Catalog Management System allows users to manage and track a collection of books. The solution consists of two main components:

1. **Backend**: Exposes API endpoints for managing books.
2. **Frontend**: Provides a web interface for users to interact with the catalog.

## Features

### Backend

* **CRUD Operations**: Create, Read, Update, and Delete books.
* **Search and Filter**: Search and filter books by title, author, and genre.
* **Pagination and Sorting**: View books with pagination and sorting options.
* **Bulk Upload**: Import multiple books using a CSV file.
* **Bonus**: Rate limiting to prevent API abuse.

### Frontend

* **Table View**: Display books in a table format.
* **Search and Filter**: Search and filter books by title, author, and genre.
* **Add Books**: Add new books using a form.
* **Update Books**: Edit book details.
* **Delete Books**: Remove books from the catalog.
* **Responsive Design**: Works seamlessly on desktop and mobile devices.
* **Bonus**: Real-time updates to the book list.

## Technologies

* **Backend**: C#, ASP.NET Core Minimal API
* **Frontend**: Blazor WebAssembly
* **Database**: In-memory storage
* **Containerization**: Docker, Docker Compose

## Prerequisites

Before running the application, ensure you have the following installed on your system:

* **.NET SDK (version 8.0 or later)**: [Download .NET SDK](https://dotnet.microsoft.com/download)
* **Docker and Docker Compose**: [Download Docker](https://www.docker.com/products/docker-desktop)
* **A modern web browser**: e.g., Chrome, Edge, Firefox

## Running the Application

You can run the application locally using scripts or by following manual steps.

### 1. Using the Provided Scripts

#### Windows

1. Open a terminal and navigate to the root of the repository.
2. Use one of the following scripts:
   * **Run with Docker Compose:**

     ```
     run-docker.bat
     ```

     The application will start:

     * **API:** [http://localhost:5000/swagger](http://localhost:5000/swagger)
     * **Frontend:** [http://localhost:8080](http://localhost:8080)
   * **Run without Docker:**

     ```
     run-bookcatalog.bat
     ```

     The application will start:

     * **API:** [http://localhost:5000/swagger](http://localhost:5000/swagger)
     * **Frontend:** [http://localhost:5163](http://localhost:5163)
   * **Clean Docker Environment:**

     ```
     clean-docker.bat
     ```

     Stops and removes all Docker containers, images, and networks associated with the project.

#### Linux/Mac

1. Open a terminal and navigate to the root of the repository.
2. Use one of the following scripts:
   * **Run with Docker Compose:**

     ```
     ./run-docker.sh
     ```

     The application will start:

     * **API:** [http://localhost:5000/swagger](http://localhost:5000/swagger)
     * **Frontend:** [http://localhost:8080](http://localhost:8080)
   * **Run without Docker:**

     ```
     ./run-bookcatalog.sh
     ```

     The application will start:

     * **API:** [http://localhost:5000/swagger](http://localhost:5000/swagger)
     * **Frontend:** [http://localhost:5163](http://localhost:5163)

### 2. Running Manually

If you prefer not to use the scripts, follow these steps:

#### Step 1: Start the API

1. Open `<span>BookCatalog.sln</span>` in an IDE (e.g., Visual Studio or JetBrains Rider).
2. Restore dependencies and run the solution.
3. The API will be available at [http://localhost:5000/swagger](http://localhost:5000/swagger).

#### Step 2: Start the Frontend

1. Navigate to the `<span>Frontend</span>` folder.
2. Restore dependencies and run the project:
   ```
   dotnet restore
   dotnet run
   ```
3. The frontend will be available at [http://localhost:5163](http://localhost:5163).

## Folder Structure

```
.
├── .github/             # GitHub workflows and templates
├── BookCatalog.API/     # Backend API project
├── BookCatalog.Frontend/ # Frontend Blazor WebAssembly project
├── clean-docker.bat     # Script to clean Docker environment (Windows)
├── run-bookcatalog.bat  # Script to run projects without Docker (Windows)
├── run-docker.bat       # Script to run projects with Docker Compose (Windows)
├── books.csv            # Sample CSV file for bulk upload
├── README.md            # Project documentation
├── run-bookcatalog.sh   # Script to run projects without Docker (Linux/Mac)
├── run-docker.sh        # Script to run projects with Docker Compose (Linux/Mac)
├── BookCatalog.sln      # Solution file
├── docker-compose.yml   # Docker Compose file
```

## API Endpoints

### Base URL

`<span>http://localhost:5000</span>`

### Endpoints

* **GET** `<span>/books</span>` - Retrieve a paginated list of books with optional search and filtering.
* **GET** `<span>/books/{id}</span>` - Retrieve details of a specific book.
* **POST** `<span>/books</span>` - Add a new book.
* **PUT** `<span>/books/{id}</span>` - Update book details.
* **DELETE** `<span>/books/{id}</span>` - Delete a book.
* **POST** `<span>/books/bulk-upload</span>` - Upload books from a CSV file.

## CSV File Format for Bulk Upload

The CSV file should have the following columns:

* Title
* Author
* Genre

Example:

```
Title,Author,Genre
The Great Gatsby,F. Scott Fitzgerald,Fiction
To Kill a Mockingbird,Harper Lee,Fiction
```

## Known Issues

* None reported.

## Future Enhancements

* Integration with a persistent database (e.g., SQL Server).
* Enhanced user authentication and authorization.
* Advanced filtering and sorting options.

## License

This project is open-source and available under the MIT License.

## Contributing

Contributions are welcome! Please submit a pull request or open an issue for any bugs or feature requests.

---

Thank you for using the Book Catalog Management System!
