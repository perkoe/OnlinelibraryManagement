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
    public partial class adminauthormanagement : System.Web.UI.Page
    {
        //connection to database
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        //add button click
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
                Response.Write("<script>alert('Author with this ID already exists. You cannot add another Author with the same Author ID');</script>");
            }
            else
            {
                addNewAuthor();
            }
        }

        //update button click
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
                UpdateAuthor();
               
            }
            else
            {
                Response.Write("<script>alert('Author does not exists');</script>");
            }
        }
        //delete button click
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfAuthorExists())
            {
               deleteAuthor();

            }
            else
            {
                Response.Write("<script>alert('Author does not exists');</script>");
            }
        }

        //go button click
        protected void Button1_Click(object sender, EventArgs e)
        {
            getAuthorID();
        }

        //user defined funciton


        void getAuthorID()
        {
            try
            {  
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                
                SqlCommand cmd = new SqlCommand("SELECT * FROM author_master_tbl WHERE author_id='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Author ID');</script>");
                }
            }
            catch (Exception ex)
            {       
                Response.Write("<script>alert('" + ex.Message + "');</script>");
               
            }
        }

        void deleteAuthor()
        {
            try
            {   //object with a parameter strcon which is holding the connection string made in webconfig
                SqlConnection con = new SqlConnection(strcon);
                //check if its connected to the database, if closed then open
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                //sql query for inputs and column names from databes, for values i used placeholder

                //vaues are placeholders, you can name them whatever u want.
                SqlCommand cmd = new SqlCommand("DELETE FROM author_master_tbl WHERE author_id='" + TextBox1.Text.Trim() + "'", con);

                
                cmd.ExecuteNonQuery(); //take all placeholders and execute the query
                con.Close();
                Response.Write("<script>alert('Author Deleted Successfully');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {       //whatever expection that we caught,we .message, so we can see what the exception was
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        void UpdateAuthor()
        {///the same as addNewAuthor we
            try
            {   //object with a parameter strcon which is holding the connection string made in webconfig
                SqlConnection con = new SqlConnection(strcon);
                //check if its connected to the database, if closed then open
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                //sql query for inputs and column names from databes, for values i used placeholder

                //vaues are placeholders, you can name them whatever u want.
                SqlCommand cmd = new SqlCommand("UPDATE author_master_tbl SET author_name=@author_name WHERE author_id='"+TextBox1.Text.Trim()+"'", con);

                cmd.Parameters.AddWithValue("@author_name", TextBox2.Text.Trim());
                cmd.ExecuteNonQuery(); //take all placeholders and execute the query
                con.Close();

                //when the button is pressed i get the 'Author updated successfully' response
                Response.Write("<script>alert('Author Upated Successfully');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {       //whatever expection tht we caught .message, so we can see what the exception was
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }




        void addNewAuthor()
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

                //vaues are placeholders, you can name them whatever u want.
                SqlCommand cmd = new SqlCommand("INSERT INTO author_master_tbl(author_id,author_name) values(" +"@a1,@a2)", con);

                cmd.Parameters.AddWithValue("@a1", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@a2", TextBox2.Text.Trim());
                cmd.ExecuteNonQuery(); //take all placeholders and execute the query
                con.Close();

                //when the button is pressed i get the 'Author added successfully' response
                Response.Write("<script>alert('Author added successfully');</script>");
                clearForm();
                //it refreshes the data shown on website so when we add,delete it shows right away. 
                //it binds back to data source, then the data source connects to 
                GridView1.DataBind();


            }
            catch (Exception ex)
            {       //whatever expection tht we caught .message, so we can see what the exception was
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }
    

        bool checkIfAuthorExists()
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

                SqlCommand cmd = new SqlCommand("SELECT * FROM author_master_tbl WHERE author_id='" + TextBox1.Text.Trim() + "';", con);
            
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);// use this query in sqlcommand and fill it inside this data table

                //if there is already an entry then return true, means this member id already exists
                if (dt.Rows.Count >= 1)
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

        void clearForm()
        {
            TextBox1.Text = "";
            TextBox2.Text = "";
        }

        protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
        {

        }
    }
}
