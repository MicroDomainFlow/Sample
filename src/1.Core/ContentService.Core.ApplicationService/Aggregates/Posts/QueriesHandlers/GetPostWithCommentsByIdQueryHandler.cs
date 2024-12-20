﻿using ContentService.Core.Contracts.Aggregates.Posts.Queries.GetPostAndCommentById;
using ContentService.Core.Contracts.Aggregates.Posts.Queries.ResultViewModel;
using ContentService.Core.Contracts.Aggregates.Posts.QueryRepositories;

using FluentResults;

using MDF.Framework.LayersContracts.ApplicationServices.MediatorExtensions.CQRS;
using MDF.Resources.Common;
using MDF.Resources.Common.FormattedMessages;

namespace ContentService.Core.ApplicationService.Aggregates.Posts.QueriesHandlers;
public class GetPostWithCommentsByIdQueryHandler : IQueryHandler<GetPostWithCommentsByIdQuery, PostWithCommentsQueryResult>
{
	private readonly IPostQueryRepository _postQueryRepository;

	public GetPostWithCommentsByIdQueryHandler(IPostQueryRepository postQueryRepository)
	{
		_postQueryRepository = postQueryRepository;
	}
	public async Task<Result<PostWithCommentsQueryResult>> Handle(GetPostWithCommentsByIdQuery request, CancellationToken cancellationToken)
	{
		var result = new Result<PostWithCommentsQueryResult>();
		var postFound = await _postQueryRepository.ExecuteAsync(request);
		if (postFound == null)
		{
			result.WithError(ErrorMessages.NotFound(DataDictionary.Post + "  id: " + request.PostId));
			return result;
		}
		result.WithValue(postFound);
		return result;
	}
}
