using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace SH.Dialogs
{
    public class Choice : IDialogElement
    {
        public struct Option
        {
            public string Goto;
            public string Text;
        }

        public string ID { get; private set; }
        public string Speaker { get; private set; }
        public string Prompt { get; private set; }
        public List<Option> Options { get; private set; } = new List<Option>();

        public Choice() { }
        public Choice(string prompt, params Option[] options)
        {
            Prompt = prompt;
            Options = options.ToList();
        }
        public Choice(params Option[] options)
        {
            Options = options.ToList();
        }

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            string tmp = null;
            tmp = reader.GetAttribute("id");
            if (tmp != null)
                ID = tmp;
            tmp = reader.GetAttribute("speaker");
            if (tmp != null)
                Speaker = tmp;
            bool moved = false;
            var inner = reader.ReadSubtree();
            inner.MoveToContent();
            inner.Read();
            do
            {
                moved = false;
                if (inner.NodeType == XmlNodeType.Element)
                {
                    switch (inner.Name)
                    {
                        case "Prompt":
                            Prompt = inner.ReadElementContentAsString();
                            break;
                        case "Option":
                            Option elem = new Option();
                            elem.Goto = inner.GetAttribute("goto") ?? "$end";
                            elem.Text = inner.ReadElementContentAsString();
                            Options.Add(elem);
                            break;
                    }
                    //inner.Skip();
                    moved = true;
                }
            } while (moved || inner.Read());
        }

        public void WriteXml(XmlWriter writer)
        {
            if (!ID.IsNullOrEmpty())
                writer.WriteAttributeString("id", ID);
            foreach (Option element in Options)
            {
                var xml = new XmlSerializer(element.GetType());
                xml.Serialize(writer, element, new XmlSerializerNamespaces());
            }
        }
    }
}
