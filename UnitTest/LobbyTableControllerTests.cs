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
using WebDevSem2ClientMVC.Models;

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
            //_lobbyTableController = new LobbyTableController(_httpClientManagerMock.Object);
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
            LobbyTable lobbyTable = new LobbyTable
            {
                TableName = "ValidTableName",
                NumberOfPlayers = 4
            };
            

            var expectedResponse = new HttpResponseMessage(HttpStatusCode.OK);

            _httpClientManagerMock.Setup(manager => manager.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _lobbyTableController.CreateTable(lobbyTable) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Table created successfully", result.Value);
        }

        [Test]
        public async Task CreateTable_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            LobbyTable lobbyTable = new()
            {
                TableName = string.Empty,
                NumberOfPlayers = 6
            };

            // Act
            var result = await _lobbyTableController.CreateTable(lobbyTable) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Invalid input"));
        }

        [Test]
        public async Task CreateTable_PostAsyncFails_ReturnsBadRequest()
        {
            // Arrange
            LobbyTable lobbyTable = new()
            {
                TableName = "ValidTableName",
                NumberOfPlayers = 4
            };

            var expectedResponse = new HttpResponseMessage(HttpStatusCode.InternalServerError);

            _httpClientManagerMock.Setup(manager => manager.PostAsync(It.IsAny<string>(), It.IsAny<HttpContent>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _lobbyTableController.CreateTable(lobbyTable) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(400, result.StatusCode);
            Assert.IsTrue(result.Value.ToString().Contains("Something went wrong"));
        }
        [Test]
        public async Task CreateTable_NameWithEscapeCharacters_ReturnsOk()
        {
            // Arrange
            LobbyTable lobbyTable = new()
            {
                TableName = "Na\\m'e;",
                NumberOfPlayers = 4
            };

            // Act
            var result = await _lobbyTableController.CreateTable(lobbyTable) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Table created successfully", result.Value);
        }
    }
}
