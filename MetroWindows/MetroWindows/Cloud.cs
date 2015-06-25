using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Table;
namespace MetroWindows
{
    public class Cloud
    {
        public CloudStorageAccount storageAccount;
        public CloudBlobClient blobClient;
        public static CloudBlobContainer container;
        public CloudTableClient tableClient;
        public CloudTable table;
        public static string cache_path = @"Cache\";
        public string table_name = "credentials";
        public static UnicodeEncoding ByteConverter = new UnicodeEncoding();
        public void ConnectToCloud(string accountName, string accountKey) {
            storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(accountName, accountKey), true);
            tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference(table_name);
            table.CreateIfNotExists();
        }
        public void Connect(string accountName, string accountKey, string container)
        {
            storageAccount = new CloudStorageAccount(new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(accountName, accountKey), true);
            blobClient = storageAccount.CreateCloudBlobClient();
            Cloud.container = blobClient.GetContainerReference((String)container);
            Cloud.container.CreateIfNotExists();
            Console.WriteLine("CON UPLOAD "+ Cloud.container.ToString());
            tableClient = storageAccount.CreateCloudTableClient();
            table = tableClient.GetTableReference(table_name);
            table.CreateIfNotExists();
         //   RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        }
        static string blbName = "";
        static string blbPth = "";
        Thread downloadThread = new Thread(new ThreadStart(downloadThreadStart));
        Thread uploadThread = new Thread(new ThreadStart(uploadThreadStart));
        public static void uploadThreadStart()
        {
            Console.WriteLine("CONTAINER = ", container.ToString());
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blbName);
            Console.WriteLine("UPLOAD PATH" + blbPth);
            using (var fileStream = System.IO.File.OpenRead(@blbPth))
            {
                blockBlob.UploadFromStream(fileStream);
            }
            Console.WriteLine("SUCCESS!!!");
        }
        public static void downloadThreadStart()
        {
            Console.WriteLine("Downloading...");
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(blbName);
            using (var fileStream = System.IO.File.OpenWrite(cache_path + blbName))
            {
                blockBlob.DownloadToStream(fileStream);
            }
            Console.WriteLine("Download Complete!");
            Thread.Sleep(1000);
            if (MainWindow.left == true)
            {
                MainWindow.deck1DownloadRead = true;
            }
            else
            {
                MainWindow.deck2DownloadRead = true;
            }
        }
        public Thread DownloadThread(string blobName) 
        {
            var t = new Thread(() => DownloadBlob(blobName));
            t.Start();
            return t;
        }
        
        public void DownloadBlob(string blobName)
        {
            blbName = blobName;
            downloadThread.Start();
        }
        public Thread UploadThread(string name, string path) 
        {
            var t = new Thread(() => UploadBlob(name, path));
            t.Start();
            return t;
        }
        public void UploadBlob(string name, string path)
        {
            blbName = name;
            blbPth = path;
            uploadThread.Start();
         }
   /*     public Thread DeleteThread(string name, ref ListBox l)
        {
            var t = new Thread(() => DeleteBlob(name,ref l));
            t.Start();
            return t;
        }
        */
       
        public void DeleteBlob(string name) 
        {
            // Retrieve reference to a blob named "myblob.txt".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(name);

            // Delete the blob.
            blockBlob.Delete();
            Console.WriteLine("File deleted!");
        }
        public void ListBlobsInContainer(ref ListBox l)
        {
           foreach (var blobItem in container.ListBlobs())
            {
                l.Items.Add(blobItem.Uri.ToString().Split('/')[blobItem.Uri.ToString().Split('/').Length - 1]);
            }
        }

