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
            //test
            //var myClass = new Contact();
            //var obj=Class
            //Type open = typeof(Dictionary<,>);
            //Type closeType = open.MakeGenericType(typeof(String), typeof(object));
            //object obj = Activator.CreateInstance(closeType);
            //textBox2.Text =  obj.GetType().GetTypeInfo().ToString();
            //测试
            //string text = string.Empty;
            //text = typeof(Contact).ToString();
            //textBox2.Text = text;
            //Contact a = new Contact();
            //string abc = "YwkManage.OA.Model.ModelClass.Contact";
            //var tt = Type.GetType(abc);
            //textBox2.Text +=tt;

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
            openFile.Filter = "All files (*.*)|*.*| files (*.xls)|*.xls| files (*.xlsx)|*.xlsx";
            openFile.InitialDirectory = "..\\";
            openFile.RestoreDirectory = true;
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                result = openFile.FileName;
            }
            textBox1.Text = result;
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
            //1 获取选择的excel文件和类名称
            string modelName = cmbModel.SelectedItem.ToString();
            string fileName = textBox1.Text;
            //判断选择是否正确
            if (modelName == string.Empty || fileName == string.Empty)
            {
                return;
            }
            //2 

            //3 创建写入的类
            //3.2  获取程序集
            //IDataManage dataManage = new DataManage();
            //var modellist = dataManage.GetAllEntities(modelName);//获取所有该模型类的数据库记录集合


            //var model= dataManage.GetModelInstance(modelName);


            Assembly assemblyDal = Assembly.Load("YwkManage.OA.Dal");
            Assembly assemblyModel = Assembly.Load("YwkManage.OA.Model");
            //3.3 获取类型,模型类及Dal类的类型
            Type modelType = assemblyModel.GetType("YwkManage.OA.Model.ModelClass." + modelName);
            Type typeDal = assemblyDal.GetType("YwkManage.OA.Dal." + modelName + "Dal");
            //3.4 创建类实例，可以直接指定全名创建
            var modelClass = assemblyModel.CreateInstance(modelType.FullName);
            var dal = assemblyDal.CreateInstance(typeDal.FullName);
            //3.5 获取类型中的指定方法，类型转换
            MethodInfo mi = typeDal.GetMethod("LoadAllEntities");
            object[] arg = new object[] { };
            //object sourceModelData = mi.Invoke(dal, arg);

            //创建空类型集合，用于lambda表达式，例如Expression<Func<modelType,bool>> lambda,e=>e.id=3
            Type expressoinType = typeof(Dictionary<,>);
            // Type text = typeof(Expression<Func<typeof(Dictionary<,>)>>);
            Type lambdaExpressionGenericType = expressoinType.MakeGenericType(modelType, typeof(bool));
            //////
            //获取propertyName
            string propertyOrFieldName = string.Empty;
            PropertyInfo[] modelProperties = modelType.GetProperties();
            foreach (var modelProperty in modelProperties)
            {
                if (modelProperty.Name == modelName + "ID")
                {
                    //modelProperties.SetValue(modelProperty, 123);
                    propertyOrFieldName = modelProperty.Name;
                    break;
                }
            }



            ParameterExpression parameter = Expression.Parameter(modelType, "p");//创建参数p
            MemberExpression member = Expression.PropertyOrField(parameter, propertyOrFieldName);
            ConstantExpression constant = Expression.Constant(123);//创建常数
            object expression = Expression.Lambda(Expression.Equal(member, constant), parameter);
            object exression1 = LambdaHelper.True(modelType);
            object expression2 = LambdaHelper.CreateEqual(modelType, propertyOrFieldName, 234);
            MethodInfo mi1 = typeDal.GetMethod("LoadEntities");
            //Expression<Func<object, bool>> lambda =Contact => true;
            object[] arg1 = new object[] { exression1 };
            object sourceModelData1 = mi1.Invoke(dal, arg1);

            //4 读取excel文件内容
            //创建空的接口，HSSFWorkbook 类来处理 xls，XSSFWorkbook 类来处理 xlsx，它们都继承接口 IWorkbook
            // IWorkbook wk = null;


        }
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

        private void btnAddTitleInfo_Click(object sender, EventArgs e)
        {
            //1 获取选择的excel文件和类名称
            string modelName = cmbModel.SelectedItem.ToString();
            string fileName = textBox1.Text;
            //判断选择是否正确
            if (modelName == string.Empty || fileName == string.Empty)
            {
                return;
            }
            //2 获取程序集
            Assembly assemblyDal = Assembly.Load("YwkManage.OA.Dal");
            Assembly assemblyModel = Assembly.Load("YwkManage.OA.Model");
            //3.3 获取类型,模型类及Dal类的类型
            Type modelType = assemblyModel.GetType("YwkManage.OA.Model.ModelClass." + modelName);
            Type typeDal = assemblyDal.GetType("YwkManage.OA.Dal." + modelName + "Dal");
            //3.4 创建类实例，可以直接指定全名创建
            var modelClass = assemblyModel.CreateInstance(modelType.FullName);
            var dal = assemblyDal.CreateInstance(typeDal.FullName);
            //直接创建类实例
            TitleLevelInfo titleLevelInfo = new TitleLevelInfo()
            {
                TitleLevel = 1,
                TitleName = "医师"
            };

            MethodInfo miAdd = typeDal.GetMethod("AddEntity");
            object[] arg1 = new object[] { titleLevelInfo };
            object result =miAdd.Invoke(dal, arg1);
            if (result!=null)
            {
                    MessageBox.Show("添加成功");

                MethodInfo miLoad = typeDal.GetMethod("LoadEntities");
               Expression expresion= LambdaHelper.CreateEqual(modelType, modelName + "ID", ((TitleLevelInfo)result).TitleLevelInfoID);
                object[] arg3 = new object[] { expresion };
                object result1 = miLoad.Invoke(dal, arg3);
                TitleLevelInfo aaa = ((IQueryable<TitleLevelInfo>)result1).FirstOrDefault();
                textBox2.Text = aaa.TitleName;
            }
            else
            {
                MessageBox.Show("添加失败");
            }

        }
        //自定义方法


    }
}
