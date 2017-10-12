string DATE = "1061010";

string new_date = Convert.ToString(Convert.ToInt32(DATE) + 19110000);
IFormatProvider culture = new System.Globalization.CultureInfo("zh-TW", true);
DateTime NewDate = DateTime.ParseExact(new_date, "yyyyMMdd", culture);
string DATE_ADD90 = Convert.ToString(Convert.ToInt32(NewDate.AddDays(89).ToString("yyyyMMdd")) - 19110000);