﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4 Toolbox工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//        如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类实现 Department分部方法（partial）。
//		  将本文件拷贝到目录外即Mapping目录下，根据示例修改各项属性。
// </auto-generated>
//
// <copyright file="DepartmentModelMapping.generated.cs">
//  
//        所属工程：YwkManage.OA 中西医薛思源 siyuanxue@outlook.com
//        生成时间：2017-09-12 23:50
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
	/// 模型关系设置，Fruent API设置，ModelMapping-数据表映射——Department
	///	中西医薛思源 siyuanxue@outlook.com
	/// </summary> 
	public partial class DepartmentMapping:EntityTypeConfiguration<Department>
	{
		//public DepartmentMapping()
        //{
		//	 // Primary Key
		//	 this.HasKey(e => e.DepartmentID);

		//	 // Properties
		//
		//	 // Table & Column Mappings
		//   this.ToTable("Department");

		//	 // Relationships
		//
        //}

        #region 示例
        //public DepartmentMapping()
        //{
        //    // Primary Key
        //    this.HasKey(e => e.DepartmentID);

        //    // Properties
        //    this.Property(e => e.DepartmentID)
        //        .IsRequired()
        //        .HasMaxLength(10);

        //    // Table & Column Mappings
        //    this.ToTable("Department");

        //    // Relationships
        //    this.HasOptional(e => e.OtherClass)
        //        .WithMany(e => e.Department)
        //        .HasForeignKey(e => e.OtherClassID);
        //}
        #endregion 示例
	}
}
	
	