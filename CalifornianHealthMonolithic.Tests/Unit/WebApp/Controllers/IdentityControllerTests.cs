using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using CalifornianHealthMonolithic.APIBooking.Controllers;
using CalifornianHealthMonolithic.APIBooking.Models;
using CalifornianHealthMonolithic.APIBooking.Repositories;
using CalifornianHealthMonolithic.APIBooking.Services;
using CalifornianHealthMonolithic.APIConsultant.Repositories;
using CalifornianHealthMonolithic.APIConsultant.Services;
using CalifornianHealthMonolithic.Shared;
using CalifornianHealthMonolithic.Shared.DBContext;
using CalifornianHealthMonolithic.Shared.Mappers;
using CalifornianHealthMonolithic.WebApp.Mappers;
using CalifornianHealthMonolithic.Shared.Models;
using CalifornianHealthMonolithic.Shared.Models.Entities;
using CalifornianHealthMonolithic.WebApp.Areas.Identity.Pages.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using static CalifornianHealthMonolithic.WebApp.Areas.Identity.Pages.Account.LoginModel;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;
using CalifornianHealthMonolithic.Tests.Integration;

namespace CalifornianHealthMonolithic.Tests.Unit.WebApp.Controllers
{
    [Collection("Sequential")]
    public class IntentityControllerTests : UnitBase
    {
        public IntentityControllerTests() : base() {}
        [Fact]
        public async Task Login_Should_Return_Token()
        {
            // Arrange
            // Claims that will be added to user during login
            List<Claim> claims = new();

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);

            // Set up parameters and instantiate service
            // Create mock instances using Moq
            #nullable disable
            var userStoreMock = new Mock<IUserStore<Patient>>();
            var userManagerMock = new Mock<UserManager<Patient>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(patients.First());
            userManagerMock.Setup(x => x.AddClaimsAsync(It.IsAny<Patient>(), It.IsAny<List<Claim>>())).Callback<Patient, IEnumerable<Claim>>((i, obj) => claims = obj.ToList()).ReturnsAsync(new IdentityResult());

            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<Patient>>();

            var signInManagerMock = new Mock<SignInManager<Patient>>(userManagerMock.Object, contextAccessorMock.Object, userPrincipalFactoryMock.Object, null, null, null, null);
            signInManagerMock.Setup(x => x.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>(), It.IsAny<bool>())).ReturnsAsync(SignInResult.Success);
            #nullable restore

            string fakeToken = "123456789ACCESSTOKEN";
            Mock<IAuthenticationService> mockAuthenticationService = new();
            mockAuthenticationService.Setup(x => x.GenerateAccessToken(It.IsAny<Patient>())).Returns(fakeToken);
            
            Mock<ILogger<LoginModel>> mockLogger = new Mock<ILogger<LoginModel>>();

            // Set the user data in the model
            InputModel inputModel = new() 
            {
                UserName = patients.First().UserName,
                Password = "Password.123",
                RememberMe = false
            };
            LoginModel loginModel = new(signInManagerMock.Object, userManagerMock.Object, mockLogger.Object, mockAuthenticationService.Object)
            {
                Input = inputModel
            };

            // Act
            var response = await loginModel.OnPostAsync("/");

            // Assert
            Assert.NotNull(response);
            Assert.NotEmpty(claims);
            Assert.Single(claims);
            Assert.Equal(claims.First().Value, fakeToken);
        }
        [Fact]
        public async Task Register_Should_Return_Token()
        {
            // Arrange
            // Claims that will be added to user during login
            List<Claim> claims = new();

            // Get seed data for database
            List<Patient> patients = GetSeedPatients(1);

            // Set up parameters and instantiate service
            // Create mock instances using Moq
            #nullable disable
            // UserStore
            var userStoreMock = new Mock<IUserStore<Patient>>();
            // UserManager
            var userManagerMock = new Mock<UserManager<Patient>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
            userManagerMock.Setup(x => x.FindByNameAsync(It.IsAny<string>())).ReturnsAsync(patients.First());
            userManagerMock.Setup(x => x.AddClaimsAsync(It.IsAny<Patient>(), It.IsAny<List<Claim>>())).Callback<Patient, IEnumerable<Claim>>((i, obj) => claims = obj.ToList()).ReturnsAsync(new IdentityResult());

            // IHttpContextAccessor
            var contextAccessorMock = new Mock<IHttpContextAccessor>();
            // IUserClaimsPrincipalFactory
            var userPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<Patient>>();

            // SignInManager
            var signInManagerMock = new Mock<SignInManager<Patient>>(userManagerMock.Object, contextAccessorMock.Object, userPrincipalFactoryMock.Object, null, null, null, null);
            userManagerMock.Setup(x => x.CreateAsync(It.IsAny<Patient>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            #nullable restore

            string fakeToken = "123456789ACCESSTOKEN";
            Mock<IAuthenticationService> mockAuthenticationService = new();
            mockAuthenticationService.Setup(x => x.GenerateAccessToken(It.IsAny<Patient>())).Returns(fakeToken);
            
            Mock<ILogger<RegisterModel>> mockLogger = new Mock<ILogger<RegisterModel>>();

            IMapper mapper = new Mapper(new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CalifornianHealthMonolithic.WebApp.Mappers.MappingProfiles());
            }));

            // Set the user data in the model
            RegisterModel.InputModel inputModel = new() 
            {
                UserName = patients.First().UserName,
                Password = "Password.123",
                ConfirmPassword = "Password.123",
                FName = patients.First().FName,
                LName = patients.First().LName,
                Address1 = patients.First().Address1,
                Address2 = patients.First().Address2,
                City = patients.First().City,
                Postcode = patients.First().Postcode
            };
            RegisterModel registerModel = new(userManagerMock.Object, userStoreMock.Object, signInManagerMock.Object, mockAuthenticationService.Object, mockLogger.Object, mapper)
            {
                Input = inputModel
            };

            // Act
            var response = await registerModel.OnPostAsync("/");

            // Assert
            Assert.NotNull(response);
            Assert.NotEmpty(claims);
            Assert.Single(claims);
            Assert.Equal(claims.First().Value, fakeToken);
        }
        public interface ISignInResultWrapper
        {
            bool Succeeded { get; }
        }

        public class SignInResultWrapper : ISignInResultWrapper
        {
            private readonly SignInResult _signInResult;

            public SignInResultWrapper(SignInResult signInResult)
            {
                _signInResult = signInResult;
            }

            public bool Succeeded => _signInResult.Succeeded;
        }
    }
}