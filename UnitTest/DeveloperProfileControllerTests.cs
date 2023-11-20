using AutoFixture;
using AutoFixture.AutoMoq;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Options;
using Moq;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDevSem2ClientMVC.Areas.Identity.Data;
using WebDevSem2ClientMVC.Controllers;
using WebDevSem2ClientMVC.Models;

namespace UnitTest
{
    public class DeveloperProfileControllerTests
    {
        private DbContextOptions<ApplicationDBContext> _dbContextOptions;
        private IFixture _fixture;
        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            // Configure an in-memory database for testing
            _dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;

        }

        [Test]
        public async Task Index_ReturnsView()
        {
            // Arrange
            var context = new ApplicationDBContext(_dbContextOptions); // options moet je juist configureren voor tests
            var controller = new DeveloperProfileController(context);

            // Act
            var result = await controller.Index();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ObjectResult>());
            // Voer verdere asserties uit op de viewResult zoals controleren op modelgegevens, statuscodes, enz.
        }


        [Test]
        public async Task Index_ReturnsViewWithData()
        {
            // Arrange
            var dbContext = new ApplicationDBContext(_dbContextOptions);

            var controller = new DeveloperProfileController(dbContext);
            using (var context = new ApplicationDBContext(_dbContextOptions))
            {
                // Voeg testgegevens in
                context.DeveloperProfile.Add(new DeveloperProfile { Discription = "Discription", Email = "email@email.nl", Name = "name", PictureURL = "PictureURL", Skills = "Skills" });
                context.SaveChanges();
            }

            // Act

            var result = await controller.Index() as ViewResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.InstanceOf<ViewResult>());

            var model = result.Model as List<DeveloperProfile>;
            Assert.That(model, Is.Not.Null);
            Assert.That(model, Is.Not.Empty);
        }
        [Test]
        public async Task Details_ReturnsViewWithValidId()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                // Voeg een testgebruikersprofiel toe aan de in-memory database
                context.DeveloperProfile.Add(new DeveloperProfile
                {
                    DeveloperProfileId = 1,
                    Discription = "Discription",
                    Email = "email@email.nl",
                    Name = "name",
                    PictureURL = "PictureURL",
                    Skills = "Skills"
                });
                context.SaveChanges();
            }

            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                var controller = new DeveloperProfileController(context);

                // Act
                var result = await controller.Details(1); // Verzend het gewenste ID

                // Assert
                Assert.That(result, Is.InstanceOf<ViewResult>());
                var viewResult = result as ViewResult;
                Assert.That(viewResult?.Model, Is.Not.Null);
                Assert.That(viewResult.Model, Is.InstanceOf<DeveloperProfile>());
                var model = viewResult.Model as DeveloperProfile;
                Assert.That(model, Is.Not.Null);
                Assert.That(model.DeveloperProfileId, Is.EqualTo(1));
            }
        }

        [Test]
        public async Task Details_ReturnsNotFoundWithInvalidId()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                // Voeg geen ontwikkelaarsprofiel toe aan de in-memory database
            }

            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                var controller = new DeveloperProfileController(context);

                // Act
                var result = await controller.Details(1); // Verzend een ongeldig ID

                // Assert
                Assert.That(result, Is.InstanceOf<NotFoundResult>());
            }
        }
        [Test]
        public void Create_Get_ReturnsView()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                // Arrange
                var controller = new DeveloperProfileController(context);

                // Act
                var result = controller.Create() as ViewResult;

                // Assert
                Assert.That(result, Is.Not.Null);

            }
        }
        [Test]
        public async Task Create_Post_WithValidModel_ReturnsRedirectToAction()
        {
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                // Arrange
                var controller = new DeveloperProfileController(context);
                var model = new DeveloperProfile { Discription = "Discription", Email = "email@email.nl", Name = "name", PictureURL = "PictureURL", Skills = "Skills" };

                // Act
                var result = await controller.Create(model) as RedirectToActionResult;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.ActionName, Is.EqualTo("Index"));
            }
        }

        [Test]
        public async Task Create_Post_WithInvalidModel_ReturnsView()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                var controller = new DeveloperProfileController(context);
                var model = new DeveloperProfile { Discription = "Discription", Email = "email@email.nl", Name = "n", PictureURL = "PictureURL", Skills = "Skills" };
                controller.ModelState.AddModelError("Name", "Must be 2 long");

                // Act
                var result = await controller.Create(model) as ViewResult;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.ViewName, Is.Null.Or.EqualTo(string.Empty).Or.EqualTo("Create"));
                Assert.That(result.Model, Is.InstanceOf<DeveloperProfile>());

                Assert.That(controller.ModelState.IsValid, Is.False);
                Assert.That(controller.ModelState.ContainsKey("Name"), Is.True);
                Assert.That(controller.ModelState.ContainsKey("Email"), Is.False);
            }
        }
        [Test]
        public async Task Edit_Get_WithValidId_ReturnsViewWithDeveloperProfile()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                // Voeg een voorbeeld DeveloperProfile toe aan de in-memory database
                var sampleDeveloperProfile = new DeveloperProfile
                {
                    DeveloperProfileId = 1,
                    Name = "Sample Developer",
                    Skills = "Sample Skills",
                    Discription = "Sample Description",
                    PictureURL = "https://sampleurl.com",
                    Email = "sample@email.com"
                };

                context.DeveloperProfile.Add(sampleDeveloperProfile);
                context.SaveChanges();

                var controller = new DeveloperProfileController(context);

                // Act
                var result = await controller.Edit(1) as ViewResult;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.ViewName, Is.Null.Or.Empty.Or.EqualTo("Edit"));
                Assert.That(result.Model, Is.InstanceOf<DeveloperProfile>());

                // Voeg meer assertions toe afhankelijk van wat je wilt valideren
                var model = result.Model as DeveloperProfile;
                Assert.That(model, Is.Not.Null);
                Assert.That(model.DeveloperProfileId, Is.EqualTo(1));
                Assert.That(model.Name, Is.EqualTo("Sample Developer"));
                Assert.That(model.Skills, Is.EqualTo("Sample Skills"));
                Assert.That(model.Discription, Is.EqualTo("Sample Description"));
                Assert.That(model.PictureURL, Is.EqualTo("https://sampleurl.com"));
                Assert.That(model.Email, Is.EqualTo("sample@email.com"));
            }
        }

        [Test]
        public async Task Edit_Get_WithInvalidId_ReturnsNotFound()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                var controller = new DeveloperProfileController(context);

                // Act
                var result = await controller.Edit(1) as NotFoundResult;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.StatusCode, Is.EqualTo(404));
            }
        }

        [Test]
        public async Task Edit_Post_ValidModel_ReturnsRedirectToIndex()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                var controller = new DeveloperProfileController(context);
                var existingProfile = new DeveloperProfile { DeveloperProfileId = 1, Name = "ExistingProfile", Discription = "Discription", Email = "email@email.nl", PictureURL = "PictureURL", Skills = "Skills" };
                context.Add(existingProfile);
                await context.SaveChangesAsync();
            }
            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                var controller = new DeveloperProfileController(context);
                var updatedModel = new DeveloperProfile { DeveloperProfileId = 1, Name = "UpdatedProfile", Discription = "Discription", Email = "email@email.nl", PictureURL = "PictureURL", Skills = "Skills" };

                // Act
                var result = await controller.Edit(1, updatedModel) as RedirectToActionResult;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.That(result.ActionName, Is.EqualTo("Index"));
            }
        }

        [Test]
        public async Task Edit_Post_InvalidModel_ReturnsViewWithSameProfile()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                var controller = new DeveloperProfileController(context);
                var existingProfile = new DeveloperProfile { DeveloperProfileId = 1, Name = "ExistingProfile", Discription = "Discription", Email = "email@email.nl", PictureURL = "PictureURL", Skills = "Skills" };
                context.Add(existingProfile);
                await context.SaveChangesAsync();
            }

            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                var controller = new DeveloperProfileController(context);
                var updatedModel = new DeveloperProfile { DeveloperProfileId = 1, Name = "UpdatedProfile", Discription = "Discription", Email = "email@email.nl", PictureURL = "PictureURL", Skills = "Skills" };
                controller.ModelState.AddModelError("Name", "Name must be 2 characters long");

                // Act
                var result = await controller.Edit(1, updatedModel) as ViewResult;

                // Assert
                Assert.That(result, Is.Not.Null);
                Assert.Multiple(() =>
                {
                    Assert.That(result.ViewName, Is.Null.Or.Empty);
                    Assert.That(result.Model, Is.InstanceOf<DeveloperProfile>());
                    Assert.That(controller.ModelState.IsValid, Is.False);
                });
            }
        }

        [Test]
        public async Task Edit_Post_NonExistentProfile_ReturnsNotFound()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ApplicationDBContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            using (var context = new ApplicationDBContext(dbContextOptions))
            {
                var controller = new DeveloperProfileController(context);
                var updatedModel = new DeveloperProfile { DeveloperProfileId = 1, Name = "UpdatedProfile" };

                // Act
                var result = await controller.Edit(1, updatedModel) as NotFoundResult;

                // Assert
                Assert.That(result, Is.Not.Null);
            }
        }

    }

}
