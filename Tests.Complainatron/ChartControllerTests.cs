using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Complainatron.Controllers;
using Complainatron.Core.Services;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Assertions;
using Complainatron.Core.DTOs;
using Complainatron.Core.Extensions;
using System.Web.Mvc;
using Complainatron.Models;

namespace Tests.Complainatron
{
    [TestClass]
    public class ChartControllerTests
    {
        private ChartController _controller;
        private ILogService _loggingService;
        private ITagService _tagService;
        private IFacebookService _facebookService;

        [TestInitialize]
        public void SetUp()
        {
            _loggingService = A.Fake<ILogService>();
            _tagService = A.Fake<ITagService>();
            _facebookService = A.Fake<IFacebookService>();
            _controller = new ChartController(_facebookService, _loggingService, _tagService);
        }

        [TestMethod]
        [TestCategory("Creation")]
        public void It_should_throw_an_exception_if_tag_service_is_null()
        {
            //arrange
            Action creation = () => new ChartController(_facebookService, _loggingService, null);

            //acts and assert
            creation.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_ask_the_service_to_get_all_tags_with_their_respective_counts()
        {
            //arrange
            _controller.Index();

            A.CallTo(() => _tagService.GetTagsWithCount()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_return_a_view()
        {
            var result = _controller.Index();
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_have_a_chart_data_view_model_in_its_view()
        {
            var tagsWithCount = TestUtilities.GetTestTagsWithCount();

            A.CallTo(() => _tagService.GetTagsWithCount()).Returns(tagsWithCount);

            var result = _controller.Index() as ViewResult;
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<ChartModel>();
        }
    }
}
