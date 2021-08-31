using FSD.Context.Services;
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
    public class RetentionController : ControllerBase
    {
        private readonly ICalculate calculate;
        private readonly ReportService service;

        public RetentionController(ICalculate Calculate,ReportService service)
        {
            this.calculate = Calculate;
            this.service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public ActionResult<double> GetRetention(int days=7)
        {
            service.Start = DateTime.Now;
            service.Text = "время выполенения контроллера GetRetention";

            if (days <= 0)
            {
                service.Text = "(ОШИБКА ( days<=0 )) время выполенения контроллера GetRetention";
                service.Save();
                return BadRequest();
            }
            
            
            var retention = calculate.CalculateRetention(days);
            service.Save();
            return retention;
        }

        [HttpGet("LifeSpan")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IEnumerable<int> GetLifeSpan()
        {
            service.Start = DateTime.Now;
            service.Text = "время выполенения контроллера GetLifeSpan";
            var lifeSpanUser = calculate.LifeSpanOfUsers();
            service.Save();
            return lifeSpanUser;
        }
    }
}
