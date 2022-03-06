using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using SoftlogicHoldingsPLC.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SoftlogicHoldingsPLC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CutomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CutomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult Get() {
            DataTable table = new DataTable();
            try{
                string query = @"SELECT * FROM dbo.Customer";
                string sqlDataSource = _configuration.GetConnectionString("SoftlogicAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource)){
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon)){
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message); Console.WriteLine(ex.StackTrace); Console.WriteLine(ex.InnerException);

            }
        
            return new JsonResult(table);
        }

        [HttpPost]
        public JsonResult Post(Customer customer)
        {
            DataTable table = new DataTable();
            try
            {
                string query = @" INSERT INTO dbo.Customer (name, phone, address, email) VALUES ( '" + customer.name + @"', '" + customer.phone + @"', '" + customer.address + @"', '" + customer.email + @"' ) ";
                string sqlDataSource = _configuration.GetConnectionString("SoftlogicAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); Console.WriteLine(ex.StackTrace); Console.WriteLine(ex.InnerException);
                return new JsonResult("Failed!.");

            }

            return new JsonResult("Add Successfully!.");
        }

        [HttpPut]
        public JsonResult Put(Customer customer)
        {
            DataTable table = new DataTable();
            try
            {
                string query = @" UPDATE dbo.Customer SET name = '" + customer.name + @"', phone = '" + customer.phone + @"', address = '" + customer.address + @"', email = '" + customer.email + @"' WHERE customer_id = " + customer.customer_id + @" ";
                string sqlDataSource = _configuration.GetConnectionString("SoftlogicAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); Console.WriteLine(ex.StackTrace); Console.WriteLine(ex.InnerException);
                return new JsonResult("Failed!.");

            }

            return new JsonResult("Updated Successfully!.");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            DataTable table = new DataTable();
            try
            {
                string ex_query = @" DELETE FROM dbo.Customer WHERE customer_id = "+ id + @"";
                string sqlDataSource = _configuration.GetConnectionString("SoftlogicAppCon");
                SqlDataReader myReader;
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(ex_query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message); Console.WriteLine(ex.StackTrace); Console.WriteLine(ex.InnerException);
                return new JsonResult("Failed!.");

            }

            return new JsonResult("Deleted Successfully!.");
        }


   


    }
}
