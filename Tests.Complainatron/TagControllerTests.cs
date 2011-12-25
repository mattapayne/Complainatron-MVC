using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Complainatron.Controllers;
using Complainatron.Core.Services;
using Complainatron.Builders;
using FakeItEasy;
using Complainatron.Domain;
using Complainatron.Models;
using System.Linq.Expressions;
using System.Web.Mvc;
using FluentAssertions;
using FluentAssertions.Assertions;

namespace Tests.Complainatron
{
    [TestClass]
    public class TagControllerTests
    {
        private TagController _controller;
        private ILogService _loggingService;
        private ITagService _tagService;
        private IFacebookService _facebookService;
        private ITagBuilder _tagBuilder;

        [TestInitialize]
        public void SetUp()
        {
            _loggingService = A.Fake<ILogService>();
            _tagService = A.Fake<ITagService>();
            _facebookService = A.Fake<IFacebookService>();
            _tagBuilder = A.Fake<ITagBuilder>();
            _controller = new TagController(_facebookService, _loggingService, _tagService, _tagBuilder);
        }

        [TestMethod]
        [TestCategory("Creation")]
        public void It_should_throw_an_exception_if_tag_builder_is_null()
        {
            Action creation = () => new TagController(_facebookService, _loggingService, _tagService, null);

            creation.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        [TestCategory("Creation")]
        public void It_should_throw_an_exception_if_tag_service_is_null()
        {
            Action creation = () => new TagController(_facebookService, _loggingService, null, _tagBuilder);

            creation.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_ask_the_tag_service_for_all_tags()
        {
            //arrange

            //act
            _controller.Index();
            
            //assert
            A.CallTo(() => _tagService.GetAll(A<Expression<Func<Tag, bool>>[]>.That.IsSameSequenceAs(TestUtilities.EmptyTagFilters()))).MustHaveHappened();
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_use_the_tag_builder_to_create_a_list_of_view_models()
        {
            //arrange
            var tags = TestUtilities.GetTestTags();

            A.CallTo(() => _tagService.GetAll(A<Expression<Func<Tag, bool>>[]>.That.IsSameSequenceAs(TestUtilities.EmptyTagFilters()))).Returns(tags);

            //act
            _controller.Index();

            //assert
            A.CallTo(() => _tagBuilder.BuildViewModel(A<Tag>.Ignored)).MustHaveHappened(Repeated.Exactly.Times(tags.Count()));
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_return_a_partial_view()
        {
            //arrange

            //act
            var result = _controller.Index();

            //assert
            result.Should().BeOfType<PartialViewResult>();
        }
    }
}
