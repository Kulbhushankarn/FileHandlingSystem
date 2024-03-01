using System;
using System.Windows.Forms;
using VO_FileHandle;
using BL_FileHandle;
using RC_Resources;
using System.Diagnostics.Eventing.Reader;

namespace FileHandlingSystem
{
    public partial class frm_UserForm : Form
    {
        public long serialNum;
        private string operationperformed;
        readonly Resources rsc;
        readonly ValueLayerObject  vlo;
        
        public frm_UserForm(ValueLayerObject vlo)
        {
            InitializeComponent();
            
            foreach (Qualification day in Enum.GetValues(typeof(Qualification)))
            {
                cb_qualification.Items.Add(day);
            }

            this.vlo = vlo;
            textbox_serialNumber.Enabled = false;
            operationperformed = "";
            rsc = new Resources();
        }

        private void InitializeElement()
        {
            dtp_dateofBirth.Format = DateTimePickerFormat.Short;
            dtp_joiningDate.Format = DateTimePickerFormat.Short;
            dtp_dateofBirth.Value = DateTime.Now.Date;
            dtp_joiningDate.Value = DateTime.Now.Date;

            if (vlo.UserFormType == "New")
            {
                DisableScrollButtons();
                btn_Edit.Enabled = false;
                textbox_serialNumber.Text = vlo.serialNumber.ToString();
            }
            else if (vlo.UserFormType == "Update")
            {
                DisableScrollButtons();
                btn_add.Enabled = false;
                serialNum = FindRowWithSerialNumber(Convert.ToInt64(vlo.values[0]?.ToString()));
                InsertDataInViewForm(serialNum);
            }
            else if (vlo.UserFormType == "View")
            {
                btn_add.Visible = false;
                btn_clear.Visible = false;
                btn_Edit.Visible = false;
                serialNum = FindRowWithSerialNumber(Convert.ToInt64(vlo.values[0]?.ToString()));
                EnableButtonOfView(serialNum);
                InsertDataInViewForm(serialNum);
                OpenFormInReadOnly();
            }
        }

        private void OpenFormInReadOnly()
        {
            cb_prefix.Enabled = false;
            textbox_firstname.Enabled = false;
            textbox_lastname.Enabled = false;
            textbox_middlename.Enabled = false;
            cb_qualification.Enabled = false;
            textbox_currentcompany.Enabled = false;
            textbox_currentaddress.Enabled = false;
            dtp_dateofBirth.Enabled = false;
            dtp_joiningDate.Enabled = false;
        }

        private void InsertDataInForm()
        {
            textbox_serialNumber.Text = vlo.values[0]?.ToString();
            cb_prefix.Text = vlo.values[1]?.ToString();
            textbox_firstname.Text = vlo.values[2]?.ToString();
            textbox_middlename.Text = vlo.values[3]?.ToString();
            textbox_lastname.Text = vlo.values[4]?.ToString();
            cb_qualification.Text = vlo.values[6]?.ToString();
            textbox_currentcompany.Text = vlo.values[7]?.ToString();
            textbox_currentaddress.Text = vlo.values[9]?.ToString();
            try
            {
                dtp_dateofBirth.Value = DateTime.Parse(vlo.values[5].ToString());
                dtp_joiningDate.Value = DateTime.Parse(vlo.values[8].ToString());
            }
            catch
            {
                dtp_dateofBirth.Visible = false;
                dtp_joiningDate.Visible = false;
            }
        }

        private void InsertDataInViewForm(long index)
        {
            textbox_serialNumber.Text = vlo.userData[index, 0];
            cb_prefix.Text = vlo.userData[index, 1];
            textbox_firstname.Text = vlo.userData[index, 2];
            textbox_middlename.Text = vlo.userData[index, 3];
            textbox_lastname.Text = vlo.userData[index, 4];
            cb_qualification.Text = vlo.userData[index, 6];
            textbox_currentcompany.Text = vlo.userData[index, 7];
            textbox_currentaddress.Text = vlo.userData[index, 9];
            try
            {
                dtp_dateofBirth.Value = DateTime.Parse(vlo.userData[index, 5]);
                dtp_joiningDate.Value = DateTime.Parse(vlo.userData[index, 8]);
            }
            catch
            {
                dtp_dateofBirth.Visible = false;
                dtp_joiningDate.Visible = false;
            }
        }

