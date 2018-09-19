using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Data;

namespace MvcApplication40.Models   
{
    public class DataLayer
    {
        SqlConnection Con;
        string s_Con;
        public DataLayer()
        {
            Connection();
        }
        private void Connection()
        {
            s_Con = ConfigurationManager.ConnectionStrings["Db"].ToString();
            Con = new SqlConnection(s_Con);
            if (Con.State == System.Data.ConnectionState.Open)
            {
                Con.Close();
            }
            Con.Open();
        }
        public bool Login(Users U)
        {
            DynamicParameters para = new DynamicParameters();
            para.Add("@UserName", U.UserName);
            para.Add("@Password", U.Password);
            para.Add("@Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
            Con.Execute("Pr_Login", para, commandType: System.Data.CommandType.StoredProcedure);
            string s = para.Get<string>("@Status");
            return Convert.ToBoolean(para.Get<string>("@Status")); 
        }
        public List<Users> GetRolesForUser(string UserName)
        {
            try
            {
                List<Users> DetList = new List<Users>();
                DynamicParameters para = new DynamicParameters();
                para.Add("@UserName", UserName);
                para.Add("@ProcID", 1);
                DetList = SqlMapper.Query<Users>(Con, "Pr_Login", para,
                    commandType: CommandType.StoredProcedure).ToList();
                return DetList;
            }
            catch (Exception E) { return null; }
        }
        public bool AddTechnology(BlogTbl Blg)
        {
            try
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("@Name", Blg.Technology);
                para.Add("@ImageData", Blg.ImageData);
                para.Add("@ProcID", 1);
                para.Add("@Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Con.Execute("Add_Blog", para, commandType: CommandType.StoredProcedure);
                return Convert.ToBoolean(para.Get<string>("@Status"));
            }
            catch (Exception E) { return false; }
        }
        public List<BlogTbl> TechnologiesList(int id=0)
        {
            DynamicParameters para = new DynamicParameters();
            para.Add("@ProcID", 2);
            if (id != 0) { para.Add("@ID", id); }
            List<BlogTbl> mlist = SqlMapper.Query<BlogTbl>(Con, "Add_Blog", para, commandType: CommandType.StoredProcedure).ToList();
            return mlist;
        }
        public List<BlogTbl> BlogTechnologies()
        {
            DynamicParameters para = new DynamicParameters();
            para.Add("@ProcID", 10);
            List<BlogTbl> mlist = SqlMapper.Query<BlogTbl>(Con, "Add_Blog", para, commandType: CommandType.StoredProcedure).ToList();
            return mlist;
        }
        public bool AddBlog(BlogTbl blg)
        {
            try
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("@Name", blg.CreatedBy);
                para.Add("@BlogTitle", blg.BlogTitle);
                para.Add("@BlogContent", blg.Content);
                para.Add("@Technology", blg.Technology);
                para.Add("@ProcID", 3);
                para.Add("@Status", dbType: DbType.String, direction: ParameterDirection.Output, size: 5215585);
                Con.Execute("Add_Blog", para, commandType: CommandType.StoredProcedure);
                string B = para.Get<string>("@Status");
                return Convert.ToBoolean(B);
            }
            catch (Exception E) { return false; }

        }
        public List<BlogTbl> GetBlogs()
        {
             List<BlogTbl> mlist = new List<BlogTbl>();
            DynamicParameters para = new DynamicParameters();
            para.Add("@ProcID", 4);
            mlist=SqlMapper.Query<BlogTbl>(Con,"Add_Blog",para,commandType:CommandType.StoredProcedure).ToList();
            return mlist;
        }
        public List<BlogTbl> GetBlogDetails(string Name)
        {
            List<BlogTbl> mlist = new List<BlogTbl>();
            DynamicParameters para = new DynamicParameters();
            para.Add("@ProcID", 5);
            para.Add("@Name", Name);
            mlist= SqlMapper.Query<BlogTbl>(Con, "Add_Blog", para, commandType: CommandType.StoredProcedure).ToList();
            return mlist;
        }
        public List<BlogTbl> ReadBlog(int id)
        {
            List<BlogTbl> mlist = new List<BlogTbl>();
            DynamicParameters para = new DynamicParameters();
            para.Add("@ProcID", 6);
            para.Add("@id", id);
            mlist = SqlMapper.Query<BlogTbl>(Con, "Add_Blog", para, commandType: CommandType.StoredProcedure).ToList();
            return mlist;
        }
        public bool AddNewUser(Users U)
        {
            DynamicParameters Para = new DynamicParameters();
            Para.Add("@Name", U.UserName);
            Para.Add("@Password", U.Password);
            Para.Add("@Image", U.ImageData);
            Para.Add("@ImageLength", U.File.ContentLength);
            Para.Add("@ImageName", U.File.FileName);
            Para.Add("@ProcID", 1);
            Con.Execute("Add_User", Para, commandType: CommandType.StoredProcedure);
            return true;
        }
        public List<Users> UsersList(int id=0)
        {
            DynamicParameters para = new DynamicParameters();
            if (id != 0) { para.Add("@ID", id); }
            para.Add("@ProcID", 2);
            List<Users> mlist = new List<Users>();
            mlist = SqlMapper.Query<Users>(Con, "Add_User", para, commandType: CommandType.StoredProcedure).ToList();
            return mlist;
        }
        public List<BlogTbl> GetCompleteBlogDetails(int id=0)
        {
            DynamicParameters Para = new DynamicParameters();
            if(id!=0){Para.Add("@ID",id);}
            Para.Add("@ProcID",7);
            List<BlogTbl> mlist = new List<BlogTbl>();
            mlist=SqlMapper.Query<BlogTbl>(Con,"Add_Blog",Para,commandType:CommandType.StoredProcedure).ToList();
            return mlist;
        }
        public List<BlogTbl> GetCompleteBlogDetailsByTechnology(int id = 0)
        {
            DynamicParameters Para = new DynamicParameters();
            if (id != 0) { Para.Add("@Technology", id); }
            Para.Add("@ProcID", 7);
            List<BlogTbl> mlist = new List<BlogTbl>();
            mlist = SqlMapper.Query<BlogTbl>(Con, "Add_Blog", Para, commandType: CommandType.StoredProcedure).ToList();
            return mlist;
        }
        public List<BlogTbl> GetCompleteBlogDetailsByAuthor(string id ="0")
        {
            try
            {
                DynamicParameters Para = new DynamicParameters();
                if (id != "0") { Para.Add("@Author", id); }
                Para.Add("@ProcID", 7);
                List<BlogTbl> mlist = new List<BlogTbl>();
                mlist = SqlMapper.Query<BlogTbl>(Con, "Add_Blog", Para, commandType: CommandType.StoredProcedure).ToList();
                return mlist;
            }
            catch (Exception E) { return null; }
        }
        public bool EditBlog(BlogTbl B)
        {
            try
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("@BlogContent", B.Content);
                para.Add("@BlogTitle", B.BlogTitle);
                para.Add("@Technology", B.Technology);
                para.Add("@ID", B.BlogID);
                para.Add("@ProcID", 8);
                Con.Execute("Add_Blog", para, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception E) { return false; }
        }
        public bool DeleteBlog(BlogTbl B)
        {
            try
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("@ID", B.BlogID);
                para.Add("@ProcID", 9);
                Con.Execute("Add_Blog", para, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception E) { return false; }
        }

        public bool EditUser(Users U)
        {
            try
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("@Name", U.UserName);
                para.Add("@Password", U.Password);
                para.Add("@ID", U.ID);
                if (U.File != null)
                {
                    byte[] data = new byte[U.File.ContentLength];
                    U.File.InputStream.Read(data, 0, U.File.ContentLength);
                    U.ImageData = data;
                    para.Add("@Image", data);
                    para.Add("@ImageLength", U.File.ContentLength);
                    para.Add("@ImageName", U.File.FileName);
                    para.Add("@ProcID", 4);
                }
                else
                {
                    para.Add("@ProcID", 3);
                }
                Con.Execute("Add_User", para, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception E) { return false; }
        }
        public bool DeleteUser(Users B)
        {
            try
            {
                DynamicParameters para = new DynamicParameters();
                para.Add("@ID", B.ID);
                para.Add("@ProcID", 5);
                Con.Execute("Add_User", para, commandType: CommandType.StoredProcedure);
                return true;
            }
            catch (Exception E) { return false; }
        }
    }
}
