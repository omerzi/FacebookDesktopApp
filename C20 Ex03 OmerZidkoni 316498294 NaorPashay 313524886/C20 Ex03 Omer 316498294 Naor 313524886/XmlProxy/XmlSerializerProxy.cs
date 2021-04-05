using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XmlProxy
{
    public class XmlSerializerProxy
    {
        private readonly XmlSerializer r_Serializer;

        public XmlSerializerProxy(Type i_TypeOfSerialization)
        {
            r_Serializer = new XmlSerializer(i_TypeOfSerialization);
        }

        public void Serialize(Stream i_Stream, object i_ObjectToSerialize)
        {
            try
            {
                r_Serializer.Serialize(i_Stream, i_ObjectToSerialize);
            }
            catch
            {
                i_Stream.Dispose();
            }
        }

        public object Deserialize(Stream i_Stream)
        {
            object deserializedObject;

            try
            {
               deserializedObject = r_Serializer.Deserialize(i_Stream);
            }
            catch
            {
                i_Stream.Dispose();
                deserializedObject = null;
            }

            return deserializedObject;
        }
    }
}
