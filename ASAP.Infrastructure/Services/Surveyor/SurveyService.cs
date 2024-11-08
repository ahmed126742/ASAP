using ASAP.Application.Common.Enums;
using ASAP.Application.Common.Models;
using ASAP.Application.Services.ContractItems.DTOs;
using ASAP.Application.Services.User.DTOs;
using ASAP.Application.Services.User.Fitting.DTOs.Retrieval;
using ASAP.Application.Services.User.Survey;
using ASAP.Application.Services.User.Survey.DTOs.Processing;
using ASAP.Application.Services.User.Survey.DTOs.Retrieval;
using ASAP.Domain.Entities;
using ASAP.Domain.Repositories;
using ASAP.Domain.Repositories.Common;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ASAP.Infrastructure.Services.Surveyor
{
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly IMapper _mapper;
        private readonly IContractItemRepository _contractItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SurveyService(
            IContractItemRepository contractItemRepository,
            IMapper mapper,
            ISurveyRepository surveyRepository,
            IUnitOfWork unitOfWork)
        {
            _surveyRepository = surveyRepository;
            _contractItemRepository = contractItemRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<PagedReponse<GetUserJobsResponse>> GetMyJobs(PaginationRequest<GetUserJobsRequest, GetUserJobsResponse> request, CancellationToken cancellationToken)
        {
            var filteredContractItems = _contractItemRepository.GetAllAsQuarble(x => x.SurveyorId == request.Filters.UserId && x.Status == (int)JobStatusEnum.ToSurvey)
                .Include(c => c.Contract)
                .Select(x => _mapper.Map<GetUserJobsResponse>(x));

            return new PagedReponse<GetUserJobsResponse>(filteredContractItems, await filteredContractItems.CountAsync(cancellationToken), request.PageNumber, request.PageSize);
        }

        public async Task<Guid> CreateSurvey(CreateSurveyRequest request, CancellationToken cancellationToken)
        {
            var survey = _mapper.Map<Survey>(request);
            _surveyRepository.Create(survey);
            await _unitOfWork.Save(cancellationToken);
            return survey.Id;
        }

        public async Task<GetSurveyResponse> GetSurvey(GetSurveyRequest request, CancellationToken cancellationToken)
        {
            var survey = await _surveyRepository.Get(request.Id.Value, cancellationToken);
            if (survey == null)
                throw new Exception("Survery does not exist!");

            return _mapper.Map<GetSurveyResponse>(survey);
        }

        public async Task<PagedReponse<GetSurveyResponse>> GetSurveys(PaginationRequest<GetSurverysRequest, GetSurveyResponse> request, CancellationToken cancellationToken)
        {
            var surveys = _surveyRepository.GetAllAsQuarble();
            var pagedsurveys = surveys.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => _mapper.Map<GetSurveyResponse>(x));

            return new PagedReponse<GetSurveyResponse>(pagedsurveys, await surveys.CountAsync(), request.PageNumber, request.PageSize); ;

        }

        public async Task<IList<GetSurveyResponse>> GetSurveysByContractItem(ContractItemIdentity request, CancellationToken cancellationToken)
        {
            var surveys = await _surveyRepository.GetAllAsQuarble(x => x.ContractItemId == request.Id).ToListAsync(cancellationToken);
            if (!surveys.Any())
                throw new Exception("contract Item does not exist!");

            return _mapper.Map<List<GetSurveyResponse>>(surveys);
        }

        public async Task UpdateSurvey(UpdateSurveyRequest request, CancellationToken cancellationToken)
        {
            var survey = await _surveyRepository.Get(request.Id.Value, cancellationToken);
            if (survey == null)
                throw new Exception("Survery does not exist!");

            _mapper.Map(request, survey);
            _surveyRepository.Update(survey);
            await _unitOfWork.Save(cancellationToken);
        }

        public async Task DeleteSurvey(DeleteSurveyRequest request, CancellationToken cancellationToken)
        {
            var survey = await _surveyRepository.Get(request.Id.Value, cancellationToken);
            if (survey == null)
                throw new Exception("Survery does not exist!");

            _surveyRepository.Delete(survey);
            await _unitOfWork.Save(cancellationToken);
        }

    }
}
