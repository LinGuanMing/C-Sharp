---
tags: C#
disqus: hackmd
---

C# 開發筆記
===

[![hackmd-github-sync-badge](https://hackmd.io/CyYANBG0Q7ahdUIBhSbJpA/badge)](https://hackmd.io/CyYANBG0Q7ahdUIBhSbJpA)



> 鍵盤事件處理
> 時間處理

---

鍵盤事件處理
===

ENTER自動找到下一個焦點
---
```csharp=
private void Form1_KeyDown(object sender, KeyEventArgs e)
{
    if (e.KeyCode == Keys.Enter)
    {
        Control ctrl = this.GetNextControl(this.ActiveControl, true);
        while (ctrl is TextBox == false && ctrl is ComboBox == false)
        {
            ctrl = this.GetNextControl(ctrl, true);
        }
        ctrl.Focus();
    }
}
```

限制輸入數字+英文並轉大寫
---
```csharp=
private void txt_KeyPress(object sender, KeyPressEventArgs e)
{
    if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) &&
        ((int)e.KeyChar < 65 || (int)e.KeyChar > 90) &&
        ((int)e.KeyChar < 97 || (int)e.KeyChar > 122) &&
        (int)e.KeyChar != 8 &&
        (int)e.KeyChar != 1 && (int)e.KeyChar != 3 && (int)e.KeyChar != 22)
    {
        e.Handled = true;
    }
    if ((int)e.KeyChar >= 97 | (int)e.KeyChar <= 122)
    {
        e.KeyChar = Char.ToUpper(e.KeyChar);
    }
}
```

限制輸入數字事件
---
```csharp=
private void txt_KeyPress(object sender, KeyPressEventArgs e)
{
    if (((int)e.KeyChar < 48 || (int)e.KeyChar > 57) && (int)e.KeyChar != 8 &&
        (int)e.KeyChar != 1 && (int)e.KeyChar != 3 && (int)e.KeyChar != 22)
    {
        e.Handled = true;
    }
}
```

覆寫按鍵Enter轉Tab
---
```csharp=
/// <summary>覆寫[ProcessCmdKey]之事件</summary>
/// <param name="msg"></param>
/// <param name="keyData"></param>
/// <returns></returns>
protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
{
    if (keyData == Keys.Enter && !btn.Focused)
    {
        SendKeys.Send("{TAB}");
        return true;
    }
    return base.ProcessCmdKey(ref msg, keyData);
}
```

---

時間處理
===

日期加天數
---
```csharp=
string DATE = "1061010";

string new_date = Convert.ToString(Convert.ToInt32(DATE) + 19110000);
IFormatProvider culture = new System.Globalization.CultureInfo("zh-TW", true);
DateTime NewDate = DateTime.ParseExact(new_date, "yyyyMMdd", culture);
string DATE_ADD90 = Convert.ToString(Convert.ToInt32(NewDate.AddDays(89).ToString("yyyyMMdd")) - 19110000);
```

時間擴充
---
```csharp=
public static class DateTimeEx
{
    public static string ToStringTWNDate(this DateTime dt) //傳回1061020
    {
        return Convert.ToString(int.Parse(dt.ToString("yyyyMMdd")) - 19110000);
    }

    public static string ToStringDate(this DateTime dt) //傳回1061020
    {
        return dt.ToString("yyyy/MM/dd HH:mm:ss");
    }
}
```