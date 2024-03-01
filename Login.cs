using BL_FileHandle;
using System;
using System.Windows.Forms;

namespace FileHandlingSystem
{
    public partial class LoginForm : Form
    {
        private BusinessLogic businessLogic;

        // Use this constructor if you want to create a new instance of BusinessLogic internally
        public LoginForm()
        {
            InitializeComponent();
            this.businessLogic = new BusinessLogic(); 
        }

        // Use this constructor if you want to inject an existing instance of BusinessLogic
        public LoginForm(BusinessLogic businessLogic)
        {
            InitializeComponent();
            this.businessLogic = businessLogic; 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string userId = txtUsername.Text;
            string password = txtPassword.Text;

            if (businessLogic.AuthenticateUser(userId, password))
            {
                // Successful login
                DialogResult = DialogResult.OK;
                Close();
            }
            else
            {
                // Authentication failed
                MessageBox.Show("Invalid username or password. Please try again.", "Authentication Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            // If the user cancels the login, set DialogResult to Cancel and close the form
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
