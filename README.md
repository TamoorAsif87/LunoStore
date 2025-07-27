# 🛍️ LunoStore

**LunoStore** is a **Modular Monolithic e-commerce platform**, built using **ASP.NET Core** on the backend and **React + Vite + TypeScript** on the frontend. It’s designed with scalability, clean code principles, and developer experience in mind — following modern design patterns and domain-driven design principles.

---

## 🧱 Architecture Overview

### ✅ Modular Monolith (ASP.NET Core)

This project follows a **Modular Monolithic** structure — each module is independent, encapsulated, and communicates through well-defined contracts.

- 🔧 **Modules**:
  - `Catalog` – Product and category management
  - `Basket` – Shopping cart and item operations
  - `Ordering` – Checkout process using **Outbox Pattern**
  - `Identity` – Authentication, profile management with JWT & refresh tokens

- ✳️ **Cross-cutting Concerns**:
  - Logging via pipeline
  - Validation using FluentValidation
  - CQRS using **MediatR**
  - Specification Pattern for complex querying
  - Decorator & Factory Patterns for extensibility
  - Repository Pattern for persistence abstraction

---

## 🗂️ Project Structure

/Bootstrapper/Api → Entry point for API project
/Modules/
├── Catalog
├── Basket
├── Ordering
└── Identity
/BuildingBlocks → Shared,Shared.Contracts,Shared.Messaging(MassTransit)


Each module:
- Has its own domain, application, and infrastructure layers
- Registers its own services and dependencies
- Can evolve independently

---

## 🐳 Dockerized Infrastructure

```yaml
services:
  inkwell:               # PostgreSQL for main DB
    image: postgres

  distributedcache:      # Redis for caching
    image: redis

  seq:                   # Seq for structured logging
    image: datalust/seq:latest

  messagebus:            # RabbitMQ for messaging/event-driven support
    image: rabbitmq:management

  api:                   # Your .NET API
    build:
      context: ./Bootstrapper/Api
      dockerfile: Dockerfile

🧪 Backend Tech Stack
ASP.NET Core 8

PostgreSQL (via EF Core)

Redis (caching)

RabbitMQ (event bus)

MediatR (CQRS, pipeline behaviors)

FluentValidation

Outbox Pattern in Ordering

Specification Pattern for flexible filtering

Serilog + Seq for logging

Cloudinary for image uploads

JWT + Refresh Tokens


🖼️ Frontend – Modern React Stack

/UI
  ├── features/           → Feature-based logic (basket, catalog,  etc.) (hooks,schemas)
  ├── pages/              → Route-level pages
  ├── components/         → Reusable UI parts
  ├── hooks/             → Reusable Hooks
  ├── store/             → Redux Toolkit slices
  ├── Api/               → Api Calls like(AUTH,Order etc)


🔧 Tools & Libraries
React 19 + TypeScript

Vite for dev server and build

DaisyUI + TailwindCSS for elegant styling

Zod + React Hook Form for type-safe form validation

React Query for async state management

Redux Toolkit for app-wide state

React Helmet Async for SEO/title management

Feature-based layout support for Admin & User



🔐 Authentication & Identity
JWT Access & Refresh tokens

Protected routes and authorization guards

Profile update, password change, secure auth flows

💡 Highlights
🧩 Modular and loosely-coupled architecture

🧠 Clean code practices with layered separation

🧾 Outbox pattern ensures reliable event publishing

📷 Cloudinary-powered image upload and management

🧵 Validation & Logging via MediatR Pipelines

⚛️ Snappy React frontend with Vite + modern DX

🧪 Built-in loading, error handling, and UX feedback

🛠️ Getting Started
Clone the repo

git clone https://github.com/TamoorAsif87/LunoStore.git
cd LunoStore
Run backend & services (Docker)

docker compose up --build
🔐 Make sure .env files for backend services and frontend are properly configured (Cloudinary keys, JWT secrets, DB strings, etc.)


Start frontend (Dev)
cd UI
npm install
npm run dev

Future RoadMap

 Payment integration (Stripe or PayPal)

 Product ratings and reviews

 Admin analytics dashboard

 Email notifications (SendGrid or SMTP)

 Localization (i18n)



📄 License
Licensed under the MIT License.

Developed with ❤️ by Tamoor Asif


---

Let me know if you'd like:

- Badges for CI/CD, DockerHub, License, etc.
- GitHub Pages/docs structure
- Auto-generated API docs (Swagger UI section)

Ready to export this into `README.md` or push directly to your repo if needed.



