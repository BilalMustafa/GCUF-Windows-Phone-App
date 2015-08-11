using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GCUF.DataModel
{
    public class Contact
    {
        public string Name { get; set; }
        public string JobTitle { get; set; }
        public string PhoneNumber { get; set; }
        public Contact(string name, string jobTitle, string phoneNumber)
        {
            this.JobTitle = jobTitle;
            this.Name = name;
            this.PhoneNumber = phoneNumber;

        }

    }
}
