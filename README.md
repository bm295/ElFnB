# El FnB Restaurant Management (Hexagonal Architecture)

This repository includes a **Hexagonal Architecture (Ports and Adapters)** implementation for the **El** restaurant management system sized for **80-120 seats**.

## Architecture

```text
/src
  /Domain                 # Core domain model (entities, domain services, business rules)
  /Application            # Core application layer (use cases + ports)
  /Adapters
    /Inbound              # Driving adapters (entrypoints/controllers)
    /Outbound             # Driven adapters (repositories, gateways, external integrations)
      /Persistence
      /Integrations
  /Infrastructure         # Composition root / dependency injection wiring
  /Web                    # Current executable inbound adapter
```

## Supported restaurant flows

1. Create order for a table
2. Add / remove items
3. Send order to kitchen
4. Process payment
5. Deduct inventory
6. Close order
7. Generate basic daily sales report

## Runtime target

All new projects target **.NET 10** (`net10.0`).

## Run web app

```bash
dotnet run --project src/Web/Web.csproj
```

To open the full solution in an IDE, use `ElFnB.sln`.

The `Web` project now hosts a server-rendered web dashboard for:

- opening a table ticket
- adding and removing order items
- sending orders to kitchen
- processing payment
- closing orders and reviewing the daily sales snapshot

When the app starts, open the local URL printed by ASP.NET Core in the terminal.

## Docker

Build the image from the repository root:

```bash
docker build -t elfnb-web .
```

Run the web app on port `8080`:

```bash
docker run --rm -p 8080:8080 elfnb-web
```

Then open `http://localhost:8080`.

Useful variants:

```bash
# run in detached mode
docker run -d --name elfnb-web -p 8080:8080 elfnb-web

# stop the detached container
docker stop elfnb-web
```

Notes:

- the container starts the `src/Web` ASP.NET Core host
- the app uses in-memory repositories, so container restarts reset all orders, payments, and reports
