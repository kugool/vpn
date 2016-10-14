using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VPNResourceManage
{
    class VPNResource
    {

        private List<VPNEntity> vpnList;

        public List<VPNEntity> VpnList
        {
            get { return vpnList; }
        }

        public VPNResource()
        {
            vpnList = new List<VPNEntity>();

        }

        public bool LoadFromFile(String filePath)
        {

            XmlDocument xmlDoc = new XmlDocument();
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            XmlReader reader = null;

            try
            {
                reader = XmlReader.Create(filePath, settings);
                xmlDoc.Load(reader);

                XmlNode root = xmlDoc.SelectSingleNode("Source");
                XmlNodeList sourceList = root.ChildNodes;

                vpnList.Clear();

                foreach (XmlNode xn in sourceList)
                {
                    VPNEntity entity = new VPNEntity();

                    XmlElement vpnEntity = (XmlElement)xn;
                    XmlNodeList vpnAttrs = vpnEntity.ChildNodes;
                    entity.ConnectionAddr = vpnAttrs.Item(0).InnerText;
                    entity.AccountName = vpnAttrs.Item(1).InnerText;
                    entity.AccountPassword = vpnAttrs.Item(2).InnerText;
                    entity.DialType = vpnAttrs.Item(3).InnerText;
                    entity.Key = vpnAttrs.Item(4).InnerText;
                    entity.Supplementary = vpnAttrs.Item(5).InnerText;

                    vpnList.Add(entity);
                }
            }
            catch(Exception e)
            {
                return false;
            }
            finally
            {
                if(reader != null)
                {
                
                    reader.Close();

                }
            }

            return true;
        }

        public bool SaveToFile(String filePath)
        {

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml("<Source></Source>");

            XmlNode root = xmlDoc.SelectSingleNode("Source");

            foreach(VPNEntity entity in vpnList)
            {
                XmlElement xmlVpnEntity = xmlDoc.CreateElement("VPNItem");

                XmlElement xmlAddr = xmlDoc.CreateElement("Addr");
                xmlAddr.InnerText = entity.ConnectionAddr;
                XmlElement xmlUser = xmlDoc.CreateElement("User");
                xmlUser.InnerText = entity.AccountName;
                XmlElement xmlPass = xmlDoc.CreateElement("Pass");
                xmlPass.InnerText = entity.AccountPassword;
                XmlElement xmlDialType = xmlDoc.CreateElement("DialType");
                xmlDialType.InnerText = entity.DialType;
                XmlElement xmlKey = xmlDoc.CreateElement("Key");
                xmlKey.InnerText = entity.Key;
                XmlElement xmlSupplementary = xmlDoc.CreateElement("Supplementary");
                xmlSupplementary.InnerText = entity.Supplementary;

                xmlVpnEntity.AppendChild(xmlAddr);
                xmlVpnEntity.AppendChild(xmlUser);
                xmlVpnEntity.AppendChild(xmlPass);
                xmlVpnEntity.AppendChild(xmlDialType);
                xmlVpnEntity.AppendChild(xmlKey);
                xmlVpnEntity.AppendChild(xmlSupplementary);

                root.AppendChild(xmlVpnEntity);
            }

            xmlDoc.Save(filePath);

            return false;
        }

        public void RemoveVPN(VPNEntity entity)
        {
            if(vpnList.Contains(entity))
            {
                vpnList.Remove(entity);
            }

        }

        public void AddVPN(VPNEntity entity)
        {
            if (!vpnList.Contains(entity))
            {
                vpnList.Add(entity);
            }

        }

        public void ModifyVPN(VPNEntity oEntity, VPNEntity mEntity)
        {
            int index = vpnList.IndexOf(oEntity);
            if(index != -1)
            {
                vpnList[index] = mEntity;
            }
        }

        public void Clear()
        {
            vpnList.Clear();
        }


        public int GetSourceSize()
        {
            return vpnList.Count;
        }


    }
}
