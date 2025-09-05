# EcommerceWebAPI
**StackBuld Technical Assessment**

This is a simple .NET 8 Web API project built using Model–View–Controller (MVC) architecture.
It implements basic product and order management with proper error handling, validation, and clean API design.

**Technologies Used:**

⦁	Microsoft SQL Server (SQL Express). when creating a new database

⦁	Entity Framework Core:

 	Microsoft.EntityFrameworkCore.SqlServer

 	Microsoft.EntityFrameworkCore.Tools

⦁	NSwag.AspNetCore for OpenAPI/Swagger docs

⦁	ASP.NET Core MVC (ApiController, routing, model binding, validation)

**How to Run**

Clone the repository

Ensure SQL Server Express is installed and running

Update your appsettings.json with this connection string below:

**Connection String:**

 ("Data Source=.\\sqlexpress;Initial Catalog=StackBuldTechnicalAssessment;Integrated Security=True;TrustServerCertificate=True")

Run the project with:

dotnet run --launch-profile https (to make .Net 8 run the program using the https profiles defined in the launchsetting.json)


Server runs at:
https://localhost:7000/swagger

**Database Setup & Seeding**

On application startup, the database is automatically seeded with:

⦁	15 sample products

⦁	OrderStatus reference data 

ORDER STATUS:
1 = Pending
2 = OrderPlaced
3 = Successful (when the order is successfully delivered to customer)
4 = Failed (when order isn't successful for some reason)


For testing, runtime seeding provides dynamic sample data for products and OrderStatus.

**Orders Workflow**

Create Order Endpoint requires a parameter with this structure below

Request body parameter example:

{
  "customerName": "Alice Johnson",
  "customerEmail": "alice@example.com",
  "customerAddress": "123 Main Street, City",
  "items": [
    { "productId": 1, "quantity": 2 },
    { "productId": 3, "quantity": 1 }
  ]
}


**Notes:**

IsPaid is automatically set to true (simulated payment success for this assessment).

In production, payment gateway integration would be used, and order would only be marked paid on confirmation.

Hardcoded customer details for now, in a real app, Users would be authenticated and Order would only store CustomerId which we can use to retrieve customer details via navigation property.

**Payment Handling**

ConfirmPayment endpoint updates:

⦁ OrderStatus → pending/orderplaced

⦁ IsPaid → true/false

For simplicity, payments are assumed successful in this assessment. In production, integration with Stripe/PayPal/etc. would handle this.

**Get Orders**

For this assessment, GetOrders returns all orders (with order items & products). In a real app with authentication, Admins see all orders, while customers only see their own orders (match UserId with CustomerId).

**Products**

Products are stored in the Products table with fields: Id, Name, Description, Price, StockQuantity, etc.

Supports CRUD operations:

Create (POST /products)

Read (single or paginated list)

Update (PUT /products/{id})

Delete (DELETE /products/{id})

**Error Handling**

Implemented using ProblemDetails Middleware. Example error response when sending invalid JSON:

{
  "type": "https://httpstatuses.com/400",
  "title": "Validation errors occurred.",
  "status": 400,
  "errors": {
    "$": [
      "The JSON object contains a trailing comma..."
    ],
    "productDto": [
      "The productDto field is required."
    ]
  }
}

**Design Decisions**

IDs: Used integer auto-increment IDs for simplicity. In distributed/multi-tenant scenarios, GUIDs would be preferred for uniqueness.

Transactions: Database transactions ensure stock quantities and orders stay consistent and if transaction fails the process is rolled back.

Concurrency: RowVersion tokens protect against multiple users editing the same record at once.

Future-proofing: OrderItems model allows easy extension (discounts, tax, shipping fees).

**Summary**

This project demonstrates my skills and experience in building robust, scalable and clean REST API design, database seeding, proper use of ProblemDetails for errors, basic transactional order creation, extendable order/payment model.

---
Developed by Omotola Odumosu as part of StackBuld Technical Assessment.
