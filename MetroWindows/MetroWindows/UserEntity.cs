using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
namespace MetroWindows
{
    class UserEntity : TableEntity
    {
        public UserEntity(string uname, string email) {
            this.PartitionKey = uname;
            this.RowKey = email;
        }
        public UserEntity() { }
        public string password { get; set; }
        public string privatekey { get; set; }
        public string name { get; set; }
    }
}
