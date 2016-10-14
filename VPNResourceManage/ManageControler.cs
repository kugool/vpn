using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNResourceManage
{
    class ManageControler
    {
        private VPNResource vpnResource;
        private ManageForm view;
        private VPNDialer vpnDialer;
        private PhoneBookManager phoneBookManager;

        private String currentResourceFile;

        public String CurrentResourceFile
        {
            get { return currentResourceFile; }
            set { currentResourceFile = value; }
        }

        public ManageControler(ManageForm view, VPNResource vpnResource)
        {
            this.view = view;
            this.vpnResource = vpnResource;
            
            this.phoneBookManager = new PhoneBookManager(Global.phoneBookPath);

            this.vpnDialer = new VPNDialer(this.phoneBookManager.CurrentRasPhoneBook);
            this.vpnDialer.SetStateChangedEventHandler(view.Dialer_StateChanged);
            this.vpnDialer.SetDialCompletedEventHandler(view.Dialer_DialCompleted);
            this.vpnDialer.SetDialErrorEventHandler(view.Dialer_Error);

        }

        public bool LoadVPNResourceFromFile(String dataFilePath)
        {
            if(vpnResource.LoadFromFile(dataFilePath))
            {
                this.CurrentResourceFile = dataFilePath;
                return true;
            }
            else
            {
                this.currentResourceFile = "";
                return false;
            }

        }

        public void RefreshVPNListToView()
        {
            view.UpdateSourceList(vpnResource.VpnList);
        }


        public void SaveVPNSourceToFile()
        {
            vpnResource.SaveToFile(this.CurrentResourceFile);
        }

        public void SaveVPNSourceToFile(String dataFilePath)
        {

            vpnResource.SaveToFile(dataFilePath);

        }

        public void AddVpnEntityToSource(VPNEntity entity)
        {
            vpnResource.AddVPN(entity);
        }

        public void DelVpnEntityFromSource(VPNEntity entity)
        {
            vpnResource.RemoveVPN(entity);
        }

        public void ModifyVpnEntityInSource(VPNEntity oEntity, VPNEntity mEntity)
        {
            vpnResource.ModifyVPN(oEntity, mEntity);
        }


        public void DialVPN_PPTP(String ip, String user, String password)
        {
            phoneBookManager.AddPPTPVpnEntity(PhoneBookManager.PPTP_VPN, ip);
            if(vpnDialer.LinkToVPNEntity(PhoneBookManager.PPTP_VPN))
            {
                vpnDialer.SetVPNUserCredential(user, password);
                vpnDialer.AsyncDialVPN();
            }
            
        }

        public void DialVPN_L2TP(String ip, String user, String password, String key)
        {
            phoneBookManager.AddL2TPVpnEntity(PhoneBookManager.L2TP_VPN, ip, key);
            if (vpnDialer.LinkToVPNEntity(PhoneBookManager.L2TP_VPN))
            {
                vpnDialer.SetVPNUserCredential(user, password);
                vpnDialer.AsyncDialVPN();
            }

        }

        public void DisconnectVPN()
        {
            vpnDialer.DisconnectVPN();
            ClearVPNEntityFromPhoneBook();

        }

        public void AbortDialing()
        {
            vpnDialer.DisconnectVPN();
            ClearVPNEntityFromPhoneBook();
        }

        public void ClearVPNEntityFromPhoneBook()
        {
            if(phoneBookManager != null)
            {
            
                this.phoneBookManager.RemoveEntity(PhoneBookManager.L2TP_VPN);
                this.phoneBookManager.RemoveEntity(PhoneBookManager.PPTP_VPN);

            }
        }


    }
}
