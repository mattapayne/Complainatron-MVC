using System;
using System.Collections.Generic;
using Complainatron.Core.Paging;
using Complainatron.Domain;
using Complainatron.Domain.Validation;
using MvcPaging;
using System.Linq.Expressions;

namespace Complainatron.Core.Services
{
    public interface IService<TEntityType> where TEntityType : BaseEntity
    {
        TEntityType Get(Guid id);
        IEnumerable<TEntityType> GetAll(params Expression<Func<TEntityType, bool>>[] filters);
        IPagedList<TEntityType> PagedGetAll(IPagingInformation pagingInformation, params Expression<Func<TEntityType, bool>>[] filters);
        IEnumerable<IValidationError> Create(TEntityType entity);
        IEnumerable<IValidationError> Update(TEntityType entity);
        IEnumerable<IValidationError> Delete(TEntityType entity);
    }
}
