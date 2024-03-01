using BL_FileHandle;
using RC_Resources;
using System;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using UI_UserInterFace;
using VO_FileHandle;

namespace FileHandlingSystem
{
    public partial class frm_MainForm : Form
    {
        private frm_UserForm userForm;
        private Resources rsc;
        private ValueLayerObject vlo;

        public frm_MainForm()
        {
            InitializeComponent();
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            ShowLoginForm();
        }

        private void ShowLoginForm()
        {
            using (LoginForm loginForm = new LoginForm())
            {
                DialogResult result = loginForm.ShowDialog();
                if (result == DialogResult.OK)
                {
                    InitializeElements();
                }
                else
                {
                    Application.Exit();
                }
            }
        }
        private void InitializeElements()
        {
            vlo = new ValueLayerObject();
            BusinessLogic.Initialize();
            userForm = new frm_UserForm(vlo);
            vlo.serialNumber = 1;
            vlo.values = null;
            rsc = new Resources();
            ShowData();
            ExtractData();
        }

        private void ShowData()
        {
            ui_Controls.AddColumnInGrid(grd_userData);

            btn_delete.Enabled = false;
            btn_update.Enabled = false;
            btnclearsearch.Enabled = false;

            userForm.Closed += (sender, e) =>
            {
                ExtractData();
            };
        }

        public void ExtractData()
        {
            if (ValueLayerObject.records.Count > 0)
            {
                ui_Controls.ExtractDataIntoGrid(grd_userData, vlo);

                btn_delete.Enabled = false;
                btn_update.Enabled = false;
            }

            btn_new.Enabled = true;
            grd_userData.Rows[0].Selected = false;
            grd_userData.Rows[grd_userData.Rows.Count - 1].Cells[0].Selected = true;
        }

        private void btn_new_Click(object sender, EventArgs e)
        {
            vlo.UserFormType = "New";
            userForm.Text = rsc.GetEnumDescription(Title.addeditform);
            userForm.ShowDialog();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            vlo.UserFormType = "Update";
            userForm.Text = rsc.GetEnumDescription(Title.addeditform);
            userForm.ShowDialog();
        }

        private void grid_userData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 1 && e.RowIndex <= grd_userData.Rows.Count)
            {
                DataGridViewRow selectedRow = grd_userData.Rows[e.RowIndex];
                if (selectedRow.Cells[ColumnNamesEnum.SerialNumber.ToString()].Value != null)
                {
                    btn_new.Enabled = false;
                    btn_update.Enabled = true;
                    btn_delete.Enabled = true;

                    vlo.values = null;
                    vlo.values = selectedRow.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value).ToArray();
                }
                else
                {
                    btn_update.Enabled = false;
                    btn_delete.Enabled = false;
                    btn_new.Enabled = true;
                }
            }
            else if (e.RowIndex == 0)
            {
                grd_userData.Rows[0].Selected = false;
            }
        }

        private void grid_userData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 1 && e.RowIndex < grd_userData.Rows.Count - 1)
            {
                DataGridViewRow selectedRow = grd_userData.Rows[e.RowIndex];
                vlo.values = selectedRow.Cells.Cast<DataGridViewCell>().Select(cell => cell.Value).ToArray();
                vlo.UserFormType = "View";
                userForm.Text = rsc.GetEnumDescription(Title.viewOnlyForm);
                userForm.ShowDialog();
            }
            else if (e.RowIndex == 0)
            {
                grd_userData.Rows[grd_userData.Rows.Count - 1].Selected = false;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            BusinessLogic.ReleaseObjects();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            if (vlo.values != null)
            {
                DialogResult wantToDelete = MessageBox.Show(rsc.GetEnumDescription(Messages.deleteRecord), rsc.GetEnumDescription(MessageCaptions.deleteRecord), MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (wantToDelete == DialogResult.Yes)
                {
                    ValueObject vo = new ValueObject();
                    BusinessLogic bl = new BusinessLogic();
                    vo.editMode = EditMode.Delete;
                    if (bl.Save(vo, vlo))
                    {
                        ExtractData();
                    }
                    else
                    {
                        MessageBox.Show("Record not deleted : " + vo.userDefineError);
                    }
                }
            }
        }

        private void grd_userData_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0)
            {
                grd_userData.Rows[0].Selected = false;
                DataGridViewRow selectedRow = grd_userData.Rows[0];
                ui_Controls.HideRowsWithEmptyCells(e.ColumnIndex, selectedRow.Cells[e.ColumnIndex].Value, grd_userData);
                btnclearsearch.Enabled = true;
            }
        }

        private void btnexit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnclearsearch_Click(object sender, EventArgs e)
        {
            textbox_search.Clear();

            foreach (DataGridViewCell cell in grd_userData.Rows[0].Cells)
            {
                cell.Value = "";
            }
            ExtractData();
            btnclearsearch.Enabled = false;
        }

        private void textbox_search_TextChanged(object sender, EventArgs e)
        {
            SearchRowsWithSearchText(textbox_search.Text);
            btnclearsearch.Enabled = true;
        }

        public void SearchRowsWithSearchText(string data)
        {
            if (data.ToString().Length > 0)
            {
                if (data.ToString().Length == 1)
                {
                    foreach (DataGridViewCell cell in grd_userData.Rows[0].Cells)
                    {
                        cell.Value = "";
                    }
                }
                for (int index = 1; index < grd_userData.Rows.Count - 1; index++)
                {
                    DataGridViewRow selectedRow = grd_userData.Rows[index];
                    bool rowVisible = false;
                    foreach (DataGridViewCell cell in selectedRow.Cells)
                    {
                        if ((cell.Value?.ToString().ToUpper()).Contains(data.ToUpper()))
                        {
                            rowVisible = true;
                            break;
                        }
                    }
                    if (!rowVisible)
                    {
                        selectedRow.Visible = false;
                    }
                    else
                    {
                        selectedRow.Visible = true;
                    }
                }
            }
        }

        private void btnaboutus_Click(object sender, EventArgs e)
        {
            AboutUs aboutUs = new AboutUs();
            aboutUs.ShowDialog();
        }

        private void grd_userData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
