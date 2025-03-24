using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PinterestCheckLinked.Models;

namespace PinterestCheckLinked.Services
{
    public class ViewGridDataService
    {
        public DataGridView dataGridView { get; set; }
        public List<Data> datas = new List<Data>();

        public ViewGridDataService(DataGridView gridView)
        {
            dataGridView = gridView;
        }
        //private void SetDataForRow(int row, string proxy , string email  ) {
        //    SafeSetValueCell(row, DataGridCellName.CellEmail, email);
        //    SafeSetValueCell(row, DataGridCellName.CellProxy, proxy);
        //    SafeSetValueCell(row, DataGridCellName.Cellstt, _number);
        //}
        public Data AddNewRow()
        {
            int row = 0;
            this.dataGridView.Invoke(new Action(() =>
            {
                row = this.dataGridView.Rows.Add();
            }));
            int number = row + 1;
            Data data = new Data();
            data.Index = row;
            SafeSetValueCell(row, DataGridCellName.CellStt, number);
            datas.Add(data);
            return data;
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
