﻿using ARDC.BizCard.Core.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ARDC.BizCard.Core.Services
{
    public interface IBizCardAgendaService
    {
        Task AddCardAsync(BizCardContent newCard, CancellationToken ct = default);

        Task RemoveCardAsync(BizCardContent card, CancellationToken ct = default);

        Task<IList<BizCardContent>> GetCardsAsync(CancellationToken ct = default);
    }
}