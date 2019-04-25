namespace SimpleOA.Model.ErpModel
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ErpDataModelContext : DbContext
    {
        public ErpDataModelContext()
            : base("name=ErpDataModel")
        {
        }

        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<FileList> FileList { get; set; }
        public virtual DbSet<tbRight> tbRight { get; set; }
        public virtual DbSet<tbRoLe> tbRoLe { get; set; }
        public virtual DbSet<tbUserRight> tbUserRight { get; set; }
        public virtual DbSet<TreeMenu> TreeMenu { get; set; }
        public virtual DbSet<UserManager> UserManager { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FileList>()
                .Property(e => e.FileContent)
                .IsUnicode(false);
        }
    }
}
