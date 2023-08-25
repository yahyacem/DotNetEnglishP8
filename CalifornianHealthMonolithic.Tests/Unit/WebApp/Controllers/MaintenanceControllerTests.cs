using System.Security.Claims;
using AutoMapper;
using CalifornianHealthMonolithic.Shared;
using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.Tests.Integration;
using CalifornianHealthMonolithic.WebApp.Controllers;
using CalifornianHealthMonolithic.WebApp.Mappers;
using CalifornianHealthMonolithic.WebApp.Models.ViewModels;
using CalifornianHealthMonolithic.WebApp.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace CalifornianHealthMonolithic.Tests.Unit.WebApp.Controllers
{
    public class MaintenanceControllerTests : UnitBase
    {
        public MaintenanceControllerTests() : base() {}
        [Fact]
        public void Index_Should_Return_View()
        {
            // Arrange
            var maintenanceController = new MaintenanceController();

            // Act
            var result = maintenanceController.Index();

            // Assert
            Assert.IsType<ViewResult>(result);
            Assert.Equal(true, ((ViewResult)result).ViewData["HideBanner"]);
        }
    }
}