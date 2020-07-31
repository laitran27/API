using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthApi.Models
{
    public class Entities
    {
    }

    public partial class AR_ShopType
    {
        public AR_ShopType()
        {
        }

        public AR_ShopType(string code)
        {
            Code = code;
        }

        public string Code { get; set; }
        public string Descr { get; set; }
        public DateTime LUpd_Datetime { get; set; }
        public string LUpd_Prog { get; set; }
        public string LUpd_User { get; set; }
        public DateTime Crtd_Datetime { get; set; }
        public string Crtd_Prog { get; set; }
        public string Crtd_User { get; set; }
    }

    public partial class AR_Channel
    {
        public AR_Channel()
        {
        }

        public AR_Channel(string code)
        {
            Code = code;
        }

        public string Code { get; set; }
        public string Descr { get; set; }
    }

    public partial class VS_Company
    {
        public VS_Company()
        {
        }

        public VS_Company(string cpnyID)
        {
            CpnyID = cpnyID;
        }

        public string CpnyID { get; set; }
        public string CpnyName { get; set; }
    }
    public partial class IN_Inventory
    {
        public IN_Inventory()
        {
        }

        public IN_Inventory(string invtID)
        {
            InvtID = invtID;
        }

        public string InvtID { get; set; }
        public string Descr { get; set; }
        public string StkUnit { get; set; }
    }

    public partial class IN_UnitConversion
    {
        public IN_UnitConversion()
        {
        }

        public IN_UnitConversion(string invtID)
        {
            InvtID = invtID;
        }

        public string InvtID { get; set; }
        public string FromUnit { get; set; }
        
    }

    public partial class IN_TransApi
    {
        public IN_TransApi()
        {
        }

        public IN_TransApi(string batNbr, string branchID)
        {
            BatNbr = batNbr;
            BranchID = branchID;
        }

        public string BranchID { get; set; }
        public string BatNbr { get; set; }
        public string InvtID { get; set; }
        public string UnitDesc { get; set; }
        public double Qty { get; set; }
        public double UnitPrice { get; set; }
        public double TranAmt { get; set; }
        public DateTime LUpd_Datetime { get; set; }
        public string LUpd_Prog { get; set; }
        public string LUpd_User { get; set; }
        public DateTime Crtd_Datetime { get; set; }
        public string Crtd_Prog { get; set; }
        public string Crtd_User { get; set; }
    }

    public partial class Product
    {
        public string InvtID { get; set; }
        public string Descr { get; set; }
        public string StkUnit { get; set; }
        public double Qty { get; set; }
        public double CartQty { get; set; }
        public double Price { get; set; }
      
    } 
    
    public partial class CartDetail
    {
        public string UserID { get; set; }
        public string InvtID { get; set; }
        public string Descr { get; set; }
        public string StkUnit { get; set; }
        public int Qty { get; set; }
        public int CartQty { get; set; }
        public double Price { get; set; }
      
    }
    public partial class Batch
    {
        public Batch()
        {
        }
        public Batch(string batNbr, string branchID)
        {
            BatNbr = batNbr;
            BranchID = branchID;
        }
        public string BranchID { get; set; }
        public string Module { get; set; }
        public string BatNbr { get; set; }
        public string Status { get; set; }
        public double TotAmt { get; set; }
        public DateTime DateEnt { get; set; }
        public string Descr { get; set; }
        public string OrigBranchID { get; set; }
        public int NoteID { get; set; }
        public short Rlsed { get; set; }
        public DateTime LUpd_Datetime { get; set; }
        public string LUpd_Prog { get; set; }
        public string LUpd_User { get; set; }
        public DateTime Crtd_Datetime { get; set; }
        public string Crtd_Prog { get; set; }
        public string Crtd_User { get; set; }
    }

    public class AR_ShopTypesConfiguration : IEntityTypeConfiguration<AR_ShopType>
    {
        public void Configure(EntityTypeBuilder<AR_ShopType> builder)
        {
            // Set configuration for entity
            builder.ToTable("AR_ShopType");
            // Set key for entity
            builder.HasKey(p => p.Code);
            builder.Property(p => p.Descr).HasColumnType("nvarchar(200)").IsRequired();
        }
    }

    public class AR_ChannelsConfiguration : IEntityTypeConfiguration<AR_Channel>
    {
        public void Configure(EntityTypeBuilder<AR_Channel> builder)
        {
            // Set configuration for entity
            builder.ToTable("AR_Channel");
            // Set key for entity
            builder.HasKey(p => p.Code);
            builder.Property(p => p.Descr).HasColumnType("nvarchar(200)").IsRequired();
        }
    }
    public class VS_CompanyConfiguration : IEntityTypeConfiguration<VS_Company>
    {
        public void Configure(EntityTypeBuilder<VS_Company> builder)
        {
            // Set configuration for entity
            builder.ToTable("VS_Company");
            // Set key for entity
            builder.HasKey(p => p.CpnyID);
            builder.Property(p => p.CpnyName).HasColumnType("nvarchar(200)").IsRequired();
        }
    }

    public class IN_InventoryConfiguration : IEntityTypeConfiguration<IN_Inventory>
    {
        public void Configure(EntityTypeBuilder<IN_Inventory> builder)
        {
            // Set configuration for entity
            builder.ToTable("IN_Inventory");
            // Set key for entity
            builder.HasKey(p => p.InvtID);
            builder.Property(p => p.Descr).HasColumnType("nvarchar(200)").IsRequired();
        }
    }

    public class IN_UnitConversionConfiguration : IEntityTypeConfiguration<IN_UnitConversion>
    {
        public void Configure(EntityTypeBuilder<IN_UnitConversion> builder)
        {
            // Set configuration for entity
            builder.ToTable("IN_UnitConversion");
            // Set key for entity
            builder.HasKey(p => new { p.InvtID, p.FromUnit } );
        }
    }

    public class IN_TransApiConfiguration : IEntityTypeConfiguration<IN_TransApi>
    {
        public void Configure(EntityTypeBuilder<IN_TransApi> builder)
        {
            // Set configuration for entity
            builder.ToTable("IN_TransApi");
            // Set key for entity
            builder.HasKey(p => new { p.BranchID, p.BatNbr });
        }
    } 
    
    public class CartDetailConfiguration : IEntityTypeConfiguration<CartDetail>
    {
        public void Configure(EntityTypeBuilder<CartDetail> builder)
        {
            // Set configuration for entity
            builder.ToTable("CartDetail");
            // Set key for entity
            builder.HasKey(p => new { p.InvtID, p.UserID });
        }
    }


    public class BatchesConfiguration : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            // Set configuration for entity
            builder.ToTable("Batch");
            // Set key for entity
            builder.HasKey(p => new { p.BranchID, p.BatNbr });
            builder.Property(p => p.BranchID).HasColumnType("nvarchar(60)").IsRequired();
            builder.Property(p => p.Module).HasColumnType("varchar(2)").IsRequired();
            builder.Property(p => p.BatNbr).HasColumnType("varchar(2)").IsRequired();
            builder.Property(p => p.TotAmt).HasColumnType("float").IsRequired();
            builder.Property(p => p.DateEnt).HasColumnType("smalldatetime").IsRequired();
            builder.Property(p => p.Descr).HasColumnType("nvarchar(200)").IsRequired();
            builder.Property(p => p.OrigBranchID).HasColumnType("varchar(30)").IsRequired();
            builder.Property(p => p.NoteID).HasColumnType("int").IsRequired();
            builder.Property(p => p.Rlsed).HasColumnType("smallint").IsRequired();
        }
    }


    public class WideWorldImportersDbContext : DbContext
    {
        public WideWorldImportersDbContext(DbContextOptions<WideWorldImportersDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply configurations for entity

            modelBuilder
                .ApplyConfiguration(new AR_ShopTypesConfiguration());

            modelBuilder
                .ApplyConfiguration(new AR_ChannelsConfiguration());
            modelBuilder
               .ApplyConfiguration(new VS_CompanyConfiguration());
            modelBuilder
                .ApplyConfiguration(new BatchesConfiguration());
            modelBuilder
                .ApplyConfiguration(new IN_InventoryConfiguration());
            modelBuilder
                .ApplyConfiguration(new IN_TransApiConfiguration());
            modelBuilder
                .ApplyConfiguration(new IN_UnitConversionConfiguration()); 
            modelBuilder
                .ApplyConfiguration(new CartDetailConfiguration());

            base.OnModelCreating(modelBuilder);
        }

     

        public DbSet<AR_ShopType> AR_ShopTypes { get; set; }
        public DbSet<AR_Channel> AR_Channels { get; set; }
        public DbSet<VS_Company> VS_Company { get; set; }
        public DbSet<Batch> Batches { get; set; }
        public DbSet<IN_Inventory> IN_Inventory { get; set; }
        public DbSet<IN_TransApi> IN_Trans { get; set; }
        public DbSet<IN_UnitConversion> IN_UnitConversion { get; set; }
        public DbSet<CartDetail> CartDetails { get; set; }
    }
}
