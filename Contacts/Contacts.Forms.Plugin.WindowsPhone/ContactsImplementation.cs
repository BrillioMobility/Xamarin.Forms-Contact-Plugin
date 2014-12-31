using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contacts.Forms.Plugin.Abstractions;
using System;
using Contacts.Forms.Plugin.Abstractions.Model;
using Microsoft.Phone.UserData;
using Xamarin.Forms;
using Contacts.Forms.Plugin.WindowsPhone;

[assembly: Dependency(typeof(ContactsImplementation))]
namespace Contacts.Forms.Plugin.WindowsPhone
{
    /// <summary>
    /// Contacts Implementation
    /// </summary>
    public class ContactsImplementation : IContacts
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
       // public static void Init() { }
      async  public Task<List<Abstractions.Model.PhoneContact>> GetContacts(string name)
        {
            var phonecontacts = new List<Abstractions.Model.PhoneContact>();

            //TODO


            return phonecontacts;

        }
    }
}
