using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.CardModels
{
    public partial class PaystackCardsContext : DbContext
    {
        public PaystackCardsContext()
        {
        }

        public PaystackCardsContext(DbContextOptions<PaystackCardsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Authorised> Authoriseds { get; set; }
        public virtual DbSet<BankCode> BankCodes { get; set; }
        public virtual DbSet<CardTransaction> CardTransactions { get; set; }
        public virtual DbSet<ChargeAttempt> ChargeAttempts { get; set; }
        public virtual DbSet<ChargeHistory> ChargeHistories { get; set; }
        public virtual DbSet<CustomerInvite> CustomerInvites { get; set; }
        public virtual DbSet<IncomeDatum> IncomeData { get; set; }
        public virtual DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                 .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                 .AddJsonFile("appsettings.json")
                 .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("CardConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Authorised>(entity =>
            {
                entity.ToTable("Authorised");

                entity.Property(e => e.AppId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AppKey)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<BankCode>(entity =>
            {
                entity.HasNoKey();

                entity.HasIndex(e => e.Code, "IX_Banks")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gateway)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CardTransaction>(entity =>
            {
                entity.Property(e => e.AuthorisationCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CardBin)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Message)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("message");

                entity.Property(e => e.Reference)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("reference");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Trans)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("trans");

                entity.Property(e => e.Transactions)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("transactions");
            });

            modelBuilder.Entity<ChargeAttempt>(entity =>
            {
                entity.Property(e => e.ChargeAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ChargedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ChargeHistory>(entity =>
            {
                entity.ToTable("ChargeHistory");

                entity.HasIndex(e => e.ReferenceId, "IX_ChargeHistory")
                    .IsUnique();

                entity.Property(e => e.AmountCharge).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.ChargedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerInvite>(entity =>
            {
                entity.ToTable("CustomerInvite");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InviteStatus)
                    .IsRequired()
                    .HasMaxLength(55)
                    .IsUnicode(false);

                entity.Property(e => e.InvitedBy)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IncomeDatum>(entity =>
            {
                entity.HasKey(e => new { e.IppisNum, e.MonthlyIncome });

                entity.ToTable("Income_Data");

                entity.Property(e => e.IppisNum)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("IPPIS_Num");

                entity.Property(e => e.MonthlyIncome)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("Monthly_Income");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Mda)
                    .IsRequired()
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("MDA");

                entity.Property(e => e.Surname)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Log>(entity =>
            {
                entity.Property(e => e.Details)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.LogTime).HasColumnType("datetime");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
