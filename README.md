# Note taking app - API

A basic ASP.NET Core API for performing CRUD operations on a PostGreSQL database.

### Installing .NET SDK using the C# Dev Kit VS Code extension

Install [C# Dev Kit VS Code Extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit). The [C# extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) and the [.NET Install Tool](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-runtime) will automatically be installed.

When you open a folder or workspace that contains a C# project, such as this project, the extension will activate. Do the first two getting started steps in the welcome page:

- Connect your Microsoft account
- Install the .NET SDK

![](/assets/images/csharp-vscode-extension.png)

## Installing dependencies

Run the following command to install the project dependencies:

```bash
dotnet restore
```


## Installing PostgreSQL

If you use MacOS, install PostgreSQL using [Homebrew](https://brew.sh/):

```sh
brew install postgresql
```

If you use a different operating system, see the [PostgreSQL downloads page](https://www.postgresql.org/download/). 


## Creating a PostgreSQL table for todos

First, run the PostgreSQL database:

```bash
brew services start postgresql
```

Then, connect to PostgreSQL using the `psql` command-line tool:

```bash
psql postgres
```

Once you're in the PostgreSQL prompt, which starts with `postgres=#`, run the following SQL command:

```
CREATE USER user WITH PASSWORD '<create-a-password>';
```

Create a `todos` database and give the user all privileges:

```sql
CREATE DATABASE todos;
GRANT ALL PRIVILEGES ON DATABASE todos TO admin;
```

Exit the `postgres` terminal by entering `/q`. Run a migration to create a "TodoItem" table using the C# model in `Models/TodoItem`:

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

Connect to the database again:

```bash
psql postgres
```

Populate the "TodoItems" table with some todos:

```sql
INSERT INTO "TodoItems" ("Title", "IsDone") VALUES
    ('Buy groceries', false),
    ('Learn ASP.NET Core', false),
    ('Go to the gym', true),
    ('Read a book', false),
    ('Write documentation', false);
```

Run the server:

```bash
dotnet run
```