        private void DisableScrollButtons()
        {
            btn_first.Visible = false;
            btn_last.Visible = false;
            btn_previous.Visible = false;
            btn_next.Visible = false;
        }

        private void EnableElements()
        {
            btn_first.Visible = true;
            btn_last.Visible = true;
            btn_previous.Visible = true;
            btn_next.Visible = true;
            btn_add.Visible = true;
            btn_Edit.Visible = true;
            btn_clear.Visible = true;
            btn_add.Enabled = true;
            btn_Edit.Enabled = true;

            cb_prefix.Enabled = true;
            textbox_firstname.Enabled = true;
            textbox_lastname.Enabled = true;
            textbox_middlename.Enabled = true;
            cb_qualification.Enabled = true;
            textbox_currentcompany.Enabled = true;
            textbox_currentaddress.Enabled = true;
            dtp_dateofBirth.Enabled = true;
            dtp_joiningDate.Enabled = true;
        }

        private void UserForm_Load(object sender, EventArgs e)
        {
            InitializeElement();
        }

        private string GetPaddedInputdata()
        {
            string data = "";
            data += PadString(textbox_serialNumber.Text, 18);
            data += PadString(cb_prefix.Text, 7);
            data += PadString(textbox_firstname.Text, 50);
            data += PadString(textbox_middlename.Text, 25);
            data += PadString(textbox_lastname.Text, 50);
            data += PadString(dtp_dateofBirth.Text, 10);
            if (Enum.IsDefined(typeof(Qualification), cb_qualification.Text))
            {
                Qualification qualification = (Qualification)Enum.Parse(typeof(Qualification), cb_qualification.Text);
                int enumIndex = (int)qualification;
                data += PadString(enumIndex.ToString(), 2);
            }
            else
            {
                data += PadString("-1", 2);
            }
            data += PadString(textbox_currentcompany.Text, 255);
            data += PadString(dtp_joiningDate.Text, 10);
            data += PadString(textbox_currentaddress.Text, 500);

            return data;
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            string data = GetPaddedInputdata();
            ValueObject vo = new ValueObject(data);
            BusinessLogic bl = new BusinessLogic();
            vo.editMode = EditMode.Create;
            if (bl.Save(vo, vlo))
            {
                operationperformed = "saved";
                this.Close();
            }
            else
            {
                MessageBox.Show(rsc.GetEnumDescription(vo.error), rsc.GetEnumDescription(vo.caption), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        public static string PadString(string input, int length)
        {
            if (input.Length >= length)
            {
                return input.Substring(0, length); 
            }
            else
            {
                return input.PadRight(length);
            }
        }

        private bool checkChange()
        {
            if (vlo.UserFormType == "Update")
            {
                if (cb_prefix.Text != vlo.values[1]?.ToString() || textbox_firstname.Text != vlo.values[2]?.ToString() || textbox_middlename.Text != vlo.values[3]?.ToString()
                    || textbox_lastname.Text != vlo.values[4]?.ToString() || cb_qualification.Text != vlo.values[6]?.ToString() || textbox_currentcompany.Text != vlo.values[7]?.ToString()
                    || textbox_currentaddress.Text != vlo.values[9]?.ToString())
                {
                    return true;
                }
                else if (dtp_dateofBirth.Value != Convert.ToDateTime(vlo.values[5]) || dtp_joiningDate.Value != Convert.ToDateTime(vlo.values[8]))
                {
                    if (dtp_dateofBirth.Visible || dtp_joiningDate.Visible)
                    {
                        return true;
                    }
                }

            }
            else if (vlo.UserFormType == "New")
            {
                if (cb_prefix.Text != "" || textbox_firstname.Text != "" || textbox_middlename.Text != ""
                    || textbox_lastname.Text != "" || cb_qualification.Text != "" || textbox_currentcompany.Text != ""
                    || textbox_currentaddress.Text != "")
                {
                    return true;
                }
                else if (dtp_dateofBirth.Value != DateTime.Now.Date || dtp_joiningDate.Value != DateTime.Now.Date)
                {
                    return true;
                }
            }
            return false;
        }

        private void btn_Edit_Click(object sender, EventArgs e)
        {
            if (checkChange())
            {
                string dataUpdated = GetPaddedInputdata();

                ValueObject vo = new ValueObject(dataUpdated);
                BusinessLogic bl = new BusinessLogic();
                vo.editMode = EditMode.Update;
                if (bl.Save(vo, vlo))
                {
                    operationperformed = "updated";
                    this.Close();
                }
                else
                {
                    return;
                }
            }
            operationperformed = "updated";
            this.Close();
        }

        private void ClearInEditMode()
        {
            if (vlo.UserFormType == "Update")
            {
                vlo.UserFormType = "New";
                textbox_serialNumber.Text = vlo.serialNumber.ToString();
                btn_add.Enabled = true;
                btn_Edit.Enabled = false;
            }
        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            if (!checkChange())
            {
                Cleardata();
                ClearInEditMode();
            }
            else
            {
                DialogResult wantToClear = MessageBox.Show(rsc.GetEnumDescription(Messages.clearForm), rsc.GetEnumDescription(MessageCaptions.clearForm), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                if (wantToClear == DialogResult.OK)
                {
                    Cleardata();
                    ClearInEditMode();
                }
            }
        }
        /// <summary>
        /// This is clear button function 
        /// </summary>

        private void Cleardata()
        {
            cb_prefix.Text = "";
            textbox_firstname.Text = "";
            textbox_lastname.Text = "";
            textbox_middlename.Text = "";
            cb_qualification.Text = "";
            textbox_currentcompany.Text = "";
            textbox_currentaddress.Text = "";
            dtp_dateofBirth.Text = "";
            dtp_joiningDate.Text = "";
            cb_prefix.SelectedIndex = -1;
            cb_qualification.SelectedIndex = -1;
            dtp_dateofBirth.Value = DateTime.Now.Date;
            dtp_joiningDate.Value = DateTime.Now.Date;
        }


        private int FindRowWithSerialNumber(long serial)
        {
            for (int i = 0; i < vlo.userData.GetLength(0); i++)
            {
                if (vlo.userData[i, 0] == serial.ToString())
                {
                    return i;
                }
            }
            return -1;
        }

        private void EnableButtonOfView(long index)
        {
            if (index == 0)
            {
                btn_previous.Enabled = false;
                btn_first.Enabled = false;
                btn_next.Enabled = true;
                btn_last.Enabled = true;
            }
            else if (index == vlo.userData.GetLength(0) - 1)
            {
                btn_next.Enabled = false;
                btn_last.Enabled = false;
                btn_previous.Enabled = true;
                btn_first.Enabled = true;
            }
            else
            {
                btn_previous.Enabled = true;
                btn_first.Enabled = true;
                btn_next.Enabled = true;
                btn_last.Enabled = true;
            }
        }

        private void btn_previous_Click(object sender, EventArgs e)
        {
            if (serialNum > 0)
            {
                serialNum--;
                EnableButtonOfView(serialNum);
                InsertDataInViewForm(serialNum);
            }
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            if (serialNum < vlo.userData.GetLength(0) - 1)
            {
                serialNum++;
                EnableButtonOfView(serialNum);
                InsertDataInViewForm(serialNum);
            }
        }

        private void btn_first_Click(object sender, EventArgs e)
        {
            serialNum = 0;
            EnableButtonOfView(serialNum);
            InsertDataInViewForm(serialNum);
        }

        private void btn_last_Click(object sender, EventArgs e)
        {
            serialNum = vlo.userData.GetLength(0) - 1;
            EnableButtonOfView(serialNum);
            InsertDataInViewForm(serialNum);
        }

        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            EnableElements();
            Cleardata();
        }

        private void UserForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (operationperformed != "saved" && operationperformed != "updated")
            {
                if (checkChange())
                {
                    DialogResult wantToClear = MessageBox.Show(rsc.GetEnumDescription(Messages.clearForm), rsc.GetEnumDescription(MessageCaptions.clearForm), MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (wantToClear != DialogResult.OK)
                    {
                        e.Cancel = true;
                    }
                }
            }
        }
    }
}
