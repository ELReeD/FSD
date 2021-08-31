using FSD.Context.DTO;
using FSD.Context.Model;
using FSD.Service;
using FSD.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FSD.Controller.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserDbContext dBContext;
        private readonly ReportService service;

        public UsersController(UserDbContext dBContext,ReportService service)
        {
            this.dBContext = dBContext;
            this.service = service;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> Add([FromForm] UserInfoDTO userDto)
        {
            service.Start = DateTime.Now;
            service.Text = "время выполенения контроллера Add";

            var userInfo = new UserInformation(userDto.DateRegistration, userDto.DateLastActivity);

            try
            {
                await dBContext.UsersInformation.AddAsync(userInfo);
                await dBContext.SaveChangesAsync();
            }
            catch (Exception ex )
            {
                service.Text = ex.Message;
                service.Save();
                return BadRequest(ex.Message);
            }

            service.Save();

            return Ok();
        }

        [HttpPost("AddRange")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddRange(IEnumerable<UserInfoDTO> usersDto)
        {
            service.Start = DateTime.Now;
            service.Text = "время выполенения контроллера AddRange";

            var userList = new List<UserInformation>();
            foreach (var item in usersDto)
            {
                userList.Add(new UserInformation(item.DateRegistration, item.DateLastActivity)); 
            }

            try
            {
                await dBContext.UsersInformation.AddRangeAsync(userList);
                await dBContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                service.Text = ex.Message;
                service.Save();
                return BadRequest(ex.Message);
            }
            service.Save();
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetAll()
        {
            service.Start = DateTime.Now;
            service.Text = "время выполенения контроллера GetAll";

            var allUsers = dBContext.UsersInformation.ToList();

            service.Save();

            if (allUsers.Count == 0)
                return NoContent();

            return Ok(allUsers);
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Delete(string id)
        {
            service.Start = DateTime.Now;
            service.Text = "время выполенения контроллера Delete";

            if (id == null)
            {
                service.Text = "(Ошибка) id==null в контроллере DELETE";
                service.Save();
                return BadRequest();
            }
            
            var user = dBContext.UsersInformation.Where(x => x.Id == id).FirstOrDefault();

            if (user == null)
            {
                service.Text = "(Ошибка) user==null в контроллере DELETE";
                service.Save();
                return BadRequest();
            }

            dBContext.UsersInformation.Remove(user);
            await dBContext.SaveChangesAsync();
            service.Save();
            return Ok();
        }
    }
}
