using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace XML_Write
{
    class Program
    {
        static void Main(string[] args)
        {
            //*** ApplicationData       = C:\Users\Diesel\AppData\Roaming\Sausa\project.xml //WORKING DIRECTORY
            //*** CommonApplicationData = C:\ProgramData\Sausa\project.xml                  //SETTINGS DIRECTORY
            //*** MyDocuments           = C:\Users\Diesel\Documents\Sausa\project.xml       //DEFAULT Projects Folder

            string RECENT = @"\Sausa\writing.xml";
            string SETTINGS = @"writing.xml";

            string xmlPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + RECENT; //actual path to my recent projects xml directory
            
            if (!File.Exists(xmlPath)) //writes a new settings XML file to the given directory IF it does not exist.
            {

                /*writes the following blank xml structure NOTE: **OVERWRITES FILE IF IT EXISTS**

                <Sausa >
                  <Projects>
                  </Projects >
                  <Misc>
                  </Misc>
                </Sausa >*/

                   var xmlNode =
                       new XElement("Sausa",   ///root of xml node
                                new XElement("Projects", ""), //first child node
                                new XElement("Misc", ""), //firt second inner child node
                       "");
                xmlNode.Save(xmlPath);
            }
            else //appends to already existing XML file
            {
                
                XmlDocument doc = new XmlDocument();
                doc.Load(xmlPath);

                XmlNode sausa = doc.DocumentElement; //gets root element in XML file
                XmlNode projects = sausa.FirstChild; //gets first child node of the root node
                
                XmlElement newProject = doc.CreateElement("Project"); //create basic node element

                //add attributes to new node
                newProject.SetAttribute("Definition","Endcap");
                newProject.SetAttribute("Path", @"C:\Users\Diesel\Documents\Sausa\EndCap.sau");
                newProject.SetAttribute("Time", "2155:04 / 11 / 19");

                //append new node to the end of the child nodes
                projects.AppendChild(newProject);

                //save the xml file
                doc.Save(xmlPath);                               
            }           

        }// end of main
    }
}
