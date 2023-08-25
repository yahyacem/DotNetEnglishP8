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
    public class HomeControllerTests : UnitBase
    {
        public HomeControllerTests() : base() {}
        [Fact]
        public async Task Index_Should_Return_ConsultantListViewModel()
        {
            // Arrange
            List<Consultant> consultants = GetSeedConsultants(10);
            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfiles());
            }));
            ConsultantListViewModel consultantListViewModel = new();
            foreach (var c in consultants)
            {
                consultantListViewModel.ConsultantViewModels.Add(mapper.Map<ConsultantViewModel>(c));
            }
            Mock<IAPIService> apiService = new(); 
            apiService.Setup(x => x.GetConsultantListViewModel())
                .ReturnsAsync(consultantListViewModel);

            HomeController homeController = new(apiService.Object);

            // Act
            var response = await homeController.Index();

            // Assert
            Assert.IsType<ViewResult>(response);
            Assert.NotNull(((ViewResult)response).Model);
        }
        [Fact]
        public void About_Should_Return_ViewBag()
        {
            // Arrange
            Mock<IAPIService> apiService = new(); 
            HomeController homeController = new(apiService.Object);

            // Act
            var response = homeController.About();

            // Assert
            Assert.IsType<ViewResult>(response);
            Assert.Equal("Your application description page.", ((ViewResult)response).ViewData["Message"]);
        }
        [Fact]
        public void Contact_Should_Return_ViewBag()
        {
            // Arrange
            Mock<IAPIService> apiService = new(); 
            HomeController homeController = new(apiService.Object);

            // Act
            var response = homeController.Contact();

            // Assert
            Assert.IsType<ViewResult>(response);
            Assert.Equal("Your contact page.", ((ViewResult)response).ViewData["Message"]);
        }
    }
}