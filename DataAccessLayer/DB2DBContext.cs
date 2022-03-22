using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Common;
using CS872_WebApp.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using IBM.Data.DB2.Core;


namespace CS872_WebApp.DataAccessLayer
{
    public class DB2DBContext
    {
        private DB2ConnectionStringBuilder dbConnectionString; // = "database=alias;uid=userid;pwd=password;";
        private DB2Connection dbConnection;
        private DB2Command dbCommand;
        private DB2DataReader db2DataReader;
        public static bool operationSucceeded;
        public static bool operationFailed;


        public DB2DBContext()
        {
            this.dbConnectionString = new DB2ConnectionStringBuilder();
            this.dbConnectionString.Database = "BLUDB";
            this.dbConnectionString.Password = "c7nRREgQTvAtBdra";
            this.dbConnectionString.UserID = "fpz70189";
            this.dbConnectionString.Server = "b1bc1829-6f45-4cd4-bef4-10cf081900bf.c1ogj3sd0tgtu0lqde00.databases.appdomain.cloud:30387";

            this.dbConnection = new DB2Connection(dbConnectionString.ConnectionString);

        }


        public void openDBConnection()
        {
            try
            {
                dbConnection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async void getUser(string emailAddress)
        {
            openDBConnection();

            try
            {
                this.dbCommand = dbConnection.CreateCommand();

                this.dbCommand.CommandText = String.Format("SELECT * FROM FPZ70189.USER WHERE emailAddress = '{0}'", emailAddress);

                db2DataReader = (DB2DataReader)await read_async();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                closeAndDisposeConnections();
            }
        }


        public async void getUsers()
        {
            openDBConnection();

            List<string> users = new List<string>();

            try
            {
                this.dbCommand = dbConnection.CreateCommand();

                this.dbCommand.CommandText = "SELECT * FROM FPZ70189.USER;";

                db2DataReader = (DB2DataReader)await read_async();


                while (db2DataReader.Read())
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        if (!db2DataReader.IsDBNull(i))
                        {

                        }

                    }
                }
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                closeAndDisposeConnections();
            }

        }


        public async void getAccount(string emailAddress)
        {
            openDBConnection();

            try
            {
                this.dbCommand = dbConnection.CreateCommand();

                this.dbCommand.CommandText = String.Format("SELECT * FROM FPZ70189.ACCOUNT WHERE emailAddress = '{0}';", emailAddress);

                db2DataReader = (DB2DataReader)await read_async();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                closeAndDisposeConnections();
            }
        }


        public async void getSession(string emailAddress)
        {
            openDBConnection();

            try
            {
                this.dbCommand = dbConnection.CreateCommand();

                this.dbCommand.CommandText = String.Format("SELECT * FROM FPZ70189.SESSION WHERE emailAddress = '{0}';", emailAddress);

                db2DataReader = (DB2DataReader)await read_async();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                closeAndDisposeConnections();
            }
        }

        public async void getBill(string emailAddress)
        {
            openDBConnection();

            try
            {
                this.dbCommand = dbConnection.CreateCommand();

                this.dbCommand.CommandText = String.Format("SELECT * FROM FPZ70189.BILL WHERE emailAddress = '{0}';", emailAddress);

                db2DataReader = (DB2DataReader)await read_async();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                closeAndDisposeConnections();
            }
        }

        public async Task<List<BillViewModel>> getBills(string emailAddress)
        {
            openDBConnection();

            List<BillViewModel> bills = new List<BillViewModel>();
            operationSucceeded = false;
            operationFailed = false;

            try
            {
                this.dbCommand = dbConnection.CreateCommand(); //.ExecuteNonQuery();

                this.dbCommand.CommandText = String.Format("SELECT * FROM FPZ70189.BILL WHERE emailAddress = '{0}';", emailAddress);

                db2DataReader = (DB2DataReader)await read_async();

                bills = mapDBBillsToBillModel(db2DataReader);
                operationSucceeded = true;
            }
            catch (Exception ex)
            {
                operationFailed = true;
                throw new Exception(ex.Message);
            }
            finally
            {
                closeAndDisposeConnections();
            }


            if (operationSucceeded)
            {
                return bills;
            }
            return (null);

        }



        public async Task<List<UserViewModel>> getUsers(string emailAddress)
        {
            openDBConnection();

            List<UserViewModel> users = new List<UserViewModel>();
            operationSucceeded = false;
            operationFailed = false;

            try
            {
                this.dbCommand = dbConnection.CreateCommand();

                this.dbCommand.CommandText = String.Format("SELECT * FROM FPZ70189.BILL WHERE emailAddress = '{0}';", emailAddress);

                db2DataReader = (DB2DataReader)await read_async();

                users = mapDBUsersToUserModel(db2DataReader);

                operationSucceeded = true;
            }
            catch (Exception ex)
            {
                operationFailed = true;
                throw new Exception(ex.Message);
            }
            finally
            {
                closeAndDisposeConnections();
            }


            if (operationSucceeded)
            {
                return users;
            }
            return (null);
        }

