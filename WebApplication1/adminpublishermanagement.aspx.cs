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
    public partial class adminpublishermanagement : System.Web.UI.Page
    {
        //connection to database
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            GridView1.DataBind();
        }
        //ADD
        protected void Button2_Click(object sender, EventArgs e)
        {
            if (checkIfPublisherExists())
            {
                Response.Write("<script>alert('Publisher with this ID already exists. You cannot add another Publisher with the same Author ID');</script>");
            }
            else
            {
                addNewPublisher();
            }
        }
        //UPDATE
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (checkIfPublisherExists())
            {
                UpdatePublisher();

            }
            else
            {
                Response.Write("<script>alert('Publisher does not exists');</script>");
            }
        }
        //DELETE
        protected void Button4_Click(object sender, EventArgs e)
        {
            if (checkIfPublisherExists())
            {
                deletePublisher();

            }
            else
            {
                Response.Write("<script>alert('Publisher does not exists');</script>");
            }
        }
        //GO
        protected void Button1_Click(object sender, EventArgs e)
        {
            getPublisherID();
        }



        //user defined funciton
              void getPublisherID()
        {
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }

                SqlCommand cmd = new SqlCommand("SELECT * FROM publisher_master_tbl WHERE publisher_id='" + TextBox1.Text.Trim() + "';", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    TextBox2.Text = dt.Rows[0][1].ToString();
                }
                else
                {
                    Response.Write("<script>alert('Invalid Publisher ID');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");

            }
        }

        void deletePublisher()
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
                SqlCommand cmd = new SqlCommand("DELETE FROM publisher_master_tbl WHERE publisher_id='" + TextBox1.Text.Trim() + "'", con);


                cmd.ExecuteNonQuery(); //take all placeholders and execute the query
                con.Close();
                Response.Write("<script>alert('Publisher Deleted Successfully');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {       //whatever expection that we caught,we .message, so we can see what the exception was
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }


        void UpdatePublisher()
        {///the same as addNewPublisher we
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
                SqlCommand cmd = new SqlCommand("UPDATE publisher_master_tbl SET publisher_name=@publisher_name WHERE publisher_id='" + TextBox1.Text.Trim() + "'", con);

                cmd.Parameters.AddWithValue("@publisher_name", TextBox2.Text.Trim());
                cmd.ExecuteNonQuery(); //take all placeholders and execute the query
                con.Close();

                //when the button is pressed i get the 'Publisher updated successfully' response
                Response.Write("<script>alert('Publisher Upated Successfully');</script>");
                clearForm();
                GridView1.DataBind();
            }
            catch (Exception ex)
            {       //whatever expection tht we caught .message, so we can see what the exception was
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
        }




        void addNewPublisher()
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
                SqlCommand cmd = new SqlCommand("INSERT INTO publisher_master_tbl(publisher_id,publisher_name) values(" +
                    "@a1,@a2)", con);

                cmd.Parameters.AddWithValue("@a1", TextBox1.Text.Trim());
                cmd.Parameters.AddWithValue("@a2", TextBox2.Text.Trim());
                cmd.ExecuteNonQuery(); //take all placeholders and execute the query
                con.Close();

                //when the button is pressed i get the 'publisher added successfully' response
                Response.Write("<script>alert('Publisher added successfully');</script>");
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


        bool checkIfPublisherExists()
        {
            try
            {   //object with a parameter strcon which is holding the connection string made in webconfig
                SqlConnection con = new SqlConnection(strcon);
                //check if its connected to the database, if closed then open
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                //sql query for checking if publisher id is already in the database

                SqlCommand cmd = new SqlCommand("SELECT * FROM publisher_master_tbl WHERE publisher_id='" + TextBox1.Text.Trim() + "';", con);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);// use this query in sqlcommand and fill it inside this data table

                //if there is already an entry then return true, means this book id already exists
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