using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DAL.Models;

public partial class DB_Manager : DbContext
{
    public DB_Manager()
    {
    }

    public DB_Manager(DbContextOptions<DB_Manager> options)
        : base(options)
    {
    }

    public virtual DbSet<AvailableQueue> AvailableQueues { get; set; }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClinicQueue> ClinicQueues { get; set; }

    public virtual DbSet<Day> Days { get; set; }

    public virtual DbSet<DayDoctor> DayDoctors { get; set; }

    public virtual DbSet<Doctor> Doctors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=H:\\לאהלה\\project correct\\ClinicProject\\fullstackProject\\DAL\\data\\db.mdf;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AvailableQueue>(entity =>
        {
            entity.HasKey(e => e.QueueId).HasName("PK__Availabl__2294FA6E3A560B7D");

            entity.Property(e => e.QueueId).HasColumnName("queue_id");
            entity.Property(e => e.AppointmentDate)
                .HasColumnType("datetime")
                .HasColumnName("appointment_date");

            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");


            //entity.HasOne(d => d.Client).WithMany(p => p.AvailableQueues)
            //    .HasForeignKey(d => d.ClientId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("FK__Available__clien__778AC167");

            entity.HasOne(d => d.Doctor).WithMany(p => p.AvailableQueues)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Available__docto__787EE5A0");
        });

        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__Clients__BF21A4248370C9B3");

            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("address");
            entity.Property(e => e.BirthDate).HasColumnName("birth_date");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("last_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(15)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("phone");
        });

        modelBuilder.Entity<ClinicQueue>(entity =>
        {
            entity.HasKey(e => e.QueueId).HasName("PK__ClinicQu__2294FA6E4B08951A");

            entity.ToTable("ClinicQueue");

            entity.Property(e => e.QueueId).HasColumnName("queue_id");
            entity.Property(e => e.AppointmentDate)
                .HasColumnType("datetime")
                .HasColumnName("appointment_date");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");


            entity.HasOne(d => d.Client).WithMany(p => p.ClinicQueues)
                .HasForeignKey(d => d.ClientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClinicQue__clien__49C3F6B7");

            entity.HasOne(d => d.Doctor).WithMany(p => p.ClinicQueues)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ClinicQue__docto__5BE2A6F2");
        });

        modelBuilder.Entity<Day>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Day__3214EC078C4201A4");

            entity.ToTable("Day");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DayNum).HasColumnName("day_num");
            entity.Property(e => e.EndHour).HasColumnName("end_hour");
            entity.Property(e => e.StartHour).HasColumnName("start_hour");
        });

        modelBuilder.Entity<DayDoctor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Day_doct__3214EC07CE939F9F");

            entity.ToTable("Day_doctor");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.DayId).HasColumnName("dayId");
            entity.Property(e => e.DoctorId).HasColumnName("doctorId");

            entity.HasOne(d => d.Day).WithMany(p => p.DayDoctors)
                .HasForeignKey(d => d.DayId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Day_docto__dayId__71D1E811");

            entity.HasOne(d => d.Doctor).WithMany(p => p.DayDoctors)
                .HasForeignKey(d => d.DoctorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Day_docto__docto__72C60C4A");
        });

        modelBuilder.Entity<Doctor>(entity =>
        {
            entity.HasKey(e => e.DoctorId).HasName("PK__Doctor__F3993564BBA3F6E9");

            entity.ToTable("Doctor");

            entity.Property(e => e.DoctorId).HasColumnName("doctor_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("first_name");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("last_name");
            entity.Property(e => e.Specialization)
                .HasMaxLength(50)
                .IsUnicode(false)
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasColumnName("specialization");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
