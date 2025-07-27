global using Shared.DDD;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.EntityFrameworkCore.Metadata.Builders;
global using Shared.Data;
global using Shared.Contracts.CQRS;
global using MediatR;
global using AutoMapper;
global using FluentValidation;
global using Microsoft.Extensions.Logging;
global using MassTransit;
global using System.Reflection;

global using Microsoft.AspNetCore.Builder;
global using Microsoft.EntityFrameworkCore.Diagnostics;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Ordering.Data;
global using Shared.Interceptors;

global using Ordering.Orders.Dtos;
global using Ordering.Orders.Models;
global using Ordering.ValueObjects;
global using Ordering.Orders.Exceptions;