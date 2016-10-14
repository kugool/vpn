using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VPNResourceManage
{
    public class VPNEntity
    {
        private String connectionAddr;
        public String ConnectionAddr
        {
            get { return connectionAddr; }
            set { connectionAddr = value; }
        }

        private String accountName;
        public String AccountName
        {
            get { return accountName; }
            set { accountName = value; }
        }

        private String accountPassword;
        public String AccountPassword
        {
            get { return accountPassword; }
            set { accountPassword = value; }
        }

        private String dialType;

        public String DialType
        {
            get { return dialType; }
            set { dialType = value; }
        }
        private String key;

        public String Key
        {
            get { return key; }
            set { key = value; }
        }
        private String supplementary;

        public String Supplementary
        {
            get { return supplementary; }
            set { supplementary = value; }
        }


        public override int GetHashCode()
        {
            return (connectionAddr + accountName + accountPassword + dialType + key + supplementary).GetHashCode();
        }

        public override bool Equals(object obj)
        {
            if(!(obj is VPNEntity))
            {
                return false;
            }

            VPNEntity entity = obj as VPNEntity;

            if(entity.ConnectionAddr.Equals(this.connectionAddr) &&
                entity.AccountName.Equals(this.accountName) &&
                entity.AccountPassword.Equals(this.accountPassword) &&
                entity.DialType.Equals(this.dialType) &&
                entity.Key.Equals(this.key) &&
                entity.Supplementary.Equals(this.supplementary))
                
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
