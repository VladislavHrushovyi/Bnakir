﻿using AutoMapper;
using MapsterCard.AppDbContext.Entities;
using MapsterCard.AppDbContext.Repositories.Interfaces;
using MediatR;

namespace MapsterCard.Services.SystemService.TransactionConfirmation;

public sealed class
    TransactionConfirmationHandler : IRequestHandler<TransactionConfirmationRequest, TransactionConfirmationResponse>
{
    private readonly IMapper _mapper;
    private readonly ISystemCard _cardRepository;
    private readonly IMapsterMain _mainRepository;

    public TransactionConfirmationHandler(IMapper mapper, ISystemCard cardRepository, IMapsterMain mainRepository)
    {
        _mapper = mapper;
        _cardRepository = cardRepository;
        _mainRepository = mainRepository;
    }

    public async Task<TransactionConfirmationResponse> Handle(TransactionConfirmationRequest request,
        CancellationToken cancellationToken)
    {
        SystemCard cardMap = _mapper.Map<SystemCard>(request.cardInfo);
        var cardInfos = await _cardRepository.FindCardsByProperties(cardMap);
        if (!cardInfos.Any())
        {
            return _mapper.Map<TransactionConfirmationResponse>(new SystemCard()
                { ExpireTo = DateOnly.FromDateTime(DateTime.Now.AddYears(-2)) });
        }

        var findCard = cardInfos.First();
        if (findCard.ExpireTo >= DateOnly.FromDateTime(DateTime.Now))
        {
            var systemSettings = await _mainRepository.GetAllAsync();
            var systemEntity = systemSettings.First();
            systemEntity.Balance += request.Commission;
            await _mainRepository.UpdateAsync(systemEntity);
        }

        return _mapper.Map<TransactionConfirmationResponse>(findCard);
    }
}