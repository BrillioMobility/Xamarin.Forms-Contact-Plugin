using System.Collections.Generic;
using Android.Database;
using Android.Provider;
using Contacts.Forms.Plugin.Abstractions;
using System;
using Contacts.Forms.Plugin.Abstractions.Model;
using Xamarin.Forms;
using Contacts.Forms.Plugin.Droid;

[assembly: Dependency(typeof(ContactsImplementation))]

namespace Contacts.Forms.Plugin.Droid
{
    /// <summary>
    /// Contacts Implementation
    /// </summary>
    public class ContactsImplementation : Java.Lang.Object, IContacts
    {
        /// <summary>
        /// Used for registration with dependency service
        /// </summary>
        public static void Init()
        {
        }

        public List<PhoneContact> GetContacts(string name)
        {
            var contacts = new List<PhoneContact>();

            #region newCode

            var projection = new String[]
            {
                ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Id,
                ContactsContract.CommonDataKinds.Phone.InterfaceConsts.DisplayName,
                ContactsContract.CommonDataKinds.Phone.InterfaceConsts.Data1
            };

            String[] mSelectionArgs = {name};
            //mSelectionArgs[0] = "1";
            mSelectionArgs[0] = "%" + name + "%";

            // var whereClause = ContactsContract.CommonDataKinds.Phone.InterfaceConsts.HasPhoneNumber+ " = ?" + " AND " +
            var whereClause = ContactsContract.CommonDataKinds.Phone.InterfaceConsts.DisplayName + " LIKE ?";


            var cr = Xamarin.Forms.Forms.Context.ContentResolver;
            ICursor cursor = null;
            if (name == "*")
            {
                var countClause = ContactsContract.CommonDataKinds.Phone.InterfaceConsts.HasPhoneNumber + " = ?";
                cursor = cr.Query(
                    ContactsContract.CommonDataKinds.Phone.ContentUri,
                    projection,
                    countClause,
                    new[] {"1"},
                    null);
            }
            else
            {
                cursor = cr.Query(
                    ContactsContract.CommonDataKinds.Phone.ContentUri,
                    projection,
                    whereClause,
                    mSelectionArgs,
                    null);
            }
            while (cursor.MoveToNext())
            {


                contacts.Add(new Abstractions.Model.PhoneContact()
                {
                    Id = cursor.GetString(0),
                    DisplayName = cursor.GetString(1),

                    Number = cursor.GetString(2)

                });
            }

            #endregion

            return contacts;
        }

    }
}
