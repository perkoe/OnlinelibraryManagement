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
    public partial class adminusermanagement : System.Web.UI.Page
    {
        string strcon = ConfigurationManager.ConnectionStrings["con"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GridView1.DataBind();
        }

        //DELETE BUTTON
        protected void Button2_Click(object sender, EventArgs e)
        {
           deleteMember();
        }
        //GO BLUE
        protected void LinkButton4_Click(object sender, EventArgs e)
        {
            getMemberID();
        }

        //GREEN
        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            updateMemberStatusByID("active");
        }
        //YELLOW
        protected void LinkButton2_Click(object sender, EventArgs e)
        {
            updateMemberStatusByID("pending");
        }
        //RED
        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            updateMemberStatusByID("deactive");
        }



        //USER DEFINED

        bool checkIfMemberExists()
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

                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='" + TextBox1.Text.Trim() + "';", con);

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
    void deleteMember()
        {
            if (checkIfMemberExists())
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
                    SqlCommand cmd = new SqlCommand("DELETE FROM member_master_tbl WHERE member_id='" + TextBox1.Text.Trim() + "'", con);


                    cmd.ExecuteNonQuery(); //take all placeholders and execute the query
                    con.Close();
                    Response.Write("<script>alert('Member Deleted Successfully');</script>");
                    clearForm();
                    GridView1.DataBind();
                }
                catch (Exception ex)
                {       //whatever expection that we caught,we .message, so we can see what the exception was
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID');</script>");
            }
            
        }


        void updateMemberStatusByID(string status)
        {
            if (checkIfMemberExists())
            {
                try
                {   //repeat from usersignup
                    SqlConnection con = new SqlConnection(strcon);
                    if (con.State == System.Data.ConnectionState.Closed)
                    {
                        con.Open();
                    }
                    SqlCommand cmd = new SqlCommand("UPDATE member_master_tbl SET account_status='" + status + "' WHERE member_id='" + TextBox1.Text.Trim() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    GridView1.DataBind();
                    Response.Write("<script>alert('Member Status Updated');</script>");

                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('" + ex.Message + "');</script>");
                }
            }
            else
            {
                Response.Write("<script>alert('Invalid Member ID');</script>");
            }
        }

        void getMemberID()
        {
            try
            {   //repeat from usersignup
                SqlConnection con = new SqlConnection(strcon);
                if (con.State == System.Data.ConnectionState.Closed)
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("SELECT * FROM member_master_tbl WHERE member_id='" + TextBox1.Text.Trim()+ "'", con);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TextBox2.Text = dr.GetValue(0).ToString();
                        TextBox7.Text = dr.GetValue(10).ToString();
                        TextBox8.Text = dr.GetValue(1).ToString();
                        TextBox3.Text = dr.GetValue(2).ToString();
                        TextBox4.Text = dr.GetValue(3).ToString();
                        TextBox9.Text = dr.GetValue(4).ToString();
                        TextBox10.Text = dr.GetValue(5).ToString();
                        TextBox11.Text = dr.GetValue(6).ToString();
                        TextBox6.Text = dr.GetValue(7).ToString(); 
                    }
                }
                else
                {
                    Response.Write("<script>alert('Invalid credentals');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.Message + "');</script>");
            }
            
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {



        }

        void clearForm()
        {
            TextBox2.Text ="";
            TextBox7.Text ="";
            TextBox8.Text ="";
            TextBox3.Text ="";
            TextBox4.Text ="";
            TextBox9.Text ="";
            TextBox10.Text ="";
            TextBox11.Text ="";
            TextBox6.Text ="";
        }
    }
}