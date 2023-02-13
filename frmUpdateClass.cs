﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace GymSYS
{
    public partial class frmUpdateClass : Form
    {
        public frmUpdateClass()
        {
            InitializeComponent();
        }

        private void backToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            frmMainMenu mainMenu = new frmMainMenu();
            mainMenu.Show();
        }

        private void mnuRegisterMember_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmRegisterMember registerMember = new frmRegisterMember();
            registerMember.Show();
        }

        private void mnuUpdateMember_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmUpdateMember updateMember = new frmUpdateMember();
            updateMember.Show();
        }

        private void mnuTopUpMemberWallet_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmTopUpMemberWallet topUpMember = new frmTopUpMemberWallet();
            topUpMember.Show();
        }

        private void scheduleClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmScheduleClass scheduleClass = new frmScheduleClass();
            scheduleClass.Show();
        }

        private void cancelClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCancelClass cancelClass = new frmCancelClass();
            cancelClass.Show();
        }

        private void makeBookingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmMakeBooking makeBooking = new frmMakeBooking();
            makeBooking.Show();
        }

        private void cancelBookingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmCancelBooking cancelBooking = new frmCancelBooking();
            cancelBooking.Show();
        }

        private void yearlyRevenueAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmYearlyRevenueAnalysis yearlyRevenueAnalysis = new frmYearlyRevenueAnalysis();
            yearlyRevenueAnalysis.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            //valiadte memberId
            if (txtClassId.Text.Equals(""))
            {
                MessageBox.Show("Class ID must be entered", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtClassId.Focus();
                return;
            }
            for (int i = 0; i < txtClassId.TextLength; i++)
            {
                if (txtClassId.Text.Any(char.IsLetter) == true)
                {
                    MessageBox.Show("Class ID contains a letter", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtClassId.Focus();
                    return;
                }
            }
            //end of validation

            //conect to database
            OracleConnection conn = new OracleConnection(DBConnect.oracledb);

            //define sql query
            String sqlQuery = "SELECT * " +
                "FROM Classes WHERE Class_Id = " + Convert.ToInt32(txtClassId.Text);

            //execute query
            OracleCommand cmd = new OracleCommand(sqlQuery, conn);
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();
            if (!dr.Read())
            {
                MessageBox.Show("There are no classes found with that Class ID");
            }
            else
            {
                txtClassName.Text = dr.GetString(1);
                txtClassTeacher.Text = dr.GetString(2);
                txtClassFee.Text = dr.GetString(3);
            }

            conn.Close();
        }

        private void btnUpdateClass_Click(object sender, EventArgs e)
        {
            //Create Class Object
            Session updateClass = new Session();

            //change the data
            updateClass.setClassId(Convert.ToInt32(txtClassId.Text));
            updateClass.setClassName(txtClassName.Text);
            updateClass.setClassTeacher(txtClassTeacher.Text);
            updateClass.setClassFee(Convert.ToInt32(txtClassFee.Text));

            //update the data
            updateClass.updateClass();

            //Display Confirmation Message
            MessageBox.Show("Class has been updated successfully", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Reset UI
            txtClassId.Clear();
            txtClassName.Clear();
            txtClassTeacher.Clear();
            txtClassFee.Clear();
        }
    }
}
