using System;

namespace BizDayCalc;

public class USRegionFixture
{
    public Calculator Calc { get; private set; }

    public USRegionFixture()
    {
        Calc = new Calculator();
        // Calc.AddRule(new WeekendRule());
        Calc.AddRule(new HolidayRule());
    }
}
