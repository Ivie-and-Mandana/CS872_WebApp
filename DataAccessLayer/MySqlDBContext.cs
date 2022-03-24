﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Data.Common;
using CS872_WebApp.Models;
using System.Data;
using System.Data.SqlClient;
using System.Text.Json;
using MySqlConnector;

namespace CS872_WebApp.DataAccessLayer
{
    public class MySqlDBContext
    {
        private MySqlConnectionStringBuilder dbConnectionString; // = "database=alias;uid=userid;pwd=password;";
        private MySqlConnection dbConnection;
        private MySqlCommand dbCommand;
        private MySqlDataReader mySqlDataReader;
        public static bool operationSucceeded;
        public static bool operationFailed;
        //private MySqlDataReader mySqlDataReader;
        


        public MySqlDBContext()
        {
            this.dbConnectionString = new MySqlConnectionStringBuilder();
            //this.dbConnectionString.Password = "Oseyi1234";
            //this.dbConnectionString.UserID = "admin";
            //this.dbConnectionString.Server = "cs872.ccd5phonjhwq.us-east-1.rds.amazonaws.com:3306";
            //dbConnectionString.ConnectionString = "Server=cs872.ccd5phonjhwq.us-east-1.rds.amazonaws.com;port=3306;Uid=admin;Pwd=Oseyi1234;";
            dbConnectionString.ConnectionString = "Server=dbcsproj.ccd5phonjhwq.us-east-1.rds.amazonaws.com;Database=CS872Proj;port=3306;Uid=admin;Pwd=Oseyi1234;";
            //

            this.dbConnection = new MySqlConnection(dbConnectionString.ConnectionString);
            //this.mySqlDataReader = new MySqlDataReader();

            }

        internal MySqlConnection getConnection() => dbConnection;

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

                this.dbCommand.CommandText = String.Format("SELECT * FROM CS872.USER WHERE emailAddress = '{0}';", emailAddress);

                mySqlDataReader = (MySqlDataReader)await read_async();
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

        internal async Task<ResponseModel<string>> SaveChanges(BillViewModel bill)
        {
            ResponseModel<string> response = new ResponseModel<string>();

            openDBConnection();

            try
            {
                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = string.Format(@"UPDATE Bill SET emailAddress = '{0}',billDateTIme = '{1}',amount = '{2}',houseArea = '{3}',numberOfRooms = '{4}',
                                        numberOfChildren = '{6}',numberOfPeople = '{7}',isAirCondtion = '{8}',isTelevision = '{9}',
                                        isFlat = '{10}',isUrban = '{11}',billStatus = '{12}' WHERE billID = '{13}';",
                                        bill.billID, bill.emailAddress, bill.billDateTIme, bill.amount, bill.houseArea, bill.numberOfChildren,
                                        bill.numberOfPeople, bill.isAirCondtion, bill.isTelevision, bill.isFlat, bill.isUrban, bill.billStatus,
                                        bill.billID); ;

                int result = await dbCommand.ExecuteNonQueryAsync();

            }
            catch (Exception ex)
            {

                //throw new Exception(ex.Message + " " + "Unable to register User!");
                response.message = ex.Message + ". Unable to register User!";
                response.resultCode = 500;
                return response;
            }
            finally
            { closeAndDisposeConnections(); }

            response.message = "User has been registered!";
            response.resultCode = 200;
            return response;
        }

