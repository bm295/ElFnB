# El Gaucho Hanoi - FnB Management (Hexagonal Architecture)

This repository now includes a **Hexagonal Architecture (Ports and Adapters)** implementation for a restaurant management system sized for **80-120 seats**.

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
  /Api                    # Current executable inbound adapter
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

## Run sample flow

```bash
dotnet run --project src/Api/Api.csproj
```

The sample runs a full order lifecycle and prints a basic report.
