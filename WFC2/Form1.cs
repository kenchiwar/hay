using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WFC2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async  void Form1_Load(object sender, EventArgs e)
        {
            using (
                //var or
                WinEntities ef = new WinEntities())

            {
                bindingSource1.DataSource = ef.Students.ToList();
                dataGridView1.DataSource = bindingSource1;
                bindingNavigator1.BindingSource = bindingSource1;

                var findId = txtId.Text;
                txtId.DataBindings.Add("Text", bindingSource1, "Id", true, DataSourceUpdateMode.OnPropertyChanged);
                txtFirstName.DataBindings.Add("Text", bindingSource1, "FirstName", true, DataSourceUpdateMode.OnPropertyChanged);
                txtLastName.DataBindings.Add("Text", bindingSource1, "LastName", true,
                    DataSourceUpdateMode.OnPropertyChanged);
                dptDob.DataBindings.Add("Value", bindingSource1, "Dob", true, DataSourceUpdateMode.OnPropertyChanged);

                //mở dữ liệu kiểu này khó đóng lắm phải , tốn ram , phải để trong using
            }
        }
        //UPDate 
        private async  void toolStripButton1_Click(object sender, EventArgs e)
        {
            using (var ef = new WinEntities())
            {
                var findId = Convert.ToInt32(txtId.Text);
                //var var obj =  ef.Students.FirstOrDefault(stu => stu.Id == findId);
                var obj =await ef.Students.FirstOrDefaultAsync(stu => stu.Id==findId);
                if(obj != null)
                {
                    obj.FirstName = txtFirstName.Text;
                    obj.LastName = txtLastName.Text;
                    obj.Dob = dptDob.Value;

                }
                ef.SaveChangesAsync();
                bindingSource1.DataSource = await  ef.Students.ToListAsync();
                //ef.SaveChanges();
                //bindingSource1.DataSource = await ef.Students.ToList();

            }
        }
        //insert 
        private async  void Insert_Click(object sender, EventArgs e)
        {
            using (var ef = new WinEntities())
            {
                var stu = new Student ();   
                stu.FirstName = txtFirstName.Text;
                stu.LastName = txtLastName.Text;
                stu.Dob = dptDob.Value;
                ef.Students.Add(stu);
                await ef.SaveChangesAsync();
                bindingSource1.DataSource = await ef.Students.ToListAsync();
            }
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {

        }
    }
}
