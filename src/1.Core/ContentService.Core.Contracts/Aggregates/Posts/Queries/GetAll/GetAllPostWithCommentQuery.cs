﻿using ContentService.Core.Contracts.Aggregates.Posts.Queries.Models;

using MDF.Contract.ApplicationServices.MediatorExtensions.CQRS;

namespace ContentService.Core.Contracts.Aggregates.Posts.Queries.GetAll;
public record GetAllPostWithCommentQuery : IQuery<List<PostWithCommentsQueryDto>>
{
}
