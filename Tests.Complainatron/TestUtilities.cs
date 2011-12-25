using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Complainatron.Domain;
using Complainatron.Models;
using System.Linq.Expressions;
using Complainatron.Core.DTOs;
using Complainatron.Core.Paging;
using MvcPaging;

namespace Tests.Complainatron
{
    public static class TestUtilities
    {
        public static IEnumerable<Tag> GetTestTags()
        {
            return new[] { new Tag() { DateCreated = DateTime.Now, Name = "Tag 1" }, new Tag() { DateCreated = DateTime.Now, Name = "Tag 2" }};
        }

        public static IEnumerable<TagViewModel> GetTestTagViewModels()
        {
            return new[] { new TagViewModel() { Name = "Tag 1" }, new TagViewModel() { Name = "Tag 2" } };
        }

        public static Expression<Func<Tag, bool>>[] EmptyTagFilters()
        {
            return new Expression<Func<Tag, bool>>[0];
        }

        public static Expression<Func<Complaint, bool>>[] EmptyComplaintFilters()
        {
            return new Expression<Func<Complaint, bool>>[0];
        }

        public static IEnumerable<CountedTag> GetTestTagsWithCount()
        {
            return new[] { new CountedTag() { Count = 10, Name = "Tag A" }, new CountedTag() { Count = 4, Name = "Tag B"} };
        }

        public static IPagedList<Complaint> GetPagedTestComplaints(IPagingInformation paging)
        {
            var c = GetTestComplaints();
            return new PagedList<Complaint>(c, paging.Page, paging.ResultsPerPage, c.Count());
        }

        public static IEnumerable<FacebookFriendDTO> GetTestFriends()
        {
            return new[] { new FacebookFriendDTO(), new FacebookFriendDTO() };
        }

        public static PagingInformation GetPagingInformation(int pageNumber = 1, int resultsPerPage = 10, string sortBy = "Id", SortDirection sortDirection = SortDirection.Asc)
        {
            return new PagingInformation() { Page = pageNumber, ResultsPerPage = resultsPerPage, SortBy = sortBy, SortDirection = sortDirection };
        }

        public static IEnumerable<Complaint> GetTestComplaints()
        {
            return new[] { new Complaint() { }, new Complaint() { } };
        }

        public static IEnumerable<ComplaintSeverity> GetTestComplaintSeverities()
        {
            return new[] { new ComplaintSeverity(), new ComplaintSeverity() };
        }

        public static Expression<Func<ComplaintSeverity, bool>>[] EmptyComplaintSeverityFilters()
        {
            return new Expression<Func<ComplaintSeverity, bool>>[0];
        }
    }
}
