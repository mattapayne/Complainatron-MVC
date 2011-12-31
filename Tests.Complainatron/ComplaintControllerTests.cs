using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FluentAssertions;
using FluentAssertions.Assertions;
using FakeItEasy;
using Complainatron.Controllers;
using Complainatron.Core.Services;
using Complainatron.Builders;
using Complainatron.Core.Paging;
using Complainatron.Domain;
using MvcPaging;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web;
using Complainatron.Core.DTOs;
using Facebook.Web.Mvc;
using System.Linq.Expressions;
using Complainatron.Models;

namespace Tests.Complainatron
{
    [TestClass]
    public class ComplaintControllerTests
    {
        private ComplaintController _controller;
        private ILogService _loggingService;
        private ITagService _tagService;
        private IFacebookService _facebookService;
        private IComplaintSeverityService _complaintSeverityService;
        private IComplaintService _complaintService;
        private IUserService _userService;
        private ITagBuilder _tagBuilder;
        private IComplaintBuilder _complaintBuilder;

        [TestInitialize]
        public void SetUp()
        {
            _loggingService = A.Fake<ILogService>();
            _tagService = A.Fake<ITagService>();
            _facebookService = A.Fake<IFacebookService>();
            _complaintSeverityService = A.Fake<IComplaintSeverityService>();
            _complaintService = A.Fake<IComplaintService>();
            _userService = A.Fake<IUserService>();
            _tagBuilder = A.Fake<ITagBuilder>();
            _complaintBuilder = A.Fake<IComplaintBuilder>();

            _controller = new ComplaintController(_facebookService, _loggingService, _tagService, 
                _complaintSeverityService, _complaintService, _userService, _tagBuilder, _complaintBuilder);
        }

        private void StubContext()
        {
            var httpContext = A.Fake<HttpContextBase>();

            _controller.Url = new UrlHelper(new RequestContext(httpContext, new RouteData()));
        }

