using Moq;
using MySocialApp.Application;
using MySocialApp.Domain;
using MySocialApp.Infrastructure.Exceptions;

namespace MySocialApp.Tests
{
    public class PostAppServiceTests
    {
        private readonly Mock<IUserSession> _userSessionMock;
        private readonly Mock<IPostService> _postServiceMock;
        private readonly Mock<IPostRepository> _postRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly PostAppService _postAppService;

        public PostAppServiceTests()
        {
            _userSessionMock = new Mock<IUserSession>();
            _postServiceMock = new Mock<IPostService>();
            _postRepositoryMock = new Mock<IPostRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            _postAppService = new PostAppService(
                _userSessionMock.Object,
                _postServiceMock.Object,
                _postRepositoryMock.Object,
                _unitOfWorkMock.Object);
        }

        [Fact]
        public async Task Add_ShouldAddPostAndCommit()
        {
            // Arrange
            var request = new PostRequest { Content = "Test content" };
            _userSessionMock.Setup(us => us.Id).Returns("userId");

            // Act
            await _postAppService.Add(request);

            // Assert
            _postServiceMock.Verify(ps => ps.Add(It.IsAny<Post>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public async Task Update_ShouldUpdatePostAndCommit()
        {
            // Arrange
            var post = Post.Create("Old content", "userId");
            var postId = post.Id;
            var request = new PostRequest { Content = "Updated content" };

            _postRepositoryMock.Setup(pr => pr.Get(postId, "userId")).ReturnsAsync(post);
            _userSessionMock.Setup(us => us.Id).Returns("userId");

            // Act
            await _postAppService.Update(postId, request);

            // Assert
            _postServiceMock.Verify(ps => ps.Update(post), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public async Task LikePost_ShouldAddLikeAndCommit()
        {
            // Arrange
            var post = Post.Create("", "userId");

            _postRepositoryMock.Setup(pr => pr.Get(post.Id, "userId")).ReturnsAsync(post);
            _userSessionMock.Setup(us => us.Id).Returns("userId");

            // Act
            await _postAppService.LikePost(post.Id);

            // Assert
            _postServiceMock.Verify(ps => ps.Add(It.IsAny<PostLike>()), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public async Task UnlikePost_ShouldRemoveLikeAndCommit()
        {
            // Arrange
            var post = Post.Create("", "userId");
            var likePost = PostLike.Create(post, "userId");

            _postRepositoryMock.Setup(pr => pr.GetPostLikeUser(post.Id, "userId")).ReturnsAsync(likePost);
            _userSessionMock.Setup(us => us.Id).Returns("userId");

            // Act
            await _postAppService.UnlikePost(post.Id);

            // Assert
            _postServiceMock.Verify(ps => ps.Remove(likePost), Times.Once);
            _unitOfWorkMock.Verify(uow => uow.Commit(), Times.Once);
        }

        [Fact]
        public async Task GetUserPost_ShouldReturnPost()
        {
            // Arrange
            var post = Post.Create("", "userId");
            var postId = post.Id;

            _postRepositoryMock.Setup(pr => pr.Get(postId, "userId")).ReturnsAsync(post);
            _userSessionMock.Setup(us => us.Id).Returns("userId");

            // Act
            var result = await _postAppService.GetUserPost(postId);

            // Assert
            Assert.Equal(post, result);
        }

        [Fact]
        public async Task GetUserPost_ShouldThrowNotFoundException_WhenPostNotFound()
        {
            // Arrange
            var postId = Guid.NewGuid();

            _postRepositoryMock.Setup(pr => pr.Get(postId, "userId")).ReturnsAsync((Post)null);
            _userSessionMock.Setup(us => us.Id).Returns("userId");
            _userSessionMock.Setup(us => us.Name).Returns("userName");

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _postAppService.GetUserPost(postId));
        }

        [Fact]
        public async Task GetFeed_ShouldReturnPostFeedResponses()
        {
            // Arrange
            var filterRequest = new FilterPaginatedRequest { Page = 1, PageSize = 10 };
            var feedQueryDtos = new List<FeedQueryDto>
            {
                new FeedQueryDto
                {
                    Id = Guid.NewGuid(),
                    Content = "Test content",
                    LikesCount = 5,
                    Liked = true,
                    CreateAt = DateTimeOffset.UtcNow
                }
            };
            _postRepositoryMock.Setup(pr => pr.GetFeed("userId", filterRequest.Page, filterRequest.PageSize))
                .ReturnsAsync(new Tuple<IEnumerable<FeedQueryDto>, int>(feedQueryDtos, 1));
            _userSessionMock.Setup(us => us.Id).Returns("userId");

            // Act
            var result = await _postAppService.GetFeed(filterRequest);

            // Assert
            Assert.Single(result.Data);
            Assert.Equal(feedQueryDtos.First().Content, result.Data.First().Content);
        }
    }
}
