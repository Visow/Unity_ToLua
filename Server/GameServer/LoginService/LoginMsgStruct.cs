using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public static class LOGIN_DEFINE {
    public const int VAR_CHAR_LENGHT_32 = 32;
    public const int VAR_CHAR_LENGHT_255 = 255;

 

}

public class RES_Common {
    public int errcode { get; set; }

    public string errmsg { get; set; }
}

public class RES_LoginSuc : RES_Common {
    public string token { get; set; }

    public DateTime tokenstamp { get; set; }
}

