﻿global using Microsoft.AspNetCore.Builder;
global using MyKitchen.Data;
global using System;
global using System.Diagnostics;
global using Microsoft.AspNetCore.Http;
global using Microsoft.Azure.KeyVault;
global using Microsoft.Azure.Services.AppAuthentication;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.Configuration.AzureKeyVault;
global using System.Threading.Tasks;
global using Microsoft.AspNetCore.Hosting;
global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.UI.Services;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using MyKitchen.Controllers;
global using MyKitchen.Models;
global using MyKitchen.Pages;
global using MyKitchen.Services;
global using MyKitchen.Models.BL;
global using MyKitchen.Utilities;
global using Exceptionless;
global using JetBrains.Annotations;
global using Microsoft.Extensions.Logging;
global using MyKitchen.Middleware;