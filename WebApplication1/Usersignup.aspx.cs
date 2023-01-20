using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1
{
    public partial class Usersignup : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
             
        }

        //This is member sign up button event
        protected void Button1_Click(object sender, EventArgs e)
        {
            if (checkMemberExists()) //checks if member id already exists
            {
                Response.Write("<script>alert('Member already exists with this Member ID, try other ID');</script>");
            }
            else
            { 
            signUpNewMember(); //called only when an unique memberID is added, if an member that already exists is written then it should return an error because database has memberID as a primary key
            }
        }

            //user defined method

        bool checkMemberExists()
        {

            try
            {   //object with a parameter strcon which is holding the connection string made in webconfig
                SqlConnection con = new SqlConnection(strcon);
                //check if its connected to the database, if closed then open
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                //sql query for checking if member id is already in the database

                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='"+TextBox9.Text.Trim()+"';", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);// use this query in sqlcommand and fill it inside this data table

                //if there is already an entry then return true, means this member id already exists
                if(dt.Rows.Count >= 1)
                {
                    return true;
                }
                else
                {
                    return false; // it will go to signUpNewMember
                }
            }
            catch (Exception ex)
            {       //whatever expection tht we caught .message, so we can see what the exception was
                Response.Write("<script>alert('" + ex.Message + "');</script>");
                return false;
            }
        }






            void signUpNewMember()
            {
                try
                {   //object with a parameter strcon which is holding the connection string made in webconfig
                    SqlConnection con = new SqlConnection(strcon);
                    //check if its connected to the database, if closed then open
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    //sql query for inputs and column names from databes, for values i  used placeholder

                    SqlCommand cmd = new SqlCommand("INSERT INTO member_master_tbl(full_name,dob,contact_no,email,state,city,pincode,full_address,member_id,password,account_status) values(" +
                        "@full_name,@dob,@contact_no,@email,@state,@city,@pincode,@full_address,@member_id,@password,@account_status)", con);

                    cmd.Parameters.AddWithValue("@full_name", TextBox1.Text.Trim());
                    cmd.Parameters.AddWithValue("@dob", TextBox2.Text.Trim());
                    cmd.Parameters.AddWithValue("@contact_no", TextBox3.Text.Trim());
                    cmd.Parameters.AddWithValue("@email", TextBox4.Text.Trim());
                    cmd.Parameters.AddWithValue("@state", DropDownList1.SelectedItem.Value);
                    cmd.Parameters.AddWithValue("@city", TextBox6.Text.Trim());
                    cmd.Parameters.AddWithValue("@pincode", TextBox7.Text.Trim());
                    cmd.Parameters.AddWithValue("@full_address", TextBox8.Text.Trim());
                    cmd.Parameters.AddWithValue("@member_id", TextBox9.Text.Trim());
                    cmd.Parameters.AddWithValue("@password", TextBox10.Text.Trim());
                    cmd.Parameters.AddWithValue("@account_status", "pending");
                    cmd.ExecuteNonQuery(); //take all placeholders and execute the query
                    con.Close();

                    //when the button is pressed i get the 'Sign up Successful. Go to user login to login' response
                    Response.Write("<script>alert('Sign up Successful. Go to user login to login');</script>");
                }
                catch (Exception ex)
                {       //whatever expection tht we caught .message, so we can see what the exception was
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
        
    }
}