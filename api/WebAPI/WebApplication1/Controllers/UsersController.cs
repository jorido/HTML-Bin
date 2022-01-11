using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [System.Web.Http.Cors.EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UsersController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"select usersId, usersEmail from dbo.Users";
            DataTable table = new DataTable();
            using(var con= new SqlConnection(ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(Users user)
        {
            try
            {
                string query = @"insert into dbo.Users values ('" + user.usersEmail + @"')";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "User Addition Sucessful!";
            }
            catch(Exception)
            {
                return "User Addition Unsucessful";
            }
        }

        public string Put(Users user)
        {
            try
            {
                string query = @"update dbo.Users set usersEmail='" + user.usersEmail + @"' where usersId="+user.usersId+@"";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "User Update Sucessful!";
            }
            catch (Exception)
            {
                return "User Update Unsucessful";
            }
        }

        public string Delete(int id)
        {
            try
            {
                string query = @"delete from dbo.Users where usersId='" + id + @"'";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "User Deletion Sucessful!";
            }
            catch (Exception)
            {
                return "User Deletion Unsucessful";
            }
        }
    }
}
