﻿using TaskoMask.Services.Owners.Write.Application.UseCases.Projects.UpdateProject;
using TaskoMask.Services.Owners.Write.UnitTests.Fixtures;

namespace TaskoMask.Services.Owners.Write.UnitTests.UseCases.Projects
{
    public class UpdateProjectTests : TestsBaseFixture
    {

        #region Fields

        private UpdateProjectUseCase _updateProjectUseCase;

        #endregion

        #region Ctor

        public UpdateProjectTests()
        {
        }

        #endregion

        #region Test Methods





        #endregion

        #region Fixture

        protected override void TestClassFixtureSetup()
        {
            _updateProjectUseCase = new UpdateProjectUseCase(OwnerAggregateRepository, MessageBus, InMemoryBus);
        }

        #endregion
    }
}