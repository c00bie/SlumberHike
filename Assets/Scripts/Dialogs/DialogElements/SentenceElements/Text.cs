using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Schema;

namespace SH.Dialogs
{
    public class Text : ISentenceElement, IEnumerable<char>, IEquatable<string>, IEquatable<Text>, IComparable<Text>, IComparable<string>
    {
        public string Content { get; set; }

        public Text() { }
        public Text(string text)
        {
            Content = text;
        }

        public static implicit operator string(Text t) => t.Content;
        public static implicit operator Text(string t) => new Text(t);
        public override string ToString() => Content;

        public XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            Content = reader.ReadString();
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteString(Content);
        }

        public IEnumerator<char> GetEnumerator()
        {
            return Content.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Content.GetEnumerator();
        }

        public bool Equals(string other)
        {
            return Content.Equals(other);
        }

        public bool Equals(Text other)
        {
            return Content.Equals(other);
        }

        public int CompareTo(Text other)
        {
            return Content.CompareTo(other);
        }

        public int CompareTo(string other)
        {
            return Content.CompareTo(other);
        }
    }
}
