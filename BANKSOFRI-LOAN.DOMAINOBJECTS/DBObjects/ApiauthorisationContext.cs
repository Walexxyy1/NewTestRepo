using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace BANKSOFRI_LOAN.DOMAINOBJECTS.DBObjects
{
    public partial class ApiauthorisationContext : DbContext
    {
        public ApiauthorisationContext()
        {
        }

        public ApiauthorisationContext(DbContextOptions<ApiauthorisationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AddressVerificationDetail> AddressVerificationDetails { get; set; }
        public virtual DbSet<AggregatedCounter> AggregatedCounters { get; set; }
        public virtual DbSet<Authorised> Authoriseds { get; set; }
        public virtual DbSet<AvgScoreSetup> AvgScoreSetups { get; set; }
        public virtual DbSet<BankCode> BankCodes { get; set; }
        public virtual DbSet<BankMapping> BankMappings { get; set; }
        public virtual DbSet<Counter> Counters { get; set; }
        public virtual DbSet<CreditBureauResult> CreditBureauResults { get; set; }
        public virtual DbSet<CreditRiskAssessment> CreditRiskAssessments { get; set; }
        public virtual DbSet<CreditScoreData> CreditScoreData { get; set; }
        public virtual DbSet<CustomerAccount> CustomerAccounts { get; set; }
        public virtual DbSet<CustomerBvndata> CustomerBvndata { get; set; }
        public virtual DbSet<CustomerDetail> CustomerDetails { get; set; }
        public virtual DbSet<CustomerOffer> CustomerOffers { get; set; }
        public virtual DbSet<CustomersTable> CustomersTables { get; set; }
        public virtual DbSet<DailyBillsPayment> DailyBillsPayments { get; set; }
        public virtual DbSet<Defaulter> Defaulters { get; set; }
        public virtual DbSet<Hash> Hashes { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
        public virtual DbSet<JobParameter> JobParameters { get; set; }
        public virtual DbSet<JobQueue> JobQueues { get; set; }
        public virtual DbSet<LienHistory> LienHistories { get; set; }
        public virtual DbSet<List> Lists { get; set; }
        public virtual DbSet<LoanProcessingData> LoanProcessingData { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<MonoAccount> MonoAccounts { get; set; }
        public virtual DbSet<NanoLoan> NanoLoans { get; set; }
        public virtual DbSet<NanoLoanRepayment> NanoLoanRepayments { get; set; }
        public virtual DbSet<NanoLoanRepaymentSchedule> NanoLoanRepaymentSchedules { get; set; }
        public virtual DbSet<NanoLoanRequest> NanoLoanRequests { get; set; }
        public virtual DbSet<OnboardingIssue> OnboardingIssues { get; set; }
        public virtual DbSet<OtherLoan> OtherLoans { get; set; }
        public virtual DbSet<PaystackBank> PaystackBanks { get; set; }
        public virtual DbSet<PndRecord> PndRecords { get; set; }
        public virtual DbSet<PromoCodeUser> PromoCodeUsers { get; set; }
        public virtual DbSet<ProvenirDecisionData> ProvenirDecisionData { get; set; }
        public virtual DbSet<Rate> Rates { get; set; }
        public virtual DbSet<Reversal> Reversals { get; set; }
        public virtual DbSet<RiskAssessmentScore> RiskAssessmentScores { get; set; }
        public virtual DbSet<Schema> Schemas { get; set; }
        public virtual DbSet<Server> Servers { get; set; }
        public virtual DbSet<ServiceLog> ServiceLogs { get; set; }
        public virtual DbSet<Set> Sets { get; set; }
        public virtual DbSet<SofriOtherLoanCustomer> SofriOtherLoanCustomers { get; set; }
        public virtual DbSet<SofriPromo> SofriPromos { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<TestBvnrecord> TestBvnrecords { get; set; }
        public virtual DbSet<TransactionDetail> TransactionDetails { get; set; }
        public virtual DbSet<UserAuthentication> UserAuthentications { get; set; }
        public virtual DbSet<VerificationRequest> VerificationRequests { get; set; }
       

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                                               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                                               .AddJsonFile("appsettings.json")
                                               .Build();
                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddressVerificationDetail>(entity =>
            {
                entity.Property(e => e.Birthdate)
                    .HasColumnType("datetime")
                    .HasColumnName("birthdate");

                entity.Property(e => e.City)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("city");

                entity.Property(e => e.Country)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("country");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstname");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.Lattitude)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lattitude");

                entity.Property(e => e.Lga)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lga");

                entity.Property(e => e.Longitude)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("longitude");

                entity.Property(e => e.Middlename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("middlename");

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Reference)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("reference");

                entity.Property(e => e.State)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("state");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.Street)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("street");
            });

            modelBuilder.Entity<AggregatedCounter>(entity =>
            {
                entity.ToTable("AggregatedCounter", "HangFire");

                entity.HasIndex(e => e.Key, "UX_HangFire_CounterAggregated_Key")
                    .IsUnique();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

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

            modelBuilder.Entity<AvgScoreSetup>(entity =>
            {
                entity.ToTable("AvgScoreSetup");

                entity.Property(e => e.RiskName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WeightAvg).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<BankCode>(entity =>
            {
                entity.Property(e => e.Code)
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

            modelBuilder.Entity<BankMapping>(entity =>
            {
                entity.ToTable("BankMapping");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.BOneCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("B_ONE_CODE");

                entity.Property(e => e.BOneName)
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("B_ONE_NAME");

                entity.Property(e => e.InstitutionCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Institution_code");

                entity.Property(e => e.InstitutionName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false)
                    .HasColumnName("Institution_name");
            });

            modelBuilder.Entity<Counter>(entity =>
            {
                entity.ToTable("Counter", "HangFire");

                entity.HasIndex(e => e.Key, "IX_HangFire_Counter_Key");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<CreditBureauResult>(entity =>
            {
                entity.Property(e => e.CreditBureauDetails)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.CreditRemark)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");
            });

            modelBuilder.Entity<CreditRiskAssessment>(entity =>
            {
                entity.ToTable("CreditRiskAssessment");

                entity.Property(e => e.Options)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RiskItem)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Score).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<CreditScoreData>(entity =>
            {
                entity.Property(e => e.Bureau)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.ScoreData)
                    .IsRequired()
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerAccount>(entity =>
            {
                entity.Property(e => e.AccountNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Bvn).HasColumnName("BVN");

                entity.Property(e => e.City).HasColumnName("city");

                entity.Property(e => e.CustomerId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.GeneralStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.HomeAddress).HasColumnName("homeAddress");

                entity.Property(e => e.IdentificationImage).HasColumnName("identificationImage");

                entity.Property(e => e.IdentificationType).HasColumnName("identificationType");

                entity.Property(e => e.Image).HasColumnName("image");

                entity.Property(e => e.ImageType).HasColumnName("imageType");

                entity.Property(e => e.LastName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Lga).HasColumnName("lga");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Signature).HasColumnName("signature");

                entity.Property(e => e.SignatureType).HasColumnName("signatureType");

                entity.Property(e => e.State).HasColumnName("state");

                entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            });

            modelBuilder.Entity<CustomerBvndata>(entity =>
            {
                entity.ToTable("CustomerBVNData");

                entity.Property(e => e.Birthdate)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("birthdate");

                entity.Property(e => e.Bvn)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("bvn");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.EnrollmentBank)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("enrollmentBank");

                entity.Property(e => e.EnrollmentBranch)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("enrollmentBranch");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstname");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("gender");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.LevelOfAccount)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("levelOfAccount");

                entity.Property(e => e.LgaOfOrigin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lgaOfOrigin");

                entity.Property(e => e.LgaOfResidence)
                    .HasMaxLength(150)
                    .IsUnicode(false)
                    .HasColumnName("lgaOfResidence");

                entity.Property(e => e.MaritalStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("maritalStatus");

                entity.Property(e => e.Middlename)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("middlename");

                entity.Property(e => e.NameOnCard)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("nameOnCard");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.Photo)
                    .IsUnicode(false)
                    .HasColumnName("photo");

                entity.Property(e => e.ResidentialAddress)
                    .HasMaxLength(350)
                    .IsUnicode(false)
                    .HasColumnName("residentialAddress");

                entity.Property(e => e.StateOfOrigin)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("stateOfOrigin");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            modelBuilder.Entity<CustomerDetail>(entity =>
            {
                entity.Property(e => e.Bvn)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("BVN");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Country)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DateOfBirth)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.EmploymentType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.Income).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Industry)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.KycScore)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("KYC_score");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LevelOfEducation)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.LoanAmount).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.LoanPurpose)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.MaritalStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PostalCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.ResidenceStatus)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CustomerOffer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Offers)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<CustomersTable>(entity =>
            {
                entity.ToTable("CustomersTable");

                entity.HasIndex(e => e.Email, "IX_Customers");

                entity.Property(e => e.DateCreated).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<DailyBillsPayment>(entity =>
            {
                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.Narration)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Defaulter>(entity =>
            {
                entity.HasIndex(e => e.CustomerId, "IX_Defaulters")
                    .IsUnique();

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");
            });

            modelBuilder.Entity<Hash>(entity =>
            {
                entity.ToTable("Hash", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Hash_ExpireAt");

                entity.HasIndex(e => e.Key, "IX_HangFire_Hash_Key");

                entity.HasIndex(e => new { e.Key, e.Field }, "UX_HangFire_Hash_Key_Field")
                    .IsUnique();

                entity.Property(e => e.Field)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.ToTable("Job", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Job_ExpireAt");

                entity.HasIndex(e => e.StateName, "IX_HangFire_Job_StateName");

                entity.Property(e => e.Arguments).IsRequired();

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.InvocationData).IsRequired();

                entity.Property(e => e.StateName).HasMaxLength(20);
            });

            modelBuilder.Entity<JobParameter>(entity =>
            {
                entity.ToTable("JobParameter", "HangFire");

                entity.HasIndex(e => new { e.JobId, e.Name }, "IX_HangFire_JobParameter_JobIdAndName");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobParameters)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_JobParameter_Job");
            });

            modelBuilder.Entity<JobQueue>(entity =>
            {
                entity.ToTable("JobQueue", "HangFire");

                entity.HasIndex(e => new { e.Queue, e.FetchedAt }, "IX_HangFire_JobQueue_QueueAndFetchedAt");

                entity.Property(e => e.FetchedAt).HasColumnType("datetime");

                entity.Property(e => e.Queue)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<LienHistory>(entity =>
            {
                entity.ToTable("LienHistory");

                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.LienAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.LienReferenceId)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LienStatus)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.LoanReferenceId)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.RemoveDate).HasColumnType("date");
            });

            modelBuilder.Entity<List>(entity =>
            {
                entity.ToTable("List", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_List_ExpireAt");

                entity.HasIndex(e => e.Key, "IX_HangFire_List_Key");

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);
            });

            modelBuilder.Entity<LoanProcessingData>(entity =>
            {
                entity.Property(e => e.ApplicationId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ApplicationXml)
                    .IsRequired()
                    .IsUnicode(false)
                    .HasColumnName("ApplicationXML");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DeclineReasons)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.EmploymentType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Errors)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Guid)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.OfferStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Offers)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.OverallDecision)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RequestId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RiskLevel)
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

            modelBuilder.Entity<MonoAccount>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("date");

                entity.Property(e => e.MonoAccountId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NanoLoan>(entity =>
            {
                entity.HasIndex(e => e.LoanReferenceId, "IX_NanoLoans")
                    .IsUnique();

                entity.Property(e => e.BankOneReferenceId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.InterestAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.InterestRate).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.LoanAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.LoanBalance).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.LoanReferenceId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NextRepaymentDate)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).IsUnicode(false);

                entity.Property(e => e.RepaymentEndDate).HasColumnType("datetime");

                entity.Property(e => e.RepaymentStartDate).HasColumnType("datetime");

                entity.Property(e => e.SofriAccountNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TotalPayable).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<NanoLoanRepayment>(entity =>
            {
                entity.ToTable("NanoLoanRepayment");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AmountPaid).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.LoanBalance).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.LoanReferenceId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.NewBalance).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.Remark)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.RepaymentDiscountAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RepaymentDiscountRate).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RepaymentReference)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NanoLoanRepaymentSchedule>(entity =>
            {
                entity.ToTable("NanoLoanRepaymentSchedule");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.InterestDue).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.LoanReferenceId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PrincipalDue).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.RepaymentDate).HasColumnType("date");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<NanoLoanRequest>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EmploymentType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Income).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoanAmount).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OnboardingIssue>(entity =>
            {
                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.IssueDetails)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OtherLoan>(entity =>
            {
                entity.Property(e => e.Bvn)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .HasColumnName("BVN");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoanAmount).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.LoanPurpose)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.LoanType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PaystackBank>(entity =>
            {
                entity.HasIndex(e => e.Code, "IX_PaystackBanks")
                    .IsUnique();

                entity.Property(e => e.Code)
                    .IsRequired()
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(150)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PndRecord>(entity =>
            {
                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AuthCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PromoCodeUser>(entity =>
            {
                entity.Property(e => e.AccountNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.PromoCode)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ProvenirDecisionData>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.LoanData)
                    .IsRequired()
                    .IsUnicode(false);

                entity.Property(e => e.ReferenceId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("ReferenceID");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Rate>(entity =>
            {
                entity.Property(e => e.BasisPoint).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.EmploymentStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.InterestRate).HasColumnType("decimal(18, 1)");

                entity.Property(e => e.RiskLevel)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Reversal>(entity =>
            {
                entity.HasIndex(e => e.RetrievalReference, "IX_Reversals")
                    .IsUnique();

                entity.Property(e => e.Amount).HasColumnType("numeric(18, 0)");

                entity.Property(e => e.RetrialTime).HasColumnType("datetime");

                entity.Property(e => e.RetrievalReference)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TransactionDate).HasColumnType("datetime");

                entity.Property(e => e.TransactionType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RiskAssessmentScore>(entity =>
            {
                entity.ToTable("RiskAssessmentScore");

                entity.Property(e => e.AssessmentScore).HasColumnType("numeric(18, 2)");

                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("CustomerID");

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.RiskItem)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.WeightedAvgScore).HasColumnType("numeric(18, 2)");
            });

            modelBuilder.Entity<Schema>(entity =>
            {
                entity.HasKey(e => e.Version)
                    .HasName("PK_HangFire_Schema");

                entity.ToTable("Schema", "HangFire");

                entity.Property(e => e.Version).ValueGeneratedNever();
            });

            modelBuilder.Entity<Server>(entity =>
            {
                entity.ToTable("Server", "HangFire");

                entity.Property(e => e.Id).HasMaxLength(200);

                entity.Property(e => e.LastHeartbeat).HasColumnType("datetime");
            });

            modelBuilder.Entity<ServiceLog>(entity =>
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

            modelBuilder.Entity<Set>(entity =>
            {
                entity.ToTable("Set", "HangFire");

                entity.HasIndex(e => e.ExpireAt, "IX_HangFire_Set_ExpireAt");

                entity.HasIndex(e => e.Key, "IX_HangFire_Set_Key");

                entity.HasIndex(e => new { e.Key, e.Value }, "UX_HangFire_Set_KeyAndValue")
                    .IsUnique();

                entity.Property(e => e.ExpireAt).HasColumnType("datetime");

                entity.Property(e => e.Key)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasMaxLength(256);
            });

            modelBuilder.Entity<SofriOtherLoanCustomer>(entity =>
            {
                entity.Property(e => e.CustomerId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoanId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.LoanRepaymentAmount)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("loanRepaymentAmount");

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.SofriAccountNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<SofriPromo>(entity =>
            {
                entity.ToTable("SofriPromo");

                entity.Property(e => e.AmountValue).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Balance).HasColumnName("balance");

                entity.Property(e => e.CodeType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.DateUsed)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Expiration).HasColumnType("datetime");

                entity.Property(e => e.Indentifier)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PromoCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.PromoOwnerAccountNumber)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PromoOwnerCustomerId)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UsedBy)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<State>(entity =>
            {
                entity.ToTable("State", "HangFire");

                entity.HasIndex(e => e.JobId, "IX_HangFire_State_JobId");

                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Reason).HasMaxLength(100);

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.States)
                    .HasForeignKey(d => d.JobId)
                    .HasConstraintName("FK_HangFire_State_Job");
            });

            modelBuilder.Entity<TestBvnrecord>(entity =>
            {
                entity.ToTable("TestBVNRecords");

                entity.Property(e => e.Bvn)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("BVN");

                entity.Property(e => e.Dob)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("DOB");

                entity.Property(e => e.Gender)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<TransactionDetail>(entity =>
            {
                entity.Property(e => e.Amount)
                    .HasColumnType("numeric(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.CurrencyCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("currencyCode");

                entity.Property(e => e.CustNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("custNo");

                entity.Property(e => e.Date)
                    .HasColumnType("datetime")
                    .HasColumnName("date");

                entity.Property(e => e.DestinationAccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("destinationAccountName");

                entity.Property(e => e.DestinationAccountNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("destinationAccountNo");

                entity.Property(e => e.DestinationBankCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("destinationBankCode");

                entity.Property(e => e.LookupReference)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lookupReference");

                entity.Property(e => e.Narration)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("narration");

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("referenceNo");

                entity.Property(e => e.SourceAccount)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sourceAccount");

                entity.Property(e => e.SourceAccountName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("sourceAccountName");

                entity.Property(e => e.TransactionType)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserAuthentication>(entity =>
            {
                entity.ToTable("UserAuthentication");

                entity.Property(e => e.ExpirationTime).HasColumnType("datetime");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TimeIssued).HasColumnType("datetime");

                entity.Property(e => e.TimeUsed).HasColumnType("datetime");

                entity.Property(e => e.TokenCode)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<VerificationRequest>(entity =>
            {
                entity.ToTable("VerificationRequest");

                entity.Property(e => e.Date).HasColumnType("datetime");

                entity.Property(e => e.Dob)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("dob");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("firstname");

                entity.Property(e => e.IdNumber)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("idNumber");

                entity.Property(e => e.IdType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("idType");

                entity.Property(e => e.Landmark)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("landmark");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lastname");

                entity.Property(e => e.Lga)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("lga");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("phone");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("state");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("street");

                entity.Property(e => e.VerificationStatus)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
