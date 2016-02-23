using HiddenWordDALXml.XmlManager;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace HiddenWordDALXml.Classes
{
    public class GenericMethodes
    {
        string pathFolder;
        string xmlFileName;
        string tmpFileName;
        string fullXmlFileName;
        string fullTmpFileName;
        XNamespace XmlNameSpace;

        public GenericMethodes()
        {
            pathFolder = @"..\..\Files";
            xmlFileName = @"HiddenWorld.xml";
            tmpFileName = @"tmp.xml";
            fullXmlFileName = System.IO.Path.Combine(this.pathFolder, this.xmlFileName);
            fullTmpFileName = System.IO.Path.Combine(this.pathFolder, this.tmpFileName);
            XmlNameSpace = "http://tempuri.org/HiddenWordXMLSchema.xsd";


            if (!System.IO.Directory.Exists(pathFolder))
            {
                System.IO.Directory.CreateDirectory(pathFolder);
            }

            if (!File.Exists(fullXmlFileName))
            {
                using (System.IO.FileStream fs = System.IO.File.Create(fullXmlFileName))
                using (System.IO.FileStream fs2 = System.IO.File.Create(fullTmpFileName)) { }
            }
        }

        public elType saveXmlData<elType>(HiddenWord hiddenWord,string elName, string idEl) where elType: new()
        {
            TextWriter writer;
            XmlSerializer xs = new XmlSerializer(hiddenWord.GetType());

            if (File.ReadLines(fullXmlFileName).Count() == 0)
            {
                writer = new StreamWriter(fullXmlFileName);
                xs.Serialize(writer, hiddenWord);
                writer.Close();
            }
            else
            {
                writer = new StreamWriter(fullTmpFileName);
                xs.Serialize(writer, hiddenWord);
                writer.Close();

                XElement xeXml = XElement.Load(fullXmlFileName);
                XElement xeTmp = XElement.Load(fullTmpFileName);

                xeXml.Add(xeTmp.FirstNode);
                xeXml.Save(fullXmlFileName);
            }
            var elTypeString = typeof(elType).ToString();
            return getXmlDataByAttribute<elType>(elName, "ID", idEl);
        }

        public elType getXmlDataByAttribute<elType>(string elName, string attributeName, string attributeValue) where elType : new()
        {
            elType result = new elType();
            if (File.ReadLines(fullXmlFileName).Count() != 0)
            {
                XElement xe = XElement.Load(fullXmlFileName);

                XName fullNameSpaceEle = XmlNameSpace + elName;

                IEnumerable<XElement> allElements =
                    from listEl in xe.Elements(fullNameSpaceEle)
                    select listEl;

                IEnumerable<XElement> userElementList =
                    from el in allElements
                    where (string)el.Attribute(attributeName) == "" + attributeValue
                    select el;


                foreach (var userElement in userElementList)
                {
                    result = DeSerializer<elType>(elName, userElement);
                    break;
                }
            }

            return result;
        }

        public elType getXmlDataByValue<elType>(string elName, string value) where elType : new()
        {
            elType result = new elType();
            if (File.ReadLines(fullXmlFileName).Count() != 0)
            {
                XElement xe = XElement.Load(fullXmlFileName);

                XName fullNameSpaceEle = XmlNameSpace + elName;

                IEnumerable<XElement> allElements =
                    from listEl in xe.Elements(fullNameSpaceEle)
                    select listEl;

                IEnumerable<XElement> userElementList =
                    from el in allElements.Elements()
                    where (string)el.Value == "" + value
                    select el.Parent;

                foreach (var userElement in userElementList)
                {
                    result = DeSerializer<elType>(elName, userElement);
                    break;
                }
            }
            return result;
        }

        public elType DeSerializer<elType>(string elName, XElement element)
        {
            XmlRootAttribute xRoot = new XmlRootAttribute();
            xRoot.Namespace = XmlNameSpace.ToString();
            xRoot.ElementName = elName;
            xRoot.IsNullable = true;
            var serializer = new XmlSerializer(typeof(elType), xRoot);
            return (elType)serializer.Deserialize(element.CreateReader());
        }

        public List<elType> getListXmlData<elType>(string elName) where elType : new()
        {
            List<elType> result = new List<elType>();
            if (File.ReadLines(fullXmlFileName).Count() != 0)
            {
                XElement xe = XElement.Load(fullXmlFileName);

                XName fullNameSpaceEle = XmlNameSpace + elName;

                IEnumerable<XElement> elementList =
                    from listEl in xe.Elements(fullNameSpaceEle)
                    select listEl;

                foreach (var userElement in elementList)
                {
                    result.Add(DeSerializer<elType>(elName, userElement));
                }
            }
            return result;
        }

        public List<elType> getListXmlDataByValue<elType>(string elName, string value) where elType : new()
        {
            List<elType> result = new List<elType>();
            if (File.ReadLines(fullXmlFileName).Count() != 0)
            {
                XElement xe = XElement.Load(fullXmlFileName);

                XName fullNameSpaceEle = XmlNameSpace + elName;

                IEnumerable<XElement> allElements =
                    from listEl in xe.Elements(fullNameSpaceEle)
                    select listEl;

                var userElementFIlterList =
                    from el in allElements.Elements()
                    where (string)el.Value == "" + value
                    select el.Parent;

                foreach (var element in userElementFIlterList)
                {
                    result.Add(DeSerializer<elType>(elName, element));
                }
            }
            return result;
        }

        public List<elType> getListXmlDataByValueInnerJoin<elType>(string elName, string elNameJoin, string value) where elType : new()
        {
            List<elType> result = new List<elType>();
            if (File.ReadLines(fullXmlFileName).Count() != 0)
            {
                XElement xe = XElement.Load(fullXmlFileName);

                XName fullNameSpaceEle = XmlNameSpace + elName;

                IEnumerable<XElement> allElements =
                    from listEl in xe.Elements(fullNameSpaceEle)
                    select listEl;

                IEnumerable<XElement> elementList =
                    from el in allElements
                    where el.Element(XmlNameSpace + elNameJoin).Value.Equals("" + value)
                    select el;

                foreach (var element in elementList)
                {
                    result.Add(DeSerializer<elType>(elName, element));
                }
            }
            return result;
        }



        public elType updateXmlData<elType>(string elName, Dictionary<string, string> param) where elType : new()
        {
            elType result = new elType();
            if (File.ReadLines(fullXmlFileName).Count() != 0)
            {
                XElement xe = XElement.Load(fullXmlFileName);

                XName fullNameSpaceEle = XmlNameSpace + elName;

                IEnumerable<XElement> allElements =
                    from listEl in xe.Elements(fullNameSpaceEle)
                    select listEl;

                var foundEl = allElements
                                        .Where(x => x.Attribute("ID")
                                        .Value == param["ID"])
                                        .SingleOrDefault();

                foreach (var elChild in param)
                {
                    if (!elChild.Key.Equals("ID"))
                        foundEl.SetElementValue(XmlNameSpace + elChild.Key, elChild.Value);
                }

                xe.Save(fullXmlFileName);
            }
            return getXmlDataByAttribute<elType>(elName, "ID", param["ID"]);
        }


        public elType deleteXmlData<elType>(string elName, string id) where elType : new()
        {
            if (File.ReadLines(fullXmlFileName).Count() != 0)
            {
                XElement xe = XElement.Load(fullXmlFileName);

                XName fullNameSpaceEle = XmlNameSpace + elName;

                IEnumerable<XElement> allElements =
                    from listEl in xe.Elements(fullNameSpaceEle)
                    select listEl;

                var foundEl = allElements
                                        .Where(x => x.Attribute("ID")
                                        .Value == id);

                foundEl.Remove();
                xe.Save(fullXmlFileName);
            }
            return getXmlDataByAttribute<elType>(elName, "ID", id);
        }


        public int autoIncrementXmlDataPrimaryKey(string elName, string attributeName)
        {
            int result = 1;

            if (File.ReadLines(fullXmlFileName).Count() != 0)
            {
                XElement xe = XElement.Load(fullXmlFileName);

                XName fullNameSpaceEle = XmlNameSpace + elName;

                IEnumerable<XElement> allElements =
                    from listEl in xe.Elements(fullNameSpaceEle)
                    select listEl;

                IEnumerable<int> userElementList =
                    from el in allElements.Attributes(attributeName)
                    select int.Parse(el.Value);

                int max = 0;
                foreach (var key in userElementList)
                {
                    if (key > max)
                        max = key;
                }
                result = (max + 1);
            }
            return result;
        }



    }
}
