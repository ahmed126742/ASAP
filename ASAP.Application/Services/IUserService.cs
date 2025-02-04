﻿using ASAP.Application.Common.Models;
using ASAP.Application.Features.Users.CreateUser;
using ASAP.Application.Features.Users.DeleteUser;
using ASAP.Application.Features.Users.GetFilteredUsers;
using ASAP.Application.Features.Users.GetUser;
using ASAP.Application.Features.Users.UpdateUser;

namespace ASAP.Application.Services
{
    public interface IUserService
    {
        Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken);

        Task DeleteUserAsync(DeleteUserRequest request, CancellationToken cancellationToken);

        Task<GetUserRsponse> GetUserAsync(GetUserRequest request, CancellationToken cancellationToken);

        Task UpdateUserAsync(UpdateUserRequest request, CancellationToken cancellationToken);

        Task<ICollection<string>> GetUsersEmails(CancellationToken cancellationToken);

        Task<PagedReponse<GetFilteredUsersResponse>>GetPagedFilteresUsers(PaginationRequest<GetFilteredUsersRequest, GetFilteredUsersResponse>  request, CancellationToken CancellationToken);

    }
}
