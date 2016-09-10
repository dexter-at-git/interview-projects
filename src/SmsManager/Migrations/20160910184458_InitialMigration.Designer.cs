using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SmsManager.Data;

namespace SmsManager.Migrations
{
    [DbContext(typeof(SmsManagerContext))]
    [Migration("20160910184458_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("SmsManager.Data.CountryEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 2);

                    b.Property<string>("MobileCode")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 3);

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<decimal>("SmsPrice");

                    b.HasKey("Id");

                    b.ToTable("Country");
                });

            modelBuilder.Entity("SmsManager.Data.SmsMessageEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("DateSent");

                    b.Property<string>("From")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.Property<int>("Status");

                    b.Property<string>("To")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.ToTable("SmsMessage");
                });

            modelBuilder.Entity("SmsManager.Data.SmsMessageEntity", b =>
                {
                    b.HasOne("SmsManager.Data.CountryEntity", "Country")
                        .WithMany("SmsMessages")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
