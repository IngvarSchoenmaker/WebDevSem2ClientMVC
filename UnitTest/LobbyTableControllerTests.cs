using AutoFixture.AutoMoq;
using AutoFixture;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using Moq;
using WebDevSem2ClientMVC.Controllers;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using WebDevSem2ClientMVC.Interfaces;

namespace UnitTest
{
    class LobbyTableControllerTests
    {
        private LobbyTableController _lobbyTableController;
        private Mock<IHttpClientManager> _httpClientManagerMock;

        [SetUp]
        public void Setup()
        {
            // Initialiseer de benodigde componenten voor je tests

            _httpClientManagerMock = new Mock<IHttpClientManager>();
            _lobbyTableController = new LobbyTableController(_httpClientManagerMock.Object);
        }

        [TearDown]
        public void Teardown()
        {
            // Voer opruimacties uit na elke test
            _lobbyTableController.Dispose();
        }
        [Test]
        public async Task CreateTable_ValidInput_ReturnsOk()
        {
            // Arrange
            var tableName = "ValidTableName";
            var numberOfPlayers = 4;

            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);

            _httpClientManagerMock.Setup(manager => manager.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _lobbyTableController.CreateTable(tableName, numberOfPlayers) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Table created successfully", result.Value);
        }

        [Test]
        public async Task CreateTable_InvalidInput_ReturnsBadRequest()
        { 
            // Arrange
            var tableName = string.Empty;
            var numberOfPlayers = 6;


            _httpClientManagerMock.Setup(manager => manager.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
       .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.BadRequest));

            // Act
            var result = await _lobbyTableController.CreateTable(tableName, numberOfPlayers) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Invalid input"));
        }

        [Test]
        public async Task CreateTable_PostAsyncFails_ReturnsBadRequest()
        {
            // Arrange
            var tableName = "ValidTableName";
            var numberOfPlayers = 4;

            var expectedResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            _httpClientManagerMock.Setup(manager => manager.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _lobbyTableController.CreateTable(tableName, numberOfPlayers) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Something went wrong"));
        }
        [Test]
        public async Task CreateTable_NameWithEscapeCharacters_ReturnsOk()
        {
            // Arrange
            var tableName = "Na\\m'e;"; // Na\m'e

            _httpClientManagerMock.Setup(manager => manager.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK));

            // Act
            var result = await _lobbyTableController.CreateTable(tableName, 4) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Table created successfully", result.Value);
        }
    }
}
