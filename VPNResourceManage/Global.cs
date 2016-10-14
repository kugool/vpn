using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNResourceManage
{
    class Global
    {
        public static String startPath = System.Windows.Forms.Application.StartupPath;

        public static String hashFilePath = startPath + @"\account";
        public static String phoneBookPath = startPath + @"\ras.pbk";
        public static String defaultVPNResourceFile = startPath + @"\default";
        public static String loginFormBackgroundPicture = startPath + @"\ico\login.jpg";
        public static String mainFormBackgroundPicture = startPath + @"\ico\main.jpg";
        public static String largeIconPicture = startPath + @"\ico\vpn.png";
        public static String smallIconPicture = startPath + @"\ico\vpn.png";
        public static String notifyIconPicture = startPath + @"\ico\vpn_notify.ico";
    }
}
