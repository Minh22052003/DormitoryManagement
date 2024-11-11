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
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<DormInvoice> DormInvoices { get; set; } = null!;
        public virtual DbSet<Equipment> Equipment { get; set; } = null!;
        public virtual DbSet<Faculty> Faculties { get; set; } = null!;
        public virtual DbSet<Invoice> Invoices { get; set; } = null!;
        public virtual DbSet<InvoiceDetail> InvoiceDetails { get; set; } = null!;
        public virtual DbSet<News> News { get; set; } = null!;
        public virtual DbSet<ParkingHistory> ParkingHistories { get; set; } = null!;
        public virtual DbSet<ParkingTicket> ParkingTickets { get; set; } = null!;
        public virtual DbSet<Province> Provinces { get; set; } = null!;
        public virtual DbSet<Registration> Registrations { get; set; } = null!;
        public virtual DbSet<Relative> Relatives { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Room> Rooms { get; set; } = null!;
        public virtual DbSet<RoomEquipment> RoomEquipments { get; set; } = null!;
        public virtual DbSet<RoomStatus> RoomStatuses { get; set; } = null!;
        public virtual DbSet<RoomType> RoomTypes { get; set; } = null!;
        public virtual DbSet<Service> Services { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<SupportRequest> SupportRequests { get; set; } = null!;
        public virtual DbSet<SupportRequestType> SupportRequestTypes { get; set; } = null!;
        public virtual DbSet<UtilityMeter> UtilityMeters { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MEWMINH;Initial Catalog=DataDormitory;Integrated Security=True;Multiple Active Result Sets=True;Trust Server Certificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountStaff>(entity =>
            {
                entity.HasKey(e => e.AccountStaff1)
                    .HasName("PK__AccountS__F669B8109F5A62C9");

                entity.ToTable("AccountStaff");

                entity.Property(e => e.AccountStaff1).HasColumnName("AccountStaff");

                entity.Property(e => e.Email).HasMaxLength(255);

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
                    .HasConstraintName("FK__AccountSt__RoleI__2E70E1FD");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.AccountStaffs)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__AccountSt__Staff__2F650636");
            });

            modelBuilder.Entity<AccountStudent>(entity =>
            {
                entity.HasKey(e => e.AccountStudent1)
                    .HasName("PK__AccountS__C6A8EE53A06F3CDE");

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
                    .HasConstraintName("FK__AccountSt__Stude__28B808A7");
            });

            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.ToTable("Announcement");

                entity.Property(e => e.AnnouncementId).HasColumnName("AnnouncementID");

                entity.Property(e => e.CreationDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Image)
                    .HasMaxLength(255)
                    .IsUnicode(false);

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
                    .HasConstraintName("FK__Announcem__Staff__30592A6F");
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

                entity.Property(e => e.CourseId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CourseID");

                entity.Property(e => e.FacultyId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("FacultyID");

                entity.HasOne(d => d.Course)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.CourseId)
                    .HasConstraintName("FK__Class__CourseID__3DB3258D");

                entity.HasOne(d => d.Faculty)
                    .WithMany(p => p.Classes)
                    .HasForeignKey(d => d.FacultyId)
                    .HasConstraintName("FK__Class__FacultyID__220B0B18");
            });

            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.CourseId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("CourseID");

                entity.Property(e => e.CourseName).HasMaxLength(255);
            });

            modelBuilder.Entity<DormInvoice>(entity =>
            {
                entity.HasKey(e => e.InvoiceId)
                    .HasName("PK__DormInvo__D796AAD555485888");

                entity.ToTable("DormInvoice");

                entity.Property(e => e.InvoiceId).HasColumnName("InvoiceID");

                entity.Property(e => e.InvoiceType).HasMaxLength(255);

                entity.Property(e => e.IssueDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PayDate).HasColumnType("datetime");

                entity.Property(e => e.StaffIdCreate)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID_Create");

                entity.Property(e => e.StaffIdPay)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID_Pay");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.StaffIdCreateNavigation)
                    .WithMany(p => p.DormInvoiceStaffIdCreateNavigations)
                    .HasForeignKey(d => d.StaffIdCreate)
                    .HasConstraintName("FK__DormInvoi__Staff__38EE7070");

                entity.HasOne(d => d.StaffIdPayNavigation)
                    .WithMany(p => p.DormInvoiceStaffIdPayNavigations)
                    .HasForeignKey(d => d.StaffIdPay)
                    .HasConstraintName("FK__DormInvoi__Staff__39E294A9");
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

                entity.Property(e => e.Payer).HasMaxLength(100);

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
                    .HasConstraintName("FK__Invoice__RoomID__3429BB53");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.Invoices)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__Invoice__StaffID__3335971A");
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
                    .HasConstraintName("FK__InvoiceDe__Invoi__351DDF8C");

                entity.HasOne(d => d.Service)
                    .WithMany(p => p.InvoiceDetails)
                    .HasForeignKey(d => d.ServiceId)
                    .HasConstraintName("FK__InvoiceDe__Servi__361203C5");
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

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.Tag).HasMaxLength(255);

                entity.Property(e => e.Title).HasMaxLength(255);

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__News__StaffID__3F9B6DFF");
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
                    .HasConstraintName("FK__ParkingHi__Ticke__2D7CBDC4");
            });

            modelBuilder.Entity<ParkingTicket>(entity =>
            {
                entity.HasKey(e => e.TicketId)
                    .HasName("PK__ParkingT__712CC6270A79168C");

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
                    .HasConstraintName("FK__ParkingTi__Stude__2C88998B");
            });

            modelBuilder.Entity<Province>(entity =>
            {
                entity.ToTable("Province");

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.ProvinceName).HasMaxLength(255);
            });

            modelBuilder.Entity<Registration>(entity =>
            {
                entity.Property(e => e.RegistrationId).HasColumnName("RegistrationID");

                entity.Property(e => e.AcademicYear).HasMaxLength(20);

                entity.Property(e => e.ApplicationStatus).HasMaxLength(20);

                entity.Property(e => e.RoomTypeId).HasColumnName("RoomTypeID");

                entity.Property(e => e.Semester).HasMaxLength(10);

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StudentID");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.RoomTypeId)
                    .HasConstraintName("FK__Registrat__RoomT__7132C993");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.Registrations)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__Registrat__Stude__7226EDCC");
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
                    .HasConstraintName("FK__Relative__Studen__2B947552");
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

                entity.Property(e => e.RoomName).HasMaxLength(255);

                entity.Property(e => e.RoomNote).HasMaxLength(255);

                entity.Property(e => e.RoomStatusId).HasColumnName("RoomStatusID");

                entity.Property(e => e.RoomTypeId).HasColumnName("RoomTypeID");

                entity.HasOne(d => d.Building)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.BuildingId)
                    .HasConstraintName("FK__Room__BuildingID__23F3538A");

                entity.HasOne(d => d.RoomStatus)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomStatusId)
                    .HasConstraintName("FK__Room__RoomStatus__3EA749C6");

                entity.HasOne(d => d.RoomType)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(d => d.RoomTypeId)
                    .HasConstraintName("FK__Room__RoomTypeID__22FF2F51");
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
                    .HasConstraintName("FK__Room_Equi__Equip__25DB9BFC");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.RoomEquipments)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Room_Equi__RoomI__24E777C3");
            });

            modelBuilder.Entity<RoomStatus>(entity =>
            {
                entity.ToTable("RoomStatus");

                entity.Property(e => e.RoomStatusId).HasColumnName("RoomStatusID");

                entity.Property(e => e.RoomStatusName).HasMaxLength(255);
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

                entity.Property(e => e.AnhBhytmatTruoc)
                    .HasMaxLength(255)
                    .HasColumnName("AnhBHYTMatTruoc");

                entity.Property(e => e.AnhCmndmatSau)
                    .HasMaxLength(255)
                    .HasColumnName("AnhCMNDMatSau");

                entity.Property(e => e.AnhCmndmatTruoc)
                    .HasMaxLength(255)
                    .HasColumnName("AnhCMNDMatTruoc");

                entity.Property(e => e.AnhThe4x6).HasMaxLength(255);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.ClassId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("ClassID");

                entity.Property(e => e.DateOfIssueOfIdcard)
                    .HasColumnType("date")
                    .HasColumnName("DateOfIssueOfIDCard");

                entity.Property(e => e.District).HasMaxLength(255);

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Ethnicity).HasMaxLength(50);

                entity.Property(e => e.GiaTriSuDungTuNgay).HasColumnType("date");

                entity.Property(e => e.Idcard)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("IDCard");

                entity.Property(e => e.IdtinhCapBhxh).HasColumnName("IDTinhCapBHXH");

                entity.Property(e => e.InsuranceNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.KhamBenhBanDau).HasMaxLength(255);

                entity.Property(e => e.Nationality).HasMaxLength(50);

                entity.Property(e => e.NgayCapBhxh)
                    .HasColumnType("date")
                    .HasColumnName("NgayCapBHXH");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PlaceOfIssueOfIdcard)
                    .HasMaxLength(50)
                    .HasColumnName("PlaceOfIssueOfIDCard");

                entity.Property(e => e.PolicyCoverage).HasMaxLength(255);

                entity.Property(e => e.ProvinceId).HasColumnName("ProvinceID");

                entity.Property(e => e.Religion).HasMaxLength(50);

                entity.Property(e => e.RoomId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("RoomID");

                entity.Property(e => e.Street).HasMaxLength(255);

                entity.Property(e => e.ThoiDiem5NamLienTuc).HasColumnType("date");

                entity.Property(e => e.Ward).HasMaxLength(255);

                entity.HasOne(d => d.Class)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.ClassId)
                    .HasConstraintName("FK__Student__ClassID__26CFC035");

                entity.HasOne(d => d.IdtinhCapBhxhNavigation)
                    .WithMany(p => p.StudentIdtinhCapBhxhNavigations)
                    .HasForeignKey(d => d.IdtinhCapBhxh)
                    .HasConstraintName("FK__Student__IDTinhC__3BCADD1B");

                entity.HasOne(d => d.Province)
                    .WithMany(p => p.StudentProvinces)
                    .HasForeignKey(d => d.ProvinceId)
                    .HasConstraintName("FK__Student__Provinc__3AD6B8E2");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Students)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("FK__Student__RoomID__27C3E46E");
            });

            modelBuilder.Entity<SupportRequest>(entity =>
            {
                entity.HasKey(e => e.RequestId)
                    .HasName("PK__SupportR__33A8519AE7A22CBB");

                entity.ToTable("SupportRequest");

                entity.Property(e => e.RequestId).HasColumnName("RequestID");

                entity.Property(e => e.Image).HasMaxLength(255);

                entity.Property(e => e.RequestProcessDate).HasColumnType("datetime");

                entity.Property(e => e.RequestSentDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RequestTypeId).HasColumnName("RequestTypeID");

                entity.Property(e => e.StaffId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StaffID");

                entity.Property(e => e.Status).HasMaxLength(50);

                entity.Property(e => e.StudentId)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("StudentID");

                entity.HasOne(d => d.RequestType)
                    .WithMany(p => p.SupportRequests)
                    .HasForeignKey(d => d.RequestTypeId)
                    .HasConstraintName("FK__SupportRe__Reque__3CBF0154");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.SupportRequests)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__SupportRe__Staff__324172E1");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.SupportRequests)
                    .HasForeignKey(d => d.StudentId)
                    .HasConstraintName("FK__SupportRe__Stude__314D4EA8");
            });

            modelBuilder.Entity<SupportRequestType>(entity =>
            {
                entity.HasKey(e => e.RequestTypeId)
                    .HasName("PK__SupportR__4D328BA3F44A6F7B");

                entity.ToTable("SupportRequestType");

                entity.Property(e => e.RequestTypeId).HasColumnName("RequestTypeID");

                entity.Property(e => e.RequestTypeName).HasMaxLength(255);
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
                    .HasConstraintName("FK__UtilityMe__RoomI__370627FE");

                entity.HasOne(d => d.Staff)
                    .WithMany(p => p.UtilityMeters)
                    .HasForeignKey(d => d.StaffId)
                    .HasConstraintName("FK__UtilityMe__Staff__37FA4C37");
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

                entity.Property(e => e.Ethnicity).HasMaxLength(50);

                entity.Property(e => e.FullName).HasMaxLength(250);

                entity.Property(e => e.Hometown).HasMaxLength(255);

                entity.Property(e => e.Idcard)
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("IDCard");

                entity.Property(e => e.InsuranceNumber)
                    .HasMaxLength(12)
                    .IsUnicode(false);

                entity.Property(e => e.Nationality).HasMaxLength(50);

                entity.Property(e => e.Office).HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Religion).HasMaxLength(50);

                entity.Property(e => e.WorkSchedule).HasMaxLength(100);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
