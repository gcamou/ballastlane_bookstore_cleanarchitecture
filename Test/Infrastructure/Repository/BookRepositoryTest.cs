using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using FizzWare.NBuilder;

namespace Test.Infrastructure.Repository;

public class BookRepositoryTest
{
    private readonly DbContextOptions<ApplicationDbContext> _dbContextOptions;

    public BookRepositoryTest()
    {
        _dbContextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "GenericRepositoryTestDatabase")
            .Options;
    }

    [Fact]
    public async Task Should_Returns_All_Entities()
    {
        // Arrange
        using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        {
            var repository = new GenericRepository<Book>(dbContext);

            // Add test data
            var books = Builder<Book>.CreateListOfSize(5)
                                        .All()
                                        .With(book => book.Id = Guid.NewGuid())
                                        .Build();

            dbContext.Books.AddRange(books);
            await dbContext.SaveChangesAsync();

            // Act
            var result = repository.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Count() > 1);
        }
    }

    [Fact]
    public async Task Should_Return_Book_When_Id_Is_Valid()
    {
        // Arrange
        using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        {
            var repository = new GenericRepository<Book>(dbContext);

            var book = Builder<Book>.CreateNew()
                                    .With(book => book.Id = Guid.NewGuid())
                                    .Build();
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await repository.GetByIdAsync(book.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
        }
    }

    [Fact]
    public async Task Should_Create_A_Book()
    {
        // Arrange
        using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        {
            var repository = new GenericRepository<Book>(dbContext);

            var book = Builder<Book>.CreateNew()
                                    .With(book => book.Id = Guid.NewGuid())
                                    .Build();
            // Act
            var result = await repository.InsertAsync(book);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(book.Id, result.Id);
        }
    }

    [Fact]
    public async Task Should_Update_Book()
    {
        // Arrange
        using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        {
            var repository = new GenericRepository<Book>(dbContext);

            var book = Builder<Book>.CreateNew()
                                    .With(book => book.Id = Guid.NewGuid())
                                    .Build();
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();

            book.Title = "Updated Book Title";

            // Act
            var result = await repository.UpdateAsync(book);

            // Assert
            Assert.True(result);
        }
    }

    [Fact]
    public async Task Should_Delete_The_Book()
    {
        // Arrange
        using (var dbContext = new ApplicationDbContext(_dbContextOptions))
        {
            var repository = new GenericRepository<Book>(dbContext);

            var book = Builder<Book>.CreateNew()
                                    .With(book => book.Id = Guid.NewGuid())
                                    .Build();
            dbContext.Books.Add(book);
            await dbContext.SaveChangesAsync();

            // Act
            var result = await repository.DeleteAsync(book);

            // Assert
            Assert.True(result);
            Assert.Null(await repository.GetByIdAsync(book.Id));
        }
    }
}
