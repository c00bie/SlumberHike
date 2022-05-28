using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SH.Dialogs
{
    public interface IDialogElement : IXmlSerializable
    {
        public string ID { get; }

        public string Speaker { get; }
    }
}