using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WebApplication2
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            con.Open();
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into bstore values('" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
            Label1.Text = "Data has been insterted";
            GridView1.DataBind();
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM bstore WHERE id = @value", con);
            cmd.Parameters.AddWithValue("@value", TextBox1.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            Label1.Text = "Data has been deleted";
            GridView1.DataBind();
            TextBox1.Text = "";

        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("UPDATE bstore SET Bookname = @Bookname, Author = @Author WHERE id = @id", con);
            cmd.Parameters.AddWithValue("@Bookname", TextBox2.Text);  // New value for Bookname
            cmd.Parameters.AddWithValue("@Author", TextBox3.Text);    // New value for Author
            cmd.Parameters.AddWithValue("@id", TextBox1.Text);        // ID of the record to update
            cmd.ExecuteNonQuery();
            con.Close();
            Label1.Text = "Data has been updated";
            GridView1.DataBind();
            TextBox1.Text = "";
            TextBox2.Text = "";
            TextBox3.Text = "";

        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("SELECT Bookname, Author FROM bstore WHERE ID = @id", con);
            cmd.Parameters.AddWithValue("@id", TextBox1.Text);  // Assuming you're selecting by ID

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())  // If a record is found
            {
                TextBox2.Text = reader["Bookname"].ToString();  // Display Bookname in TextBox2
                TextBox3.Text = reader["Author"].ToString();    // Display Author in TextBox3
                Label1.Text = "Data retrieved successfully";
            }
            else
            {
                Label1.Text = "No record found with the given ID";
            }

            reader.Close();
            con.Close();

        }
    }
}