using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KK.SARIcon
{
    public class Common
    {

        public static String DefaultXmlPath
        {
            get
            {
                String xmlFileName = "DesktopIconLocation.xml";
                return ConfigFolder + xmlFileName;
            }
        }

        public static String AppRoot
        {
            get
            {
                String appRootPath = AppDomain.CurrentDomain.BaseDirectory;
                if (!appRootPath.EndsWith("\\"))
                {
                    appRootPath += "\\";
                }
                return appRootPath;
            }
        }

        /// <summary>
        /// 配置文件存储位置
        /// </summary>
        public static String ConfigFolder
        {
            get { 
                String folderName = $"{System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\DHD\\DHDSRDesktopIconPosition\\";
                if (!System.IO.Directory.Exists(folderName))
                { 
                    System.IO.Directory.CreateDirectory(folderName);
                }
                return folderName;
            }
        }

    }
}
