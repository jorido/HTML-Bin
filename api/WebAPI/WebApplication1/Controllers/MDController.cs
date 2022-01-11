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
    public class MDController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"select MDId, MDName, Author, MDFile from dbo.MD";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        public string Post(MD md)
        {
            try
            {
                string query = @"insert into dbo.MD values ('" + md.MDName + @"', '" + md.Author + @"', '" + md.MDFile + @"')";
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
            catch (Exception)
            {
                return "User Addition Unsucessful";
            }
        }

        public string Put(MD md)
        {
            try
            {
                string query = @"update dbo.MD set MDName='" + md.MDName + @"', Author='" + md.Author + @"', MDFile='" + md.MDFile + @"' where MDId=" + md.MDId + @"";
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
                string query = @"delete from dbo.MD where MDId='" + id + @"'";
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

        [Route("api/MD/GetUserMD/{user}")]
        [HttpGet]
        public HttpResponseMessage GetUserMD(string user)
        {
            string query = @"select MDName from dbo.MD where Author='" + user + @"'";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/MD/GetMDFile/{Author}/{MDName}")]
        [HttpGet]
        public HttpResponseMessage GetUserMD(string Author, string MDName)
        {
            string query = @"select MDId, MDFile from dbo.MD where MDName='" + MDName + @"' and Author='" + Author + @"'" ;
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

        [Route("api/MD/MDExist/{Author}/{MDName}")]
        [HttpGet]
        public HttpResponseMessage MDExist(string Author, string MDName)
        {
            string query = @"select count(*) from dbo.MD where MDName='" + MDName + @"' and Author='" + Author + @"'";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["UserDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);
        }
    }
}
