﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4 Toolbox工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//        如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类实现 DepartmentAttribute分部方法（partial）。
// </auto-generated>
//
// <copyright file="DepartmentAttributeBll.generated.cs">
//  
//        所属工程：YwkManage.OA 中西医薛思源 siyuanxue@outlook.com
//        生成时间：2017-10-04 00:39
// </copyright>
//------------------------------------------------------------------------------

using YwkManage.OA.IBll;
using YwkManage.OA.IDal;
using YwkManage.OA.Model.ModelClass;

namespace YwkManage.OA.Bll
{
	 public partial class DepartmentAttributeService : BaseService<DepartmentAttribute>, IDepartmentAttributeService
	{
		//使用spring.net的IOC注入方法，动态生成Dal实例。重载父类抽象方法（属性）
		public override IBaseDal<DepartmentAttribute> CurrentDal { get; set; }
	}
}