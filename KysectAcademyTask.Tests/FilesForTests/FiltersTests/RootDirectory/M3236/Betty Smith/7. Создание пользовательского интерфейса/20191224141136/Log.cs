using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


    class Log
    {
    private static Log instance;
    public StringBuilder sb = new StringBuilder();

    private Log()
    { }

    public static Log getInstance()
    {
        if (instance == null)
            instance = new Log();
        return instance;
    }


}
