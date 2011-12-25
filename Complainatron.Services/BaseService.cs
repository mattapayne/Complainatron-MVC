using System;
using System.Collections.Generic;
using System.Linq;
using Complainatron.Core.DataAccess;
using Complainatron.Core.DTOs;
using Complainatron.Core.Paging;
using Complainatron.Core.Services;
using Complainatron.Domain;
using Complainatron.Domain.Validation;
using MvcPaging;
using Complainatron.Core.LinqExtensions;
using System.Linq.Expressions;
using Complainatron.Core.Utility;

namespace Complainatron.Services
{
    public abstract class BaseService<TEntityType> : IService<TEntityType>
        where TEntityType : BaseEntity
    {
        private IDbContext _context;

        public BaseService(IDbContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            _context = context;
        }

        protected IDbContext DbContext
        {
            get
            {
                return _context;
            }
        }

        public virtual TEntityType Get(Guid id)
        {
            return _context.Set<TEntityType>().Where(e => e.Id == id).FirstOrDefault();
        }

        public virtual IEnumerable<TEntityType> GetAll(params Expression<Func<TEntityType, bool>>[] filters)
        {
            var items = _context.Set<TEntityType>().AsQueryable();

            if (filters != null)
            {
                filters.ForEach(f => items = items.Where(f));
            }

            return items.ToList();
        }

        public virtual IPagedList<TEntityType> PagedGetAll(IPagingInformation pagingInformation, params Expression<Func<TEntityType, bool>>[] filters)
        {
            var items = _context.Set<TEntityType>().AsQueryable();

            if (filters != null && filters.Any())
            {
                filters.ForEach(f => items = items.Where(f));
            }

            var totalItemCount = items.Count();

            switch (pagingInformation.SortDirection)
            {
                case SortDirection.Asc:
                    items = items.OrderBy(pagingInformation.SortBy);
                    break;
                case SortDirection.Desc:
                    items = items.OrderByDescending(pagingInformation.SortBy);
                    break;
            }

            return items.Skip(((pagingInformation.Page - 1) * pagingInformation.ResultsPerPage)).
                Take(pagingInformation.ResultsPerPage).
                ToPagedList(pagingInformation.Page - 1, pagingInformation.ResultsPerPage, totalItemCount);
        }

        public virtual IEnumerable<IValidationError> Create(TEntityType item)
        {
            var errors = item.ValidateForCreate();

            if (!errors.Any())
            {
                _context.Set<TEntityType>().Add(item);
            }

            return errors;
        }

        public virtual IEnumerable<IValidationError> Update(TEntityType item)
        {
            var errors = item.ValidateForUpdate();

            if (!errors.Any())
            {
                _context.Set<TEntityType>().Update(item);
            }

            return errors;
        }

        public virtual IEnumerable<IValidationError> Delete(TEntityType item)
        {
            var errors = item.ValidateForDelete();

            if (!errors.Any())
            {
                _context.Set<TEntityType>().Remove(item);
            }

            return errors;
        }
    }
}