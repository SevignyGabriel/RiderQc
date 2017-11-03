﻿using RiderQc.Web.App_Start;
using RiderQc.Web.Models;
using RiderQc.Web.Repository.Interface;
using RiderQc.Web.ViewModels.Ride;
using RiderQc.Web.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;

namespace RiderQc.Web.Controllers.API
{
    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        private readonly IUserRepository repo;

        public UserController(IUserRepository _repo)
        {
            repo = _repo;
        }

        /// <summary>
        /// Register a user
        /// </summary>
        /// <param name="userViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        [ResponseType(typeof(UserRegisterViewModel))]
        public IHttpActionResult Register(UserRegisterViewModel userViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (repo.CheckUserExistence(userViewModel.Username))
                return BadRequest("That username is already taken.");

            DateTime? userDOB = userViewModel.DateOfBirth;
            string region = userViewModel.Region;
            string ville = userViewModel.Ville;
            string dpURL = userViewModel.DpUrl;

            bool result = repo.RegisterUser(userViewModel);

            if (result)
            {
                return Ok("User '" + userViewModel.Username +"' successfully registered!");
            }
            else
            {
                return BadRequest("Error while register user.");
            }
        }
        
        /// <summary>
        /// Delete a user
        /// </summary>
        /// <param name="username">username of the user.</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{username}")]
        [AuthTokenAuthorization]
        [ResponseType(typeof(string))]
        public IHttpActionResult DeleteUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest("Please enter a valid username.");
            }

            bool result = repo.DeleteUser(username);

            if (result)
            {
                return Ok("User successfully deleted!");
            }
            else
            {
                return BadRequest("Error while deleting user.");
            }
        }

        /// <summary>
        /// List all the users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("list")]
        [ResponseType(typeof(List<UserViewModel>))]
        public IHttpActionResult GetAllUsers()
        {
            List<UserViewModel> users = repo.GetAllUsers();

            return Ok(users);
        }

        /// <summary>
        /// List all partipating rides of user
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("myrides")]
        [ResponseType(typeof(List<RideViewModel>))]
        public IHttpActionResult GetMyRides([FromUri] string username)
        {
            List<RideViewModel> rides = repo.GetMyRides(username);

            if (rides?.Count == 0)
            {
                return NotFound();
            }
            else if (rides?.Count > 0)
            {
                return Ok(rides);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get User by username
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(UserViewModel))]
        public IHttpActionResult GetUserById([FromUri] string username)
        {
            UserViewModel user = repo.GetUserByName(username);

            if(user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get User by Authorization token
        /// </summary>
        /// <param name="auth_token"></param>
        /// <returns></returns>
        [AuthTokenAuthorization]
        [HttpGet]
        [Route("bytoken")]
        [ResponseType(typeof(UserViewModel))]
        public IHttpActionResult GetUserByAuthToken()
        {
            ApplicationUser user = (ApplicationUser)User;

            UserViewModel userViewModel = repo.GetUserByTokenIfLastTokenIsValid(user.Token);

            if (userViewModel != null)
            {
                return Ok(userViewModel);
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