        public async void getUsers()
        {
            openDBConnection();

            List<string> users = new List<string>();

            try
            {
                this.dbCommand = dbConnection.CreateCommand();

                this.dbCommand.CommandText = "SELECT * FROM CS872.USER;";

                mySqlDataReader = (MySqlDataReader)await read_async();


                while (mySqlDataReader.Read())
                {
                    for (int i = 0; i <= 1; i++)
                    {
                        if (!mySqlDataReader.IsDBNull(i))
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




        public async Task<BillViewModel> getBill(int billId)
        {
            openDBConnection();

            BillViewModel bill = new BillViewModel();
            operationSucceeded = false;
            operationFailed = false;

            try
            {
                dbCommand = dbConnection.CreateCommand();

                dbCommand.CommandText = String.Format("SELECT * FROM Bill WHERE billID = '{0}';", billId);

                mySqlDataReader = (MySqlDataReader)await dbCommand.ExecuteReaderAsync();

                operationSucceeded = true;
            }
            catch (Exception ex)
            {

                operationFailed = true;
            }
            finally
            {
                closeAndDisposeConnections();
            }

            if (operationSucceeded)
            {
                return bill;
            }
            return null;
        }

        public async Task<List<BillViewModel>> getBills(string emailAddress)
        {
            openDBConnection();

            List<BillViewModel> bills = new List<BillViewModel>();
            ResponseModel<string> response = new ResponseModel<string>();
            operationSucceeded = false;
            operationFailed = false;

            try
            {
                dbCommand = dbConnection.CreateCommand(); 

                dbCommand.CommandText = String.Format("SELECT * FROM Bill WHERE emailAddress = '{0}';", emailAddress);

                mySqlDataReader = (MySqlDataReader)await dbCommand.ExecuteReaderAsync();

                bills = mapDBBillsToBillModel(mySqlDataReader).ToList<BillViewModel>();
                operationSucceeded = true;
            }
            catch (Exception ex)
            {
                response.message = ex.Message + " No bills available for user!";
                operationFailed = true;
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
            ResponseModel<string> response = new ResponseModel<string>();

            List<UserViewModel> users = new List<UserViewModel>();
            operationSucceeded = false;
            operationFailed = false;

            try
            {
                this.dbCommand = dbConnection.CreateCommand();

                this.dbCommand.CommandText = String.Format("SELECT * FROM BILL WHERE emailAddress = '{0}';", emailAddress);

                mySqlDataReader = (MySqlDataReader)await read_async();

                users = mapDBUsersToUserModel(mySqlDataReader);

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
                try 
                {

                    dbCommand = dbConnection.CreateCommand();

                    dbCommand.CommandText = string.Format("SELECT * FROM User WHERE userPassword = '{0}' and emailAddress='{1}';", user.userPassword, user.emailAddress);

                    mySqlDataReader = (MySqlDataReader)await dbCommand.ExecuteReaderAsync();

                    if (mySqlDataReader.Read())
                    {
                        LoginViewModel lg = new LoginViewModel() { emailAddress = mySqlDataReader.GetValue(0).ToString(), userPassword = mySqlDataReader.GetValue(0).ToString() };
                        response.Data = JsonSerializer.Serialize<LoginViewModel>(lg);
                        response.resultCode = 200;
                    }
 
                }
                catch(Exception ex)
                {

                    response.message = "User Not Found!";
                    response.resultCode = 500;

                 }
                finally { closeAndDisposeConnections(); }


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

                    try { 
                            dbCommand.CommandText = string.Format(@"INSERT INTO User (emailAddress, firstname, lastname, 
                                                                    address, city, postalCode, province,userType,userPassword,userStatus)
                                                                    VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}');",
                                                                    user.emailAddress, user.firstName, user.lastName,
                                                                    user.address, user.city, user.postalCode, user.province, user.userType, user.userPassword, user.userStatus);

                            int result = await dbCommand.ExecuteNonQueryAsync();

                    }
                    catch(Exception ex) 
                    {

                        //throw new Exception(ex.Message + " " + "Unable to register User!");
                        response.message = ex.Message + ". Unable to register User!";
                        response.resultCode = 500;
                        return response;
                    }
                    finally 
                    { closeAndDisposeConnections(); }

                    response.message = "User has been registered!";
                    response.resultCode = 200;

                }
            }
            return response;
        }

 

        private List<BillViewModel> mapDBBillsToBillModel(MySqlDataReader db2DataReader)
        {
            List<BillViewModel> bills = new List<BillViewModel>();


            while (db2DataReader.Read())
            {
                BillViewModel bill = new BillViewModel();

                bill.billID = (int) db2DataReader.GetValue(0);
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


        private List<UserViewModel> mapDBUsersToUserModel(MySqlDataReader db2DataReader)
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

        public async Task<ResponseModel<string>> createBill(BillViewModel bill)
        {
            openDBConnection();
            ResponseModel<string> response = new ResponseModel<string>();

            try
            {
                this.dbCommand = dbConnection.CreateCommand();

                this.dbCommand.CommandText = "SELECT * FROM CS872.BILL;";

                mySqlDataReader = (MySqlDataReader)await read_async();
            }
            catch (Exception ex)
            {

                throw ;
                response.message = ex.Message + " Bill Not Created!";
                response.resultCode = 500;
            }
            finally
            {
                closeAndDisposeConnections();
            }


            return response;
        }

        private void closeAndDisposeConnections()
        {
            if(mySqlDataReader != null)
                    mySqlDataReader.Close();
            dbCommand.Dispose();
            dbConnection.Close();
        }

        private Task<MySqlDataReader> read_async()
        {
            return dbCommand.ExecuteReaderAsync(); //ExecuteReaderAsync();
        }
    }
}

