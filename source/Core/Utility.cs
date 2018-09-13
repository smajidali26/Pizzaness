using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;
using System.Text;
using System.Data;
using System.ComponentModel;
using System.Reflection;
using System.Web;
using System.Web.UI.WebControls;
using System.IO;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Security;

namespace Core
{
    public static class Utility
    {
        #region Private Members

        private static byte[] _keyByte = { };
        //Default Key
        private static string _key = "voicekey";
        //Default initial vector
        private static byte[] _ivByte = { 0x01, 0x12, 0x23, 0x34, 0x45, 0x56, 0x67, 0x78 };

        private static readonly Random _rng = new Random();

        private static string _chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";

        #endregion

        #region Private Methods

        #endregion

        #region Static Methods


        public static string CollectionXml<T>(this ICollection<T> list,string DatasetName, string DataTableName)
        {

            Type elementType = typeof(T);
            DataSet ds = new DataSet();
            DataTable t = new DataTable();
            if (list != null)
            {
                if (list.Count > 0)
                {
                    ds.DataSetName = DatasetName;
                    t.TableName = DataTableName;
                    ds.Tables.Add(t);

                    //add a column to table for each public property on T
                    foreach (var propInfo in elementType.GetProperties())
                    {
                        Type ColType = Nullable.GetUnderlyingType(propInfo.PropertyType) ?? propInfo.PropertyType;

                        t.Columns.Add(propInfo.Name, ColType);
                    }

                    //go through each property on T and add each value to the table
                    foreach (T item in list)
                    {
                        DataRow row = t.NewRow();

                        foreach (var propInfo in elementType.GetProperties())
                        {
                            row[propInfo.Name] = propInfo.GetValue(item, null) ?? DBNull.Value;
                        }

                        t.Rows.Add(row);
                    }
                }
            }
            return ds.GetXml();
        }

        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue(obj, value, null);
                    }
                    catch
                    {
                        // You can log something here
                        throw;
                    }
                }
            }

            return obj;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }

        ///http://www.codeproject.com/Articles/10429/Convert-XML-data-to-object-and-back-using-serializ
        /// <summary>
        /// Convert Entity to Xmll
        /// </summary>
        /// <typeparam name="T">DataType</typeparam>
        /// <param name="entity">Object</param>
        /// <returns></returns>
        public static string EntityToXml<T>(T entity)
        {
            MemoryStream stream = null;
            TextWriter writer = null;
            try
            {
                stream = new MemoryStream(); // read xml in memory
                writer = new StreamWriter(stream, Encoding.UTF8);
                // get serialise object
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                serializer.Serialize(writer, entity); // read object
                int count = (int)stream.Length; // saves object in memory stream
                byte[] arr = new byte[count];
                stream.Seek(0, SeekOrigin.Begin);
                // copy stream contents in byte array
                stream.Read(arr, 0, count);
                UTF8Encoding utf = new UTF8Encoding(); // convert byte array to string
                return utf.GetString(arr).Trim().Replace("\n", "").Replace("\r", ""); ;
            }
            catch
            {
                return string.Empty;
            }
            finally
            {
                if (stream != null) stream.Close();
                if (writer != null) writer.Close();
            }
        }

        /// <summary>
        /// Converts xml to Class
        /// </summary>
        /// <typeparam name="T">DataType</typeparam>
        /// <param name="xml">xml</param>
        /// <returns></returns>
        private static T XmlToEntity<T>(string xml)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                // serialise to object
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                stream = new StringReader(xml); // read xml data
                reader = new XmlTextReader(stream); // create reader
                // covert reader to object
                return (T)serializer.Deserialize(reader);
            }
            catch
            {
                return default(T);
            }
            finally
            {
                if (stream != null) stream.Close();
                if (reader != null) reader.Close();
            }
        }

        /// <summary> 
        /// Converts the XML to a list of T
        /// </summary> 
        /// <typeparam name="T">Type to return</typeparam> 
        /// <param name="xml">XML string to convert</param> 
        /// <param name="nodePath">XML Node path to select <example>//People/Person</example></param> 
        /// <returns></returns> 
        public static List<T> XmlToObjectList<T>(string xml, string nodePath)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);

            var returnItemsList = new List<T>();

            foreach (XmlNode xmlNode in xmlDocument.SelectNodes(nodePath))
            {
                returnItemsList.Add(XmlToObject<T>(xmlNode.OuterXml));
            }

            return returnItemsList;
        }

        /// <summary> 
        /// Converts a single XML tree to the type of T 
        /// </summary> 
        /// <typeparam name="T">Type to return</typeparam> 
        /// <param name="xml">XML string to convert</param> 
        /// <returns></returns> 
        public static T XmlToObject<T>(string xml)
        {
            using (var xmlStream = new StringReader(xml))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(XmlReader.Create(xmlStream));
            }
        }

        /// <summary>
        /// Method check textbox text. If text is empty then return true otherwise false.
        /// </summary>
        /// <param name="textBox"></param>
        /// <returns></returns>
        public static bool CheckTextBoxValueEmpty(TextBox textBox)
        {
            if (string.IsNullOrEmpty(textBox.Text.Trim()))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// File name with Path.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadFile(String fileName)
        {
            string fileContent = string.Empty;
            StreamReader sr = null;
            try
            {
                sr = new StreamReader(fileName);
                fileContent = sr.ReadToEnd();
            }
            finally
            {
                if (sr != null)
                {
                    sr.Close();
                }
            }
            return fileContent;
        }
        #endregion

        /// <summary>
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, a random salt
        /// is generated and appended to the plain text. This salt is stored at
        /// the end of the hash value, so it can be used later for hash
        /// verification.
        /// </summary>
        /// <param name="plainText">
        /// Plaintext value to be hashed. The function does not check whether
        /// this parameter is null.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Name of the hash algorithm. Allowed values are: "MD5", "SHA1",
        /// "SHA256", "SHA384", and "SHA512" (if any other value is specified
        /// MD5 hashing algorithm will be used). This value is case-insensitive.
        /// </param>
        /// <param name="saltBytes">
        /// Salt bytes. This parameter can be null, in which case a random salt
        /// value will be generated.
        /// </param>
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        public static string ComputeHash(string plainText,
                                         string hashAlgorithm,
                                         byte[] saltBytes)
        {
            // If salt is not specified, generate it on the fly.
            if (saltBytes == null)
            {
                // Define min and max salt sizes.
                int minSaltSize = 4;
                int maxSaltSize = 8;

                // Generate a random number for the size of the salt.
                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                // Allocate a byte array, which will hold the salt.
                saltBytes = new byte[saltSize];

                // Initialize a random number generator.
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                // Fill the salt with cryptographically strong byte values.
                rng.GetNonZeroBytes(saltBytes);
            }

            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
                    new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            // Because we support multiple hashing algorithms, we must define
            // hash object as a common (abstract) base class. We will specify the
            // actual hashing algorithm class later during object creation.
            HashAlgorithm hash;

            // Make sure hashing algorithm name is specified.
            if (hashAlgorithm == null)
                hashAlgorithm = "";

            // Initialize appropriate hashing algorithm class.
            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hash = new SHA1Managed();
                    break;

                case "SHA256":
                    hash = new SHA256Managed();
                    break;

                case "SHA384":
                    hash = new SHA384Managed();
                    break;

                case "SHA512":
                    hash = new SHA512Managed();
                    break;

                default:
                    hash = new MD5CryptoServiceProvider();
                    break;
            }

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
                                                saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        /// <summary>
        /// Compares a hash of the specified plain text value to a given hash
        /// value. Plain text is hashed with the same salt value as the original
        /// hash.
        /// </summary>
        /// <param name="plainText">
        /// Plain text to be verified against the specified hash. The function
        /// does not check whether this parameter is null.
        /// </param>
        /// <param name="hashAlgorithm">
        /// Name of the hash algorithm. Allowed values are: "MD5", "SHA1", 
        /// "SHA256", "SHA384", and "SHA512" (if any other value is specified,
        /// MD5 hashing algorithm will be used). This value is case-insensitive.
        /// </param>
        /// <param name="hashValue">
        /// Base64-encoded hash value produced by ComputeHash function. This value
        /// includes the original salt appended to it.
        /// </param>
        /// <returns>
        /// If computed hash mathes the specified hash the function the return
        /// value is true; otherwise, the function returns false.
        /// </returns>
        public static bool VerifyHash(string plainText,
                                      string hashAlgorithm,
                                      string hashValue)
        {
            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            // We must know size of hash (without salt).
            int hashSizeInBits, hashSizeInBytes;

            // Make sure that hashing algorithm name is specified.
            if (hashAlgorithm == null)
                hashAlgorithm = "";

            // Size of hash is based on the specified algorithm.
            switch (hashAlgorithm.ToUpper())
            {
                case "SHA1":
                    hashSizeInBits = 160;
                    break;

                case "SHA256":
                    hashSizeInBits = 256;
                    break;

                case "SHA384":
                    hashSizeInBits = 384;
                    break;

                case "SHA512":
                    hashSizeInBits = 512;
                    break;

                default: // Must be MD5
                    hashSizeInBits = 128;
                    break;
            }

            // Convert size of hash from bits to bytes.
            hashSizeInBytes = hashSizeInBits / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[hashWithSaltBytes.Length -
                                        hashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            // Compute a new hash string.
            string expectedHashString =
                        ComputeHash(plainText, hashAlgorithm, saltBytes);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            return (hashValue == expectedHashString);
        }

        #region Public Methods

        #region Encryption/Decryption

        /// <summary> 
        /// Encrypt text 
        /// </summary> 
        /// <param name="value">plain text</param> 
        /// <returns>encrypted text</returns> 
        public static string Encrypt(string value)
        {
            return Encrypt(value, string.Empty);
        }

        /// <summary> 
        /// Encrypt text by key 
        /// </summary> 
        /// <param name="value">plain text</param> 
        /// <param name="key"> string key</param> 
        /// <returns>encrypted text</returns> 
        public static string Encrypt(string value, string key)
        {
            return Encrypt(value, key, string.Empty);
        }

        /// <summary> 
        /// Encrypt text by key with initialization vector 
        /// </summary> 
        /// <param name="value">plain text</param> 
        /// <param name="key"> string key</param> 
        /// <param name="iv">initialization vector</param> 
        /// <returns>encrypted text</returns> 
        public static string Encrypt(string value, string key, string iv)
        {
            string encryptValue = string.Empty;
            MemoryStream ms = null;
            CryptoStream cs = null;
            if (!string.IsNullOrEmpty(value))
            {
                try
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        _keyByte = Encoding.UTF8.GetBytes
                                (key.Substring(0, 8));
                        if (!string.IsNullOrEmpty(iv))
                        {
                            _ivByte = Encoding.UTF8.GetBytes
                                (iv.Substring(0, 8));
                        }
                    }
                    else
                    {
                        _keyByte = Encoding.UTF8.GetBytes(_key);
                    }
                    using (DESCryptoServiceProvider des =
                            new DESCryptoServiceProvider())
                    {
                        byte[] inputByteArray =
                            Encoding.UTF8.GetBytes(value);
                        ms = new MemoryStream();
                        cs = new CryptoStream(ms, des.CreateEncryptor
                        (_keyByte, _ivByte), CryptoStreamMode.Write);
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        encryptValue = Convert.ToBase64String(ms.ToArray());
                    }
                }
                catch
                {
                    //TODO: write log 
                }
                finally
                {
                    cs.Dispose();
                    ms.Dispose();
                }
            }
            return encryptValue;
        }

        /// <summary> 
        /// Decrypt text 
        /// </summary> 
        /// <param name="value">encrypted text</param> 
        /// <returns>plain text</returns> 
        public static string Decrypt(string value)
        {
            return Decrypt(value, string.Empty);
        }

        /// <summary> 
        /// Decrypt text by key 
        /// </summary> 
        /// <param name="value">encrypted text</param> 
        /// <param name="key">string key</param> 
        /// <returns>plain text</returns> 
        public static string Decrypt(string value, string key)
        {
            return Decrypt(value, key, string.Empty);
        }

        /// <summary> 
        /// Decrypt text by key with initialization vector 
        /// </summary> 
        /// <param name="value">encrypted text</param> 
        /// <param name="key"> string key</param> 
        /// <param name="iv">initialization vector</param> 
        /// <returns>encrypted text</returns> 
        public static string Decrypt(string value, string key, string iv)
        {
            string decrptValue = string.Empty;
            if (!string.IsNullOrEmpty(value))
            {
                MemoryStream ms = null;
                CryptoStream cs = null;
                value = value.Replace(" ", "+");
                byte[] inputByteArray = new byte[value.Length];
                try
                {
                    if (!string.IsNullOrEmpty(key))
                    {
                        _keyByte = Encoding.UTF8.GetBytes
                                (key.Substring(0, 8));
                        if (!string.IsNullOrEmpty(iv))
                        {
                            _ivByte = Encoding.UTF8.GetBytes
                                (iv.Substring(0, 8));
                        }
                    }
                    else
                    {
                        _keyByte = Encoding.UTF8.GetBytes(_key);
                    }
                    using (DESCryptoServiceProvider des =
                            new DESCryptoServiceProvider())
                    {
                        inputByteArray = Convert.FromBase64String(value);
                        ms = new MemoryStream();
                        cs = new CryptoStream(ms, des.CreateDecryptor
                        (_keyByte, _ivByte), CryptoStreamMode.Write);
                        cs.Write(inputByteArray, 0, inputByteArray.Length);
                        cs.FlushFinalBlock();
                        Encoding encoding = Encoding.UTF8;
                        decrptValue = encoding.GetString(ms.ToArray());
                    }
                }
                catch
                {
                    //TODO: write log 
                }
                finally
                {
                    cs.Dispose();
                    ms.Dispose();
                }
            }
            return decrptValue;
        }

        /// <summary> 
        /// Compute Hash without salt
        /// </summary> 
        /// <param name="plainText">plain text</param> 
        /// <param name="hashName">Hash Name</param> 
        /// <returns>string</returns> 
        public static string ComputeHash(string plainText, HashName hashName)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                // Convert plain text into a byte array. 
                byte[] plainTextBytes = ASCIIEncoding.ASCII.GetBytes(plainText);
                // Allocate array, which will hold plain text and salt. 
                                
                HashAlgorithm hash = null;
                switch (hashName)
                {
                    case HashName.SHA1:
                        hash = new SHA1Managed();
                        break;
                    case HashName.SHA256:
                        hash = new SHA256Managed();
                        break;
                    case HashName.SHA384:
                        hash = new SHA384Managed();
                        break;
                    case HashName.SHA512:
                        hash = new SHA512Managed();
                        break;
                    case HashName.MD5:
                        hash = new MD5CryptoServiceProvider();
                        break;
                }
                // Compute hash value of our plain text with appended salt. 
                byte[] hashBytes = hash.ComputeHash(plainTextBytes);
                
                // Convert result into a base64-encoded string. 
                string hashValue = Convert.ToBase64String(hashBytes);
                // Return the result. 
                return hashValue;
            }
            return string.Empty;
        }

        /// <summary> 
        /// Compute Hash 
        /// </summary> 
        /// <param name="plainText">plain text</param> 
        /// <param name="salt">salt string</param> 
        /// <returns>string</returns> 
        public static string ComputeHash(string plainText, string salt)
        {
            return ComputeHash(plainText, salt, HashName.MD5);
        }

        /// <summary> 
        /// Compute Hash 
        /// </summary> 
        /// <param name="plainText">plain text</param> 
        /// <param name="salt">salt string</param> 
        /// <param name="hashName">Hash Name</param> 
        /// <returns>string</returns> 
        public static string ComputeHash(string plainText, string salt, HashName hashName)
        {
            if (!string.IsNullOrEmpty(plainText))
            {
                // Convert plain text into a byte array. 
                byte[] plainTextBytes = ASCIIEncoding.ASCII.GetBytes(plainText);
                // Allocate array, which will hold plain text and salt. 
                byte[] plainTextWithSaltBytes = null;
                byte[] saltBytes;
                if (!string.IsNullOrEmpty(salt))
                {
                    // Convert salt text into a byte array. 
                    saltBytes = ASCIIEncoding.ASCII.GetBytes(salt);
                    plainTextWithSaltBytes =
                        new byte[plainTextBytes.Length + saltBytes.Length];
                }
                else
                {
                    // Define min and max salt sizes. 
                    int minSaltSize = 4;
                    int maxSaltSize = 8;
                    // Generate a random number for the size of the salt. 
                    Random random = new Random();
                    int saltSize = random.Next(minSaltSize, maxSaltSize);
                    // Allocate a byte array, which will hold the salt. 
                    saltBytes = new byte[saltSize];
                    // Initialize a random number generator. 
                    RNGCryptoServiceProvider rngCryptoServiceProvider =
                                new RNGCryptoServiceProvider();
                    // Fill the salt with cryptographically strong byte values. 
                    rngCryptoServiceProvider.GetNonZeroBytes(saltBytes);
                    plainTextWithSaltBytes =
                        new byte[plainTextBytes.Length + saltBytes.Length];
                }
                // Copy plain text bytes into resulting array. 
                for (int i = 0; i < plainTextBytes.Length; i++)
                {
                    plainTextWithSaltBytes[i] = plainTextBytes[i];
                }
                // Append salt bytes to the resulting array. 
                for (int i = 0; i < saltBytes.Length; i++)
                {
                    plainTextWithSaltBytes[plainTextBytes.Length + i] =
                                        saltBytes[i];
                }
                HashAlgorithm hash = null;
                switch (hashName)
                {
                    case HashName.SHA1:
                        hash = new SHA1Managed();
                        break;
                    case HashName.SHA256:
                        hash = new SHA256Managed();
                        break;
                    case HashName.SHA384:
                        hash = new SHA384Managed();
                        break;
                    case HashName.SHA512:
                        hash = new SHA512Managed();
                        break;
                    case HashName.MD5:
                        hash = new MD5CryptoServiceProvider();
                        break;
                }
                // Compute hash value of our plain text with appended salt. 
                byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);
                // Create array which will hold hash and original salt bytes. 
                byte[] hashWithSaltBytes =
                    new byte[hashBytes.Length + saltBytes.Length];
                // Copy hash bytes into resulting array. 
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    hashWithSaltBytes[i] = hashBytes[i];
                }
                // Append salt bytes to the result. 
                for (int i = 0; i < saltBytes.Length; i++)
                {
                    hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];
                }
                // Convert result into a base64-encoded string. 
                string hashValue = Convert.ToBase64String(hashWithSaltBytes);
                // Return the result. 
                return hashValue;
            }
            return string.Empty;
        }

        /// <summary>
        /// Get Random String
        /// </summary>
        /// <param name="size">salt size</param>
        /// <returns>return random salt string</returns>
        public static string RandomString(int size)
        {
            char[] buffer = new char[size];

            for (int i = 0; i < size; i++)
            {
                buffer[i] = _chars[_rng.Next(_chars.Length)];
            }
            return new string(buffer);
        }
        
        /// <summary>
        /// RSA Decrypt Algorithm
        /// </summary>
        /// <param name="encryptedData">Encrypted string</param>
        /// <returns>Decrypted string</returns>
        public static string RsaDecrypt(string encryptedData)
        {
            ASCIIEncoding asciiEncoding = new ASCIIEncoding();

            byte[] data = Convert.FromBase64String(encryptedData);


            string fileName = HttpContext.Current.Server.MapPath("/privatekey.pem");
            AsymmetricCipherKeyPair keyPair;

            using (var reader = File.OpenText(fileName))
                keyPair = (AsymmetricCipherKeyPair)new PemReader(reader).ReadObject();

            RSAParameters RsaKey = DotNetUtilities.ToRSAParameters((RsaPrivateCrtKeyParameters) keyPair.Private);

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();

            rsa.ImportParameters(RsaKey);

            byte[] decipheredBytes = rsa.Decrypt(data, false);

            //AsymmetricKeyParameter key = keyPair.Private;

            //RsaEngine e = new RsaEngine();
            //e.Init(false, key);

            //byte[] decipheredBytes = e.ProcessBlock(data, 0, data.Length);

            string plainText = Encoding.UTF8.GetString(decipheredBytes);

            return plainText;

        }

        #endregion

        #region General Methods
        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="senderEmail">A valid email of sender</param>
        /// <param name="receiverEmail">A valid email recipient</param>
        /// <param name="subject">Subject of email</param>
        /// <param name="body">Body of email</param>
        /// <param name="isHtmlBody">A boolean value to identify that email body is Html or Plain Text</param>
        public static void SendEmail(string senderEmail,string receiverEmail,string subject,string body,bool isHtmlBody)
        {
            MailMessage mailMessage = new MailMessage();
            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"], Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]));
            mailMessage.From = new MailAddress(senderEmail);
            string[] emailArray = null;
            if (!String.IsNullOrEmpty(receiverEmail))
            {
                emailArray = receiverEmail.Split(new char[] { ';' });
                foreach (string email in emailArray)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        mailMessage.To.Add(email);
                    }
                }
            }

            smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["EmailAddress"], ConfigurationManager.AppSettings["Password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
            mailMessage.IsBodyHtml = isHtmlBody;
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            smtp.Send(mailMessage);
        }

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="senderEmail">A valid email of sender</param>
        /// <param name="receiverEmail">A valid email recipient</param>
        /// <param name="ccEmail">A valid semi colon(;) seprated emails</param>
        /// <param name="subject">Subject of email</param>
        /// <param name="body">Body of email</param>
        /// <param name="isHtmlBody">A boolean value to identify that email body is Html or Plain Text</param>
        public static void SendEmail(string senderEmail, string receiverEmail, string ccEmail,String bccEmail, string subject, string body, bool isHtmlBody)
        {
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(senderEmail);
            string[] ccArray = null;
            if (!String.IsNullOrEmpty(receiverEmail))
            {
                ccArray = receiverEmail.Split(new char[] { ';' });
                foreach (string email in ccArray)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        mailMessage.To.Add(email);
                    }
                }
            }

            if (!String.IsNullOrEmpty(ccEmail))
            {
                ccArray = ccEmail.Split(new char[] { ';' });
                foreach (string email in ccArray)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        mailMessage.CC.Add(email);
                    }
                }
            }

            if (!String.IsNullOrEmpty(bccEmail))
            {
                ccArray = bccEmail.Split(new char[] { ';' });
                foreach (string email in ccArray)
                {
                    if (!string.IsNullOrEmpty(email))
                    {
                        mailMessage.Bcc.Add(email);
                    }
                }
            }
            SmtpClient smtp = new SmtpClient(ConfigurationManager.AppSettings["SmtpServer"], Convert.ToInt32(ConfigurationManager.AppSettings["SmtpPort"]));
            smtp.Credentials = new System.Net.NetworkCredential(ConfigurationManager.AppSettings["EmailAddress"], ConfigurationManager.AppSettings["Password"]);
            smtp.EnableSsl = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
            mailMessage.IsBodyHtml = isHtmlBody;
            mailMessage.Subject = subject;
            mailMessage.Body = body;

            smtp.Send(mailMessage);
        }

        /// <summary>
        /// Get Sub String
        /// </summary>
        /// <param name="template"></param>
        /// <param name="startString"></param>
        /// <param name="endString"></param>
        /// <returns></returns>
        public static String GetSubString(String template,String startString, String endString,bool includeStartEndString)
        {
            int startIndex = 0, endIndex = 0;
            startIndex = template.IndexOf(startString);
            endIndex = template.IndexOf(endString);
            if (includeStartEndString)
                return template.Substring(startIndex, endIndex - startIndex + endString.Length);
            else
                return template.Substring(startIndex + startString.Length, endIndex - startIndex - endString.Length + 1);
        }

        public static String ConvertBoolToYesNo(bool value)
        {
            if (value)
                return "Yes";
            return "No";
        }

        #endregion

        #endregion

        #region Extension Methods

        public static DateTime ConvertToDateTime(this string p)
        {
            DateTime dt;
            if (DateTime.TryParse(p, out dt))
                return dt;

            return DateTime.MinValue;

        }

        public static double ConvertToDouble(this string p)
        {
            double d;
            double.TryParse(p, out d);

            return d;

        }

        public static bool ConvertToBool(this string p)
        {
            return p == "1";
        }

        public static int ConvertToInt(this string p)
        {
            int i;
            int.TryParse(p, out i);

            return i;
        }

        #endregion
    }
}
