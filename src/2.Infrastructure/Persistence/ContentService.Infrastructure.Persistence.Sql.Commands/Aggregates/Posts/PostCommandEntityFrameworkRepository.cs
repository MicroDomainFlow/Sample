﻿using ContentService.Core.Contracts.Aggregates.Posts.CommandRepositories;
using ContentService.Core.Domain.Aggregates.Posts;
using ContentService.Infrastructure.Persistence.Sql.Commands.Common;

using Framework.Infrastructure.Commands;

namespace ContentService.Infrastructure.Persistence.Sql.Commands.Aggregates.Posts;
/// <summary>
/// از این ریپازیتوری باید در لایه application استفاده شود
/// نباید از خروجی های IQueryable و IEnumerable استفاده شود
/// هر متدی که لازم بود باید نوشته شود با خروجی مشخص
/// </summary>
public class PostCommandEntityFrameworkRepository : BaseCommandEntityFrameworkRepository<Post, ContentCommandDbContext>, IPostCommandRepository
{
	public PostCommandEntityFrameworkRepository(ContentCommandDbContext dbContext) : base(dbContext)
	{
	}
	//  در IPostCommandRepository اضافه کرد
	//اینجا باید متد های مورد نیاز را نوشت

}