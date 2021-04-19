# Product Services

The sample serverless service will create a REST API for products. The data will be stored in a SQL Server Database. We have a Products table having details related to Product such as Id, Name, Description and Price.

We have basically 5 functinality here :

# Create Product 
POST https://localhost:44332/api/products {"name": "Pencil", "description": "Stationary", "price": 5}

{ "id": 15, "name": "Pencil", "description": "Stationary", "price": 5 }

Request Header : POST /api/products/ HTTP/1.1

Response Header : HTTP/1.1 201 Created

# List Products
GET https://localhost:44332/api/products

[ { "id": 8, "name": "BoardGames", "description": "Games", "price": 150 }, 

{ "id": 4, "name": "Cards", "description": "Games", "price": 50 }, 

{ "id": 2, "name": "Charts", "description": "Stationary", "price": 10 }, 

{ "id": 9, "name": "Crayons", "description": "Stationary", "price": 30 }, 

{ "id": 6, "name": "DairyMilk", "description": "Chocolates", "price": 10 } ]

Request Header : GET /api/products HTTP/1.1

Response Header : HTTP/1.1 200 OK

# Get Product
GET https://localhost:44332/api/products/1

{ "id": 1, "name": "Pen", "description": "Stationary", "price": 20 }

Request Header : GET /api/products/1 HTTP/1.1

Response Header : HTTP/1.1 200 OK

Product Not Found:

{ "message": "{\"result\":null,\"error\":1,\"errorMessage\":\"Product with Id = 101 not found.\",\"count\":0}" }

Request Header : GET /api/products/101 HTTP/1.1

Response Header : HTTP/1.1 404 Not Found

# Delete Product
DELETE https://localhost:44332/api/products/14

Request Header : DELETE /api/products/14 HTTP/1.1

Response Header : HTTP/1.1 200 OK

Product Not Found:

Request Header : DELETE /api/products/23 HTTP/1.1

Response Header : HTTP/1.1 404 Not Found

# Update Product
PUT /api/products/15 HTTP/1.1

Request Header : PUT https://localhost:44332/api/products/15

Response Header : HTTP/1.1 200 OK

Product Not Found:

Request Header : PUT /api/products/23 HTTP/1.1

Response Header : HTTP/1.1 404 Not Found
