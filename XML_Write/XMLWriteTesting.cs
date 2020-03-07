using SAUSALibrary.FileHandling.Compression;
using SAUSALibrary.Defaults;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using SAUSALibrary.Models;
using SAUSALibrary.FileHandling.XML.Reading;
using SAUSALibrary.FileHandling.Database.Writing;
using System.Collections.ObjectModel;
using SAUSALibrary.FileHandling.Database.Reading;

namespace XML_Write
{
    class XMLWriteTesting
    {
        static void Main(string[] args)
        {
            const string PROJECT = @"\Sausa\Test_Write_Project.xml";
            const string SETTINGS = "Hangerbay.xml";
            const string ProjectSQLiteDBFile = "Hangerbay.sqlite";

            //*** ApplicationData       = C:\Users\Diesel\AppData\Roaming\Sausa\Test_Write_Project.xml //WORKING DIRECTORY
            //*** CommonApplicationData = C:\ProgramData\Sausa\Test_Write_Settings.xml                  //SETTINGS DIRECTORY
            //*** MyDocuments           = C:\Users\Diesel\Documents\Sausa\Test_Write_Project.xml       //DEFAULT Projects Folder

            //string _SettingsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + SETTINGS; //actual path to my recent projects xml directory
            //string _ProjectPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + PROJECT; //actual path to my recent projects xml directory


            /*if (!File.Exists(_SettingsPath)) //writes a new settings XML file to the given directory IF it does not exist.
            {

                *//* writes the following blank xml structure

                 <Sausa>
                   <ProjName>
                     <Name Name="XXX" />
                   </ProjName>
                   <Storage>
                     <Dimensions Length="XXX" Width="XXX" Height="XXX" Weight="XXX" />
                   </Storage>
                   <Stacks>
                     <Data FileName="XXX" Path="XXX" />
                   </Stacks>
                 </Sausa>
                 *//*

                var xmlNode =
                    new XElement("Sausa",                               //root of xml node
                                new XElement("ProjName",                //1st inner child node
                                    new XElement("Name",                //data node
                                        new XAttribute("Name", "XXX")   //attribute in data node
                                    ),
                                ""),                                    
                                new XElement("Storage",                 //2nd child node
                                    new XElement("Dimensions",          //data node
                                        new XAttribute("Length", "XXX"),//attribute 1
                                        new XAttribute("Width", "XXX"), //attribute 2
                                        new XAttribute("Height", "XXX"),//attribute 3
                                        new XAttribute("Weight", "XXX") //attribute 4
                                    ),
                                ""),
                                new XElement("Stacks",                  //3rd child node
                                    new XElement("Data",                //data node
                                        new XAttribute("FileName","XXX"),//first attribute
                                        new XAttribute("Path","XXX")     //2nd attribute
                                    ),
                                ""),  //3rd inner child node
                    "");
                xmlNode.Save(_SettingsPath);
            }
            else //appends to already existing XML file
            {

                *//*XmlDocument doc = new XmlDocument();
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
                doc.Save(_SettingsPath);*//*
            }*/

            //ZipFromScratchFolder.SaveProject(FilePathDefaults.ScratchFolder,"Test.sdf", FilePathDefaults.DefaultSavePath);
            //var fullProjectFilePath = Path.Combine(FilePathDefaults.DefaultSavePath, "Test.sdf");
            //UnzipFromProjectFile.OpenProject(fullProjectFilePath, FilePathDefaults.ScratchFolder);

            //ExternalDBModel test = ReadXML.GetExternalProjectDBSettings(FilePathDefaults.ScratchFolder, SETTINGS);

            //string[] blahParam = new string[4];
            //blahParam[0] = test.Server;
            //blahParam[1] = test.Database;
            //blahParam[2] = test.UserID;
            //blahParam[3] = test.PassWord;

            string[] testParm = new string[] {
                "SQL5047.site4now.net" , "DB_A4733C_SAUSA" , "DB_A4733C_SAUSA_admin" , "Sausa#1test"
            };

            ObservableCollection<FullStackModel> ListForTesting = ReadSQLite.GetEntireStack(FilePathDefaults.ScratchFolder, ProjectSQLiteDBFile);

            bool testExport = WriteExternalDB.Export_ToMSSQL(testParm, ListForTesting, FilePathDefaults.ScratchFolder, SETTINGS);

            //bool testSQL = WriteExternalDB.TestSQL(testParm);

            //bool testing = WriteExternalDB.SetUp_MSSQLDatabase(testParm, FilePathDefaults.ScratchFolder, SETTINGS);

            /*StringBuilder sBuilder = new StringBuilder();

            sBuilder.Append("CREATE TABLE ");                //1
            sBuilder.Append("testData (\n");                 //2
            sBuilder.Append("crateIndex smallint IDENTITY(1,1) NOT NULL,\n"); //3
            sBuilder.Append("xPos Decimal(10,4),\n");           //4
            sBuilder.Append("ypos Decimal(10,4),\n");           //5
            sBuilder.Append("zPos Decimal(10,4),\n");           //6
            sBuilder.Append("length Decimal(8,4) NOT NULL,\n");  //7
            sBuilder.Append("width Decimal(8,4) NOT NULL,\n");  //8
            sBuilder.Append("height Decimal(8,4) NOT NULL,\n"); //9
            sBuilder.Append("weight Decimal(7,2) NOT NULL,\n"); //10
            sBuilder.Append("name VARCHAR(150) NOT NULL\n");    //11                    
            sBuilder.Append(")");

            */
            //Console.WriteLine(testSQL + "\n" + testing);
            Console.WriteLine(testExport);
        }// end of main
    }
}
