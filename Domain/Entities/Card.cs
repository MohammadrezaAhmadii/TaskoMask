﻿using TaskoMask.Domain.Core.Enums;
using TaskoMask.Domain.Core.Models;

namespace TaskoMask.Domain.Entities
{
    public class Card : BaseEntity
    {
        #region Fields


        #endregion

        #region Ctors

        public Card(string name, string description, string boardId, CardType type, string organizationId, string projectId)
        {
            Name = name;
            Description = description;
            BoardId = boardId;
            ProjectId = projectId;
            OrganizationId = organizationId;
            Type = type;
        }

        #endregion

        #region Properties


        public string Name { get; set; }
        public string Description { get; set; }
        public CardType Type { get; set; }
        public string BoardId { get; set; }
        public string ProjectId { get; private set; }
        public string OrganizationId { get; private set; }

        #endregion

        #region Public Methods



        /// <summary>
        /// 
        /// </summary>
        public void Update(string name, string description, CardType type)
        {
            Name = name;
            Type = type;
            Description = description;
            base.Update();

        }

        #endregion

        #region Private Methods



        #endregion



    }
}
