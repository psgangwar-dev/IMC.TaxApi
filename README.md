# TaxApi

TaxApi API endpoints provide detailed sales tax rates and calculations. Currently this Api supports below two methods 
1. `GET /v1/tax/rate` : Gets the Tax rates for a given location
2. `POST /v1/tax/ordersaletax` : Calculates the taxes for an order

## Api Specifications:

* [Getting Started](#getting-started)
* [Prerequisites](#prerequisites)
* [Code Structure](#code-structure)
* [ApiConsumer Configuration](#api-configuration)
* [Api Actions](#api-actions) 
* [Swagger Documetation](#api-swagger-field) 
* [Testing](#testing)
* [Version History](#api-version-history) 


## Getting Started

These instructions will help you to get a copy of this project up and running on your local machine for development and testing purposes. 

### Prerequisites

#### Required software instalations

1. Coding Platform : [.Net Core](https://dotnet.microsoft.com/download) v3.1
1. Testing Tool (Optional) : [Postman](https://www.getpostman.com/downloads/)
1. Code Editors : [Visual Studio 2019](https://msdn.microsoft.com/en-us/) / [Visual Studio Code](https://code.visualstudio.com/download) 

#### Clone the repository from github

```bash
cd [working directory] #(e.g. ~/code or C:\users\you\src)
git clone https://github.com/psgangwar-dev/IMC.TaxApi.git
cd TaxApi
```
## Code Structure
As shown in the screen shot below this solution contains 5 different projects  
1. **IMC.TaxApi.Host** : This is the host project for this solution. It conatins the initialization of all the depencies in Startup file, Conatins TaxController , which has definition for both the action methods, launchSetting information and appsettings.json files. 
2. **IMC.TaxApi.Core** : This project contains below directories. 
    1. **Providers** : This directory contains all the providers for this solution, these providers act as an service layer and conatin all the Orchestartion logic in it. 
    2. **Repositoies** : This directory contains all "Data Access Layers"/Repository classes. These repositories can communicate to the required ApiClients based on the ApiConsumer value.
    3. **Validators** : Contains all the request valodator classes. 
    4. **Mappers** : Conatins all request and response mapper classes. 
    5. **RestClients** : Contains the wrapper for all the downstream/external partner rest clients. 
3. **IMC.TaxApi.Models** : This is a shared project which contains the definition of all request and response models, Entities required by the Api, and other shared class e.g. Contants.cs class.
4. **IMC.TaxApi.Host.Tests** : Unit test project for for IMC.TaxApi.Host project
5. **IMC.TaxApi.Core.Test** : Unit test project for for IMC.TaxApi.Core project

![image](https://user-images.githubusercontent.com/82673102/115160120-8eecf600-a064-11eb-9ab1-b00a262a3e25.png)


## ApiConsumer Configuration
Both Api Actions require a ConsumerKey to be passed in the request header. Based on the ConsumerKey RestClient Gateway can fetch the required partner configurtaions (Endpoint, AuthSecret etc) and call differnet third party endpoints accoringly. Below is the current configuration for the same.

**appSettings.json**
`"ConsumerKeyPartnerConfiguration": {
    "PartnerConfigurations": [
      {
        "ConsumerKey": "IMC",
        "PartnerBaseEndpoint": "https://api.taxjar.com/",
        "AuthToken": "5da2f821eee4035db4771edab942a4cc",
        "TokenAuthRequired": true
      },
      {
        "ConsumerKey": "IMC_TEST",
        "PartnerBaseEndpoint": "https://sample.com",
        "AuthToken": "test_key",
        "TokenAuthRequired": false
      }
    ]
  }`
  
## Api Actions/Methods
1. `GET /v1/tax/rate` : Gets the Tax rates for a given location

3. `POST /v1/tax/ordersaletax` : Calculates the taxes for an order

## Swagger Documetation
LaunchSetting.js has the swagger URL as the launch URL. Once the code is executed users will be able to see the SwaggerUI on their browsers as below. 

![image](https://user-images.githubusercontent.com/82673102/115161077-99f65500-a069-11eb-99ef-3cefcdbc7c6a.png)

## Testing
1. `GET /v1/tax/rate`


    **Request Section**
        
        ![image](https://user-images.githubusercontent.com/82673102/115161246-9a432000-a06a-11eb-9615-9c69064026d0.png)
    
    **Response**
        
        ![image](https://user-images.githubusercontent.com/82673102/115161274-cced1880-a06a-11eb-94bc-0725817c3752.png)


2. `POST /v1/tax/ordersaletax`


    **Request Section**
        
        ![image](https://user-images.githubusercontent.com/82673102/115161993-ccef1780-a06e-11eb-8cb2-6f4b972380cd.png)
    
    **Error Response**
    
        ![image](https://user-images.githubusercontent.com/82673102/115162020-fa3bc580-a06e-11eb-8417-4d7ba26d4367.png)
        
    **Success Response**
        ![image](https://user-images.githubusercontent.com/82673102/115162127-ab426000-a06f-11eb-98e8-e699de6b68de.png)
      

## Version History
    * ReadMe.md Created By Prabha S Gangwar on 04/17/2021
    * Content updated by Prabha S Gangwar on 04/18/2021
