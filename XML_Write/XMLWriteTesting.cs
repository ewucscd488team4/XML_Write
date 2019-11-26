using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace XML_Write
{
    class XMLWriteTesting
    {
        static void Main(string[] args)
        {
            const string PROJECT = @"\Sausa\Test_Write_Project.xml";
            const string SETTINGS = @"\Sausa\Test_Write_Settings.xml";

            //*** ApplicationData       = C:\Users\Diesel\AppData\Roaming\Sausa\Test_Write_Project.xml //WORKING DIRECTORY
            //*** CommonApplicationData = C:\ProgramData\Sausa\Test_Write_Settings.xml                  //SETTINGS DIRECTORY
            //*** MyDocuments           = C:\Users\Diesel\Documents\Sausa\Test_Write_Project.xml       //DEFAULT Projects Folder

            string _SettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + SETTINGS; //actual path to my recent projects xml directory
            string _ProjectPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + PROJECT; //actual path to my recent projects xml directory

            if (!File.Exists(_SettingsPath)) //writes a new settings XML file to the given directory IF it does not exist.
            {

                /*writes the following blank xml structure NOTE: **OVERWRITES FILE IF IT EXISTS**

                <Sausa>
                  <Projects></Projects>
                  <Misc></Misc>
                </Sausa>*/

                var xmlNode =
                    new XElement("Sausa",   ///root of xml node
                                new XElement("Projects", ""), //first child node
                             new XElement("Misc", ""), //firt second inner child node
                    "");
                xmlNode.Save(_SettingsPath);
            }
            else //appends to already existing XML file
            {

                XmlDocument doc = new XmlDocument();
                doc.Load(_SettingsPath);

                XmlNode sausaRoot = doc.DocumentElement; //gets root element in XML file
                XmlNode projectsRoot = sausaRoot.FirstChild; //gets first child node of the root node

                XmlElement newProject = doc.CreateElement("Project"); //create basic node element

                //add attributes to new node
                newProject.SetAttribute("Definition", "Endcap");
                newProject.SetAttribute("Path", @"C:\Users\Diesel\Documents\Sausa\EndCap.sau");
                newProject.SetAttribute("Time", "2155:04 / 11 / 19");

                //append new node to the end of the child nodes
                projectsRoot.AppendChild(newProject);

                //save the xml file
                doc.Save(_SettingsPath);
            }

        }// end of main
    }
}
