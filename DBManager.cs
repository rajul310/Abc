using MySql.Data.MySqlClient;
using BOL;
using System.Data;
using Org.BouncyCastle.Asn1.Cms;

namespace DAL
{
    public class DBManager
    {

        public static string connection = @"server=localhost;port=3306;user=root;password=ojas;database=dotnetprojs";

        public static List<Employee> GetAllEmployees()
        {
            List<Employee> empList= new List<Employee>();

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connection;
            
            try
            {

                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                
                cmd.Connection = conn;

                string query = "Select * from employees";

                cmd.CommandText = query;

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    int id = int.Parse(reader["id"].ToString());
                    string firstName = reader["firstname"].ToString();
                    string lastName = reader["lastname"].ToString();
                    string email = reader["email"].ToString();
                    double salary = double.Parse(reader["salary"].ToString());
                    DateTime dob = DateTime.Parse(reader["dob"].ToString());
                    string password = reader["password"].ToString();
                    Department dept = Enum.Parse<Department>(reader["dept"].ToString());

                    Employee emp = new Employee(id, firstName, lastName, email, salary, dob, password, dept);
                    //Console.WriteLine(emp);
                    empList.Add(emp);

                }


            }catch(Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

            return empList;
        }  
        

        public static bool AddEmployee(Employee emp)
        {
            bool status = false;
            string query = "insert into employees values(" + emp.Id + ",'" + emp.FirstName + "','" + emp.LastName + "','" + emp.Email + "'," + emp.Salary + ",'" + emp.Dob.ToString("yyyy/MM/dd")+"','" + emp.Password + "','" + emp.Dept + "');";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connection;
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query,conn);
                cmd.ExecuteNonQuery();
                status = true;

            }catch(Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return status;
        }



        public static bool DeleteEmployee( int id)
        {
            bool status = false;
            MySqlConnection conn=new MySqlConnection();
            conn.ConnectionString= connection;
            try {
                conn.Open();
                string query = "delete from employees where id=" + id;
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
            }catch(Exception e )
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }
            return status;
        }


        public static Employee GetEmployee(int id)
        {
            Employee emp = null;
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connection;
            try
            {
                conn.Open();
               
                string query = "select * from employees where id=" + id;
               
                MySqlCommand cmd = new MySqlCommand(query,conn);
                
                
                
                MySqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int empid = int.Parse(reader["id"].ToString());
                    string firstName = reader["firstname"].ToString();
                    string lastname = reader["lastname"].ToString();
                    string email = reader["email"].ToString();
                    double salary = Double.Parse(reader["salary"].ToString());
                    DateTime dob = DateTime.Parse(reader["dob"].ToString());
                    string password = reader["password"].ToString();
                    Department dept = Enum.Parse<Department>(reader["dept"].ToString());

                    emp = new Employee(empid, firstName, lastname, email, salary, dob, password, dept);
                }

            }
            catch(Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

            return emp;
        }

        public static bool UpdateEmployeeById(Employee emp)
        {
            bool status = false;
            string query = "UPDATE employees SET firstname='" + emp.FirstName + "', lastname='" + emp.LastName + "', email='" + emp.Email + "', salary=" + emp.Salary + ", dob='" + emp.Dob.ToString("yyyy/MM/dd") + "', password='" + emp.Password + "', dept='" + emp.Dept.ToString() + "' WHERE id=" + emp.Id;

            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = connection;
            try
            {
                
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query,conn);
                cmd.ExecuteNonQuery();
                status = true;

            }catch(Exception e)
            {
                throw e;
            }
            finally
            {
                conn.Close();
            }

            return status;
        }

    }

}