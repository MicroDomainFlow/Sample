﻿using ContentService.Core.Contracts.Aggregates.Categories.Queries.ResultViewModel;

using MDF.Framework.LayersContracts.ApplicationServices.MediatorExtensions.CQRS;

namespace ContentService.Core.Contracts.Aggregates.Categories.Queries.GetAll;
public class GetAllCategoryQuery : IQuery<List<CategoryQueryResult>>
{
}