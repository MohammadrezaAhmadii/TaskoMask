﻿using AutoMapper;
using TaskoMask.Application.Core.Helpers;
using System.Threading.Tasks;
using TaskoMask.Application.Common.BaseEntitiesUsers.Queries.Models;
using TaskoMask.Application.Core.Dtos.Users;
using TaskoMask.Application.Core.Notifications;
using TaskoMask.Application.Core.Bus;
using TaskoMask.Application.Common.BaseEntities.Services;
using TaskoMask.Domain.Core.Models;

namespace TaskoMask.Application.Common.BaseEntitiesUsers.Services
{
    public class BaseUserService<TEntity> : BaseEntityService<TEntity>, IBaseUserService where TEntity : BaseUser
    {
        #region Fields

        #endregion

        #region Ctors

        public BaseUserService(IInMemoryBus inMemoryBus, IMapper mapper, IDomainNotificationHandler notifications) : base(inMemoryBus, mapper, notifications)
        { }


        #endregion

        #region Public Methods




        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<UserBasicInfoDto>> GetBaseUserByIdAsync(string id)
        {
            return await SendQueryAsync(new GetUserByIdQuery<TEntity>(id));
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<UserBasicInfoDto>> GetBaseUserByUserNameAsync(string userName)
        {
            return await SendQueryAsync(new GetUserByUserNameQuery<TEntity>(userName));
        }


        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<UserBasicInfoDto>> GetBaseUserByPhoneNumberAsync(string phoneNumber)
        {
            return await SendQueryAsync(new GetUserByPhoneNumberQuery<TEntity>(phoneNumber));
        }




        /// <summary>
        /// 
        /// </summary>
        public async Task<Result<bool>> ValidateUserPasswordAsync(string userName, string password)
        {
            var query = new ValidateUserPasswordQuery<TEntity>(userName, password);
            return await SendQueryAsync(query);
        }


        #endregion

    }
}