        public async Task<ResponseModel<string>> login(LoginViewModel user)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            if (user != null)
            {
                openDBConnection();

                using (dbConnection)
                {
                    dbCommand = dbConnection.CreateCommand();

                    dbCommand.CommandText = string.Format("SELECT * FROM FPZ70189.ACCOUNT WHERE password = '{0}' and emailAddress='{1}';", user.password, user.emailAddress);

                    db2DataReader = (DB2DataReader)await read_async();

                    closeAndDisposeConnections();

                    if (db2DataReader.Read())
                    {
                        LoginViewModel lg = new LoginViewModel() { emailAddress = db2DataReader.GetValue(0).ToString(), password = db2DataReader.GetValue(0).ToString() };
                        response.Data = JsonSerializer.Serialize<LoginViewModel>(lg);
                        response.resultCode = 200;
                    }
                    else
                    {
                        response.message = "User Not Found!";
                        response.resultCode = 500;
                    }


                }
            }
            return response;
        }


        public async Task<ResponseModel<string>> register(LoginViewModel user)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            if (user != null)
            {
                openDBConnection();

                using (dbConnection)
                {
                    dbCommand = dbConnection.CreateCommand();

                    dbCommand.CommandText = string.Format(@"INSERT INTO FPZ70189.USER (emailAddress, firstName, lastName, 
                                                            fullName, address, city, province, postalCode, userType)
                                                            VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');",
                                                            user.emailAddress, user.firstName, user.lastName, user.fullName,
                                                            user.address, user.city, user.province, user.postalCode, user.userType);

                    int result = await dbCommand.ExecuteNonQueryAsync();


                    dbCommand.CommandText = string.Format(@"INSERT INTO FPZ70189.ACCOUNT ('ACCOUNTID','EMAILADDRESS','PASSWORD',
                                                                   'STATUS','DATEOPENED','DATECLOSED','LASTMODIFIED')
                                                          VALUES('{0}','{1}','{2}','{3}','{4}',NULL,NULL );",
                                                          Guid.NewGuid().ToString(), user.emailAddress, user.password, user.status, DateTime.Now);


                    int result2 = await dbCommand.ExecuteNonQueryAsync();

                    closeAndDisposeConnections();

                    if (result == 1 && result2 == 1)
                    {

                        response.message = "User has been registered!";
                        response.resultCode = 200;
                    }
                    else
                    {
                        response.message = "Unable to register User!";
                        response.resultCode = 500;
                    }


                }
            }
            return response;
        }


        private List<BillViewModel> mapDBBillsToBillModel(DB2DataReader db2DataReader)
        {
            List<BillViewModel> bills = new List<BillViewModel>();


            while (db2DataReader.Read())
            {
                BillViewModel bill = new BillViewModel();

                bill.billID = (Guid)db2DataReader.GetValue(0);
                bill.emailAddress = db2DataReader.GetValue(1).ToString();
                bill.billDateTIme = db2DataReader.GetDateTime(2);
                bill.amount = db2DataReader.GetDecimal(3);
                bill.houseArea = db2DataReader.GetDecimal(4);
                bill.numberOfRooms = db2DataReader.GetInt32(5);
                bill.numberOfChildren = db2DataReader.GetInt32(6);
                bill.numberOfPeople = db2DataReader.GetInt32(7);
                bill.isAirCondtion = db2DataReader.GetBoolean(8);
                bill.isTelevision = db2DataReader.GetBoolean(9);
                bill.isFlat = db2DataReader.GetBoolean(10);
                bill.isTelevision = db2DataReader.GetBoolean(11);
                bill.isUrban = db2DataReader.GetBoolean(12);
                bill.billStatus = db2DataReader.GetValue(13).ToString();


                if (bill.emailAddress != "")
                    bills.Add(bill);

            }

            return bills;
        }


        private List<UserViewModel> mapDBUsersToUserModel(DB2DataReader db2DataReader)
        {
            List<UserViewModel> users = new List<UserViewModel>();


            while (db2DataReader.Read())
            {
                UserViewModel user = new UserViewModel();

                user.emailAddress = db2DataReader.GetValue(0).ToString();
                user.firstName = db2DataReader.GetValue(1).ToString(); ;
                user.lastName = db2DataReader.GetValue(2).ToString();
                user.fullName = db2DataReader.GetValue(3).ToString();
                user.address = db2DataReader.GetValue(4).ToString();
                user.city = db2DataReader.GetValue(5).ToString();
                user.province = db2DataReader.GetValue(6).ToString();
                user.postalCode = db2DataReader.GetValue(7).ToString();
                user.userType = db2DataReader.GetValue(8).ToString();


                if (user.emailAddress != "")
                    users.Add(user);

            }

            return users;
        }

        public async void createBill(BillViewModel bill)
        {
            openDBConnection();

            try
            {
                this.dbCommand = dbConnection.CreateCommand();

                this.dbCommand.CommandText = "SELECT * FROM FPZ70189.BILL;";

                db2DataReader = (DB2DataReader)await read_async();
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
            finally
            {
                closeAndDisposeConnections();
            }
        }

        private void closeAndDisposeConnections()
        {
            db2DataReader.Close();
            dbCommand.Dispose();
            dbConnection.Close();
        }

        private Task<DbDataReader> read_async()
        {
            return dbCommand.ExecuteReaderAsync();
        }
    }
}

