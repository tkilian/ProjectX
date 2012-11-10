using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Data.SQLite;
using as_projekt.data;
using AS_Projekt.helper;

namespace AS_Projekt.db
{
    public class Database
    {
        private String dataSourceName = "as_projekt.db";
        private String connectionString = "";

        private SQLiteConnection dbConnection = null;
        public SQLiteConnection DbConnection
        {
            get
            {
                if (dbConnection == null)
                {
                    dbConnection = new SQLiteConnection(this.connectionString);
                }

                return dbConnection;
            }
            private set {}
        }

        public Database()
        {
            connectionString = "Data Source=" + dataSourceName;
            setup();
        }

        private void setup()
        {
            SQLiteConnection connection = DbConnection;

            try
            {
                dbConnection.Open();

                SQLiteCommand command = new SQLiteCommand(dbConnection);
                command.CommandText = "CREATE TABLE IF NOT EXISTS `employees` (id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT" +
                    ", firstname VARCHAR(255) NOT NULL, lastname VARCHAR(255) NOT NULL, gender INTEGER NOT NULL, fk_department_nr INTEGER NOT NULL)";
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }
        }
        public bool insertEmployee(Employee employee) 
        {
            SQLiteConnection connection = DbConnection;

            try
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(connection);

                command.CommandText = "INSERT INTO `employees` (firstname, lastname, gender, fk_department_nr) VALUES (@firstname, @lastname, @gender, @department)";
                command.Prepare();

                command.Parameters.AddWithValue("@firstname", employee.Firstname);
                command.Parameters.AddWithValue("@lastname", employee.Lastname);
                command.Parameters.AddWithValue("@gender", employee.Gender);
                command.Parameters.AddWithValue("@department", employee.Department.Id);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }
        
        public List<Employee> getAllEmployees()
        {
            SQLiteConnection connection;
            List<Employee> employees = new List<Employee>();

            try
            {
                connection = DbConnection;
                SQLiteCommand command = new SQLiteCommand(connection);
                SQLiteDataReader reader;

                connection.Open();

                command.CommandText = "SELECT * FROM employees";

                reader = command.ExecuteReader();

                try
                {
                    while (reader.Read())
                    {
                        employees.Add(Helper.CreateEmployee(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetInt32(3), reader.GetInt32(4)));
                    }

                }
                finally
                {
                    // Aufräumen
                    reader.Close();
                    reader.Dispose();
                    connection.Close();
                    command.Dispose();
                }
            }
            catch (Exception e) 
            {
                throw e;
            }

            return employees;
        }

