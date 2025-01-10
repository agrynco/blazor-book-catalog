# **BookCatalog Application**

This repository contains the **BookCatalog API** and **Frontend**, which can be run locally for development and testing purposes.

---

## **Prerequisites**

Before running the application, ensure you have the following installed on your system:

- **.NET SDK** (version 8.0 or later): [Download .NET SDK](https://dotnet.microsoft.com/download)
- **Docker** and **Docker Compose**: [Download Docker](https://www.docker.com/products/docker-desktop)
- A **modern web browser** (e.g., Chrome, Edge, Firefox)

---

## **Running the Application**

You can run the application locally using **scripts** or by following **manual steps**.

---

### **1. Using the Provided Scripts**

#### **Windows**

1. Open a terminal and navigate to the root of the repository.
2. Use one of the following scripts:

  - **Run with Docker Compose:**

     ```cmd
     run-docker.bat
     ```

     The application will start:

     - API: [http://localhost:5000/swagger](http://localhost:5000/swagger)
     - Frontend: [http://localhost:8080](http://localhost:8080/)
 
  - **Run without Docker:**

     ```cmd
     run-bookcatalog.bat
     ```

     The application will start:

   	- API: [http://localhost:5000/swagger](http://localhost:5000/swagger)
   	- Frontend: [http://localhost:5163](http://localhost:5163)

#### **Linux/Mac**

1. Open a terminal and navigate to the root of the repository.
2. Use one of the following scripts:

   - **Run with Docker Compose:**
     ```bash
     ./run-docker.sh
     ```
     The application will start:

     - API: [http://localhost:5000/swagger](http://localhost:5000/swagger)
     - Frontend: [http://localhost:8080](http://localhost:8080/)


   - **Run without Docker:**
     ```bash
     ./run-bookcatalog.sh
     ```
     The application will start:

   	 - API: [http://localhost:5000/swagger](http://localhost:5000/swagger)
   	 - Frontend: [http://localhost:5163](http://localhost:5163)

---

### **2. Running Manually**

If you prefer not to use the scripts, follow these steps:

#### **Step 1: Start the API**

1. Open a terminal and navigate to the **BookCatalog.API** folder:
   ```bash
   cd BookCatalog.API
   ```
