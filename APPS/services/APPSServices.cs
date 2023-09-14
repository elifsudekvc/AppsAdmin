using AppsServices.Models;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace AppsServices.Services
{
    
    public class APPSServices : IAPPSService
    {
        private readonly IConfiguration _configuration;
        

        public APPSServices (IConfiguration configuration) { 
        
            _configuration = configuration;
            ConnectionString = _configuration.GetConnectionString("DBConnection");
            providerName = "System.Data.SqlClient";
        
        }
        public string ConnectionString { get; }
        public string providerName { get; }
        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(ConnectionString);
            }
        }
        public string InsertAPPS(APPS model)
        //Bu metot veritabanına bir app kaydı ekler. 
        {
            string resault = "";
            try
                
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    //stored procedur , veriye erişim kontrolü ve veri bütünlüğünü sağlamak amaçlı
                    var APPS = dbConnection.Query<APPS>("InsertAPPSRecord", new {AppName=model.AppName, Ratings=model.Ratings,info=model.info,Link=model.Link}, commandType: CommandType.StoredProcedure).ToList();

                    if (APPS != null && APPS.FirstOrDefault().Response == "recorded")
                    {
                        resault = "recorded";
                    }

                    dbConnection.Close();
                    return resault;
                }
            } 
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                return ErrorMsg;
            }
            
        }
        public List<APPS> GetAPPSList()
        {
            //Bu metot apps tablosundaki kayıtları alır
            List<APPS> resault = new List<APPS>();
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    //stored procedur 
                    resault = dbConnection.Query<APPS>("GetAPPSList", commandType: CommandType.StoredProcedure).ToList();

                    dbConnection.Close();
                    return resault;
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg=ex.Message;
                return resault;
            }
           
        }

        public string DeleteAPPSRecord(int AppID)
        //Bu metot veritabanına bir app kaydı siler. 
        {
            string resault = "";
            try

            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    //stored procedur , veriye erişim kontrolü ve veri bütünlüğünü sağlamak amaçlı
                    var APPS = dbConnection.Query<APPS>("DeleteAPPSRecord", new { AppID = AppID }, commandType: CommandType.StoredProcedure).ToList();

                    if (APPS != null && APPS.FirstOrDefault().Response == "DELETED")
                    {
                        resault = "DELETED";
                    }

                    dbConnection.Close();
                    return resault;
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                return ErrorMsg;
            }

        }


        public string UpdateAPPSRecord(APPS model)
        //Bu metot veritabanında bir appi günceller. 
        {
            string resault = "";
            try

            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    //stored procedur , veriye erişim kontrolü ve veri bütünlüğünü sağlamak amaçlı
                    var APPS = dbConnection.Query<APPS>("UpdateAPPSRecord", new { AppName = model.AppName, Ratings = model.Ratings, info = model.info, Link = model.Link, AppID = model.AppID }, commandType: CommandType.StoredProcedure).ToList();

                    if (APPS != null && APPS.FirstOrDefault().Response == "recorded")
                    {
                        resault = "recorded";
                    }

                    dbConnection.Close();
                    return resault;
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message;
                return ErrorMsg;
            }

        }


    }



    public interface IAPPSService
    {
        public string InsertAPPS(APPS model);
        public List<APPS> GetAPPSList();
        public string DeleteAPPSRecord(int AppID);
        public string UpdateAPPSRecord(APPS model);
    }

}
