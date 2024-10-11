using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DormitoryServer.Models
{
    public partial class DataDormitoryContext : DbContext
    {
        public DataDormitoryContext()
        {
        }

        public DataDormitoryContext(DbContextOptions<DataDormitoryContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AccountStaff> AccountStaffs { get; set; } = null!;
        public virtual DbSet<AccountStudent> AccountStudents { get; set; } = null!;
        public virtual DbSet<Announcement> Announcements { get; set; } = null!;
        public virtual DbSet<Building> Buildings { get; set; } = null!;
        public virtual DbSet<Class> Classes { get; set; } = null!;
        public virtual DbSet<DormInvoice> DormInvoices { get; set; } = null!;
        public virtual DbSet<Equipment> Equipment { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<ParkingHistory> ParkingHistories { get; set; } = null!;
        public virtual DbSet<ParkingTicket> ParkingTickets { get; set; } = null!;
        public virtual DbSet<Registration> Registrations { get; set; } = null!;
        public virtual DbSet<Relative> Relatives { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomEquipment> RoomEquipments { get; set; } = null!;
        public virtual DbSet<RoomType> RoomTypes { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<SupportRequest> SupportRequests { get; set; } = null!;
        public virtual DbSet<UtilityMeter> UtilityMeters { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-CNFI749\\SQLEXPRESS;Initial Catalog=DataDormitory;Integrated Security=True;Multiple Active Result Sets=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountStaff>(entity =>
            {
                entity.HasKey(e => e.AccountStaff1)
                    .HasName("PK__AccountS__F669B8103A1904C8");

                entity.ToTable("AccountStaff");

                entity.Property(e => e.AccountStaff1).HasColumnName("AccountStaff");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AccountStaffs)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__AccountSt__RoleI__467D75B8");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.AccountStaffs)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__AccountSt__Staff__477199F1");
            });

            modelBuilder.Entity<AccountStudent>(entity =>
            {
                entity.HasKey(e => e.AccountStudent1)
                    .HasName("PK__AccountS__C6A8EE53E799B1ED");

                entity.ToTable("AccountStudent");

                entity.Property(e => e.AccountStudent1).HasColumnName("AccountStudent");

                entity.Property(e => e.Password).HasMaxLength(255);

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StudentID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.AccountStudents)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__AccountSt__Stude__40C49C62");
            });

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.ToTable("Announcement");

                entity.Property(e => e.AnnouncementId).HasColumnName("AnnouncementID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Target).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Announcements)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__Announcem__Staff__4865BE2A");
            });

            modelBuilder.Entity<Building>(entity =>
            {
                entity.ToTable("Building");

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("BuildingID");

                entity.Property(e => e.BuildingName).HasMaxLength(255);
            });

            modelBuilder.Entity<Class>(entity =>
            {
                entity.ToTable("Class");

                entity.Property(e => e.ClassId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ClassID");

                entity.Property(e => e.ClassName).HasMaxLength(255);

                entity.Property(e => e.FacultyId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FacultyID");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("FK__Class__FacultyID__3A179ED3");
            });

            modelBuilder.Entity<DormInvoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId)
                    .HasName("PK__DormInvo__D796AAD57DD97099");

                entity.ToTable("DormInvoice");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.InvoiceType).HasMaxLength(255);

                entity.Property(e => e.IssueDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.DormInvoices)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__DormInvoi__Staff__50FB042B");
            });

            modelBuilder.Entity<Equipment>(entity =>
            {
                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.EquipmentName).HasMaxLength(255);

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Faculty>(entity =>
            {
                entity.ToTable("Faculty");

                entity.Property(e => e.FacultyId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FacultyID");

                entity.Property(e => e.FacultyName).HasMaxLength(255);
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.ToTable("Invoice");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.IssueDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RoomID");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Invoice__RoomID__4C364F0E");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__Invoice__StaffID__4B422AD5");
            });

            modelBuilder.Entity<InvoiceDetail>(entity =>
            {
                entity.ToTable("InvoiceDetail");

                entity.Property(e => e.InvoiceDetailId).HasColumnName("InvoiceDetailID");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.HasOne(d => d.Invoice)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.InvoiceId)
                    .HasConstraintName("FK__InvoiceDe__Invoi__4D2A7347");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__InvoiceDe__Servi__4E1E9780");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.NewsId).HasColumnName("NewsID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID");

                entity.Property(e => e.Tag).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__News__StaffID__51EF2864");
            });

            modelBuilder.Entity<ParkingHistory>(entity =>
            {
                entity.ToTable("ParkingHistory");

                entity.Property(e => e.ParkingHistoryId).HasColumnName("ParkingHistoryID");

                entity.Property(e => e.EntryExit).HasMaxLength(50);

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.Property(e => e.Time)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Ticket)
                    .WithMany(p => p.ParkingHistories)
                    .HasForeignKey(d => d.TicketId)
                    .HasConstraintName("FK__ParkingHi__Ticke__4589517F");
            });

            modelBuilder.Entity<ParkingTicket>(entity =>
            {
                entity.HasKey(e => e.TicketId)
                    .HasName("PK__ParkingT__712CC627ECA5B444");

                entity.ToTable("ParkingTicket");

                entity.Property(e => e.TicketId).HasColumnName("TicketID");

                entity.Property(e => e.EndTime).HasColumnType("date");

                entity.Property(e => e.LicensePlate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("date");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StudentID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ParkingTickets)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__ParkingTi__Stude__44952D46");
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.ToTable("Registration");

                entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");

                entity.Property(e => e.ApplicationStatus).HasMaxLength(255);

                entity.Property(e => e.RoomId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RoomID");

                entity.Property(e => e.Semester).HasMaxLength(10);

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StudentID");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Registrat__RoomI__41B8C09B");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Registrat__Stude__42ACE4D4");
            });

            modelBuilder.Entity<Relative>(entity =>
            {
                entity.ToTable("Relative");

                entity.Property(e => e.RelativeId).HasColumnName("RelativeID");

                entity.Property(e => e.Address).HasMaxLength(255);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StudentID");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Relatives)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Relative__Studen__43A1090D");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("Role");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName).HasMaxLength(255);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.ToTable("Room");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RoomID");

                entity.Property(e => e.BuildingId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("BuildingID");

                entity.Property(e => e.RoomTypeId).HasColumnName("RoomTypeID");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK__Room__BuildingID__3BFFE745");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomTypeId)
                    .HasConstraintName("FK__Room__RoomTypeID__3B0BC30C");
            });

            modelBuilder.Entity<RoomEquipment>(entity =>
            {
                entity.ToTable("Room_Equipment");

                entity.Property(e => e.RoomEquipmentId).HasColumnName("Room_EquipmentID");

                entity.Property(e => e.Condition).HasMaxLength(255);

                entity.Property(e => e.EquipmentId).HasColumnName("EquipmentID");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RoomID");

                entity.HasOne(d => d.Equipment)
                    .WithMany(p => p.RoomEquipments)
                    .HasForeignKey(d => d.EquipmentId)
                    .HasConstraintName("FK__Room_Equi__Equip__3DE82FB7");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomEquipments)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Room_Equi__RoomI__3CF40B7E");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.ToTable("RoomType");

                entity.Property(e => e.RoomTypeId).HasColumnName("RoomTypeID");

                entity.Property(e => e.RoomPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RoomTypeName).HasMaxLength(255);
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.ToTable("Service");

                entity.Property(e => e.ServiceId).HasColumnName("ServiceID");

                entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ServiceName).HasMaxLength(255);

                entity.Property(e => e.Unit).HasMaxLength(50);
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StudentID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.ClassId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ClassID");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.Hometown).HasMaxLength(255);

                entity.Property(e => e.Idcard)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("IDCard");

                entity.Property(e => e.InsuranceNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.RoomId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RoomID");

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__Student__ClassID__3EDC53F0");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Student__RoomID__3FD07829");
            });

            modelBuilder.Entity<SupportRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__SupportR__33A8519A38CB3E03");

                entity.ToTable("SupportRequest");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.RequestDate).HasColumnType("datetime");

                entity.Property(e => e.RequestType).HasMaxLength(255);

                entity.Property(e => e.StaffId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StudentID");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.SupportRequests)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__SupportRe__Staff__4A4E069C");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.SupportRequests)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__SupportRe__Stude__4959E263");
            });

            modelBuilder.Entity<UtilityMeter>(entity =>
            {
                entity.ToTable("UtilityMeter");

                entity.Property(e => e.UtilityMeterId).HasColumnName("UtilityMeterID");

                entity.Property(e => e.RecordingDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RoomId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RoomID");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.UtilityMeters)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__UtilityMe__RoomI__4F12BBB9");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.UtilityMeters)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__UtilityMe__Staff__5006DFF2");
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID");

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.FullName).HasMaxLength(255);

                entity.Property(e => e.Hometown).HasMaxLength(255);

                entity.Property(e => e.Idcard)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("IDCard");

                entity.Property(e => e.InsuranceNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
