using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Configuration.Provider;
using Microsoft.EntityFrameworkCore;

namespace API_CRUD_ADO.Models
{
    public class EmployeeDBContext : DbContext
    {

        public readonly string cs;

        public EmployeeDBContext(IConfiguration configuration)
        {
            cs = configuration.GetConnectionString("dbcs");
        }


        public List<Employee> GetEmployees()
        {
            List<Employee> EmployeeList = new List<Employee>();
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Employee emp = new Employee();
                emp.ID = Convert.ToInt32(reader[0].ToString());
                emp.Name = (reader[1].ToString());
                emp.Location = (reader[2].ToString());
                emp.Dept = (reader[3].ToString());
                emp.Gender = (reader[4].ToString());

                EmployeeList.Add(emp);
            }

            con.Close();
            return EmployeeList;

        }

        public string AddEmployees(Employee emp)
        {
            SqlConnection con =new SqlConnection(cs);
            con.Open();

            SqlCommand cmd= new SqlCommand("spAddEmployee",con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Name", emp.Name);
            cmd.Parameters.AddWithValue("@Location", emp.Location);
            cmd.Parameters.AddWithValue("@Gender", emp.Gender);
            cmd.Parameters.AddWithValue("@Dept", emp.Dept);
            int count = cmd.ExecuteNonQuery();

            if(count > 0)
            {
                return "Added Successfully";
            }
            else
            {
               return "Failed to Add";
            }

        }
    }
}
