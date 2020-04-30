﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DoTask.Api.v1.Application.Commands.Users.CreateUserCommand;
using DoTask.Api.v1.Application.Queries.Users.ListUserSummaryQuery;
using DoTask.Api.v1.Application.Queries.Users.UserSummaryQuery.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DoTask.Api.v1.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class UsersController : ApiControllerBase
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IList<UserSummary>>> GetUsersAsync()
        {
            return Single(await QueryAsync(new ListUserSummaryQuery()));
        }

        //[HttpGet]
        //[ProducesResponseType(typeof(IEnumerable<UserSummary>), (int)HttpStatusCode.OK)]
        ////public async Task<ActionResult<IEnumerable<UserSummary>>> GetUsersAsync()
        //public string Get(int id)
        //{
        //    //var orders = await _userQueries.GetUsersAsync();

        //    //return Ok(users);
        //    return "";
        //}

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="command">Info of user</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateUserAsync([FromBody] CreateUserCommand command)
        {

            var response = await CommandAsync(command);

            if (response.Errors.Any())
            {
                return BadRequest(response.Errors);
            }

            return Ok(response.Result);
        }

        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
