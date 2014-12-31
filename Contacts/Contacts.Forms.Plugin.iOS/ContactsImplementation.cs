using System.Collections.Generic;
using Contacts.Forms.Plugin.Abstractions;
using System;
using MonoTouch.AddressBook;
using MonoTouch.Foundation;
using Xamarin.Forms;
using Contacts.Forms.Plugin.iOS;

[assembly: Dependency(typeof(ContactsImplementation))]
namespace Contacts.Forms.Plugin.iOS
{
    /// <summary>
    /// Contacts Implementation
    /// </summary>
    public class ContactsImplementation : IContacts
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init() { }

        public System.Collections.Generic.List<Abstractions.Model.PhoneContact> GetContacts()
        {
            var contacts = new List<Abstractions.Model.PhoneContact>();




            //using (var addressbook = new ABAddressBook ()) {

            NSError err;
            var ab = ABAddressBook.Create(out err);
            if (err != null)
            {
                // process error
                Console.WriteLine("error in phonebok");
                return null;
            }
            // if the app was not authorized then we need to ask permission
            if (ABAddressBook.GetAuthorizationStatus() != ABAuthorizationStatus.Authorized)
            {
                ab.RequestAccess(delegate(bool granted, NSError error)
                {
                    if (error != null)
                    {
                        // process error
                        Console.WriteLine("error in phonebok");
                    }
                    else if (granted)
                    {
                        // permission now granted -> use the address book

                        contacts = ProcessContacts(ab);

                    }
                });
            }
            else
            {
                // permission already granted -> use the address book
                contacts = ProcessContacts(ab);
            }


           
            return contacts;
        }

        private List<Abstractions.Model.PhoneContact> ProcessContacts(ABAddressBook ab)
        {
            var contacts = new List<Abstractions.Model.PhoneContact>();
            var people = ab.GetPeople();

            foreach (var item in people)
            {

                var nums = item.GetPhones().GetValues();

                foreach (var num in nums)
                {

                    contacts.Add(new Abstractions.Model.PhoneContact()
                    {
                        DisplayName = string.Format("{0} {1}",
                            !string.IsNullOrEmpty(item.FirstName) ? item.FirstName : string.Empty,
                            !string.IsNullOrEmpty(item.LastName) ? item.LastName : string.Empty),
                        Id = Convert.ToString(item.Id),
                        Number = num
                    });
                }

            }

            return contacts;
        }
    }
}
