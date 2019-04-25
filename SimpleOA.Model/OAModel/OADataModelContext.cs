namespace SimpleOA.Model.OAModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class OADataModelContext : DbContext
    {
        public OADataModelContext()
            : base("name=OADataModel")
        {
        }

        public virtual DbSet<ActionInfo> ActionInfo { get; set; }
        public virtual DbSet<CommonWFInstance> CommonWFInstance { get; set; }
        public virtual DbSet<CommonWFStep> CommonWFStep { get; set; }
        public virtual DbSet<CommonWFTemp> CommonWFTemp { get; set; }
        public virtual DbSet<DayOffTemp> DayOffTemp { get; set; }
        public virtual DbSet<FileInfo> FileInfo { get; set; }
        public virtual DbSet<MenuInfo> MenuInfo { get; set; }
        public virtual DbSet<MoneyOrderTemp> MoneyOrderTemp { get; set; }
        public virtual DbSet<OrderInfo> OrderInfo { get; set; }
        public virtual DbSet<R_UserInfo_TreeMenu> R_UserInfo_TreeMenu { get; set; }
        public virtual DbSet<RoleInfo> RoleInfo { get; set; }
        public virtual DbSet<TreeMenu> TreeMenu { get; set; }
        public virtual DbSet<UserInfo> UserInfo { get; set; }
        public virtual DbSet<UserInfoExt> UserInfoExt { get; set; }
        public virtual DbSet<WF_Instance> WF_Instance { get; set; }
        public virtual DbSet<WF_Step> WF_Step { get; set; }
        public virtual DbSet<WF_Temp> WF_Temp { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CommonWFTemp>()
                .Property(e => e.FormUrl)
                .IsFixedLength();

            modelBuilder.Entity<CommonWFTemp>()
                .HasMany(e => e.CommonWFInstance)
                .WithRequired(e => e.CommonWFTemp)
                .HasForeignKey(e => e.TempId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<DayOffTemp>()
                .Property(e => e.FormUrl)
                .IsFixedLength();

            modelBuilder.Entity<FileInfo>()
                .HasMany(e => e.WF_Instance)
                .WithMany(e => e.FileInfo)
                .Map(m => m.ToTable("WF_InstanceFileInfo"));

            modelBuilder.Entity<MoneyOrderTemp>()
                .Property(e => e.FormUrl)
                .IsFixedLength();

            modelBuilder.Entity<RoleInfo>()
                .HasMany(e => e.TreeMenu)
                .WithMany(e => e.RoleInfo)
                .Map(m => m.ToTable("RoleInfoTreeMenu").MapRightKey("TreeMenu_ID"));

            modelBuilder.Entity<RoleInfo>()
                .HasMany(e => e.UserInfo)
                .WithMany(e => e.RoleInfo)
                .Map(m => m.ToTable("UserInfoRoleInfo"));

            modelBuilder.Entity<TreeMenu>()
                .Property(e => e.Controller)
                .IsFixedLength();

            modelBuilder.Entity<TreeMenu>()
                .Property(e => e.Action)
                .IsFixedLength();

            modelBuilder.Entity<TreeMenu>()
                .HasMany(e => e.R_UserInfo_TreeMenu)
                .WithRequired(e => e.TreeMenu)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInfo>()
                .HasMany(e => e.CommonWFInstance)
                .WithRequired(e => e.UserInfo)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInfo>()
                .HasMany(e => e.CommonWFStep)
                .WithOptional(e => e.UserInfo)
                .HasForeignKey(e => e.ProcessBy);

            modelBuilder.Entity<UserInfo>()
                .HasMany(e => e.OrderInfo)
                .WithRequired(e => e.UserInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserInfo>()
                .HasMany(e => e.R_UserInfo_TreeMenu)
                .WithRequired(e => e.UserInfo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WF_Instance>()
                .HasMany(e => e.WF_Step)
                .WithRequired(e => e.WF_Instance)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<WF_Temp>()
                .HasMany(e => e.WF_Instance)
                .WithRequired(e => e.WF_Temp)
                .WillCascadeOnDelete(false);
        }
    }
}
