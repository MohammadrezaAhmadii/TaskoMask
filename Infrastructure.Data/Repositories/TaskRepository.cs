﻿using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using TaskoMask.Domain.Data;
using TaskoMask.Infrastructure.Data.DbContext;

namespace TaskoMask.Infrastructure.Data.Repositories
{
    public class TaskRepository : BaseRepository<Domain.Entities.Task>, ITaskRepository
    {
        #region Fields

        private readonly IMongoCollection<Domain.Entities.Task> _tasks;

        #endregion

        #region Ctors

        public TaskRepository(IMainDbContext dbContext) : base(dbContext)
        {
            _tasks = dbContext.GetCollection<Domain.Entities.Task>();
        }

        #endregion

        #region Public Methods



        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<Domain.Entities.Task>> GetListByCardIdAsync(string cardId)
        {
            return await _tasks.AsQueryable().Where(o => o.CardId == cardId).ToListAsync();
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<IEnumerable<Domain.Entities.Task>> GetListByOrganizationIdAsync(string organizationId, int takeCount)
        {
            return await _tasks.AsQueryable().Where(o => o.OrganizationId == organizationId).OrderByDescending(o=> o.CreationTime.CreateDateTime).Take(takeCount).ToListAsync();
        }



        /// <summary>
        /// 
        /// </summary>
        public async Task<bool> ExistByTitleAsync(string id, string title)
        {
            var task = await _tasks.Find(e => e.Title == title).FirstOrDefaultAsync();
            return task != null && task.Id != id;
        }

        #endregion

        #region Private Methods



        #endregion

    }
}
