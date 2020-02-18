﻿using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
using SAUSALibrary.FileHandling.XML.Reading;

namespace SAUSALibrary.FileHandling.Database.Writing
{
    public class WriteExternalDB
    {
        public static bool TestMySQL_DBConnection(string[] testParameters)
        {
            MySqlConnection conn;
            MySqlConnectionStringBuilder dbConString = new MySqlConnectionStringBuilder
            {
                Server = testParameters[0],
                Database = testParameters[1],
                UserID = testParameters[2],
                Password = testParameters[3]
            };
            
            try
            {
                using (conn = new MySqlConnection(dbConString.ConnectionString))
                {
                    conn.Open();
                    return true;
                }

            }
            catch (MySqlException)
            {
                return false;
            }
            
        }

        public static bool TestMSSQL_DBConnection(string[] testParameters)
        {
            SqlConnectionStringBuilder dbConString = new SqlConnectionStringBuilder
            {
                DataSource = testParameters[0],
                InitialCatalog = testParameters[1],
                UserID = testParameters[2],
                Password = testParameters[3]
            };
            using (SqlConnection connection = new SqlConnection(dbConString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }

        }        

        public static bool SetUpMySQLDatabase(string[] dbparemeters, string workingFolder, string projectXMLFile)
        {
            //TODO set up an external MySQL database to match the already extant project SQLite database that must exist for this to be an option

            /*
             CREATE TABLE TestData (
            crateIndex smallint(4) NOT NULL AUTO_INCREMENT,
            xPos Decimal(10,4),
            yPos Decimal(10,4),
            zPos Decimal(10,4),
            length Decimal(8,4) NOT NULL,
            width Decimal(8,4) NOT NULL,
            height Decimal(8,4) NOT NULL,
            weight Decimal(7,2) NOT NULL,
            name VARCHAR(150) NOT NULL,
            PRIMARY KEY (crateIndex)
             */

            string table;

            var fqProjectFilePath = Path.Combine(workingFolder, projectXMLFile);

            if(File.Exists(fqProjectFilePath))
            {
                table = ReadXML.ReadProjectDBTableName(fqProjectFilePath);
            } else
            {
                throw new FileNotFoundException("Project file not found!");
            }
            
            

            SqlConnectionStringBuilder dbConString = new SqlConnectionStringBuilder
            {
                DataSource = dbparemeters[0],
                InitialCatalog = dbparemeters[1],
                UserID = dbparemeters[2],
                Password = dbparemeters[3]
            };

            using (SqlConnection connection = new SqlConnection(dbConString.ConnectionString))
            {
                try
                {
                    connection.Open();

                    //build the command to execute
                    StringBuilder commandBuilder = new StringBuilder();
                    

                    commandBuilder.Append("CREATE TABLE ");
                    commandBuilder.Append(table + "(");
                    commandBuilder.Append("crateIndex smallint(4) NOT NULL AUTO_INCREMENT,");
                    commandBuilder.Append("xPos Decimal(10,4),");
                    commandBuilder.Append("yPos Decimal(10,4),");
                    commandBuilder.Append("zPos Decimal(10,4),");
                    commandBuilder.Append("length Decimal(8,4) NOT NULL,");
                    commandBuilder.Append("width Decimal(8,4) NOT NULL,");
                    commandBuilder.Append("height Decimal(8,4) NOT NULL,");
                    commandBuilder.Append("weight Decimal(7,2) NOT NULL,");
                    commandBuilder.Append("name VARCHAR(150) NOT NULL,");
                    commandBuilder.Append("PRIMARY KEY (crateIndex)");


                    SqlCommand cmd = new SqlCommand(commandBuilder.ToString(), connection);
                    cmd.ExecuteNonQuery();
                    
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
        
        public static bool SetUpSQLDatabase(string[] dbparameters)
        {
            //TODO set up an external Microsoft SQL database to match the already extant project SQLite database that must exist for this to be an option
            return false;
        }

        public static void ExportProjectToSQL()
        {
            //TODO write project sqlite as it stands to an ALREADY EXISTING external Microsoft SQL server
        }

        public static void ExportProjectToMySQL()
        {
            //TODO write project sqlite as it stands to an ALREADY EXISTING external MySQL server
        }

        public static void ImportProjectFromMySQL()
        {
            //TODO import data from an ALREADY EXISTING external MySQL server into the designated project sqlite file
        }

        public static void ImportProjectFromSQL()
        {
            //TODO import data from an ALREADY EXISTING external Microsoft SQL server into the designated project sqlite file
        }
    }
}
