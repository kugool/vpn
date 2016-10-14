using DotRas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNResourceManage
{
    class PhoneBookManager
    {
        public   const String PPTP_VPN = "(PPTP_VPN)";
        public   const String L2TP_VPN = "(L2TP_VPN)";

        private RasPhoneBook allUsersPhoneBook;

        public PhoneBookManager(String phoneBookPath)
        {
            allUsersPhoneBook = new RasPhoneBook();
            //String defaultPhoneBookPath = RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers);
            //allUsersPhoneBook.Open(RasPhoneBook.GetPhoneBookPath(RasPhoneBookType.AllUsers));

            allUsersPhoneBook.Open(phoneBookPath);
        }

        public RasPhoneBook CurrentRasPhoneBook
        {
            get
            {
                return this.allUsersPhoneBook;
            }
        }
        
        public void AddPPTPVpnEntity(String pptpVPNName, String address)
        {

            if(!allUsersPhoneBook.Entries.Contains(pptpVPNName))
            {
                RasEntry entry = RasEntry.CreateVpnEntry(pptpVPNName, address, RasVpnStrategy.PptpOnly, RasDevice.Create(pptpVPNName, RasDeviceType.Vpn));

                allUsersPhoneBook.Entries.Add(entry);
            }

            allUsersPhoneBook.Entries[pptpVPNName].PhoneNumber = address;
            allUsersPhoneBook.Entries[pptpVPNName].Update();

        }


        public void AddL2TPVpnEntity(String l2tpVPNName, String address, String presharedKey)
        {

            if(!allUsersPhoneBook.Entries.Contains(l2tpVPNName))
            {
                RasEntry entry = RasEntry.CreateVpnEntry(l2tpVPNName, address, RasVpnStrategy.L2tpOnly, RasDevice.Create(l2tpVPNName, RasDeviceType.Vpn));
                allUsersPhoneBook.Entries.Add(entry);
            }
            allUsersPhoneBook.Entries[l2tpVPNName].PhoneNumber = address;
            allUsersPhoneBook.Entries[l2tpVPNName].Options.UsePreSharedKey = true;
            allUsersPhoneBook.Entries[l2tpVPNName].UpdateCredentials(RasPreSharedKey.Client, presharedKey);
            allUsersPhoneBook.Entries[l2tpVPNName].Update();

        }


        public void RemoveEntity(String vpnName)
        {
            //ReadOnlyCollection<RasConnection> conList = RasConnection.GetActiveConnections();
            //foreach (RasConnection con in conList)
            //{
            //    con.HangUp();
            //}

            if(allUsersPhoneBook.Entries.Contains(vpnName))
            {
                this.allUsersPhoneBook.Entries.Remove(vpnName);
            }
        }
    }
}
