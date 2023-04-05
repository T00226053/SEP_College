﻿using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymSYS
{
    public partial class frmCancelBooking : Form
    {
        public frmCancelBooking()
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

        private void updateClassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmUpdateClass updateClass = new frmUpdateClass();
            updateClass.Show();
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

        private void yearlyRevenueAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmYearlyRevenueAnalysis yearlyRevenueAnalysis = new frmYearlyRevenueAnalysis();
            yearlyRevenueAnalysis.Show();
        }

        private void yearlyClassAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Hide();
            frmYearlyClassAnalysis yearlyClassAnalysis = new frmYearlyClassAnalysis();
            yearlyClassAnalysis.Show();
        }

        private void btnCancelClass_Click(object sender, EventArgs e)
        {
            //valiadte ClassId
            if (cboBookingId.Text.Equals(""))
            {
                MessageBox.Show("Booking ID must be selected ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboBookingId.Focus();
                return;
            }
            //End of validation

            //connect to database
            OracleConnection conn = new OracleConnection(DBConnect.oracledb);

            //sql query
            String sqlQuery = "SELECT * FROM Bookings WHERE Booking_Id = " + Convert.ToInt32(cboBookingId.Text);

            //create Booking Object
            Booking cancelBooking = new Booking();

            //execute query
            OracleCommand cmd = new OracleCommand(sqlQuery, conn);
            conn.Open();
            OracleDataReader dr = cmd.ExecuteReader();
            if (!dr.Read())
            {
                MessageBox.Show("There are no bookings found with that Booking ID");
                return;
            }
            else
            {
                cancelBooking.setBookingId(Convert.ToInt32(cboBookingId.Text));
            }

            //remove the data
            cancelBooking.cancelBooking();

            //create session object
            Session changeReg = new Session();

            //invoke method
            changeReg.removeRegister();

            //Display Confirmation Message
            MessageBox.Show("Booking has cancelled successfully", "Success",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Reset UI
            cboBookingId.SelectedIndex = -1;

            conn.Close();
        }

        private void frmCancelBooking_Load(object sender, EventArgs e)
        {
            DataSet dsB = Booking.getAllBookingIds();

            for (int i = 0; i < dsB.Tables[0].Rows.Count; i++)
            {
                cboBookingId.Items.Add(dsB.Tables[0].Rows[i][0]);
            }
        }
    }
}
