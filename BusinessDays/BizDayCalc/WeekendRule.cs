using System;

namespace BizDayCalc;

public class WeekendRule
{
    public bool CheckIsBusinessDay(DateTime date)
    {
        return
            date.DayOfWeek != DayOfWeek.Saturday &&
            date.DayOfWeek != DayOfWeek.Sunday;
    }
}
