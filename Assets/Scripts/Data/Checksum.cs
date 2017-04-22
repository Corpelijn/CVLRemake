//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;

//namespace Assets.Scripts.Data
//{
//    class Checksum
//    {
//        #region "Fields"



//        #endregion

//        #region "Constructors"



//        #endregion

//        #region "Properties"



//        #endregion

//        #region "Methods"



//        #endregion

//        #region "Abstract/Virtual Methods"



//        #endregion

//        #region "Inherited Methods"



//        #endregion

//        #region "Static Methods"

//        private static string GetChecksum(string file)
//        {
//            using (FileStream stream = File.OpenRead(file))
//            {
//                var sha = new SHA256Managed();
//                byte[] checksum = sha.ComputeHash(stream);
//                return BitConverter.ToString(checksum).Replace("-", string.Empty);
//            }
//        }

//        #endregion

//        #region "Operators"



//        #endregion
//    }
//}
