# To-Do App API

A basic ASP.NET Core API for performing CRUD operations on a PostgreSQL database.

### Install the .NET SDK Using the C# Dev Kit VS Code Extension

Install the [C# Dev Kit VS Code extension](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csdevkit). This will also install the [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp) and [.NET Install Tool](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-runtime) extensions.

The C# Dev Kit VS Code extension will activate when you open a folder or workspace that contains a C# project such as this one. 

Do the first two get-started steps in the extension's welcome page:

- Connect your Microsoft account
- Install the .NET SDK

![](/assets/images/csharp-vscode-extension.png)

## Install Dependencies

Run the following command to install the project dependencies:

```bash
dotnet restore
```


## Install PostgreSQL

If you use macOS, install PostgreSQL using [Homebrew](https://brew.sh/):

```sh
brew install postgresql
```

For installation instructions on other operating systems, see the [PostgreSQL downloads page](https://www.postgresql.org/download/).  


## Create a PostgreSQL Table for To-Dos

First, run the PostgreSQL database:

```bash
brew services start postgresql
```

Then, connect to PostgreSQL using the `psql` command-line tool:

```bash
psql postgres
```

Once you're in the PostgreSQL prompt (starting with `postgres=#`), run the following SQL command:

```
CREATE USER admin WITH PASSWORD '<create-a-password>';
```

Create a `todos` database and give the user all privileges:

```sql
CREATE DATABASE todos;
GRANT ALL PRIVILEGES ON DATABASE todos TO admin;
```

Exit the `postgres` terminal by entering `\q`. 

Create a `.env` file in the root directory and add the following PostgreSQL connection string to it:

```
ConnectionStrings__DefaultConnection='Host=localhost;Database=todos;Username=admin;Password=<your-password>'
```

Run a migration to create a "TodoItems" table using the C# model in `Models/TodoItem`:

```bash
dotnet tool install --global dotnet-ef
dotnet ef database update
```

Connect to the database again:

```bash
psql postgres
```

Connect to the todos database:

```sql
\c todos
```

Populate the "TodoItems" table with some to-dos:

```sql
INSERT INTO "TodoItems" ("Title", "IsDone") VALUES
    ('Buy groceries', false),
    ('Learn ASP.NET Core', false),
    ('Go to the gym', true),
    ('Read a book', false),
    ('Write documentation', false);
```

Exit the `postgres` terminal with `\q` and run the .NET server:

```bash
dotnet run
```

