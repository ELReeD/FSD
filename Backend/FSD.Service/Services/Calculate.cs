using FSD.Context.Model;
using FSD.Context.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FSD.Service.Services
{
   
    public class Calculate : ICalculate
    {
        private readonly UserDbContext dbContext;
        private readonly ReportService service;

        public Calculate(UserDbContext dbContext,ReportService service)
        {
            this.dbContext = dbContext;
            this.service = service;
        }

        public double CalculateRetention(int days)
        {
            service.Start = DateTime.Now;
            service.Text = "время выполнение логики CalculateRetention";


            var returneesUserCount = GetReturneesUserCount(days);
            var appInstaledCount = AppInstaledCount(days);
            var allUsersCount = AllUsersCount();
            double rollingRetention;
            try
            {
                rollingRetention = (double)returneesUserCount / appInstaledCount * allUsersCount;
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }

            //время выполнение логики CalculateRetention

            service.Save();
            return Math.Round(rollingRetention,2);
        }

        private int GetReturneesUserCount(int days)
        {
            //(количество пользователей, вернувшихся в систему в X-ый день или позже) 
            service.Start = DateTime.Now;
            service.Text = "время запроса в бд из GetReturneesUserCount";

            var returnees = dbContext.UsersInformation.Where(x => x.DateLastActivity >= x.DateRegistration.AddDays(days)).Count();

            service.Save();
            return returnees;
        }

        private int AppInstaledCount(int days)
        {
            //(количество пользователей, установивших приложение X дней назад или раньше)
            service.Start = DateTime.Now;
            service.Text = "время запроса в бд из AppInstaledCount";

            var instaled = dbContext.UsersInformation.Where(x => x.DateRegistration <= DateTime.Now.AddDays(days*-1)).Count();

            service.Save();
            return instaled;
        }
        
        private int AllUsersCount()
        {
            service.Start = DateTime.Now;
            service.Text = "время запроса в бд из AllUsersCount";


            var allUsers = dbContext.UsersInformation.Count();


            service.Save();

            return allUsers;
        }

        public IEnumerable<int> LifeSpanOfUsers()
        {
            service.Start = DateTime.Now;
            service.Text = "время выполенения LifeSpanOfUsers";

            var allUsers = dbContext.UsersInformation;
            List<int> lifeSpan = new List<int>();

            foreach (var user in allUsers)
            {
                TimeSpan ts = user.DateLastActivity.Subtract(user.DateRegistration);
                lifeSpan.Add(ts.Days);                                
            }

            service.Save();

            return lifeSpan.Distinct();
        }

    }
}
