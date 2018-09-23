using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ZJAVS
{
    public class AVSConfiguration
    {
        private static AVSConfiguration _config;
        private string _strFileName;

        public static AVSConfiguration GetAVSConfiguration()
        {
            if ( _config == null )
            {
                _config = new AVSConfiguration();
            }
            return _config;
        }

        private AVSConfiguration()
        {
            _strFileName = this.GetType().Assembly.Location + ".config";
        }

        ~AVSConfiguration()
        {
        }

        public void EnableLoggingReader(int mode)
        {
            try
            {
                XmlDocument _doc = new XmlDocument();
                _doc.Load(_strFileName);

                XmlElement _root = _doc.DocumentElement;
                XmlNode _node = _root.SelectSingleNode("log4net");

                XmlNodeList _appendList = _node.SelectNodes("appender");
                

                foreach (XmlNode _appender in _appendList)
                {
                    if (_appender.Attributes["name"].Value == "HardwareFileAppender")
                    {
                        bool b_found  = false;
                        bool b_found1 = false;
                        XmlNodeList _filterList = _appender.SelectNodes("filter");
                        foreach(XmlNode _filter in _filterList)
                        {
                            if (_filter.Attributes["type"].Value == "log4net.Filter.StringMatchFilter")
                            {
                                b_found = true;
                                /// Enable Reader
                                if (mode == 0)
                                {
                                    XmlElement _element = (XmlElement)_filter.SelectSingleNode("stringToMatch");
                                    _element.Attributes["value"].Value = "Reader";
                                }
                                else if (mode == 1)
                                {
                                    XmlElement _element = (XmlElement)_filter.SelectSingleNode("stringToMatch");
                                    _element.Attributes["value"].Value = "Indicator";
                                }
                                else
                                {
                                    _appender.RemoveChild(_filter);
                                }
                            }

                            if (_filter.Attributes["type"].Value == "log4net.Filter.DenyAllFilter")
                            {
                                b_found1 = true;
                                if (mode == 2)
                                {
                                    _appender.RemoveChild(_filter);
                                }
                            }
                        }

                        if (!b_found)
                        {
                            XmlElement _e1 = _doc.CreateElement("filter");
                            _e1.SetAttribute("type", "log4net.Filter.StringMatchFilter");
                            XmlElement _e2 = _doc.CreateElement("stringToMatch");
                            if (mode == 0) {
                                _e2.SetAttribute("value", "Reader");
                            } else if (mode == 1)
                            {
                                _e2.SetAttribute("value", "Indicator");
                            }

                            _e1.AppendChild(_e2);
                            _appender.AppendChild(_e1);

                            if (!b_found1 && (mode < 2 ))
                            {
                                XmlElement _e3 = _doc.CreateElement("filter");
                                _e3.SetAttribute("type", "log4net.Filter.DenyAllFilter");
                                _appender.AppendChild(_e3);
                            }
                        }

                        break;
                    }
                }

                _doc.Save(_strFileName);
            }
            catch (Exception e)
            {
                string errMessage = this.GetType().ToString() + ": " + e.Message;
                Logger.LogError(errMessage);
            }

        }
    }
}
