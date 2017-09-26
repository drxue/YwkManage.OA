using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using NPOI;
using System.Data.Entity;
using YwkManage.OA.Model;
using YwkManage.OA.Model.ModelClass;
using YwkManage.OA.Common;
using YwkManage.OA.IBll;
using YwkManage.OA.IDal;
using YwkManage.OA.Dal;
using System.Linq.Expressions;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;
using NPOI.XSSF.UserModel;
using YwkManage.OA.Bll;
using NPOI.HPSF;
using NPOI.POIFS.Properties;
using NPOI.POIFS.FileSystem;
using NPOI.XSSF.Streaming;

namespace YwkManage.OA.ModelDataManage
{
    public partial class ModelDataManage : Form
    {
        public ModelDataManage()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //在程序加载时，导入模型类
            Assembly assembly = Assembly.Load("YwkManage.OA.Model");
            //反射出所有继承了Entity的实体类
            IEnumerable<Type> modelTypes = assembly.GetTypes().Where(m => m.IsClass && m.Namespace == "YwkManage.OA.Model.ModelClass");
            foreach (var model in modelTypes)
            {
                T4ModelInfo modelInfo = new T4ModelInfo(model);
                cmbModel.Items.Add(modelInfo.Name);
            }
            btnInput.Enabled = false;
            //定义事件
            cmbModel.SelectedValueChanged += CmbModel_SelectedValueChanged;
            textBox1.TextChanged += TextBox1_TextChanged;
        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {
            if (cmbModel.SelectedItem != null && textBox1.Text != null)
            {
                btnInput.Enabled = true;
            }
            else
            {
                btnInput.Enabled = false;
            }
        }

        /// <summary>
        /// 类选择后使导入按钮启用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CmbModel_SelectedValueChanged(object sender, EventArgs e)
        {
            if (cmbModel.SelectedItem != null && textBox1.Text != null)
            {
                btnInput.Enabled = true;
            }
            else
            {
                btnInput.Enabled = false;
            }

        }

        private void btnSelectFile_Click(object sender, EventArgs e)
        {
            string result = String.Empty;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "files (*.xlsx)|*.xlsx| files (*.xls)|*.xls|All files (*.*)|*.*";
            openFile.InitialDirectory = "..\\";
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                result = openFile.FileName;
                textBox1.Text = result;
            }

            if (cmbModel.SelectedItem != null)
            {
                btnInput.Enabled = true;
            }
        }

        /// <summary>
        /// 导入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInput_Click(object sender, EventArgs e)
        {

            string modelName = cmbModel.SelectedItem.ToString();
            if (modelName == string.Empty || modelName == null)
            {
                return;
            }
            IReflectedService reflectedService = new ReflectedService(modelName);//1 创建bll写入类
            DataTable dt = dataGridView1.DataSource as DataTable; //获取datagridview1的数据源作为写入内容
            int returnCount = reflectedService.AddEntities(dt);
            if (returnCount > 0)
            {
                MessageBox.Show("导入成功"+returnCount+"条记录！");
                //载入所有记录到DataTable
                DataTable dt1 = reflectedService.LoadAllEntitiestoDataTable();
                //绑定数据到数据控件
                dataGridViewModel.DataSource = dt1;
            }
            else
            {
                MessageBox.Show("导入失败！");
            }


        }

        /// <summary>
        /// 内容切换后启用按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbModel_TextChanged(object sender, EventArgs e)
        {
            if (cmbModel.SelectedItem != null && textBox1.Text != null)
            {
                btnInput.Enabled = true;
            }
            else
            {
                btnInput.Enabled = false;
            }
        }


        private void btnReadExcel_Click(object sender, EventArgs e)
        {

            #region 调用excelHelper读取Excel文件内容到datagrid
            //读取Excel文件内容
            if (textBox1.Text == null)
            {
                return;
            }
            string fileName = textBox1.Text;
            ExcelHelper excelHelper = new ExcelHelper(fileName);
            string sheetName = cmbModel.SelectedText;

            //获取Excel文件内容到DataTable中
            DataTable dt = excelHelper.ExcelToDataTable(null, true);
            //填充DataTable内容到datagrid中
            if (dt != null)
            {
                dataGridView1.DataSource = dt;
            }
            else
            {
                MessageBox.Show("excel文件读取失败！");
            }

            #endregion
        }
        /// <summary>
        /// 模型类下拉列表选项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //改变下拉列表，模型变换，载入模型表数据到datagridviewModel
            string modelName = cmbModel.SelectedItem.ToString();
            IReflectedService reflextedSevice = new ReflectedService(modelName);
            //载入所有记录到DataTable
            DataTable dt = reflextedSevice.LoadAllEntitiestoDataTable();
            //绑定数据到数据控件
            dataGridViewModel.DataSource = dt;
        }

        /// <summary>
        /// 导出Excel文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExport_Click(object sender, EventArgs e)
        {
            //获取导出文件名称
            SaveFileDialog sd = new SaveFileDialog();
            if (cmbModel.SelectedItem == null)
            {
                return;
            }
            sd.FileName = cmbModel.SelectedItem.ToString();
            sd.Filter = "files (*.xlsx)|*.xlsx| files (*.xls)|*.xls";
            DialogResult result = sd.ShowDialog();
            if (result == DialogResult.OK)
            {
                ExcelHelper excelHelper = new ExcelHelper(sd.FileName);
                int count = excelHelper.DataTableToExcel(dataGridViewModel.DataSource as DataTable, cmbModel.SelectedItem.ToString(), true);
                if (count > 0)
                {
                    MessageBox.Show("成功导出" + count + "条记录！");
                }
                else
                {
                    MessageBox.Show("导出失败！");
                }
            }

        }
        /// <summary>
        /// 清空模型对应的数据库数据表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClearData_Click(object sender, EventArgs e)
        {
            string modelName = cmbModel.SelectedItem.ToString();
            if (modelName == string.Empty || modelName == null)
            {
                MessageBox.Show("Please select a Model,then click this botton!");
                return;
            }
            IReflectedService rs = new ReflectedService(modelName);
            if (rs.DeleteAllEntities())
            {
                MessageBox.Show("Empty the Table is Done!");
                //载入所有记录到DataTable
                DataTable dt = rs.LoadAllEntitiestoDataTable();
                //绑定数据到数据控件
                dataGridViewModel.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Something wrong!");
            }

        }
        //自定义方法"
    }
}
