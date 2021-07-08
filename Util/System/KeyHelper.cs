using System.Windows.Forms;

namespace Util
{
    public static class KeyHelper
    {
        /// <summary>
        /// 解析KEY.
        /// </summary>
        public static string ParseKeyCode(bool bShift, Keys keyCode, bool isJugleCap = false)
        {
            string keyName = "";
            if (keyCode >= Keys.A && keyCode <= Keys.Z)
            {
                keyName = keyCode.ToString();
            }
            else if (keyCode >= Keys.D0 && keyCode <= Keys.D9)
            {
                if (bShift)
                {
                    switch (keyCode)
                    {
                        case Keys.D0:
                            keyName = ")";
                            break;
                        case Keys.D1:
                            keyName = "!";
                            break;
                        case Keys.D2:
                            keyName = "@";
                            break;
                        case Keys.D3:
                            keyName = "#";
                            break;
                        case Keys.D4:
                            keyName = "$";
                            break;
                        case Keys.D5:
                            keyName = "%";
                            break;
                        case Keys.D6:
                            keyName = "^";
                            break;
                        case Keys.D7:
                            keyName = "&";
                            break;
                        case Keys.D8:
                            keyName = "*";
                            break;
                        case Keys.D9:
                            keyName = "(";
                            break;
                    }
                }
                else
                {
                    keyName = keyCode.ToString();
                    keyName = keyName.Substring(1, 1);
                }
            }
            else if (keyCode >= Keys.NumPad0 && keyCode <= Keys.NumPad9)
            {
                keyName = keyCode.ToString();
                keyName = keyName.Substring(6, 1);
            }
            else
            {
                switch (keyCode)
                {
                    case Keys.Space:
                        keyName = " ";
                        break;
                    case Keys.Add:
                        keyName = "+";
                        break;
                    case Keys.Subtract:
                        keyName = "-";
                        break;
                    case Keys.Divide:
                        keyName = "/";
                        break;
                    case Keys.Multiply:
                        keyName = "*";
                        break;
                    case Keys.Decimal:
                        keyName = ".";
                        break;
                    case Keys.Oemtilde:
                        keyName = "`";
                        break;
                    case Keys.OemMinus:
                        if (bShift)
                            keyName = "_";
                        else
                            keyName = "-";
                        break;
                    case Keys.Oemplus:
                        if (bShift)
                            keyName = "+";
                        else
                            keyName = "=";
                        break;
                    case Keys.Oem6:
                        if (bShift)
                            keyName = "}";
                        else
                            keyName = "]";
                        break;
                    case Keys.OemOpenBrackets:
                        if (bShift)
                            keyName = "{";
                        else
                            keyName = "[";
                        break;
                    case Keys.Oem1:
                        if (bShift)
                            keyName = ":";
                        else
                            keyName = ";";
                        break;
                    case Keys.Oem7:
                        if (bShift)
                            keyName = "\"";
                        else
                            keyName = "'";
                        break;
                    case Keys.Oem5:
                        if (bShift)
                            keyName = "|";
                        else
                            keyName = "\\";
                        break;
                    case Keys.Oemcomma:
                        if (bShift)
                            keyName = "<";
                        else
                            keyName = ",";
                        break;
                    case Keys.OemPeriod:
                        if (bShift)
                            keyName = ">";
                        else
                            keyName = ".";
                        break;
                    case Keys.OemQuestion:
                        if (bShift)
                            keyName = "?";
                        else
                            keyName = "/";
                        break;
                    case Keys.Enter:
                        keyName = "enter";
                        break;
                    case Keys.Home:
                    case Keys.End:
                    case Keys.Insert:
                    case Keys.Delete:
                    case Keys.Tab:
                    case Keys.Up:
                    case Keys.Down:
                    case Keys.Left:
                    case Keys.Right:
                    case Keys.F1:
                    case Keys.F2:
                    case Keys.F3:
                    case Keys.F4:
                    case Keys.F5:
                    case Keys.F6:
                    case Keys.F7:
                    case Keys.F8:
                    case Keys.F9:
                    case Keys.F10:
                    case Keys.F11:
                    case Keys.F12:
                        keyName = keyCode.ToString().ToLower();
                        break;
                    default:
                        //keyName = keyCode.ToString();
                        break;
                }
            }

            if (isJugleCap)
            {
                return keyName;
            }
            if (bShift)
            {
                return keyName.ToUpper();
            }
            else
            {
                return keyName.ToLower();
            }
        }
    }
}
