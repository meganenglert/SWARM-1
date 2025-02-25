﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;
using SWARM.Server.Data;

namespace SWARM.Server.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("C##_UD_MENGLERT")
                .HasAnnotation("Relational:Collation", "USING_NLS_COMP")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.15")
                .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)")
                        .HasColumnName("USER_CODE");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)")
                        .HasColumnName("CLIENT_ID");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("CREATION_TIME");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("NCLOB")
                        .HasColumnName("DATA");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)")
                        .HasColumnName("DESCRIPTION");

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)")
                        .HasColumnName("DEVICE_CODE");

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("EXPIRATION");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)")
                        .HasColumnName("SESSION_ID");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)")
                        .HasColumnName("SUBJECT_ID");

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique()
                        .HasDatabaseName("IX_DEVICE_CODES_DEVICE_CODE");

                    b.HasIndex("Expiration")
                        .HasDatabaseName("IX_DEVICE_CODES_EXPIRATION");

                    b.ToTable("DEVICE_CODES");
                });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)")
                        .HasColumnName("KEY");

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)")
                        .HasColumnName("CLIENT_ID");

                    b.Property<DateTime?>("ConsumedTime")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("CONSUMED_TIME");

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("CREATION_TIME");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasMaxLength(50000)
                        .HasColumnType("NCLOB")
                        .HasColumnName("DATA");

                    b.Property<string>("Description")
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)")
                        .HasColumnName("DESCRIPTION");

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("TIMESTAMP(7)")
                        .HasColumnName("EXPIRATION");

                    b.Property<string>("SessionId")
                        .HasMaxLength(100)
                        .HasColumnType("NVARCHAR2(100)")
                        .HasColumnName("SESSION_ID");

                    b.Property<string>("SubjectId")
                        .HasMaxLength(200)
                        .HasColumnType("NVARCHAR2(200)")
                        .HasColumnName("SUBJECT_ID");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("NVARCHAR2(50)")
                        .HasColumnName("TYPE");

                    b.HasKey("Key");

                    b.HasIndex("Expiration")
                        .HasDatabaseName("IX_PERSISTED_GRANTS_EXPIRATION");

                    b.HasIndex("SubjectId", "ClientId", "Type")
                        .HasDatabaseName("IX_PERSISTED_GRANTS_SUBJECT_ID_CLIENT_ID_TYPE");

                    b.HasIndex("SubjectId", "SessionId", "Type")
                        .HasDatabaseName("IX_PERSISTED_GRANTS_SUBJECT_ID_SESSION_ID_TYPE");

                    b.ToTable("PERSISTED_GRANTS");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("ID");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("CONCURRENCY_STAMP");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("NVARCHAR2(256)")
                        .HasColumnName("NAME");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("NVARCHAR2(256)")
                        .HasColumnName("NORMALIZED_NAME");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("ROLENAMEINDEX")
                        .HasFilter("\"NORMALIZED_NAME\" IS NOT NULL");

                    b.ToTable("ASP_NET_ROLES");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("CLAIM_TYPE");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("CLAIM_VALUE");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("ROLE_ID");

                    b.HasKey("Id");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("IX_ASP_NET_ROLE_CLAIMS_ROLE_ID");

                    b.ToTable("ASP_NET_ROLE_CLAIMS");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ID")
                        .HasAnnotation("Oracle:ValueGenerationStrategy", OracleValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("CLAIM_TYPE");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("CLAIM_VALUE");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("USER_ID");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .HasDatabaseName("IX_ASP_NET_USER_CLAIMS_USER_ID");

                    b.ToTable("ASP_NET_USER_CLAIMS");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("NVARCHAR2(128)")
                        .HasColumnName("LOGIN_PROVIDER");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("NVARCHAR2(128)")
                        .HasColumnName("PROVIDER_KEY");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("PROVIDER_DISPLAY_NAME");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("USER_ID");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId")
                        .HasDatabaseName("IX_ASP_NET_USER_LOGINS_USER_ID");

                    b.ToTable("ASP_NET_USER_LOGINS");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("USER_ID");

                    b.Property<string>("RoleId")
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("ROLE_ID");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId")
                        .HasDatabaseName("IX_ASP_NET_USER_ROLES_ROLE_ID");

                    b.ToTable("ASP_NET_USER_ROLES");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("USER_ID");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("NVARCHAR2(128)")
                        .HasColumnName("LOGIN_PROVIDER");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("NVARCHAR2(128)")
                        .HasColumnName("NAME");

                    b.Property<string>("Value")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("VALUE");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("ASP_NET_USER_TOKENS");
                });

            modelBuilder.Entity("SWARM.Server.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("NVARCHAR2(450)")
                        .HasColumnName("ID");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("NUMBER(10)")
                        .HasColumnName("ACCESS_FAILED_COUNT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("CONCURRENCY_STAMP");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("NVARCHAR2(256)")
                        .HasColumnName("EMAIL");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("NUMBER(1)")
                        .HasColumnName("EMAIL_CONFIRMED");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("NUMBER(1)")
                        .HasColumnName("LOCKOUT_ENABLED");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TIMESTAMP(7) WITH TIME ZONE")
                        .HasColumnName("LOCKOUT_END");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("NVARCHAR2(256)")
                        .HasColumnName("NORMALIZED_EMAIL");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("NVARCHAR2(256)")
                        .HasColumnName("NORMALIZED_USER_NAME");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("PASSWORD_HASH");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("PHONE_NUMBER");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("NUMBER(1)")
                        .HasColumnName("PHONE_NUMBER_CONFIRMED");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("NVARCHAR2(2000)")
                        .HasColumnName("SECURITY_STAMP");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("NUMBER(1)")
                        .HasColumnName("TWO_FACTOR_ENABLED");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("NVARCHAR2(256)")
                        .HasColumnName("USER_NAME");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EMAILINDEX");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("USERNAMEINDEX")
                        .HasFilter("\"NORMALIZED_USER_NAME\" IS NOT NULL");

                    b.ToTable("ASP_NET_USERS");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_ASP_NET_ROLE_CLAIMS_ASP_NET_ROLES_ROLE_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("SWARM.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ASP_NET_USER_CLAIMS_ASP_NET_USERS_USER_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("SWARM.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ASP_NET_USER_LOGINS_ASP_NET_USERS_USER_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_ASP_NET_USER_ROLES_ASP_NET_ROLES_ROLE_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SWARM.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ASP_NET_USER_ROLES_ASP_NET_USERS_USER_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("SWARM.Server.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_ASP_NET_USER_TOKENS_ASP_NET_USERS_USER_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
