using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BusinessEntities;
using Core.Services.Users;
using WebApi.Models.Users;
using Common.Results;

namespace WebApi.Controllers
{
    [RoutePrefix("users")]
    public class UserController : BaseApiController
    {
        private readonly ICreateUserService _createUserService;
        private readonly IDeleteUserService _deleteUserService;
        private readonly IGetUserService _getUserService;
        private readonly IUpdateUserService _updateUserService;

        public UserController(ICreateUserService createUserService, IDeleteUserService deleteUserService, IGetUserService getUserService, IUpdateUserService updateUserService)
        {
            _createUserService = createUserService;
            _deleteUserService = deleteUserService;
            _getUserService = getUserService;
            _updateUserService = updateUserService;
        }

        ///NEW ROUTES
        
        // Create user (POST /users)
        [HttpPost]
        [Route("")]
        public HttpResponseMessage CreateUser([FromBody] UserModel model)
        {
            var monthlySalary = model.MonthlySalary ??
                    (model.AnnualSalary.HasValue ? model.AnnualSalary.Value / 12 : (decimal?)null);

            if (model.MonthlySalary.HasValue && model.AnnualSalary.HasValue)
            {
                var expectedAnnual = model.MonthlySalary.Value * 12;
                if (expectedAnnual != model.AnnualSalary.Value)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        new { error = "Monthly and Annual salary values are inconsistent." });
                }
            }

            var result = _createUserService.Create(model.Name, model.Email, model.Age, model.Type, monthlySalary, model.Tags);

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = result.Errors });

            return Request.CreateResponse(HttpStatusCode.Created, new UserData(result.Value));
        }

        // Update user (PUT /users/{id})
        [HttpPut]
        [Route("{userId:guid}")]
        public HttpResponseMessage UpdateUser(Guid userId, [FromBody] UserModel model)
        {
            var user = _getUserService.GetUser(userId);
            if (user == null)
                return DoesNotExist();

            var monthlySalary = model.MonthlySalary ??
                (model.AnnualSalary.HasValue ? model.AnnualSalary.Value / 12 : (decimal?)null);

            if (model.MonthlySalary.HasValue && model.AnnualSalary.HasValue)
            {
                var expectedAnnual = model.MonthlySalary.Value * 12;
                if (expectedAnnual != model.AnnualSalary.Value)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        new { error = "Monthly and Annual salary values are inconsistent." });
                }
            }

            var result = _updateUserService.Update(user, model.Name, model.Email, model.Age, model.Type, monthlySalary, model.Tags);

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = result.Errors });

            return Request.CreateResponse(HttpStatusCode.OK, new UserData(result.Value));
        }

        // Delete user (DELETE /users/{id})
        [HttpDelete]
        [Route("{userId:guid}")]
        public HttpResponseMessage DeleteUser(Guid userId)
        {
            var result = _deleteUserService.DeleteById(userId);
            if (!result.Success)
                return DoesNotExist();

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        // Get single user (GET /users/{id})
        [HttpGet]
        [Route("{userId:guid}")]
        public HttpResponseMessage GetUser(Guid userId)
        {
            var user = _getUserService.GetUser(userId);
            if (user == null)
                return DoesNotExist();

            return Request.CreateResponse(HttpStatusCode.OK, new UserData(user));
        }

        // Get users list (GET /users/list?skip=0&take=50&type=Employee&email=b@b.com)
        [HttpGet]
        [Route("list")]
        public HttpResponseMessage GetUsers(int skip = 0, int take = 50, UserTypes? type = null, string name = null, string email = null)
        {
            if (take > 100) take = 100; // enforce max page size

            var users = _getUserService.GetUsers(type, name, email)
                                       .Skip(skip).Take(take)
                                       .Select(u => new UserData(u))
                                       .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, users);
        }

        // Delete all users (DELETE /users/clear) — dangerous
        [HttpDelete]
        [Route("clear")]
        public HttpResponseMessage DeleteAllUsers([FromUri] bool confirm = false)
        {
            if (!confirm)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { error = "Confirmation required" });

            _deleteUserService.DeleteAll();
            return Request.CreateResponse(HttpStatusCode.NoContent);
        }

        // Get users by tag (GET /users/list/tag?tag=developer)
        [HttpGet]
        [Route("list/tag")]
        public HttpResponseMessage GetUsersByTag(string tag)
        {
            var users = _getUserService.GetUsers(null, null, null)
                                       .Where(u => u.Tags.Contains(tag))
                                       .Select(u => new UserData(u))
                                       .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, users);
        }



        ///OLD ROUTES with nonstandard guid

        [Route("{userId:guid}/create")]
        [HttpPost]
        public HttpResponseMessage CreateUserLegacy(Guid userId, [FromBody] UserModel model)
        {
            var monthlySalary = model.MonthlySalary ??
                (model.AnnualSalary.HasValue ? model.AnnualSalary.Value / 12 : (decimal?)null);

            if (model.MonthlySalary.HasValue && model.AnnualSalary.HasValue)
            {
                var expectedAnnual = model.MonthlySalary.Value * 12;
                if (expectedAnnual != model.AnnualSalary.Value)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        new { error = "Monthly and Annual salary values are inconsistent." });
                }
            }

            var result = _createUserService.CreateLegacy(userId, model.Name, model.Email, model.Age, model.Type, monthlySalary, model.Tags);

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = result.Errors });

            return Request.CreateResponse(HttpStatusCode.Created, new UserData(result.Value));
        }

        [Route("{userId:guid}/update")]
        [HttpPost]
        public HttpResponseMessage UpdateUserLegacy(Guid userId, [FromBody] UserModel model)
        {
            var user = _getUserService.GetUser(userId);
            if (user == null)
                return DoesNotExist();

            var monthlySalary = model.MonthlySalary ??
                (model.AnnualSalary.HasValue ? model.AnnualSalary.Value / 12 : (decimal?)null);

            if (model.MonthlySalary.HasValue && model.AnnualSalary.HasValue)
            {
                var expectedAnnual = model.MonthlySalary.Value * 12;
                if (expectedAnnual != model.AnnualSalary.Value)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest,
                        new { error = "Monthly and Annual salary values are inconsistent." });
                }
            }

            var result = _updateUserService.Update(user, model.Name, model.Email, model.Age, model.Type, monthlySalary, model.Tags);

            if (!result.Success)
                return Request.CreateResponse(HttpStatusCode.BadRequest, new { errors = result.Errors });

            return Request.CreateResponse(HttpStatusCode.OK, new UserData(result.Value));
        }

        [Route("{userId:guid}/delete")]
        [HttpDelete]
        public HttpResponseMessage DeleteUserLegacy(Guid userId)
        {
            var result = _deleteUserService.DeleteById(userId);
            if (!result.Success)
                return DoesNotExist();

            return Request.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}