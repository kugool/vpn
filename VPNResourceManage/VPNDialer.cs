using DotRas;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VPNResourceManage
{
    class VPNDialer
    {

        private RasPhoneBook allUsersPhoneBook;
        private RasDialer dialer;

        private RasHandle handle = null;

        public VPNDialer(RasPhoneBook phoneBook)
        {
            this.dialer = new RasDialer();
            this.allUsersPhoneBook = phoneBook;

        }



        public void SetStateChangedEventHandler(EventHandler<DotRas.StateChangedEventArgs> func)
        {
            this.dialer.StateChanged += func;
        }

        public void SetDialCompletedEventHandler(EventHandler<DotRas.DialCompletedEventArgs> func)
        {
            this.dialer.DialCompleted += func;
        }

        public void SetDialErrorEventHandler(EventHandler<ErrorEventArgs> func)
        {
            this.dialer.Error += func;
        }


        public void SetVPNUserCredential(String user, String password)
        {
            dialer.Credentials = new NetworkCredential(user, password);
        }

        public bool LinkToVPNEntity(String vpnName)
        {


            if(allUsersPhoneBook.Entries.Contains(vpnName))
            {
                dialer.EntryName = vpnName;
                dialer.PhoneBookPath = allUsersPhoneBook.Path;
                return true;
            }
            else
            {
                return false;
            }

        }


        public void AsyncDialVPN()
        {
            this.handle = this.dialer.DialAsync();
        }


        public void DisconnectVPN()
        {
            if (this.dialer.IsBusy)
            {
                this.dialer.DialAsyncCancel();
            }
            else
            {

                ReadOnlyCollection<RasConnection> conList = RasConnection.GetActiveConnections();
                foreach (RasConnection con in conList)
                {
                    if(con.EntryName.Equals(PhoneBookManager.L2TP_VPN) || con.EntryName.Equals(PhoneBookManager.PPTP_VPN))
                    {
                        con.HangUp();
                    }
                }
            }
        }

        public void AsyncCancelDial()
        {
            this.dialer.DialAsyncCancel();
        }



    }
}
