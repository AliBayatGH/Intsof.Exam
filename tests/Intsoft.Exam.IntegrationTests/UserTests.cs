using Application.Common.Exceptions;
using Application.Core.User.Command.Create;
using Application.Core.User.Command.Update;
using Application.Core.User.Query.GetById;
using Domain;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace Intsoft.Exam.IntegrationTests;

[Collection("Context Collection")]
public class UserTests
{
    private readonly TestContextFixture _fixture;

    private readonly AddUSerCommandHandler _addHandler;
    private readonly UpdateUserCommandHandler _updateHandler;
    private readonly GetUserQueryHandler _getHandler;

    public UserTests(TestContextFixture fixture)
    {
        _fixture = fixture;
        _addHandler = new AddUSerCommandHandler(_fixture.Context, new AddUserValidation());
        _updateHandler = new UpdateUserCommandHandler(_fixture.Context, new UpdateUserValidation());
        _getHandler = new GetUserQueryHandler(_fixture.Context);
    }

    private async Task TruncateUserAsync()
    {
        var users = await _fixture.Context.Users.ToListAsync();
        _fixture.Context.Users.RemoveRange(users);
    }

    private async Task<User> CreateFakeUserAsync()
    {
        var testUser = new User()
        {
            FirstName = "test",
            LastName = "test",
            NationalCode = "test",
            PhoneNumber = "test"
        };

        await _fixture.Context.Users.AddAsync(testUser);
        await _fixture.Context.SaveChangesAsync();

        var user = await _fixture.Context.Users.FirstOrDefaultAsync(i => i.FirstName == testUser.FirstName);

        return user!;
    }


    [Theory]
    [InlineData("", "09917734343")]
    [InlineData("javid", "")]
    [InlineData("", "")]
    public async Task AddUser_when_Firstname_Or_phoneNumber_Is_Empty_ThrowBadRequestExceptionAsync(string name, string phoneNumber)
    {
        var command = new AddUserCommand(name, "hassani", "1234567890", phoneNumber);

        var action = async () => await _addHandler.Handle(command, CancellationToken.None);

        await action.Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task AddUser_when_PhoneNumber_Is_invalid_ThrowBadRequestException()
    {
        var command = new AddUserCommand("javid", "hassani", "1234567890", "111231231");

        var action = async () => await _addHandler.Handle(command, CancellationToken.None);

        await action.Should().ThrowAsync<BadRequestException>();
    }

    [Fact]
    public async Task AddUser_Ok_Return_UserDto()
    {
        var command = new AddUserCommand("javiddd", "hassani", "1234567890", "0912345678");

        var result = await _addHandler.Handle(command, CancellationToken.None);

        result.name.Should().Be(command.firstName);
        result.family.Should().Be(command.lastName);

        var user = _fixture.Context.Users.FirstOrDefault(i => i.FirstName == command.firstName);

        user.Should().NotBeNull();

        await TruncateUserAsync();
    }

    [Theory]
    [InlineData("", "0912345678")]
    [InlineData("hassani", "")]
    [InlineData("", "")]
    public async Task UpdateUser_When_LastName_Or_NationalCode_Is_Empty_ThrowBadRequestException(string lastName, string phoneNumber)
    {
        var user = await CreateFakeUserAsync();

        var command = new UpdateUserCommand(user.Id, "javid", lastName, phoneNumber);

        var action = async () => await _updateHandler.Handle(command, CancellationToken.None);

        await action.Should().ThrowAsync<BadRequestException>();

        await TruncateUserAsync();
    }

    [Fact]
    public async Task UpdateUser_Ok_Returns_UserDto()
    {
        var user = await CreateFakeUserAsync();

        var command = new UpdateUserCommand(user.Id, "javid", "hassani", "0912345678");

        var result = await _updateHandler.Handle(command, CancellationToken.None);

        result.name.Should().Be(command.name);

        result.family.Should().Be(command.family);

        await TruncateUserAsync();
    }

    [Fact]
    public async Task GetUser_When_Invalid_Id_Throws_NotFoundException()
    {
        var query = new GetUserQuery(999999);

        var action = async () => await _getHandler.Handle(query, CancellationToken.None);

        await action.Should().ThrowAsync<NotFoundException>();
    }

    [Fact]
    public async Task GetUser_When_ValidId_Info_Should_Match()
    {
        var user = await CreateFakeUserAsync();

        var result = await _getHandler.Handle(new GetUserQuery(user.Id), CancellationToken.None);

        result.name.Should().Be(user.FirstName);
        result.family.Should().Be(user.LastName);

        await TruncateUserAsync();
    }

}