        /*
        public bool LogExport(WindowOptions options)
        {
            MySqlConnection connection = IvxConnection;

            try
            {
                MySqlCommand command = new MySqlCommand();

                connection.Open();

                MySqlTransaction transaction = connection.BeginTransaction();

                command.Connection = connection;
                command.Transaction = transaction;
                command.CommandText = "INSERT INTO `export_log` (year, timestamp, mode) VALUES (@year, @timestamp, @mode)";
                command.Prepare();

                command.Parameters.AddWithValue("@year", DateTime.Now.ToString("yyyy"));
                command.Parameters.AddWithValue("@timestamp", IntervoxHelper.GetCurrentTimestamp());
                command.Parameters.AddWithValue("@mode", options.Mode);

                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public int GetCurrentSequenceNumber()
        {
            int sequenceNumber = 0;

            MySqlConnection connection = IvxConnection;
            MySqlDataReader reader = null;

            try
            {
                MySqlCommand command = new MySqlCommand();
                connection.Open();

                command.Connection = connection;
                command.CommandText = "SELECT COUNT(*) FROM `export_log`";

                reader = command.ExecuteReader();
                reader.Read();
                sequenceNumber = reader.GetInt32(0);

            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
                if (reader != null) reader.Close();
            } 

            return sequenceNumber;
        }

        public bool updateLabel(IvxLabel label)
        {
            try
            {
                MySqlCommand command = new MySqlCommand();
                MySqlConnection connection = IvxConnection;
                connection.Open();

                command.Connection = connection;

                command.CommandText = "UPDATE `labels` SET name = @name, name_short = @name_short, intervox_is_oe = @is_oe," + 
                                            " alt_oe_name = @alt_oe_name, alt_oe_ipi = @alt_oe_ipi, main_country = @country WHERE label_id = @id";
                command.Prepare();

                command.Parameters.AddWithValue("@id", label.Id);
                command.Parameters.AddWithValue("@name", label.Name);
                command.Parameters.AddWithValue("@name_short", label.NameShort);
                command.Parameters.AddWithValue("@is_oe", label.IsOe);
                command.Parameters.AddWithValue("@alt_oe_name", label.RealOeName);
                command.Parameters.AddWithValue("@alt_oe_ipi", label.RealOeIpi);
                command.Parameters.AddWithValue("@country", label.MainCountry);

                command.ExecuteNonQuery();

                connection.Close();
            }
            catch (Exception e)
            {
                throw e;
            }

            return true;
        }

        public bool updateSociety(IvxSociety society)
        {
            MySqlConnection connection = IvxConnection;

            try
            {
                MySqlCommand command = new MySqlCommand();
                connection.Open();

                command.Connection = connection;

                command.CommandText = "UPDATE `societies` SET name = @name, code = @code, country = @country, country_tis_code = @country_tis_code, country_publisher_name = @pub_name, country_publisher_ipi = @pub_ipi WHERE society_id = @id";
                command.Prepare();

                command.Parameters.AddWithValue("@id", society.Id);
                command.Parameters.AddWithValue("@name", society.Name);
                command.Parameters.AddWithValue("@code", society.Code);
                command.Parameters.AddWithValue("@country", society.Country);
                command.Parameters.AddWithValue("@country_tis_code", society.CountryTisCode);
                command.Parameters.AddWithValue("@pub_name", society.CountryPublisherName);
                command.Parameters.AddWithValue("@pub_ipi", society.CountryPublisherIpi);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public bool updateShareRecord(IvxShareRecord shareRecord)
        {
            try
            {
                bool recordIsAlreadyPresent = GetIfShareRecordIsPresent(shareRecord.LabelId, shareRecord.SocietyId);

                if (!recordIsAlreadyPresent)
                {
                    return insertShareRecord(shareRecord);
                }
                else
                {
                    return updateSocietyLabelShare(shareRecord);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool updateSocietyLabelShare(IvxShareRecord shareRecord)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = IvxConnection;

            try
            {
                connection.Open();

                command.Connection = connection;

                command.CommandText = "UPDATE `label_society_shares` SET per_ca = @per_ca, per_se = @per_se, mec_ca = @mec_ca, mec_se = @mec_se WHERE label_id = @label_id AND society_id = @society_id";
                command.Prepare();

                command.Parameters.AddWithValue("@label_id", shareRecord.LabelId);
                command.Parameters.AddWithValue("@society_id", shareRecord.SocietyId);

                command.Parameters.AddWithValue("@per_ca", shareRecord.PerCa);
                command.Parameters.AddWithValue("@per_se", shareRecord.PerSe);

                command.Parameters.AddWithValue("@mec_ca", shareRecord.MecCa);
                command.Parameters.AddWithValue("@mec_se", shareRecord.MecSe);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public bool insertShareRecord(IvxShareRecord shareRecord)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = IvxConnection;

            try
            {
                connection.Open();

                command.Connection = connection;

                command.CommandText = "INSERT INTO `label_society_shares`" +
                    " (label_id, society_id, per_ca, per_se, mec_ca, mec_se)" +
                    " VALUES (@label_id, @society_id, @per_ca, @per_se, @mec_ca, @mec_se)";
                command.Prepare();

                command.Parameters.AddWithValue("@label_id", shareRecord.LabelId);
                command.Parameters.AddWithValue("@society_id", shareRecord.SocietyId);

                command.Parameters.AddWithValue("@per_ca", shareRecord.PerCa);
                command.Parameters.AddWithValue("@per_se", shareRecord.PerSe);

                command.Parameters.AddWithValue("@mec_ca", shareRecord.MecCa);
                command.Parameters.AddWithValue("@mec_se", shareRecord.MecSe);

                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                connection.Close();
            }

            return true;
        }

        public List<String> GetAllLabels()
        {
            List<String> items = new List<String>();

            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = IvxConnection;
            MySqlDataReader reader;

            connection.Open();

            command.CommandText = "SELECT DISTINCT name FROM labels;";
            command.Connection = connection;

            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    items.Add(reader.GetString(0));                
                }

            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return items;
        }

        public List<String> GetAllSocities()
        {
            List<String> items = new List<String>();

            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = IvxConnection;
            MySqlDataReader reader;

            connection.Open();

            command.CommandText = "SELECT DISTINCT name FROM societies";
            command.Connection = connection;

            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    items.Add(reader.GetString(0));
                }

            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return items;
        }

        public List<String> GetAllCountries()
        {
            List<String> items = new List<String>();

            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = IvxConnection;
            MySqlDataReader reader;

            connection.Open();

            command.CommandText = "SELECT DISTINCT country FROM `societies`";
            command.Connection = connection;

            reader = command.ExecuteReader();

            try
            {
                while (reader.Read())
                {
                    items.Add(reader.GetString(0));
                }

            }
            finally
            {
                reader.Close();
                connection.Close();
            }

            return items;
        }

        public IvxLabel GetLabel(String name)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = IvxConnection;
            MySqlDataReader reader = null;

            connection.Open();

            command.CommandText = "SELECT label_id, name, name_short, intervox_is_oe, alt_oe_name, alt_oe_ipi, main_country FROM labels WHERE name = @name LIMIT 1";
            command.Connection = connection;

            command.Prepare();
            command.Parameters.AddWithValue("@name", name);
            
            IvxLabel label = null;
            String country;
            try
            {
                reader = command.ExecuteReader();
                reader.Read();
                country = !reader.IsDBNull(6) ? reader.GetString(6) : "";
                label = new IvxLabel(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetBoolean(3), reader.GetString(4), reader.GetString(5), country);
            }
            catch (Exception e)
            {
                Debug.WriteLine("An Exception of type " + e.GetType() + "was caught while fetching a label.");
            }
            finally
            {
                if(reader != null) reader.Close();
                connection.Close();
            }

            return label;
        }

        public IvxSociety GetSociety(String name)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = IvxConnection;
            MySqlDataReader reader = null;

            connection.Open();

            command.CommandText = "SELECT society_id, name, code, country, country_tis_code, country_publisher_name, country_publisher_ipi FROM `societies` WHERE name = @name LIMIT 1";
            command.Connection = connection;

            command.Prepare();
            command.Parameters.AddWithValue("@name", name);

            IvxSociety society = null;
            try
            {
                reader = command.ExecuteReader();
                reader.Read();

                String countryPublisherName = !reader.IsDBNull(4) ? reader.GetString(5) : "";
                String countryPublisherIpi = !reader.IsDBNull(5) ? reader.GetString(6) : "";

                society = new IvxSociety(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), countryPublisherName, countryPublisherIpi);
            }
            catch (Exception e)
            {
                Debug.WriteLine("An Exception of type " + e.GetType() + "was caught while fetching a society.");
            }
            finally
            {
                if(reader != null) reader.Close();
                connection.Close();
            }

            return society;
        }

        public IvxSociety GetSocietyByCountry(String country)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = IvxConnection;
            MySqlDataReader reader = null;

            connection.Open();

            command.CommandText = "SELECT society_id, name, code, country, country_tis_code, country_publisher_name, country_publisher_ipi FROM `societies` WHERE country = @country LIMIT 1";
            command.Connection = connection;

            command.Prepare();
            command.Parameters.AddWithValue("@country", country);

            IvxSociety society = null;
            try
            {
                reader = command.ExecuteReader();
                reader.Read();

                String countryPublisherName = !reader.IsDBNull(4) ? reader.GetString(5) : "";
                String countryPublisherIpi = !reader.IsDBNull(5) ? reader.GetString(6) : "";

                society = new IvxSociety(reader.GetInt32(0), reader.GetString(1), reader.GetInt32(2), reader.GetString(3), reader.GetInt32(4), countryPublisherName, countryPublisherIpi);
            }
            catch (Exception e)
            {
                Debug.WriteLine("An Exception of type " + e.GetType() + "was caught while fetching a society.");
            }
            finally
            {
                if (reader != null) reader.Close();
                connection.Close();
            }

            return society;
        }

        public bool GetIfShareRecordIsPresent(int labelId, int societyId)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = IvxConnection;
            MySqlDataReader reader = null;

            connection.Open();

            command.CommandText = "SELECT COUNT(*) FROM `label_society_shares` WHERE label_id = @label_id AND society_id = @society_id LIMIT 1";
            command.Connection = connection;

            command.Prepare();
            command.Parameters.AddWithValue("@label_id", labelId);
            command.Parameters.AddWithValue("@society_id", societyId);

            bool isPresent = false;

            try
            {
                reader = command.ExecuteReader();
                reader.Read();
                int count = reader.GetInt32(0);
                isPresent = (count >= 1);
            }
            catch (Exception e)
            {
                isPresent = false;
                Debug.WriteLine("An Exception of type " + e.GetType() + "was caught while fetching wether or not a share record was present.");
            }
            finally
            {
                if(reader != null) reader.Close();
                connection.Close();
            }

            return isPresent;
        }

        public IvxShareRecord GetShareRecordByName(String labelName, String societyName)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = IvxConnection;
            MySqlDataReader reader = null;

            IvxShareRecord record = ResolveLabelSocietyToShare(labelName.Trim(), societyName.Trim(), 0, 0, 0, 0);

            connection.Open();

            command.CommandText = "SELECT label_id, society_id, per_ca, per_se, mec_ca, mec_se FROM `label_society_shares`" +
                                    " WHERE label_id = @label_id AND" + 
                                    " society_id = @society_id LIMIT 1";
            command.Connection = connection;

            command.Prepare();
            command.Parameters.AddWithValue("@label_id", record.LabelId);
            command.Parameters.AddWithValue("@society_id", record.SocietyId);

            IvxShareRecord shareRecord = null;

            try
            {
                reader = command.ExecuteReader();
                reader.Read();
                shareRecord = new IvxShareRecord(reader.GetInt32(0), reader.GetInt32(1), reader.GetFloat(2), reader.GetFloat(3), reader.GetFloat(4), reader.GetFloat(5));
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception of type " + e.GetType() + " caught while fetching a share by name");
            }
            finally
            {
                if(reader != null) reader.Close();
                connection.Close();
            }

            return shareRecord;
        }


        public IvxShareRecord ResolveLabelSocietyToShare(String labelName, String societyName, float share_per_ca, float share_per_se, float share_mec_ca, float share_mec_se)
        {
            MySqlCommand labelCommand = new MySqlCommand();
            MySqlCommand societyCommand = new MySqlCommand();
            MySqlConnection labelConnection = new MySqlConnection(this.connectionString);
            MySqlConnection societyConnection = new MySqlConnection(this.connectionString);
            MySqlDataReader labelReader = null;
            MySqlDataReader societyReader = null;

            labelConnection.Open();

            labelCommand.CommandText = "SELECT label_id FROM `labels` WHERE name = @label_name LIMIT 1";
            labelCommand.Connection = labelConnection;
            labelCommand.Prepare();
            
            labelCommand.Parameters.AddWithValue("@label_name", labelName);

            societyConnection.Open();

            societyCommand.CommandText = "SELECT society_id FROM `societies` WHERE name = @society_name LIMIT 1";
            societyCommand.Connection = societyConnection;
            societyCommand.Prepare();

            societyCommand.Parameters.AddWithValue("@society_name", societyName);

            IvxShareRecord shareRecord = null;

            try
            {
                labelReader = labelCommand.ExecuteReader();
                societyReader = societyCommand.ExecuteReader();

                shareRecord = null;
                
                labelReader.Read();
                int labelId = labelReader.GetInt32(0);

                societyReader.Read();
                int societyId = societyReader.GetInt32(0);

                shareRecord = new IvxShareRecord(labelId, societyId, share_per_ca, share_per_se, share_mec_ca, share_mec_se);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception of type " + e.GetType() + " caught while resolving a label_id and society_id to a share.");
            }
            finally
            {
                if (labelReader != null) labelReader.Close();
                if (societyReader != null) societyReader.Close();
                labelConnection.Close();
                societyConnection.Close();
            }

            return shareRecord;
        }

        public int GetLabelIdByName(String labelName)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = IvxConnection;
            MySqlDataReader reader = null;

            connection.Open();

            command.CommandText = "SELECT label_id FROM `labels` WHERE name = @label_name";
            command.Connection = connection;

            command.Prepare();
            command.Parameters.AddWithValue("@label_name", labelName);

            int labelId = 0;

            try
            {
                reader = command.ExecuteReader();
                reader.Read();
                labelId = reader.GetInt32(0);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception of type " + e.GetType() + " caught while fetching a label by name.");
            }
            finally
            {
                if (reader != null) reader.Close();
                connection.Close();
            }

            return labelId;
        }

        public int GetSocietyIdByName(String societyName)
        {
            MySqlCommand command = new MySqlCommand();
            MySqlConnection connection = IvxConnection;
            MySqlDataReader reader = null;

            connection.Open();

            command.CommandText = "SELECT society_id FROM `societies` WHERE name = @society_name";
            command.Connection = connection;

            command.Prepare();
            command.Parameters.AddWithValue("@society_name", societyName);

            int societyId = 0;

            try
            {
                reader = command.ExecuteReader();
                reader.Read();
                societyId = reader.GetInt32(0);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Exception of type " + e.GetType() + " caught while fetching a society id by name.");
            }
            finally
            {
                if (reader != null) reader.Close();
                connection.Close();
            }

            return societyId;
        }

        public bool DeleteLabel(int labelId)
        {
            MySqlCommand labelCommand = new MySqlCommand();
            MySqlCommand shareCommand = new MySqlCommand();
            MySqlConnection shareConnection = IvxConnection;

            shareConnection.Open();

            MySqlTransaction transaction = shareConnection.BeginTransaction();

            shareCommand.CommandText = "DELETE FROM `label_society_shares` WHERE label_id = @label_id";
            shareCommand.Connection = shareConnection;
            shareCommand.Transaction = transaction;
            shareCommand.Prepare();

            shareCommand.Parameters.AddWithValue("@label_id", labelId);

            bool success = false;

            try
            {
                shareCommand.ExecuteNonQuery();

                shareCommand.CommandText = "DELETE FROM `labels` WHERE label_id = @label_id";
                shareCommand.Prepare();

                shareCommand.ExecuteNonQuery();

                transaction.Commit();
                success = true;
            }
            catch (Exception exp)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (MySqlException mexp)
                {
                    Debug.WriteLine("An Exception of type " + mexp.GetType() + " was encountered while attempting to roll back the transaction.");
                }

                Debug.WriteLine("Exception of type " + exp.GetType() + " was caught while executing the delete label queries.");
            }
            finally
            {
                shareConnection.Close();
            }

            return success;
        }

        public bool DeleteSociety(int societyId)
        {
            MySqlCommand labelCommand = new MySqlCommand();
            MySqlCommand shareCommand = new MySqlCommand();
            MySqlConnection shareConnection = IvxConnection;

            shareConnection.Open();

            MySqlTransaction transaction = shareConnection.BeginTransaction(); 

            shareCommand.CommandText = "DELETE FROM `label_society_shares` WHERE society_id = @society_id";
            shareCommand.Connection = shareConnection;
            shareCommand.Transaction = transaction;
            shareCommand.Prepare();

            shareCommand.Parameters.AddWithValue("@society_id", societyId);

            bool success = false;

            try
            {
                shareCommand.ExecuteNonQuery();

                shareCommand.CommandText = "DELETE FROM `societies` WHERE society_id = @society_id";
                shareCommand.Prepare();

                shareCommand.ExecuteNonQuery();

                transaction.Commit();
                success = true;
            }
            catch (Exception exp)
            {
                try
                {
                    transaction.Rollback();
                }
                catch (MySqlException mexp)
                {
                    Debug.WriteLine("An Exception of type" + mexp.GetType() + "was encountered while attempting to roll back the transaction.");
                }

                Debug.WriteLine("Exception of type " + exp.GetType() + " was caught while executing the delete society queries.");
            }
            finally
            {
                shareConnection.Close();
            }

            return success;
        }
        */
    }
}
