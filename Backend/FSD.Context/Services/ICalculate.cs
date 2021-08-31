using System;
using System.Collections.Generic;
using System.Text;

namespace FSD.Context.Services
{
    //Rolling Retention X day = (количество пользователей, вернувшихся в систему в X-ый день или позже) 
    //   /(количество пользователей, установивших приложение X дней назад или раньше) * 100%.
    public interface ICalculate
    {
        double CalculateRetention(int days);
        IEnumerable<int> LifeSpanOfUsers();
    }
}

