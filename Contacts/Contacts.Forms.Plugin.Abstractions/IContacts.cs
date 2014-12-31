using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Contacts.Forms.Plugin.Abstractions.Model;

namespace Contacts.Forms.Plugin.Abstractions
{
    /// <summary>
    /// Contacts Interface
    /// </summary>
    public interface IContacts
    {
        Task<List<PhoneContact>> GetContacts(string name);
    }
}
