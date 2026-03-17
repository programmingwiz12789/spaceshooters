using System;
using System.Windows.Forms;

namespace SpaceShooters
{
    public partial class Controls : Form
    {
        private Controls form;

        public Controls()
        {
            InitializeComponent();
        }

        private void Controls_Load(object sender, EventArgs e)
        {
            form = this;
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
