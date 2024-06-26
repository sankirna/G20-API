﻿using G20.API.Models.ScheduleTask;
using G20.Service.ScheduleTasks;
using Microsoft.AspNetCore.Mvc;

namespace G20.API.Controllers
{
    //do not inherit it from BasePublicController. otherwise a lot of extra action filters will be called
    //they can create guest account(s), etc
    //[AutoValidateAntiforgeryToken]
    public partial class ScheduleTaskController : BaseController
    {
        protected readonly IScheduleTaskService _scheduleTaskService;
        protected readonly IScheduleTaskRunner _taskRunner;

        public ScheduleTaskController(IScheduleTaskService scheduleTaskService,
            IScheduleTaskRunner taskRunner)
        {
            _scheduleTaskService = scheduleTaskService;
            _taskRunner = taskRunner;
        }

        [HttpPost]
        //[IgnoreAntiforgeryToken]
        public virtual async Task<IActionResult> RunTask(RunTaskRequestModel model)
        {
            var scheduleTask = await _scheduleTaskService.GetTaskByTypeAsync(model.TaskType);
            if (scheduleTask == null)
                //schedule task cannot be loaded
                return NoContent();

            await _taskRunner.ExecuteAsync(scheduleTask);

            return NoContent();
        }
    }
}
