﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由T4 Toolbox工具生成。
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
//        如存在本生成代码外的新需求，请在相同命名空间下创建同名分部类实现 Ward分部方法（partial）。
//		  将本文件拷贝到目录外即Mapping目录下，根据示例修改各项属性。
// </auto-generated>
//
// <copyright file="WardModelMapping.generated.cs">
//  
//        所属工程：YwkManage.OA 中西医薛思源 siyuanxue@outlook.com
//        生成时间：2017-08-27 14:27
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
	/// 模型关系设置，Fruent API设置，ModelMapping-数据表映射——Ward
	///	中西医薛思源 siyuanxue@outlook.com
	/// </summary> 
	public partial class WardMapping:EntityTypeConfiguration<Ward>
	{
        public WardMapping()
        {
            // Primary Key
            this.HasKey(e => e.WardID);

            // Properties

            // Table & Column Mappings
            this.ToTable("Ward");

            // Relationships
            this.HasRequired(e => e.HeadNurse)
                .WithOptional()
                .Map(e => e.MapKey("HeadNurseID"));
            this.HasOptional(e => e.DeputyHeadNurse)
                .WithOptionalDependent()
                .Map(e => e.MapKey("DeputyHeadNurseID"));

            this.HasMany(e => e.Department)
                .WithMany(e => e.Ward)
                .Map(e =>
                {
                    e.ToTable("DepartmentWardRelationship");
                    e.MapLeftKey("DepartmentID");
                    e.MapRightKey("WardID");
                });
        }

        #region 示例
        //public WardMapping()
        //{
        //    // Primary Key
        //    this.HasKey(e => e.WardID);

        //    // Properties
        //    this.Property(e => e.WardID)
        //        .IsRequired()
        //        .HasMaxLength(10);

        //    // Table & Column Mappings
        //    this.ToTable("Ward");

        //    // Relationships
        //    this.HasOptional(e => e.OtherClass)
        //        .WithMany(e => e.Ward)
        //        .HasForeignKey(e => e.OtherClassID);
        //}
        #endregion 示例
    }
}
	
	