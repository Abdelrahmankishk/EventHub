# EventHub

**EventHub** is a learning-focused C# console project that models an event-management platform where individuals and companies can organize events, attendees can register, and registrations can be used to issue attendee badges.

> **Learning objective:** This is my **first Entity Framework Core (EF Core) project**. I built it to practice EF Core’s core features and understand how C# entities are translated into a relational SQL Server database—not just to build an event-management application.

## What I Practiced with EF Core

- **Code-first database development** and schema evolution through EF Core migrations.
- **Fluent API** configuration in `OnModelCreating` and dedicated entity configuration classes.
- **Data Annotations** for validation and relationship metadata.
- **Owned types** to map an attendee’s address into columns in the `Attendees` table.
- **Shadow properties** for event timestamps that are stored in the EF model/database but are not CLR properties on `Event`.
- **Relationship mapping**: one-to-one, one-to-many, self-referencing relationships, and many-to-many relationships through a join entity.
- **Composite primary keys**, unique indexes, required/optional properties, maximum lengths, SQL default values, and delete behaviors.
- Reading the generated **SQL Server schema** and comparing it with the ERD and EF Core mapping design.

## Technology Stack

- **C# / .NET 8**
- **Entity Framework Core 8** (`Microsoft.EntityFrameworkCore.SqlServer` and `Microsoft.EntityFrameworkCore.Tools`)
- **Microsoft SQL Server**

## Domain Model

| Entity | Purpose |
| --- | --- |
| `Organizer` | Represents a person or company organizing events, including its name, optional company name, and verification status. |
| `OrganizerProfile` | Stores public profile information such as biography, website link, and logo link. |
| `Event` | Stores event details, attendee capacity, dates, organizer, and optional parent event. An event can also contain sessions represented by child `Event` records. |
| `Attendee` | Stores attendee details, email address, home address, registrations, and optional badge. |
| `Address` | An owned type containing the attendee’s street, city, country, and postal code. |
| `Registration` | Join entity between `Attendee` and `Event`, with an optional note and registration timestamp. |
| `Badge` | Represents an attendee’s badge, including badge number, issue date, and tier. |
| `BadgeTier` | Enum containing `Standard` and `VIP`. |

## EF Core Configuration Approaches

One of the project’s main goals is to practice different ways of configuring the EF Core model and to understand where each approach is useful.

| Entity / Feature | Configuration approach | What it demonstrates |
| --- | --- | --- |
| `Organizer` | **Data Annotations** on the entity | Required name, maximum string lengths, and an optional company name. |
| `Badge` | **Data Annotations** on the entity | Required badge number, maximum length, and foreign-key metadata. `Tier` uses the `BadgeTier` enum (`Standard`, `VIP`). |
| `Attendee` | `AttendeeConfiguration : IEntityTypeConfiguration<Attendee>` | Table and primary-key mapping, required fields, maximum lengths, and a unique index on `EmailAddress`. |
| `Address` | **Owned type**, configured inside `AttendeeConfiguration` with `OwnsOne` | Address fields are stored as columns in the `Attendees` table rather than in a separate `Addresses` table. The address fields are required and have explicit maximum lengths. |
| `Event` | `EventConfiguration : IEntityTypeConfiguration<Event>` | Event property constraints, optional `EndDate`, organizer relationship, self-referencing event/session relationship, and required shadow properties. |
| `OrganizerProfile` | **Fluent API** in `AppDbContext.OnModelCreating` | Table name, key, property constraints, and the one-to-one relationship with `Organizer`. |
| `Registration` | **Fluent API** in `AppDbContext.OnModelCreating` | Join table, composite primary key (`EventId`, `AttendeeId`), optional note, and a SQL default for the registration timestamp. |

### Shadow Properties for Event Timestamps

`EventConfiguration` defines `CreationDate` and `ModificationDate` as required EF Core **shadow properties**:

```csharp
builder.Property<DateTime>("CreationDate").IsRequired();
builder.Property<DateTime>("ModificationDate").IsRequired();
```

These properties belong to the EF Core model but are not declared on the `Event` C# class. This is useful for data that the persistence layer manages without exposing it as a regular entity property.

