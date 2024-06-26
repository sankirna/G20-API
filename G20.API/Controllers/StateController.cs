﻿using G20.API.Factories.States;
using G20.API.Infrastructure.Mapper.Extensions;
using G20.API.Models.States;
using G20.Core;
using G20.Core.Domain;
using G20.Service.States;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    public class StateController : BaseController
    {
        protected readonly IWorkContext _workContext;
        protected readonly IStateFactoryModel _stateFactoryModel;
        protected readonly IStateService _stateService;

        public StateController(IWorkContext workContext, IStateFactoryModel stateFactoryModel, IStateService stateService)
        {
            _workContext = workContext;
            _stateFactoryModel = stateFactoryModel;
            _stateService = stateService;
        }

        [HttpPost]
        public virtual async Task<IActionResult> List(StateSearchModel searchModel)
        {
            var model = await _stateFactoryModel.PrepareStateListModelAsync(searchModel);
            return Success(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Get(int id)
        {
            var state = await _stateService.GetByIdAsync(id);
            if (state == null)
                return Error("not found");
            return Success(state.ToModel<StateModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(StateModel model)
        {
            var state = model.ToEntity<State>();
            await _stateService.InsertAsync(state);
            return Success(state.ToModel<StateModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(StateModel model)
        {
            var state = await _stateService.GetByIdAsync(model.Id);
            if (state == null)
                return Error("not found");

            state = model.ToEntity(state);
            await _stateService.UpdateAsync(state);
            return Success(state.ToModel<StateModel>());
        }

        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            var state = await _stateService.GetByIdAsync(id);
            if (state == null)
                return Error("not found");
            await _stateService.DeleteAsync(state);
            return Success(id);
        }
    }
}
