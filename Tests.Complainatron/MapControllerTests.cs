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
using Complainatron.Domain;
using System.Linq.Expressions;
using System.Web.Mvc;
using Complainatron.Models;

namespace Tests.Complainatron
{
    [TestClass]
    public class MapControllerTests
    {
        private MapController _controller;
        private ILogService _loggingService;
        private IComplaintService _complaintService;
        private IFacebookService _facebookService;

        [TestInitialize]
        public void SetUp()
        {
            _loggingService = A.Fake<ILogService>();
            _complaintService = A.Fake<IComplaintService>();
            _facebookService = A.Fake<IFacebookService>();
            _controller = new MapController(_facebookService, _loggingService, _complaintService);
        }

        [TestMethod]
        [TestCategory("Creation")]
        public void It_should_throw_an_exception_if_complaint_service_is_null()
        {
            //arrange
            Action creation = () => new ChartController(_facebookService, _loggingService, null);

            //acts and assert
            creation.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_ask_the_complaint_service_for_all_complaints()
        {
            //arrange

            //act
            _controller.Index();

            //assert
            A.CallTo(() => _complaintService.GetAll(A<Expression<Func<Complaint, bool>>[]>.That.IsSameSequenceAs(TestUtilities.EmptyComplaintFilters()))).MustHaveHappened(Repeated.Exactly.Once);
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
            var complaints = TestUtilities.GetTestComplaints();

            A.CallTo(() => _complaintService.GetAll(TestUtilities.EmptyComplaintFilters())).Returns(complaints);

            var result = _controller.Index() as ViewResult;
            result.Model.Should().NotBeNull();
            result.Model.Should().BeOfType<MapModel>();
        }
    }
}
