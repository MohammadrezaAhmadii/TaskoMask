﻿using TaskoMask.BuildingBlocks.Domain.Specifications;
using TaskoMask.Services.Owners.Write.Api.Domain.Entities;

namespace TaskoMask.Services.Owners.Write.Api.Domain.Specifications
{
    internal class OrganizationNameAndDescriptionCannotSameSpecification : ISpecification<Organization>
    {

        /// <summary>
        /// 
        /// </summary>
        public bool IsSatisfiedBy(Organization organization)
        {
            return organization.Name.Value.ToLower() != organization.Description.Value?.ToLower();
        }
    }
}