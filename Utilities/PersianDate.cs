using System.Globalization;

namespace GCMS.Web.Utilities
{
    public class PersianDate
    {
        public string PersianDateCalender(DateTime date)
        {
            PersianCalendar pc = new PersianCalendar();
            var persiandate = pc.GetYear(date) + "/" + pc.GetMonth(date) + "/" + pc.GetDayOfMonth(date);
            return persiandate;
        }
    }
}
