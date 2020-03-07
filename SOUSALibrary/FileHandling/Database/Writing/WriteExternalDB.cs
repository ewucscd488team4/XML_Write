using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using MySql.Data.MySqlClient;
using SAUSALibrary.FileHandling.XML.Reading;
using SAUSALibrary.Models;

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

        public static bool TestSQL(string[] info)
        {
            SqlConnectionStringBuilder dbConString = new SqlConnectionStringBuilder
            {
                DataSource = info[0],
                InitialCatalog = info[1],
                UserID = info[2],
                Password = info[3]
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

        public static bool SetUp_MySQLDatabase(ExternalDBModel dbparemeters, string workingFolder, string projectXMLFile)
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

            if (File.Exists(fqProjectFilePath))
            {
                table = ReadXML.ReadProjectDBTableName(fqProjectFilePath);
            }
            else
            {
                throw new FileNotFoundException("Project file not found!");
            }



            MySqlConnectionStringBuilder dbConString = new MySqlConnectionStringBuilder
            {
                Server = dbparemeters.Server,
                Database = dbparemeters.Database,
                UserID = dbparemeters.UserID,
                Password = dbparemeters.PassWord
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

        public static bool SetUp_MSSQLDatabase(string[] dbparameters, string workingFolder, string projectXMLFile)
        {
            /*//mysql
            CREATE TABLE TestData (
            crateIndex smallint IDENTITY(1,1) NOT NULL,
            xPos Decimal(10,4) NOT NULL,
            ypos Decimal(10,4) NOT NULL,
            zPos Decimal(10,4) NOT NULL,
            length Decimal(8,4) NOT NULL,
            width Decimal(8,4) NOT NULL,
            height Decimal(8,4) NOT NULL,
            weight Decimal(7,2) NOT NULL,
            name VARCHAR(150) NOT NULL
            )
            )*/

            string table;

            var fqProjectFilePath = Path.Combine(workingFolder, projectXMLFile);

            if (File.Exists(fqProjectFilePath))
            {
                table = ReadXML.ReadProjectDBTableName(fqProjectFilePath);
            }
            else
            {
                throw new FileNotFoundException("Project file not found!");
            }
            
            SqlConnectionStringBuilder dbConString = new SqlConnectionStringBuilder
            {
                DataSource = dbparameters[0],
                InitialCatalog = dbparameters[1],
                UserID = dbparameters[2],
                Password = dbparameters[3]
            };

            using (SqlConnection connection = new SqlConnection(dbConString.ConnectionString))
            {
                try
                {
                    connection.Open();
                    StringBuilder sBuilder = new StringBuilder();

                    sBuilder.Append("CREATE TABLE ");
                    //1

                    sBuilder.Append(table + " (");
                    //2

                    sBuilder.Append("crateIndex smallint IDENTITY(1,1) NOT NULL,");
                    //3

                    sBuilder.Append("xPos Decimal(10,4),");
                    //4

                    sBuilder.Append("ypos Decimal(10,4),");
                    //5

                    sBuilder.Append("zPos Decimal(10,4),");
                    //6

                    sBuilder.Append("length Decimal(8,4) NOT NULL,");
                    //7

                    sBuilder.Append("width Decimal(8,4) NOT NULL,");
                    //8

                    sBuilder.Append("height Decimal(8,4) NOT NULL,");
                    //9

                    sBuilder.Append("weight Decimal(7,2) NOT NULL,");
                    //10

                    sBuilder.Append("name VARCHAR(150) NOT NULL");
                    //11  
                    
                    sBuilder.Append(")");
                    //12

                    SqlCommand cmd = new SqlCommand(sBuilder.ToString(), connection);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }


        }

        public static bool Export_ToMSSQL(string[] model, ObservableCollection<FullStackModel> containerList, string workingFolder, string projectXMLFile)
        {
            string table;

            var fqProjectFilePath = Path.Combine(workingFolder, projectXMLFile);

            if (File.Exists(fqProjectFilePath))
            {
                table = ReadXML.ReadProjectDBTableName(fqProjectFilePath);
            }
            else
            {
                throw new FileNotFoundException("Project file not found!");
            }

            SqlConnectionStringBuilder dbConString = new SqlConnectionStringBuilder
            {
                DataSource = model[0],
                InitialCatalog = model[1],
                UserID = model[2],
                Password = model[3]
            };

            try
            {
                using (SqlConnection conn = new SqlConnection(dbConString.ConnectionString))
                {
                    conn.Open();

                    foreach (FullStackModel listModel in containerList)
                    {                        

                        string importCommand = "INSERT INTO " + table + " (xPos, yPos, zPos, length, width, height, weight, name) values " +
                            "(@XPOS,@YPOS,@ZPOS,@LENGTH,@WIDTH,@HEIGHT,@WEIGHT,@NAME);";


                        SqlCommand pushDataCommand = new SqlCommand(importCommand, conn);
                                                
                        SqlParameter xPosition = new SqlParameter("@XPOS", listModel.XPOS.ToString());
                        pushDataCommand.Parameters.Add(xPosition);
                        
                        SqlParameter yPosition = new SqlParameter("@YPOS", listModel.YPOS.ToString());
                        pushDataCommand.Parameters.Add(yPosition);

                        SqlParameter zPosition = new SqlParameter("@ZPOS", listModel.ZPOS.ToString());
                        pushDataCommand.Parameters.Add(zPosition);

                        SqlParameter iLength = new SqlParameter("@LENGTH", listModel.Length.ToString());
                        pushDataCommand.Parameters.Add(iLength);

                        SqlParameter iWidth = new SqlParameter("@WIDTH", listModel.Width.ToString());
                        pushDataCommand.Parameters.Add(iWidth);

                        SqlParameter iHeight = new SqlParameter("@HEIGHT", listModel.Height.ToString());
                        pushDataCommand.Parameters.Add(iHeight);

                        SqlParameter iWeight = new SqlParameter("@WEIGHT", listModel.Weight.ToString());
                        pushDataCommand.Parameters.Add(iWeight);

                        SqlParameter iName = new SqlParameter("@NAME", listModel.CrateName.ToString());
                        pushDataCommand.Parameters.Add(iName);

                        pushDataCommand.ExecuteNonQuery();
                        
                    }
                    conn.Close();
                }
                return true;
            }
            catch (SqlException)
            {
                return false;
            }

        }

        public static bool Export_ToMySQL(string[] model, ObservableCollection<FullStackModel> containerList, string workingFolder, string projectXMLFile)
        {            
            string table;

            var fqProjectFilePath = Path.Combine(workingFolder, projectXMLFile);

            if (File.Exists(fqProjectFilePath))
            {
                table = ReadXML.ReadProjectDBTableName(fqProjectFilePath);
            }
            else
            {
                throw new FileNotFoundException("Project file not found!");
            }

            MySqlBaseConnectionStringBuilder dbConString = new MySqlConnectionStringBuilder
            {
                Server = model[0],
                Database = model[1],
                UserID = model[2],
                Password = model[3]
            };

            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbConString.ConnectionString))
                {
                    foreach (FullStackModel listModel in containerList)
                    {
                        string importCommand = "INSERT INTO " + table + " (xPos, yPos, zPos, length, width, height, weight, name) values " +
                            "(@XPOS,@YPOS,@ZPOS,@LENGTH,@WIDTH,@HEIGHT,@WEIGHT,@NAME);";


                        MySqlCommand pushDataCommand = new MySqlCommand(importCommand, conn);

                        MySqlParameter xPosition = new MySqlParameter("@XPOS", listModel.XPOS.ToString());
                        pushDataCommand.Parameters.Add(xPosition);

                        MySqlParameter yPosition = new MySqlParameter("@YPOS", listModel.YPOS.ToString());
                        pushDataCommand.Parameters.Add(yPosition);

                        MySqlParameter zPosition = new MySqlParameter("@ZPOS", listModel.ZPOS.ToString());
                        pushDataCommand.Parameters.Add(zPosition);

                        MySqlParameter iLength = new MySqlParameter("@LENGTH", listModel.Length.ToString());
                        pushDataCommand.Parameters.Add(iLength);

                        MySqlParameter iWidth = new MySqlParameter("@WIDTH", listModel.Width.ToString());
                        pushDataCommand.Parameters.Add(iWidth);

                        MySqlParameter iHeight = new MySqlParameter("@HEIGHT", listModel.Height.ToString());
                        pushDataCommand.Parameters.Add(iHeight);

                        MySqlParameter iWeight = new MySqlParameter("@WEIGHT", listModel.Weight.ToString());
                        pushDataCommand.Parameters.Add(iWeight);

                        MySqlParameter iName = new MySqlParameter("@NAME", listModel.CrateName.ToString());
                        pushDataCommand.Parameters.Add(iName);

                        pushDataCommand.ExecuteNonQuery();
                    }
                }
                return true;

            }
            catch (MySqlException)
            {
                return false;
            }

        }

        public static void Import_FromMySQL(ExternalDBModel model, ObservableCollection<FullStackModel> containerList)
        {
            //TODO import data from an ALREADY EXISTING external MySQL server into the designated project sqlite file
        }

        public static void Import_FromSQL(ExternalDBModel model, ObservableCollection<FullStackModel> containerList)
        {
            //TODO import data from an ALREADY EXISTING external Microsoft SQL server into the designated project sqlite file
        }
    }
}
