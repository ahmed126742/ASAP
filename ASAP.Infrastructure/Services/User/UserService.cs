using ASAP.Application.Common;
using ASAP.Application.Common.Models;
using ASAP.Application.Features.Users.CreateUser;
using ASAP.Application.Features.Users.DeleteUser;
using ASAP.Application.Features.Users.GetFilteredUsers;
using ASAP.Application.Features.Users.GetUser;
using ASAP.Application.Features.Users.UpdateUser;
using ASAP.Application.Services;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Infrastructure.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(
            IUserRepository userRepository,
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CreateUserResponse> CreateUserAsync(CreateUserRequest request, CancellationToken cancellationToken)
        {
            if (await _userRepository.CheckUserExistanceAsync(request.Email, cancellationToken))
                throw new BadRequestException("Email already exist!");

            var user = _mapper.Map<Domain.Entities.Client>(request);
            _userRepository.Create(user);
            await _unitOfWork.Save(cancellationToken);
            return new CreateUserResponse { Id = user.Id };
        }

        public async Task DeleteUserAsync(DeleteUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.Id, cancellationToken);
            if (user == null)
                throw new NotFoundException("Userd deos not exist!");

            _userRepository.Delete(user);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task<PagedReponse<GetFilteredUsersResponse>> GetPagedFilteresUsers(PaginationRequest<GetFilteredUsersRequest, GetFilteredUsersResponse> request, CancellationToken CancellationToken)
        {
            var users = _userRepository.GetFilteredUsers(request.Filters.SearchText);
            var filteredUsers = _mapper.Map<List<GetFilteredUsersResponse>>(await users.ToListAsync());
            return new PagedReponse<GetFilteredUsersResponse>(filteredUsers, await users.CountAsync(), request.PageNumber, request.PageSize);
        }

        public async Task<GetUserRsponse> GetUserAsync(GetUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.Id, cancellationToken);
            if (user == null)
                throw new NotFoundException("Userd deos not exist!");

            return _mapper.Map<GetUserRsponse>(user);
        }

        public async Task<ICollection<string>> GetUsersEmails(CancellationToken cancellationToken)
        {
              return await _userRepository.GetUsersEmailsAsync(cancellationToken);
        }

        public async Task UpdateUserAsync(UpdateUserRequest request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.Get(request.Id.GetValueOrDefault(), cancellationToken);
            if (user == null)
                throw new NotFoundException("Userd deos not exist!");

            if (request.Email.ToLower() != user.Email.ToLower())
            {
                if (await _userRepository.CheckUserExistanceAsync(request.Email, cancellationToken))
                    throw new BadRequestException("Email already exist!");
            }

            _mapper.Map(request, user);
            _userRepository.Update(user);
            await _unitOfWork.Save(cancellationToken);
        }
    }
}
