using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace SpaceShooters
{
    public partial class Leaderboard : Form
    {
        private Leaderboard form;

        public Leaderboard()
        {
            InitializeComponent();
        }

        private void Leaderboard_Load(object sender, EventArgs e)
        {
            form = this;
            SqlCommand cmd = new SqlCommand("SELECT name AS NAME, highscore AS HIGHSCORE, date AS PLAYDATE FROM leaderboard ORDER BY highscore DESC", DB.conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            dgv.DataSource = dt;
        }

        private void backBtn_Click(object sender, EventArgs e)
        {
            form.Hide();
            Menu menu = new Menu();
            menu.ShowDialog();
            form.Close();
        }
    }
}
