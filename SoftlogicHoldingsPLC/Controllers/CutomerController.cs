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


        /* SP calls
         * 
         * 
         * USE [SoftlogicHoldingsPLC]
            GO


                    SET ANSI_NULLS ON
                    GO

            SET QUOTED_IDENTIFIER ON
            GO

            CREATE PROCEDURE[dbo].[sp_get_all_customers]
                    AS
            SELECT* FROM[BackOffice].[dbo].[Customer]
                    GO;
            GO


        USE [SoftlogicHoldingsPLC]
            GO


                    SET ANSI_NULLS ON
                    GO

            SET QUOTED_IDENTIFIER ON
            GO

            create PROCEDURE[dbo].[sp_insert_customer]
                    @name NVARCHAR(50)   = NULL   ,
                   @phone NVARCHAR(500)  = NULL   ,
                   @address NVARCHAR(500)  = NULL   ,
                   @email NVARCHAR(500)  = NULL
            AS
            BEGIN
                 SET NOCOUNT ON

                 INSERT INTO[dbo].[Customer]
                    (
                       name,
                       phone,
                       address,
                       email
                     )
                VALUES
                     (
                       @name,
                       @phone,
                       @address,
                       @email

                     )

            END
            GO





          USE[SoftlogicHoldingsPLC]
             GO

 
                    SET ANSI_NULLS ON
                    GO

            SET QUOTED_IDENTIFIER ON
            GO


            CREATE PROCEDURE[dbo].[sp_delete_customer]
                    @customer_id INT,
                  @name                         NVARCHAR(50)   = NULL   ,
                   @phone NVARCHAR(500)  = NULL   ,
                   @address NVARCHAR(500)  = NULL   ,
                   @email NVARCHAR(500)  = NULL
            AS
            BEGIN
                 SET NOCOUNT ON

            DELETE FROM[dbo].[Customer]
                    WHERE customer_id = @customer_id


            END
            GO


        	USE [SoftlogicHoldingsPLC]
	        GO


                SET ANSI_NULLS ON
                GO


            SET QUOTED_IDENTIFIER ON
            GO



            create PROCEDURE[dbo].[spUpdateCustomer]
                @customer_id INT,
                  @name                         NVARCHAR(50)   = NULL   ,
		           @phone NVARCHAR(500)  = NULL   ,
		           @address NVARCHAR(500)  = NULL   ,
		           @email NVARCHAR(500)  = NULL
        AS

            BEGIN
                 SET NOCOUNT ON


             UPDATE[dbo].[Customer]
                SET[name] =  @name
		          ,[phone] = @phone
		          ,[address] =  @address
		          ,[email] = @email
        WHERE customer_id =  @customer_id;


	        END

            GO






        */


    }
}
