using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Common.Helpers;
using ProFilePOC2.Application.Common.Exceptions;
using ProFilePOC2.Application.Common.Interfaces;
using ProFilePOC2.Application.Common.Models;
using ProFilePOC2.Application.Common.Models.Dtos;
using ProFilePOC2.Domain.Entities;
using ProFilePOC2.Domain.ValueObjects;

namespace ProFilePOC2.Application.Accounts.Commands.CreateAccount;

public record CreateAccountCommand : IRequest<UserDto>
{
    public Guid CreatorId { get; set; }
    public string Email { get; init; }
    public string Password { get; init; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public string Position { get; set; }
    public User ToAccount()
    {
        return new User
        {
            Id = Guid.NewGuid(),
            Username = Email,
            Email = Email,
            PasswordHash = SecurityUtil.Hash(Password),
            FirstName = FirstName,
            LastName = LastName,
            Created = DateTime.UtcNow,
            CreatedBy = CreatorId,
            Position = Position
        };
    }
}

public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, UserDto>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    public CreateAccountCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }
    public async Task<UserDto> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
    {
        var accountEntity = await _applicationDbContext.Users.FirstOrDefaultAsync(a => a.Username.Equals(request.Email) || a.Email.Equals(request.Email), cancellationToken: cancellationToken);
        if (accountEntity is not null)
        {
            throw new UsernameConflictedException();
        }

        var account = request.ToAccount();
        var roleEntity = await _applicationDbContext.Roles.SingleOrDefaultAsync(r => r.Name.Equals(request.Role), cancellationToken: cancellationToken);

        if (roleEntity is null)
        {
            var role = new Role { Name = request.Role };
            var addedRole = await _applicationDbContext.Roles.AddAsync(role, cancellationToken);
            account.Role = addedRole.Entity;
        }
        else
        {
            account.Role = roleEntity;
        }

        var result = await _applicationDbContext.Users.AddAsync(account, cancellationToken);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);
        return _mapper.Map<UserDto>(result.Entity);
    }
}