﻿using TaskoMask.Application.Share.Dtos.Workspace.Cards;
using TaskoMask.Application.Share.Helpers;
using TaskoMask.Application.Share.ViewModels;

namespace TaskoMask.Presentation.Framework.Share.Contracts
{
    public interface ICardWebService
    {
        Task<Result<CommandResult>> Create(CardUpsertDto input);
        Task<Result<CardDetailsViewModel>> Get(string id);
        Task<Result<CommandResult>> Update(CardUpsertDto input);
    }
}