        public void InsertEntity(string name, string passwd, string uname, string email) 
        { 
            //to check: whether the email already exists
            UserEntity user = new UserEntity(uname, email);
            user.name = name;
           // RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
            
            string p1, p2;
            string e = Encrypt(passwd, out p1, out p2);
            //user.password = bytetostr(RSAEncrypt(strtobyte(passwd), RSA.ExportParameters(false), false)); 
            user.password = e;
            user.privatekey = p2;
            TableOperation insertOperation = TableOperation.Insert(user);
            table.Execute(insertOperation);
        }
        public string retuname(string mailid) 
        {
            TableQuery<UserEntity> query = new TableQuery<UserEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, mailid));
            string s="invalid";
            foreach (UserEntity entity in table.ExecuteQuery(query)) {
                s = entity.PartitionKey;
            }
            return s;
        }
        public bool MatchUname(string uname) 
        {
            bool val = false;
            TableQuery<UserEntity> query = new TableQuery<UserEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, uname));
            int count = table.ExecuteQuery(query).Count();

           
            if (count > 0)
                val = true;
            else
                val = false;
            return val;
        }
        public bool MatchPassword(string uname, string passwd) {
            bool val=false;
            TableQuery<UserEntity> query = new TableQuery<UserEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, uname));
            foreach (UserEntity entity in table.ExecuteQuery(query))
            {
               /* RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
              //  if(passwd == bytetostr(RSADecrypt(strtobyte(entity.password), RSA.ExportParameters(true), false)))
                
             //   string s = Encoding.ASCII.GetString((RSADecrypt(strtobyte(entity.password), RSA.ExportParameters(true), false)));
                //byte[] b = strtobyte(entity.password);
             //   Convert.FromBase64String Convert.ToBase64String();
                byte[] b = Convert.FromBase64String(entity.password);
                if (b == null)
                    Console.WriteLine("BEEEE ISSS NULLLL");
                byte[] b1 = RSADecrypt(b, RSA.ExportParameters(true), false);
                if (b1 == null)
                    Console.WriteLine("BEEEE ISSS NULLLL");
               string s1 = bytetostr(b1);
             //   string s = bytetostr(RSAEncrypt(strtobyte(passwd), RSA.ExportParameters(false), false));*/
                string d = Decrypt(entity.password, entity.privatekey);
                if(passwd == d)
                    val = true;
                else 
                    val = false;
            }
            return val;
        }

        public byte[] strtobyte(string s){
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            return ByteConverter.GetBytes(s);
        }

        public string bytetostr(byte[] b) { 
            UnicodeEncoding ByteConverter = new UnicodeEncoding();
            return ByteConverter.GetString(b);   
        }
        public byte[] RSAEncrypt(byte[] DataToEncrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                //Create a new instance of RSACryptoServiceProvider. 
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {

                    //Import the RSA Key information. This only needs 
                    //toinclude the public key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Encrypt the passed byte array and specify OAEP padding.   
                    //OAEP padding is only available on Microsoft Windows XP or 
                    //later.  
                    encryptedData = RSA.Encrypt(DataToEncrypt, DoOAEPPadding);
                }
                return encryptedData;
            }
            //Catch and display a CryptographicException   
            //to the console. 
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return null;
            }

        }

        public byte[] RSADecrypt(byte[] DataToDecrypt, RSAParameters RSAKeyInfo, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                //Create a new instance of RSACryptoServiceProvider. 
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    //Import the RSA Key information. This needs 
                    //to include the private key information.
                    RSA.ImportParameters(RSAKeyInfo);

                    //Decrypt the passed byte array and specify OAEP padding.   
                    //OAEP padding is only available on Microsoft Windows XP or 
                    //later.  
                    decryptedData = RSA.Decrypt(DataToDecrypt, DoOAEPPadding);
                }
                return decryptedData;
            }
            //Catch and display a CryptographicException   
            //to the console. 
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());

                return null;
            }

        }
        

        public static string Encrypt(string textToEncrypt, out string publicKey, out string privateKey)
        {
            byte[] bytesToEncrypt = ByteConverter.GetBytes(textToEncrypt);
            byte[] encryptedBytes;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                encryptedBytes = rsa.Encrypt(bytesToEncrypt, false);
                publicKey = rsa.ToXmlString(false);
                privateKey = rsa.ToXmlString(true);
            }
            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string textToDecrypt, string privateKeyXml)
        {
            if (string.IsNullOrEmpty(textToDecrypt))
            {
                throw new ArgumentException(
                    "Cannot decrypt null or blank string"
                );
            }
            if (string.IsNullOrEmpty(privateKeyXml))
            {
                throw new ArgumentException("Invalid private key XML given");
            }
            byte[] bytesToDecrypt = Convert.FromBase64String(textToDecrypt);
            byte[] decryptedBytes;
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKeyXml);
                decryptedBytes = rsa.Decrypt(bytesToDecrypt, false); // fail here
            }
            return ByteConverter.GetString(decryptedBytes);
        }

    }
}
