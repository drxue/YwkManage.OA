﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4 Toolbox工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//        如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类实现 Employee分部方法（partial）。
//		  将本文件拷贝到目录外即Mapping目录下，根据示例修改各项属性。
// </auto-generated>
//
// <copyright file="EmployeeModelMapping.generated.cs">
//  
//        所属工程：YwkManage.OA 中西医薛思源 siyuanxue@outlook.com
//        生成时间：2017-08-27 11:06
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using YwkManage.OA.Model.ModelClass;

namespace YwkManage.OA.Model.Mapping
{
	/// <summary>
	/// 模型关系设置，Fruent API设置，ModelMapping-数据表映射——Employee
	///	中西医薛思源 siyuanxue@outlook.com
	/// </summary> 
	public partial class EmployeeMapping:EntityTypeConfiguration<Employee>
	{
		//示例
		//public EmployeeMapping()
		//{
		//// Primary Key
		//this.HasKey(e => e.LID);
		//// Properties
		//this.Property(e => e.EmployeeID)
		//    .IsRequired()
		//    .HasMaxLength(10);
		//this.Property(e => e.Destination)
		//    .HasMaxLength(50);
		//// Table & Column Mappings
		//this.ToTable("Leave");
		//// Relationships
		//this.HasOptional(e => e.ProjectClassify)
		//    .WithMany(e => e.Leave)
		//    .HasForeignKey(e => e.ProjectClassifyID);
		//this.HasRequired(e => e.Employees)
		//    .WithMany(e => e.Leave)
		//    .HasForeignKey(e => e.EmployeeID);
		//}
	}
}
	
	