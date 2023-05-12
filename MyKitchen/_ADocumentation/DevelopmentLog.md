## 11/14/2020
returning after working on other projects. Reorient and test user accounts.

## 06/03/2020
move development tracker into excel to improve development process.

## 05/31/2020
set it up so each user can have their own calendar and food item collection.

## 06/01/2020
Created this running on indiehackers

## 05/24/2020
implement the task from 5/16/2020 with a unit test for both users 

## 05/16/2020
let's try having 2 diferent user accounts, and making sure that they have their own MEALS and ITEMS

## 03/01/2020
work on improving logging by using System Diagnostic Tracing

## 12/13/2019
-work to make an ajax call when getting the next perdiction, so we don't have to reload the entire page.

## 08/06/2022
To  improve our understanding of telemetry tools as well as our development workflow, I would like to see on startup logs show up about what envrionment
I'm in, what database I'm using, what .NET envrionment (prod, development) I'm using, etc

## 08/20/2022
Try to get app running locally

## Add a settings gear where we can change the image next to our meals /  need to get a deployment pipeline confident and working!
next continue to work on settings and getting the  red box to appear around the selected meal do it  now.

## 08/21/2022
Can we get the settings to save appropriately to the DB? - need to keep working on this!

# 08/22/2022 - whenw e return get the db  connection working for the user settings please!

# 08/27/2022 - fix prod

# 09/01/2022 
- create a shopping list witht he ability to export to amazon list  api the purpose of this is to make finding what we need at the grocery store more efficient.

# 09/09/2022
revisit prod and clean up some of our data? create badges on github?

# 09/10/2022
It is overwhelming seeing so many foods in our list - leads to decision fatigue - we need a queue where the list will be narrowed down to what 
we actually want to eat in the coming days!

# 09/17/2022 
Starting work on the Queue function unless we get distracted with cleanup/refactoring
DOCS - mealbuildercontroller should allow a filter to only show queued -> need  to add button to 'remove from queue'

LEFT OFF: C:\src\gh\MyKitchen\MyKitchen\Controllers\MealBuilderController.cs get the queued only checkbox working - will likely need to create a new class that contains the searcharguments
that we will bind to when posting the form

# 09/24/2022
-Get Remove from Queue Working
-Add a search box for 'queued' to the dashboard

# 09/25/2022
-get the queued box working

# 10/01/2022
-test queued functio

# 10/01/2022
-Create a cool about page to discribe this product
    
# 10/1/2022 
-create alerts and push notifications if there is nothing on the calendar for the next 3 days

# 10/2/2022 
-consider creating executive function tasks lists - display on dashboard
-let's do a bit of refactoring to stay in the groove..
-need more unit tests before we can do major refactoring

# 10/06/2022
-cleanup and removing unused code - constant refactoring
-we should create a public library of .NET tools and publish it
# 10/09/2022
    -learning entity framework! can we do a write-up about this?
        //https://learn.microsoft.com/en-us/ef/core/modeling/backing-field?tabs=data-annotations
        //TODO is it possible have entity framework write to a
        //Backing fields allow EF to read and/or write to a field rather than a property.This can be useful when encapsulation in the class is being used to restrict the use of and/or enhance the semantics around access to the data by application code, but the value should be read from and/or written to the database without using those restrictions/enhancements.

# 10/27/2022
-try fixing calendar on prod

# 10/29/2022
-get the saffolder working

# 11/02/2022
Fixed Production SQL Password
For Next Time, AddAzureWebAppDiagnostics Check to see if the azure streaming  logs are working
For Next Time, need to come back and fix the calendar

# 11/03/2022
fix calendar locally 

# 11/13/2022
it would be cool if we got a user to sign up for this - write a blog post and try to get a user to signup
*tried to make it so "sign in with google" will work for existing acounts that have not been previously  associated with google..
*test this next time we work on this!

# 11/26/2022
We want sign up with "google" to work - need to work onn changing IdentiyUser to ApplicationUser in external login page...

# 12/25/2022
[need to fix signup] [need to fix landing page] [add highlights for next 3 days on calendar]
[I think it would be better if we handled the calendar with REACT!]

# 12/25/2022 
    continue working on react test project - maybe should be a seperate branch - having react 
https://medium.com/@tysonnero/iteratively-migrating-asp-net-mvc-razor-to-react-typescript-e0330fe81b4e read this for how to add react calendar!

# 01/10/2022
continue with react test project

# 03/23/2023
consider blazor branch
# 04/20/2023
Really Enjoyed this reddit post as it relates to dedication to programming
Hobby/Job/Career/Career+/Hobby+/Career+Hobby+/P=L/EIDISLO from https://www.reddit.com/r/csharp/comments/v2osjd/generating_random_modern_houses_an_example_from/

# 04/23/2023
Why did my last release break? Consider rewriting entire app in blazor again


# 4/25/2023
why is my nuget connection broken? need to try in visual studio proper

# 4/28/2023
now in proper, debating if I should sqllite locally..

# 4/30/2023
Would still like to rewrite the entire app using web assembly blazor

# 05/02/2023
Before doing this, seperate the UI from the business logic best we can - or just dive in with a new branch

# 05/04/2023
Having Issues with NETGeneralLibrary - pipeline build in github is failling

# 05/05/2023
Put NETGeneralLibrary in Nuget? will that resolve it.
https://learn.microsoft.com/en-us/nuget/nuget-org/publish-a-package

# 05/10/2023 
Checkin2
