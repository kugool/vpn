using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VPNResourceManage
{
    
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            LoginForm lgf = new LoginForm();
            if(lgf.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            ManageForm manageForm = new ManageForm();
            VPNResource vpnSource = new VPNResource();

            ManageControler managerControler = new ManageControler(manageForm, vpnSource);
            manageForm.Controler = managerControler;

            Application.Run(manageForm);
        }


    }
}
