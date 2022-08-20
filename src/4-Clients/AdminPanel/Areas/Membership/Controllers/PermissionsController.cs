﻿using TaskoMask.Services.Monolith.Application.Membership.Permissions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using AutoMapper;
using TaskoMask.BuildingBlocks.Web.MVC.Controllers;
using TaskoMask.BuildingBlocks.Web.MVC.Extensions;
using TaskoMask.BuildingBlocks.Contracts.Dtos.Membership.Permissions;
using TaskoMask.BuildingBlocks.Web.MVC.Helpers;
using TaskoMask.BuildingBlocks.Web.MVC.Enums;
using TaskoMask.BuildingBlocks.Web.MVC.Filters;

namespace TaskoMask.Clients.AdminPanle.Areas.Membership.Controllers
{

    [Authorize]
    [Area("Membership")]
    public class PermissionsController : BaseMvcController
    {
        #region Fields

        private readonly IPermissionService _permissionService;

        #endregion

        #region Ctor

        public PermissionsController(IPermissionService permissionService, IMapper mapper) : base()
        {
            _permissionService = permissionService;

        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var permissionQueryResult = await _permissionService.SearchAsync(page: 1, recordsPerPage: recordsPerPage, term: "", groupName: "");

            #region ViewBags

            ViewBag.PageSize = permissionQueryResult.Value.PageNumber;
            ViewBag.CurrentPage = 1;
            ViewBag.TotalItemCount = permissionQueryResult.Value.TotalCount;
            ViewBag.GroupName = (await _permissionService.GetGroupedSelectListAsync()).Value.ToMvcSelectList();

            #endregion

            return View(permissionQueryResult);
        }




        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        [AjaxOnly]
        public async Task<IActionResult> Search(int page = 1, string term = "", string groupName = "")
        {
            var permissionQueryResult = await _permissionService.SearchAsync(page: page, recordsPerPage: recordsPerPage, term: term, groupName: groupName);

            if (!permissionQueryResult.IsSuccess)
                return RedirectToErrorPage(permissionQueryResult);

            #region ViewBags

            ViewBag.PageSize = permissionQueryResult.Value.PageNumber;
            ViewBag.CurrentPage = page;
            ViewBag.TotalItemCount = permissionQueryResult.Value.TotalCount;

            #endregion

            return PartialView("_ItemList", permissionQueryResult.Value.Items);
        }






        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public IActionResult Create()
        {

            return View();
        }




        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<JavaScriptResult> Create(PermissionUpsertDto input)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetErrors();
                return ScriptBox.ShowMessage(errors, MessageType.error);
            }

            var cmdResult = await _permissionService.CreateAsync(input);
            return AjaxResult(cmdResult);
        }



        /// <summary>
        /// 
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Update(string id)
        {
            var permissionQueryResult = await _permissionService.GetDetailsAsync(id);

            return View(permissionQueryResult);

        }




        /// <summary>
        /// 
        /// </summary>
        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public async Task<JavaScriptResult> Update(PermissionUpsertDto input)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.GetErrors();
                return ScriptBox.ShowMessage(errors, MessageType.error);
            }

            var cmdResult = await _permissionService.UpdateAsync(input);
            return AjaxResult(cmdResult);
        }





        #endregion

        #region Private Methods



        #endregion

    }
}