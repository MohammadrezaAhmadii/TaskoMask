﻿using FluentAssertions;
using MongoDB.Bson;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskoMask.Application.Share.Resources;
using TaskoMask.Application.Tests.Unit.TestData;
using TaskoMask.Application.Workspace.Owners.Commands.Handlers;
using TaskoMask.Application.Workspace.Owners.Commands.Models;
using TaskoMask.Domain.WriteModel.Workspace.Owners.Data;
using TaskoMask.Domain.WriteModel.Workspace.Owners.Entities;
using Xunit;

namespace TaskoMask.Application.Tests.Unit.Workspace
{
    public class OwnerCommandHandlersUnitTests : TestsBase
    {

        #region Fields

        private IOwnerAggregateRepository _ownerAggregateRepository;
        private OwnerCommandHandlers _ownerCommandHandlers;
        private List<Owner> _owners;


        #endregion

        #region Ctor

        //Run before each test method
        public OwnerCommandHandlersUnitTests()
        {
            FixtureSetup();
        }



        #endregion


        #region Test Methods




        [Fact]
        public async Task Create_Owner_Command_Is_Worked_Properly()
        {
            //Arrange
            var createOwnerCommand = new CreateOwnerCommand(ObjectId.GenerateNewId().ToString(), "Test_DisplayName", "Test@email.com", "Test_Password");

            //Act
            var result = await _ownerCommandHandlers.Handle(createOwnerCommand, CancellationToken.None);

            //Assert
            result.EntityId.Should().NotBeNullOrEmpty();
            result.Message.Should().Be(ApplicationMessages.Create_Success);
            var createdUser = _owners.FirstOrDefault(u => u.Id == result.EntityId);
            createdUser.DisplayName.Value.Should().Be(createOwnerCommand.DisplayName);

        }



        #endregion

        #region Private Methods



        private void FixtureSetup()
        {
            _owners = GetOwnersList();

            _ownerAggregateRepository = Substitute.For<IOwnerAggregateRepository>();
            _ownerAggregateRepository.CreateAsync(Arg.Any<Owner>()).Returns(async args => await AddNewOwner((Owner)args[0]));

            _ownerCommandHandlers = new OwnerCommandHandlers(_ownerAggregateRepository, _dummyInMemoryBus);

        }



        private List<Owner> GetOwnersList()
        {
            return new List<Owner>
            {

            };
        }



        private async Task AddNewOwner(Owner owner)
        {
            _owners.Add(owner);
        }



        #endregion
    }
}