using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Complainatron.Controllers;
using Complainatron.Core.Services;
using FakeItEasy;
using System.Web.Mvc;
using FluentAssertions;
using FluentAssertions.Assertions;
using Complainatron.Models;

namespace Tests.Complainatron
{
    [TestClass]
    public class FriendsControllerTests
    {
        private FriendsController _controller;
        private ILogService _loggingService;
        private IFacebookService _facebookService;

        [TestInitialize]
        public void SetUp()
        {
            _loggingService = A.Fake<ILogService>();
            _facebookService = A.Fake<IFacebookService>();
            _controller = new FriendsController(_facebookService, _loggingService);
        }


        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_ask_the_facebook_service_for_a_list_of_friends()
        {
            //Arrange


            //Act
            _controller.Index();

            //Assert
            A.CallTo(() => _facebookService.GetFriends()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_return_a_partial_view()
        {
            var result = _controller.Index();
            result.Should().BeOfType<PartialViewResult>();
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_return_a_partial_view_called__index()
        {
            var result = _controller.Index() as PartialViewResult;
            result.ViewName.Should().Be("_Index");
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_have_a_collection_of_friends_as_the_views_model()
        {
            //Arrange
            var friends = TestUtilities.GetTestFriends();

            A.CallTo(() => _facebookService.GetFriends()).Returns(friends);

            //Act
            var result = _controller.Index() as PartialViewResult;
            
            //Assert
            var model = result.Model;
            model.Should().BeOfType<FriendsIndexModel>();
            FriendsIndexModel viewModel = model as FriendsIndexModel;
            viewModel.Friends.Should().Equal(friends);
        }
    }
}
