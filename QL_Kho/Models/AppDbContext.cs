using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace QL_Kho.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DmManHinh> DmManHinhs { get; set; }

    public virtual DbSet<QlNguoiDung> QlNguoiDungs { get; set; }

    public virtual DbSet<QlNguoiDungNhomNguoiDung> QlNguoiDungNhomNguoiDungs { get; set; }

    public virtual DbSet<QlNhomNguoiDung> QlNhomNguoiDungs { get; set; }

    public virtual DbSet<QlPhanQuyen> QlPhanQuyens { get; set; }

    public virtual DbSet<DanhMucDonViTinh> DanhMucDonViTinh { get; set; }

    public virtual DbSet<DanhMucKho> DanhMucKho { get; set; }

    public virtual DbSet<DanhMucLoaiSanPham> DanhMucLoaiSanPham { get; set; }

    public virtual DbSet<DanhMucNCC> DanhMucNCC { get; set; }

    public virtual DbSet<DanhMucSanPham> DanhMucSanPham { get; set; }

    public virtual DbSet<XNK_NhapKho> XNK_NhapKho { get; set; }

    public virtual DbSet<XNK_NhapKhoRawData> XNK_NhapKhoRawData { get; set; }

    public virtual DbSet<XNK_XuatKho> XNK_XuatKho { get; set; }

    public virtual DbSet<XNK_XuatKhoRawData> XNK_XuatKhoRawData { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=KIETBANHTRAI\\SQLEXPRESS;Database=QL_Kho;User Id=sa;Password=123;Trust Server Certificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DmManHinh>(entity =>
        {
            entity.HasKey(e => e.MaManHinh).HasName("PK__DM_ManHi__D84939226E911284");
        });

        modelBuilder.Entity<QlNguoiDung>(entity =>
        {
            entity.HasKey(e => e.TenDangNhap).HasName("PK__QL_Nguoi__55F68FC105E85A2F");

            entity.Property(e => e.TrangThai).HasDefaultValue(0);
        });

        modelBuilder.Entity<QlNguoiDungNhomNguoiDung>(entity =>
        {
            entity.HasKey(e => new { e.TenDangNhap, e.MaNhomNguoiDung }).HasName("PK__QL_Nguoi__77F599D89FC2BC76");
        });

        modelBuilder.Entity<QlNhomNguoiDung>(entity =>
        {
            entity.HasKey(e => e.MaNhom).HasName("PK__QL_NhomN__234F91CD04A553B3");
        });

        modelBuilder.Entity<QlPhanQuyen>(entity =>
        {
            entity.Property(e => e.CoQuyen).HasDefaultValue(0);
        });

        modelBuilder.Entity<DanhMucDonViTinh>(entity =>
        {
            entity.HasKey(e => e.AutoId).HasName("PK__tbl_DM_D__F82B88231A225EB6");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<DanhMucKho>(entity =>
        {
            entity.HasKey(e => e.AutoId).HasName("PK__tbl_DM_K__F82B8823602BF045");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<DanhMucLoaiSanPham>(entity =>
        {
            entity.HasKey(e => e.AutoId).HasName("PK__tbl_DM_L__F82B882303EDD32B");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<DanhMucNCC>(entity =>
        {
            entity.HasKey(e => e.AutoId).HasName("PK__tbl_DM_N__F82B882348135F76");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
        });

        modelBuilder.Entity<DanhMucSanPham>(entity =>
        {
            entity.HasKey(e => e.AutoId).HasName("PK__tbl_DM_S__F82B88239BEFFC27");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.DonViTinh).WithMany(p => p.TblDmSanPhams).HasConstraintName("FK__tbl_DM_Sa__DonVi__44FF419A");

            entity.HasOne(d => d.LoaiSanPham).WithMany(p => p.TblDmSanPhams).HasConstraintName("FK__tbl_DM_Sa__LoaiS__440B1D61");
        });

        modelBuilder.Entity<XNK_NhapKho>(entity =>
        {
            entity.HasKey(e => e.AutoId).HasName("PK__tbl_XNK___F82B882382A3EEB3");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Kho).WithMany(p => p.TblXnkNhapKhos).HasConstraintName("FK__tbl_XNK_N__Kho_I__48CFD27E");

            entity.HasOne(d => d.Ncc).WithMany(p => p.TblXnkNhapKhos).HasConstraintName("FK__tbl_XNK_N__NCC_I__49C3F6B7");
        });

        modelBuilder.Entity<XNK_NhapKhoRawData>(entity =>
        {
            entity.HasKey(e => e.AutoId).HasName("PK__tbl_XNK___F82B882353D65239");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.NhapKho).WithMany(p => p.TblXnkNhapKhoRawData).HasConstraintName("FK__tbl_XNK_N__Nhap___4D94879B");

            entity.HasOne(d => d.SanPham).WithMany(p => p.TblXnkNhapKhoRawData).HasConstraintName("FK__tbl_XNK_N__San_P__4E88ABD4");
        });

        modelBuilder.Entity<XNK_XuatKho>(entity =>
        {
            entity.HasKey(e => e.AutoId).HasName("PK__tbl_XNK___F82B8823B1F907C0");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.Kho).WithMany(p => p.TblXnkXuatKhos).HasConstraintName("FK__tbl_XNK_X__Kho_I__52593CB8");
        });

        modelBuilder.Entity<XNK_XuatKhoRawData>(entity =>
        {
            entity.HasKey(e => e.AutoId).HasName("PK__tbl_XNK___F82B8823DC17A9E4");

            entity.Property(e => e.IsDeleted).HasDefaultValue(false);

            entity.HasOne(d => d.SanPham).WithMany(p => p.TblXnkXuatKhoRawData).HasConstraintName("FK__tbl_XNK_X__San_P__571DF1D5");

            entity.HasOne(d => d.XuatKho).WithMany(p => p.TblXnkXuatKhoRawData).HasConstraintName("FK__tbl_XNK_X__Xuat___5629CD9C");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