        [TestMethod]
        [TestCategory("Creation")]
        public void It_should_throw_an_exception_if_tag_service_is_null()
        {
            Action creation = () => _controller = new ComplaintController(_facebookService, _loggingService, null, _complaintSeverityService, _complaintService,
                _userService, _tagBuilder, _complaintBuilder);

            creation.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        [TestCategory("Creation")]
        public void It_should_throw_an_exception_if_complaint_severity_service_is_null()
        {
            Action creation = () => _controller = new ComplaintController(_facebookService, _loggingService, _tagService, null, _complaintService,
                _userService, _tagBuilder, _complaintBuilder);

            creation.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        [TestCategory("Creation")]
        public void It_should_throw_an_exception_if_complaint_service_is_null()
        {
            Action creation = () => _controller = new ComplaintController(_facebookService, _loggingService, _tagService, _complaintSeverityService, null,
                _userService, _tagBuilder, _complaintBuilder);

            creation.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        [TestCategory("Creation")]
        public void It_should_throw_an_exception_if_user_service_is_null()
        {
            Action creation = () => _controller = new ComplaintController(_facebookService, _loggingService, _tagService, _complaintSeverityService, _complaintService,
                null, _tagBuilder, _complaintBuilder);

            creation.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        [TestCategory("Creation")]
        public void It_should_throw_an_exception_if_tag_builder_is_null()
        {
            Action creation = () => _controller = new ComplaintController(_facebookService, _loggingService, _tagService, _complaintSeverityService, _complaintService,
                _userService, null, _complaintBuilder);

            creation.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        [TestCategory("Creation")]
        public void It_should_throw_an_exception_if_complaint_builder_is_null()
        {
            Action creation = () => _controller = new ComplaintController(_facebookService, _loggingService, _tagService, _complaintSeverityService, _complaintService,
                _userService, _tagBuilder, null);

            creation.ShouldThrow<ArgumentNullException>();
        }

        [TestMethod]
        [TestCategory("Tag Action")]
        public void It_should_ask_the_tag_service_to_get_the_requested_tag()
        {
            //Arrange
            StubContext();

            var tagId = Guid.NewGuid();
            var pagingInfo = TestUtilities.GetPagingInformation();
            IPagedList<Complaint> complaints = TestUtilities.GetPagedTestComplaints(pagingInfo);
            var tag = new Tag() { Name = "tag" };

            A.CallTo(() => _tagService.Get(tagId)).Returns(tag);
            A.CallTo(() => _complaintService.GetComplaintsByTag(pagingInfo, tagId)).Returns(complaints);

            //Act
            _controller.Tag(tagId, pagingInfo);

            //Assert
            A.CallTo(() => _tagService.Get(tagId)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [TestCategory("Tag Action")]
        public void It_should_redirect_to_the_index_action_if_tag_specified_tag_is_not_found()
        {
            //Arrange
            var tagId = Guid.NewGuid();
            var pagingInfo = TestUtilities.GetPagingInformation();

            A.CallTo(() => _tagService.Get(tagId)).Returns(null);
            
            //Act
            var result = _controller.Tag(tagId, pagingInfo);

            //Assert
            result.Should().BeOfType<CanvasRedirectToRouteResult>();
        }

        [TestMethod]
        [TestCategory("Tag Action")]
        public void It_should_ask_the_facebook_service_for_me_as_part_of_tag_action()
        {
            //Arrange
            StubContext();

            var tagId = Guid.NewGuid();
            var pagingInfo = TestUtilities.GetPagingInformation();
            IPagedList<Complaint> complaints = TestUtilities.GetPagedTestComplaints(pagingInfo);
            var tag = new Tag() { Name = "tag" };

            A.CallTo(() => _tagService.Get(tagId)).Returns(tag);
            A.CallTo(() => _complaintService.GetComplaintsByTag(pagingInfo, tagId)).Returns(complaints);

            //Act
            _controller.Tag(tagId, pagingInfo);

            //Assert
            A.CallTo(() => _facebookService.GetMe()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [TestCategory("Tag Action")]
        public void It_should_ask_the_complaint_service_for_a_paged_list_of_complaints_as_a_part_of_the_tag_action()
        {
            //Arrange
            StubContext();

            var tagId = Guid.NewGuid();
            var pagingInfo = TestUtilities.GetPagingInformation();
            IPagedList<Complaint> complaints = TestUtilities.GetPagedTestComplaints(pagingInfo);
            var tag = new Tag() { Name = "tag" };

            A.CallTo(() => _tagService.Get(tagId)).Returns(tag);
            A.CallTo(() => _complaintService.GetComplaintsByTag(pagingInfo, tagId)).Returns(complaints);

            //Act
            _controller.Tag(tagId, pagingInfo);

            //Assert
            A.CallTo(() => _complaintService.GetComplaintsByTag(pagingInfo, tagId)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [TestCategory("Tag Action")]
        public void It_should_return_a_view_as_a_part_of_the_tag_action()
        {
            //Arrange
            StubContext();

            var tagId = Guid.NewGuid();
            var pagingInfo = TestUtilities.GetPagingInformation();
            IPagedList<Complaint> complaints = TestUtilities.GetPagedTestComplaints(pagingInfo);
            var tag = new Tag() { Name = "tag" };

            A.CallTo(() => _tagService.Get(tagId)).Returns(tag);
            A.CallTo(() => _complaintService.GetComplaintsByTag(pagingInfo, tagId)).Returns(complaints);

            //Act
            var result = _controller.Tag(tagId, pagingInfo);
            
            //Assert
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        [TestCategory("Friend Action")]
        public void It_should_ask_the_user_service_to_get_the_requested_friend()
        {
            //Arrange
            StubContext();

            long friendId = 1;
            var pagingInfo = TestUtilities.GetPagingInformation();
            IPagedList<Complaint> complaints = TestUtilities.GetPagedTestComplaints(pagingInfo);
            var friend = new User() { FacebookId = friendId };

            A.CallTo(() => _userService.FindByFacebookId(friendId)).Returns(friend);
            A.CallTo(() => _complaintService.GetComplaintsByFacebookId(pagingInfo, friendId)).Returns(complaints);

            //Act
            _controller.Friend(friendId, pagingInfo);

            //Assert
            A.CallTo(() => _userService.FindByFacebookId(friendId)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [TestCategory("Friend Action")]
        public void It_should_redirect_to_the_index_action_if_specified_user_is_not_found()
        {
            //Arrange
            long friendId = 1;
            var pagingInfo = TestUtilities.GetPagingInformation();

            A.CallTo(() => _userService.FindByFacebookId(friendId)).Returns(null);

            //Act
            var result = _controller.Friend(friendId, pagingInfo);

            //Assert
            result.Should().BeOfType<CanvasRedirectToRouteResult>();
        }

        [TestMethod]
        [TestCategory("Friend Action")]
        public void It_should_ask_the_facebook_service_for_me_as_part_of_friend_action()
        {
            //Arrange
            StubContext();

            long friendId = 1;
            var pagingInfo = TestUtilities.GetPagingInformation();
            IPagedList<Complaint> complaints = TestUtilities.GetPagedTestComplaints(pagingInfo);
            var friend = new User() { FacebookId = friendId };

            A.CallTo(() => _userService.FindByFacebookId(friendId)).Returns(friend);
            A.CallTo(() => _complaintService.GetComplaintsByFacebookId(pagingInfo, friendId)).Returns(complaints);

            //Act
            _controller.Friend(friendId, pagingInfo);

            //Assert
            A.CallTo(() => _facebookService.GetMe()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [TestCategory("Friend Action")]
        public void It_should_ask_the_complaint_service_for_a_paged_list_of_complaints_as_a_part_of_the_friend_action()
        {
            //Arrange
            StubContext();

            long friendId = 1;
            var pagingInfo = TestUtilities.GetPagingInformation();
            IPagedList<Complaint> complaints = TestUtilities.GetPagedTestComplaints(pagingInfo);
            var friend = new User() { FacebookId = friendId };

            A.CallTo(() => _userService.FindByFacebookId(friendId)).Returns(friend);
            A.CallTo(() => _complaintService.GetComplaintsByFacebookId(pagingInfo, friendId)).Returns(complaints);

            //Act
            _controller.Friend(friendId, pagingInfo);

            //Assert
            A.CallTo(() => _complaintService.GetComplaintsByFacebookId(pagingInfo, friendId)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [TestCategory("Friend Action")]
        public void It_should_return_a_view_as_a_part_of_the_friends_action()
        {
            //Arrange
            StubContext();

            long friendId = 1;
            var pagingInfo = TestUtilities.GetPagingInformation();
            IPagedList<Complaint> complaints = TestUtilities.GetPagedTestComplaints(pagingInfo);
            var friend = new User() { FacebookId = friendId };

            A.CallTo(() => _userService.FindByFacebookId(friendId)).Returns(friend);
            A.CallTo(() => _complaintService.GetComplaintsByFacebookId(pagingInfo, friendId)).Returns(complaints);

            //Act
            var result = _controller.Friend(friendId, pagingInfo);

            //Assert
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_ask_the_facebook_service_to_mark_requests_consumed_if_request_ids_are_present()
        {
            //Arrange
            StubContext();
            var pagingInfo = TestUtilities.GetPagingInformation();
            string request_ids = "request_ids";

            IPagedList<Complaint> complaints = TestUtilities.GetPagedTestComplaints(pagingInfo);

            A.CallTo(() => _complaintService.PagedGetAll(pagingInfo,
                A<Expression<Func<Complaint, bool>>[]>.That.IsSameSequenceAs(TestUtilities.EmptyComplaintFilters()))).Returns(complaints);

            //Act
            _controller.Index(pagingInfo, request_ids);

            //Assert
            A.CallTo(() => _facebookService.MarkRequestsConsumed(request_ids)).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_not_ask_the_facebook_service_to_mark_requests_consumed_if_no_request_ids_are_present()
        {
            //Arrange
            StubContext();
            var pagingInfo = TestUtilities.GetPagingInformation();

            IPagedList<Complaint> complaints = TestUtilities.GetPagedTestComplaints(pagingInfo);

            A.CallTo(() => _complaintService.PagedGetAll(pagingInfo,
                A<Expression<Func<Complaint, bool>>[]>.That.IsSameSequenceAs(TestUtilities.EmptyComplaintFilters()))).Returns(complaints);

            //Act
            _controller.Index(pagingInfo);

            //Assert
            A.CallTo(() => _facebookService.MarkRequestsConsumed(A<string>.Ignored)).MustNotHaveHappened();
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_ask_the_facebook_service_for_me_as_part_of_index_action()
        {
            //Arrange
            StubContext();

            var pagingInfo = TestUtilities.GetPagingInformation();
            IPagedList<Complaint> complaints = TestUtilities.GetPagedTestComplaints(pagingInfo);

            A.CallTo(() => _complaintService.PagedGetAll(pagingInfo, 
                A<Expression<Func<Complaint, bool>>[]>.That.IsSameSequenceAs(TestUtilities.EmptyComplaintFilters()))).Returns(complaints);

            //Act
            _controller.Index(pagingInfo);

            //Assert
            A.CallTo(() => _facebookService.GetMe()).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_ask_the_complaint_service_for_a_paged_list_of_complaints_as_a_part_of_the_index_action()
        {
            //Arrange
            StubContext();

            var pagingInfo = TestUtilities.GetPagingInformation();
            IPagedList<Complaint> complaints = TestUtilities.GetPagedTestComplaints(pagingInfo);

            A.CallTo(() => _complaintService.PagedGetAll(pagingInfo,
                A<Expression<Func<Complaint, bool>>[]>.That.IsSameSequenceAs(TestUtilities.EmptyComplaintFilters()))).Returns(complaints);

            //Act
            _controller.Index(pagingInfo);

            //Assert
            A.CallTo(() => _complaintService.PagedGetAll(pagingInfo,
                A<Expression<Func<Complaint, bool>>[]>.That.IsSameSequenceAs(TestUtilities.EmptyComplaintFilters()))).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [TestCategory("Index Action")]
        public void It_should_return_a_view_as_a_part_of_the_index_action()
        {
            //Arrange
            StubContext();

            var pagingInfo = TestUtilities.GetPagingInformation();
            IPagedList<Complaint> complaints = TestUtilities.GetPagedTestComplaints(pagingInfo);

            A.CallTo(() => _complaintService.PagedGetAll(pagingInfo,
                A<Expression<Func<Complaint, bool>>[]>.That.IsSameSequenceAs(TestUtilities.EmptyComplaintFilters()))).Returns(complaints);

            //Act
            var result =_controller.Index(pagingInfo);

            //Assert
            result.Should().BeOfType<ViewResult>();
        }

        [TestMethod]
        [TestCategory("Complain Action")]
        public void It_should_return_a_partial_view_as_a_part_of_the_complain_action()
        {
            var result = _controller.Complain();
            result.Should().BeOfType<PartialViewResult>();
        }

        [TestMethod]
        [TestCategory("Complain Action")]
        public void It_should_return_a_partial_view_called__complain_as_a_part_of_the_complain_action()
        {
            var result = _controller.Complain() as PartialViewResult;
            result.ViewName.Should().Be("_Complain");
        }

        [TestMethod]
        [TestCategory("Complain Action")]
        public void It_should_have_a_complain_view_model_as_a_part_of_the_complain_action()
        {
            var result = _controller.Complain() as PartialViewResult;
            result.Model.Should().BeOfType<CreateComplaintViewModel>();
        }

        [TestMethod]
        [TestCategory("Complain Action")]
        public void It_should_ask_the_complaint_severity_service_for_all_severities()
        {
            //Arrange
            var severities = TestUtilities.GetTestComplaintSeverities();

            A.CallTo(() => _complaintSeverityService.GetAll(A<Expression<Func<ComplaintSeverity, bool>>[]>.That.IsSameSequenceAs(TestUtilities.EmptyComplaintSeverityFilters()))).Returns(severities);

            //Act
            var result = _controller.Complain() as ViewResult;

            //Assert
            A.CallTo(() => _complaintSeverityService.GetAll(A<Expression<Func<ComplaintSeverity, bool>>[]>.That.IsSameSequenceAs(TestUtilities.EmptyComplaintSeverityFilters()))).MustHaveHappened(Repeated.Exactly.Once);
        }

        [TestMethod]
        [TestCategory("Complain Action")]
        public void It_should_set_complaint_severities_on_the_view_model()
        {
            //Arrange
            var severities = TestUtilities.GetTestComplaintSeverities();

            A.CallTo(() => _complaintSeverityService.GetAll(A<Expression<Func<ComplaintSeverity, bool>>[]>.That.IsSameSequenceAs(TestUtilities.EmptyComplaintSeverityFilters()))).Returns(severities);
        
            //Act
            var result = _controller.Complain() as PartialViewResult;

            //Assert
            var model = result.Model as CreateComplaintViewModel;
            model.Severities.Should().HaveCount(severities.Count());
        }
    }
}
