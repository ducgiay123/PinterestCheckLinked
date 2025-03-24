using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChangeViaFBTool.Models;

namespace ChangeViaFBTool.Services
{
    public class ViewGridDataService
    {
        public DataGridView dataGridView { get; set; }
        public List<Data> datas = new List<Data>();

        public ViewGridDataService(DataGridView gridView)
        {
            dataGridView = gridView;
            dataGridView.DefaultCellStyle.Font = new Font("Arial", 10);
        }
        //private void SetDataForRow(int row, string proxy , string email  ) {
        //    SafeSetValueCell(row, DataGridCellName.CellEmail, email);
        //    SafeSetValueCell(row, DataGridCellName.CellProxy, proxy);
        //    SafeSetValueCell(row, DataGridCellName.Cellstt, _number);
        //}
        public Data AddNewRow(int index)
        {
            this.dataGridView.Invoke(new Action(() =>
            {
                this.dataGridView.Rows.Add();
            }));
            Data data = new Data();
            int row = index;
            dataGridView.Rows[index].Height = 40;
            //dataGridView.Rows[index].Cells["buttonCol"].Enabled = false;
            SafeSetValueCell(row, DataGridCellName.CellStt, index);
            datas.Add(data);
            return data;
        }

        public void HandleButtonColumn(int index)
        {
            try
            {
                this.dataGridView.Invoke(new Action(() =>
                {
                    dataGridView.Rows[index].Cells["buttonCol"].Value = "Open Chrome";
                }));
            }
            catch
            {

            }
            //DataGridViewButtonColumn btnCl = new DataGridViewButtonColumn();
            //btnCl.Name = "button";
            //btnCl.HeaderText = "Action";
            //btnCl.Text = "Action";
            //btnCl.UseColumnTextForButtonValue = true;
            //dataGridView.Columns.Add(btnCl);
        }
        public void SafeSetValueCell(int row, string cellname, object value)
        {
            try
            {
                this.dataGridView.Invoke(new Action(() =>
                {
                    dataGridView.Rows[row].Cells[cellname].Value = value;
                }));
            }
            catch
            {

            }
        }
        public Color GetForceColorByStatus(string status)
        {
            if (status.Contains("Success"))
            {
                return Color.Green;
            }
            if (status.Contains("Blocked") || status.Contains("ErrorCreateMail") || status.Contains("EmailCantReg") || status.Contains("CodeNotRecievied") || status.Contains("SignupCodeFail"))
            {
                return Color.Red;
            }
            if (status.Contains("Error") || status.Contains("Unknown"))
            {
                return Color.OrangeRed;
            }
            return Color.Black;
        }
    }
}
