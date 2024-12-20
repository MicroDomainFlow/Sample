using ContentService.Core.Contracts.Aggregates.Posts.Commands;
using ContentService.Core.Contracts.Aggregates.Posts.Commands.Comment;
using ContentService.Core.Contracts.Aggregates.Posts.Queries.GetAll;
using ContentService.Core.Contracts.Aggregates.Posts.Queries.GetPostAndCommentById;
using ContentService.Core.Contracts.Aggregates.Posts.Queries.GetPostById;
using ContentService.Core.Contracts.Aggregates.Posts.Queries.ResultViewModel;
using ContentService.Endpoints.API.Controllers;

using FluentResults;

using MDF.Framework.Extensions.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using Moq;

namespace ContentService.Endpoints.API.Tests.Unit.Controllers;

public class PostControllerTests
{
	private readonly Mock<IMediator> _mediatorMock;
	private readonly PostController _postController;

	public PostControllerTests()
	{
		_mediatorMock = new Mock<IMediator>();
		_postController = new PostController(_mediatorMock.Object);
	}

	[Fact]
	public async Task ShouldBe_GetAllPostAsync_ReturnsListOfPostQueryDto_When_NoInput()
	{
		// Arrange
		var expected = new List<PostQueryResult>();
		_mediatorMock.Setup(x => x.Send(It.IsAny<GetAllPostQuery>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(expected);
		var getAllPostQuery = new GetAllPostQuery
		{
			Filter = null,
			OrderBy = null,
			Page = 1,
			PageSize = 20
		};
		// Act
		var result = await _postController.GetAllPostAsync(getAllPostQuery);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var actual = Assert.IsType<CustomResult<List<PostQueryResult>>>(okResult.Value);
		Assert.Equal(expected, actual.Value);
	}

	[Fact]
	public async Task ShouldBe_GetAllPostWithCommentAsync_ReturnsListOfPostWithCommentsQueryDto_When_NoInput()
	{
		// Arrange
		var expected = new List<PostWithCommentsQueryResult>();
		_mediatorMock.Setup(x => x.Send(It.IsAny<GetAllPostWithCommentQuery>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(expected);
		var getAllPostWithCommentQuery = new GetAllPostWithCommentQuery { OrderBy = null, Filter = null, Page = 1, PageSize = 1 };
		// Act
		var result = await _postController.GetAllPostWithCommentAsync(getAllPostWithCommentQuery);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var actual = Assert.IsType<CustomResult<List<PostWithCommentsQueryResult>>>(okResult.Value);
		Assert.Equal(expected, actual.Value);
	}

	[Fact]
	public async Task ShouldBe_GetPostAsync_ReturnsPostQueryDto_When_GetPostByIdQueryInput()
	{
		// Arrange
		var id = new GetPostByIdQuery();
		var expected = new PostQueryResult
		{
			Title = null,
			Description = null,
			Text = null
		};
		_mediatorMock.Setup(x => x.Send(It.IsAny<GetPostByIdQuery>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(expected);

		// Act
		var result = await _postController.GetPostAsync(id);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var actual = Assert.IsType<CustomResult<PostQueryResult>>(okResult.Value);
		Assert.Equal(expected, actual.Value);
	}

	[Fact]
	public async Task ShouldBe_GetPostAndCommentsAsync_ReturnsPostWithCommentsQueryDto_When_GetPostWithCommentsByIdQueryInput()
	{
		// Arrange
		var id = new GetPostWithCommentsByIdQuery();
		var expected = new PostWithCommentsQueryResult
		{
			Title = null,
			Description = null,
			Text = null,
			Comments = null
		};
		_mediatorMock.Setup(x => x.Send(It.IsAny<GetPostWithCommentsByIdQuery>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(expected);

		// Act
		var result = await _postController.GetPostAndCommentsAsync(id);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var actual = Assert.IsType<CustomResult<PostWithCommentsQueryResult>>(okResult.Value);
		Assert.Equal(expected, actual.Value);
	}

	[Fact]
	public async Task ShouldBe_CreatePostAsync_ReturnsGuid_When_CreatePostCommandInput()
	{
		// Arrange
		var createPost = new CreatePostCommand
		{
			Title = null,
			Description = null,
			Text = null
		};
		var expected = Guid.CreateVersion7();
		_mediatorMock.Setup(x => x.Send(It.IsAny<CreatePostCommand>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(expected);

		// Act
		var result = await _postController.CreatePostAsync(createPost);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var actual = Assert.IsType<CustomResult<Guid>>(okResult.Value);
		Assert.Equal(expected, actual.Value);
	}

	[Fact]
	public async Task ShouldBe_UpdatePostAsync_ReturnsGuid_When_UpdatePostCommandInput()
	{
		// Arrange
		var updatePostCommand = new UpdatePostCommand
		{
			PostId = default,
			Title = null,
			Description = null,
			Text = null
		};
		var expected = Guid.CreateVersion7();
		_mediatorMock.Setup(x => x.Send(It.IsAny<UpdatePostCommand>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(expected);

		// Act
		var result = await _postController.UpdatePostAsync(updatePostCommand);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var actual = Assert.IsType<CustomResult<Guid>>(okResult.Value);
		Assert.Equal(expected, actual.Value);
	}

	[Fact]
	public async Task ShouldBe_DeletePostAsync_ReturnsOkResult_When_RemovePostCommandInput()
	{
		// Arrange
		var removePostCommand = new RemovePostCommand()
		{
			PostId = Guid.CreateVersion7()
		};
		_mediatorMock.Setup(x => x.Send(It.IsAny<RemovePostCommand>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(Result.Ok());

		// Act
		var result = await _postController.DeletePostAsync(removePostCommand);

		// Assert
		Assert.IsType<OkObjectResult>(result);
	}

	[Fact]
	public async Task ShouldBe_AddCommentToThePostAsync_ReturnsGuid_When_AddCommentToPostCommandVmInput()
	{
		// Arrange
		var addCommentToPostCommand = new AddCommentToPostCommand
		{
			PostId = default,
			DisplayName = null,
			Email = null,
			CommentText = null
		};

		var expected = Guid.CreateVersion7();

		_mediatorMock.Setup(x => x.Send(It.IsAny<AddCommentToPostCommand>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(expected);

		// Act
		var result = await _postController.AddCommentToPostAsync(addCommentToPostCommand);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var actual = Assert.IsType<CustomResult<Guid>>(okResult.Value);
		Assert.Equal(expected, actual.Value);
	}

	[Fact]
	public async Task ShouldBe_EditCommentAsync_ReturnsGuid_When_EditCommentThePostCommandInput()
	{
		// Arrange
		var editCommentThePostCommand = new EditCommentThePostCommand
		{
			PostId = default,
			DisplayName = null,
			Email = null,
			CommentText = null,
			CommentNewText = null
		};
		var expected = Guid.CreateVersion7();
		_mediatorMock.Setup(x => x.Send(It.IsAny<EditCommentThePostCommand>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(expected);

		// Act
		var result = await _postController.EditCommentAsync(editCommentThePostCommand);

		// Assert
		var okResult = Assert.IsType<OkObjectResult>(result);
		var actual = Assert.IsType<CustomResult<Guid>>(okResult.Value);
		Assert.Equal(expected, actual.Value);
	}

	[Fact]
	public async Task ShouldBe_DeleteCommentAsync_ReturnsOkResult_When_RemoveCommentFromPostCommandInput()
	{
		// Arrange
		Guid id = Guid.CreateVersion7();
		var removeCommentFromPostCommand = new RemoveCommentFromPostCommand(id, "test", "test@gmail.com", "".PadLeft(50, 's'));

		_mediatorMock.Setup(x => x.Send(It.IsAny<RemoveCommentFromPostCommand>(), It.IsAny<CancellationToken>()))
			.ReturnsAsync(Result.Ok());

		// Act
		var result = await _postController.DeleteCommentAsync(removeCommentFromPostCommand);

		// Assert
		Assert.IsType<OkObjectResult>(result);
	}
}
