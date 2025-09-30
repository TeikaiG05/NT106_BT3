using Guna.UI2.AnimatorNS;
using Guna.UI2.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace NT106_BT2
{
    public partial class Login_Signup : Form
    {
        private Guna2Transition Guna2Transistion1;

        public Login_Signup()
        {
            InitializeComponent();
            Guna2Transistion1 = new Guna2Transition();
            pn_login.Visible = true;
            pn_regis.Visible = false;
        }

        private void cToLogin_Click(object sender, EventArgs e)
        {
            pn_regis.Visible = false;
            Guna2Transistion1.ShowSync(pn_login);
        }

        private void cSignup_Click(object sender, EventArgs e)
        {
            pn_regis.Visible = true;
            Guna2Transistion1.ShowSync(pn_regis);
        }

        private void pn_regis_Paint(object sender, PaintEventArgs e)
        {

        }

        

        string[] months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun",
                        "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        private void Login_Signup_Load(object sender, EventArgs e)
        {
            InitYearComboBox();
            InitMonthComboBox();
            InitDayComboBox();
        }

        private void cYear_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cMonth_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void cDay_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void InitYearComboBox()
        {
            cYear.Items.Clear();
            int currentYear = DateTime.Now.Year;
            for (int y = currentYear; y >= 1950; y--) cYear.Items.Add(y.ToString());
            cYear.MaxDropDownItems = 5;
            cYear.DropDownHeight = 120; 
            cYear.SelectedIndex = 0;
        }
        private void InitMonthComboBox()
        {
            cMonth.Items.Clear();
            foreach (string month in months) cMonth.Items.Add(month);
            cMonth.MaxDropDownItems = 5;
            cMonth.DropDownHeight = 120;
            cMonth.SelectedIndex = 0;
        }
        private void InitDayComboBox()
        {
            cDay.Items.Clear();
            for (int d = 1; d <= 31; d++) cDay.Items.Add(d.ToString());
            cDay.MaxDropDownItems = 5;
            cDay.DropDownHeight = 120;
            cDay.SelectedIndex = 0;
        }
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private string GetSelectedGender()
        {
            if (cMale.Checked) return "Male";
            if (cFemale.Checked) return "Famale";
            if (cOther.Checked) return "Other";
            return null;
        }
        private bool HasDigit(string input)
        {
            return input.Any(char.IsDigit);
        }
        private bool IsStrongPassword(string password)
        {
            if (password.Length < 8) return false;
            bool hasUpper = password.Any(char.IsUpper);
            bool hasLower = password.Any(char.IsLower);
            bool hasDigit = password.Any(char.IsDigit);
            bool hasSpecial = password.Any(ch => !char.IsLetterOrDigit(ch));
            return hasUpper && hasLower && hasDigit && hasSpecial;
        }
        private void ClearSignupFields()
        {
            cFirstname.Text = "";
            cSurname.Text = "";
            cEmail.Text = "";
            nw_password.Text = "";
            nw_cfpassword.Text = "";
            cMale.Checked = false;
            cFemale.Checked = false;
            cOther.Checked = false;
            cYear.SelectedIndex = 0;
            cMonth.SelectedIndex = 0;
            cDay.SelectedIndex = 0;
        }
        // Hàm mã hóa mật khẩu bằng SHA-256
        private string HashPasswordSHA256(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(bytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hash)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
        private void bt_signup_Click(object sender, EventArgs e)
        {
            string firstname = cFirstname.Text.Trim();
            string surname = cSurname.Text.Trim();
            string year = cYear.SelectedItem?.ToString();
            string month = cMonth.SelectedItem?.ToString();
            string day = cDay.SelectedItem?.ToString();
            string gender = GetSelectedGender();
            string email = cEmail.Text.Trim();
            string pass = nw_password.Text;
            string confirmpass = nw_cfpassword.Text;
            // Kiểm tra Firstname và Surname
            if (string.IsNullOrEmpty(firstname))
            {
                MessageBox.Show("Vui lòng nhập họ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cFirstname.Focus();
                return;
            }
            if (HasDigit(firstname))
            {
                MessageBox.Show("Họ không được chứa số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cFirstname.Focus();
                return;
            }
            if (string.IsNullOrEmpty(surname))
            {
                MessageBox.Show("Vui lòng nhập tên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cSurname.Focus();
                return;
            }
            if (HasDigit(surname))
            {
                MessageBox.Show("Tên không được chứa số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cSurname.Focus();
                return;
            }
            // Kiểm tra ngày sinh
            int y, d;
            if (!int.TryParse(year, out y) || !months.Contains(month) || !int.TryParse(day, out d))
            {
                MessageBox.Show("Vui lòng chọn ngày sinh hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                DateTime dob = new DateTime(y, Array.IndexOf(months, month) + 1, d);
            }
            catch
            {
                MessageBox.Show("Ngày sinh không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Chuyển ngày sinh về dạng yyyy-MM-dd
            int m = Array.IndexOf(months, month) + 1;
            int dayInt = int.Parse(day); // Đổi tên biến này
            string birthday = new DateTime(y, m, dayInt).ToString("yyyy-MM-dd");
            // Kiểm tra giới tính
            if (string.IsNullOrEmpty(gender))
            {
                MessageBox.Show("Vui lòng chọn giới tính.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Kiểm tra email
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cEmail.Focus();
                return;
            }
            // Kiểm tra mật khẩu
            if (!IsStrongPassword(pass))
            {
                MessageBox.Show("Mật khẩu phải có ít nhất 8 ký tự, gồm chữ hoa, chữ thường, số và ký tự đặc biệt.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nw_password.Focus();
                return;
            }
            if (pass != confirmpass)
            {
                MessageBox.Show("Mật khẩu xác nhận không khớp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                nw_cfpassword.Focus();
                return;
            }
            string hashedPass = HashPasswordSHA256(pass);
            MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            // Lưu vào database
            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "INSERT INTO Users (Firstname, Surname, Birthday, Gender, Email, PasswordEncrypted) VALUES (@Firstname, @Surname, @Birthday, @Gender, @Email, @PasswordEncrypted)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Firstname", firstname);
                    cmd.Parameters.AddWithValue("@Surname", surname);
                    cmd.Parameters.AddWithValue("@Birthday", birthday);
                    cmd.Parameters.AddWithValue("@Gender", gender);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordEncrypted", hashedPass);
                    try
                    {
                        conn.Open();
                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            cToLogin_Click(sender, e);
                            ClearSignupFields();
                        }
                        else
                        {
                            MessageBox.Show("Đăng ký thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi lưu vào database: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void bt_login_Click(object sender, EventArgs e)
        {
            string email = cUsername.Text.Trim();
            string password = cPassword.Text.Trim();
            //Admin
            if (email=="admin"&&password=="admin")
            {
                MessageBox.Show("Hello admin", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Hide();
                Dashboard mainForm = new Dashboard();
                mainForm.ShowDialog();
                this.Close();
            }
            // Kiểm tra email hợp lệ
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Email không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cUsername.Focus();
                return;
            }
            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cPassword.Focus();
                return;
            }
            // Mã hóa mật khẩu nhập vào để so sánh
            string hashedPassword = HashPasswordSHA256(password);

            string connStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string sql = "SELECT COUNT(*) FROM Users WHERE Email = @Email AND PasswordEncrypted = @PasswordEncrypted";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.Parameters.AddWithValue("@PasswordEncrypted", hashedPassword);
                    try
                    {
                        conn.Open();
                        int count = (int)cmd.ExecuteScalar();
                        if (count > 0)
                        {
                            MessageBox.Show("Login Successfully!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.Hide();
                            Dashboard mainForm = new Dashboard();
                            mainForm.ShowDialog();
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("Login Failed! Email hoặc mật khẩu không đúng.", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi khi kiểm tra đăng nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void guna2PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
