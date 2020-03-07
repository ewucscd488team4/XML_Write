using SAUSALibrary.Defaults;
using System;
using SAUSALibrary.Models;
using SAUSALibrary.FileHandling.Database.Writing;
using System.Collections.ObjectModel;
using SAUSALibrary.FileHandling.Database.Reading;
using System.Text;

namespace XML_Write
{
    class XMLWriteTesting
    {
        static void Main(string[] args)
        {
            const string PROJECT = @"\Sausa\Test_Write_Project.xml";
            const string SETTINGS = "Hangerbay.xml";
            const string ProjectSQLiteDBFile = "Hangerbay.sqlite";

            //ExternalDBModel test = ReadXML.GetExternalProjectDBSettings(FilePathDefaults.ScratchFolder, SETTINGS);

            //string[] blahParam = new string[4];
            //blahParam[0] = test.Server;
            //blahParam[1] = test.Database;
            //blahParam[2] = test.UserID;
            //blahParam[3] = test.PassWord;

            //mysql testing credentials
            string[] testParm = new string[] {
                "MYSQL5022.site4now.net" , "db_a4733c_sausa" , "a4733c_sausa" , "Sausa#1test"
            };

            ObservableCollection<FullStackModel> ListForTesting = ReadSQLite.GetEntireStack(FilePathDefaults.ScratchFolder, ProjectSQLiteDBFile);

            //bool testExport = WriteExternalDB.Export_ToMSSQL(testParm, ListForTesting, FilePathDefaults.ScratchFolder, SETTINGS);



            //bool testSetup = WriteExternalDB.SetUp_MySQLDatabase(testParm, FilePathDefaults.ScratchFolder, SETTINGS);

            bool testExport = WriteExternalDB.Export_ToMySQL(testParm, ListForTesting, FilePathDefaults.ScratchFolder, SETTINGS);

            //Console.WriteLine(testSQL + "\n" + testing);
            Console.WriteLine(testExport);
        }// end of main
    }
}