**Implementation note:** the configuration above defines the required shadow properties; automatically setting the creation timestamp and updating the modification timestamp requires additional logic, such as a `SaveChanges` override or a save-changes interceptor, unless it is handled elsewhere.

## Relationships

| Relationship | Mapping | Notes |
| --- | --- | --- |
| `Organizer` → `OrganizerProfile` | One-to-one | A profile is dependent on an organizer and has a required `OrganizerId` foreign key. |
| `Organizer` → `Event` | One-to-many | An organizer can publish multiple events; each event references one organizer. |
| `Event` → `Event` (sessions) | Self-referencing one-to-many | A parent event can contain multiple sessions. Each session can reference at most one parent event. |
| `Attendee` → `Badge` | One-to-one, optional for attendee | An attendee can have at most one badge; a badge belongs to one attendee. |
| `Attendee` ↔ `Event` | Many-to-many through `Registration` | `Registration` stores the composite key plus registration-specific data. |
| `Attendee` → `Address` | Owned type | The address is owned by the attendee and mapped into the `Attendees` table. |

The mapping also specifies relationship delete behaviors, including `Restrict` for the organizer profile and the self-referencing event relationship, and `Cascade` for the event-to-organizer relationship.

## ERD and Mapping Diagrams

### Entity Relationship Diagram

![EventHub ERD](EventHub/files/EventHub-ERD.png)

### EF Core Mapping Diagram

![EventHub Mapping Diagram](EventHub/files/EventHub-Mapping.png)

### SQL Server Schema

![EventHub SQL Server schema](EventHub/files/EventHub-SQLServerSchema.png)

The database schema shows how the model maps to relational tables. The owned address is flattened into columns in `Attendees`, while the event timestamps are represented as shadow-property columns in `Events`.

## Migrations

EF Core migrations are included in `EventHub/Migrations`. They record the schema changes as the model evolves—for example, creating tables and adding the organizer/profile, event/session, registration, and attendee/badge relationships.

Using the **Package Manager Console** in Visual Studio, select the `EventHub` project as the target project and run:

```powershell
# Apply the existing migrations to the configured SQL Server database
Update-Database

# Create a migration after changing the model
Add-Migration <MigrationName>

# Apply the newly created migration
Update-Database
```

## Project Structure

```text
EventHub/
├── Configurations/
│   ├── AttendeeConfiguration.cs
│   └── EventConfiguration.cs
├── files/
│   ├── EventHub Bussiness Rules.pdf
│   ├── EventHub-ERD.png
│   ├── EventHub-Mapping.png
│   └── EventHub-SQLServerSchema.png
├── Migrations/
├── Models/
│   ├── Enums/
│   │   └── BadgeTier.cs
│   ├── Address.cs
│   ├── Attendee.cs
│   ├── Badge.cs
│   ├── Event.cs
│   ├── Organizer.cs
│   ├── OrganizerProfile.cs
│   └── Registration.cs
├── AppDbContext.cs
├── EventHub.csproj
└── Program.cs
```

## Getting Started

### Prerequisites

- .NET 8 SDK
- SQL Server (or a compatible local SQL Server instance)
- Visual Studio with EF Core tools, or the equivalent EF Core command-line tooling

### Setup

1. Clone the repository:

   ```bash
   git clone https://github.com/Abdelrahmankishk/EventHub.git
   cd EventHub
   ```

2. Open `EventHub.sln` in Visual Studio.

3. Check the SQL Server connection string configured in `EventHub/AppDbContext.cs` (`OnConfiguring`) and change it to match your local SQL Server instance.

4. In Visual Studio’s Package Manager Console, select `EventHub` as the target project and apply the migrations:

   ```powershell
   Update-Database
   ```

5. Build and run the console project.

> **Connection-string note:** the current project configures the connection string directly in `AppDbContext.OnConfiguring` for local development. For a deployed or shared environment, move it to configuration or a secret store rather than committing environment-specific connection details.

## Project Scope

EventHub is primarily an **EF Core learning project**. Its focus is entity modeling, mapping configuration, migrations, and understanding how relationships and CLR objects are represented in SQL Server. It is not intended to present a finished production event-management UI.
