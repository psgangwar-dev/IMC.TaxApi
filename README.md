# TaxApi

TaxApi API endpoints provide detailed sales tax rates and calculations. Currently this Api supports below two methods 
1. `GET /v1/tax/rate` : Gets the Tax rates for a given location
2. `POST /v1/tax/ordersaletax`  Calculates the taxes for an order

## Api Specifications:

* [Getting Started](#getting-started)
* [Prerequisites](#prerequisites)
* [Api UML](#api-uml-field) 
* [Api Dependency](#api-dependency-field) 
* [Api Architecture](#api-arc-field) 
* [Api Security](#api-security-field) 
* [Api Configuration](#api-config-field) 
* [Api Swagger](#api-swagger-field) 
* [Api in Action](#api-action-field) 
* [Authors](#api-authors-field) 


## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and [testing](docs/testing.md) purposes. Once you know your way around the code on your machine you can continue by reading the [deployment](docs/deploy.md) docs.

Currently the project will be developed on your laptop.

## Prerequisites

### Required software instalations

1. Coding Platform : [.Net Core](https://dotnet.microsoft.com/download) v3.1
1. Testing Tool (Optional) : [Postman](https://www.getpostman.com/downloads/)
1. Code Editors : [Visual Studio 2019](https://msdn.microsoft.com/en-us/) 

### Clone the repository from github

```bash
cd [working directory] #(e.g. ~/code or C:\users\you\src)
git clone https://github.com/psgangwar-dev/IMC.TaxApi.git
cd TaxApi
```
