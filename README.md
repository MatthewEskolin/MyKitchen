# MyKitchen - Meal Planning App

This app is a tool I created to help me spend less time thinking about what to eat. 

[![Build Status](https://dev.azure.com/BlueProgrammer1/MyKitchen/_apis/build/status/MatthewEskolin.MyKitchen?branchName=main)](https://dev.azure.com/BlueProgrammer1/MyKitchen/_build/latest?definitionId=1&branchName=main)

## Current Development
* Finish testing user accounts 
* I would like to run this as in an Azure App Service as a Progressive Web App - (so it can be used on a phone without being a mobile app)
* I think having Tags for meals would be very helpful - things like "Chicken", "Mexican", "ThanksGiving Dishes" would be very helpful
* Need to fix bug when Uploading Images, when uploading a large image, we need to scape down the image

## Existing Features
* Meal Planning Calendar
* Meal Recommendation Engine
* Meal Database 
* Food Database 

## Learning Focus
* I am using this project to improve my knowledge of programming in .NET 

## Technical Specs
* Uses Azure KeyVault for User Secrets and Passwords
* Try to stay on latest .NET version - Currently .NET 6
* Azure App Service with Azure SQL Database
* Uses Azure Devops - Builds with yml scripts inside a build pipeline - Releases with 'Azure App Service Deploy' task into our azure subscription.
* CI/CD is currently enabled to prod.



## Azure DevOps Organization

This Project currently uses Azure Devops for Project Planning. 
<https://dev.azure.com/BlueProgrammer1/MyKitchen>
