using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ReelTalkReviews.Models;

public partial class ReelTalkReviewsContext : DbContext
{
    public ReelTalkReviewsContext()
    {
    }

    public ReelTalkReviewsContext(DbContextOptions<ReelTalkReviewsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CastDetail> CastDetails { get; set; }

    public virtual DbSet<Crew> Crews { get; set; }

    public virtual DbSet<FilmCertification> FilmCertifications { get; set; }

    public virtual DbSet<FilmIndustryMember> FilmIndustryMembers { get; set; }

    public virtual DbSet<MovieDetail> MovieDetails { get; set; }

    public virtual DbSet<MoviePhoto> MoviePhotos { get; set; }

    public virtual DbSet<MovieRating> MovieRatings { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-DBQ88HK\\SQLEXPRESS2019;Database=ReelTalkReviews;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CastDetail>(entity =>
        {
            entity.HasKey(e => e.CastId).HasName("PK__CastData__68A1293C10E34199");

            entity.ToTable("CastDetail");

            entity.Property(e => e.LeadActor1As)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LeadActor2As)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LeadActor3As)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LeadActor4As)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LeadActor5As)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LeadActor6As)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LeadActor7As)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.LeadActor).WithMany(p => p.CastDetailLeadActors)
                .HasForeignKey(d => d.LeadActorId)
                .HasConstraintName("FK__CastData__LeadAc__2180FB33");

            entity.HasOne(d => d.LeadActorId2Navigation).WithMany(p => p.CastDetailLeadActorId2Navigations)
                .HasForeignKey(d => d.LeadActorId2)
                .HasConstraintName("FK__CastData__LeadAc__22751F6C");

            entity.HasOne(d => d.LeadActorId3Navigation).WithMany(p => p.CastDetailLeadActorId3Navigations)
                .HasForeignKey(d => d.LeadActorId3)
                .HasConstraintName("FK__CastData__LeadAc__236943A5");

            entity.HasOne(d => d.LeadActorId4Navigation).WithMany(p => p.CastDetailLeadActorId4Navigations)
                .HasForeignKey(d => d.LeadActorId4)
                .HasConstraintName("FK__CastData__LeadAc__245D67DE");

            entity.HasOne(d => d.LeadActorId5Navigation).WithMany(p => p.CastDetailLeadActorId5Navigations)
                .HasForeignKey(d => d.LeadActorId5)
                .HasConstraintName("FK__CastData__LeadAc__25518C17");

            entity.HasOne(d => d.LeadActorId6Navigation).WithMany(p => p.CastDetailLeadActorId6Navigations)
                .HasForeignKey(d => d.LeadActorId6)
                .HasConstraintName("FK__CastData__LeadAc__2645B050");

            entity.HasOne(d => d.LeadActorId7Navigation).WithMany(p => p.CastDetailLeadActorId7Navigations)
                .HasForeignKey(d => d.LeadActorId7)
                .HasConstraintName("FK__CastData__LeadAc__2739D489");

            entity.HasOne(d => d.Movie).WithMany(p => p.CastDetails)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__CastData__MovieI__208CD6FA");
        });

        modelBuilder.Entity<Crew>(entity =>
        {
            entity.HasKey(e => e.CrewId).HasName("PK__Crew__89BCFC2982A905CD");

            entity.ToTable("Crew");

            entity.Property(e => e.CrewId).ValueGeneratedNever();

            entity.HasOne(d => d.CinematographerNavigation).WithMany(p => p.CrewCinematographerNavigations)
                .HasForeignKey(d => d.Cinematographer)
                .HasConstraintName("FK__Crew__Cinematogr__42E1EEFE");

            entity.HasOne(d => d.DirectorNavigation).WithMany(p => p.CrewDirectorNavigations)
                .HasForeignKey(d => d.Director)
                .HasConstraintName("FK__Crew__Director__40058253");

            entity.HasOne(d => d.Movie).WithMany(p => p.Crews)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__Crew__MovieId__3F115E1A");

            entity.HasOne(d => d.MusicDirectorNavigation).WithMany(p => p.CrewMusicDirectorNavigations)
                .HasForeignKey(d => d.MusicDirector)
                .HasConstraintName("FK__Crew__MusicDirec__40F9A68C");

            entity.HasOne(d => d.ProducerNavigation).WithMany(p => p.CrewProducerNavigations)
                .HasForeignKey(d => d.Producer)
                .HasConstraintName("FK__Crew__Producer__41EDCAC5");

            entity.HasOne(d => d.WriterNavigation).WithMany(p => p.CrewWriterNavigations)
                .HasForeignKey(d => d.Writer)
                .HasConstraintName("FK__Crew__Writer__43D61337");
        });

        modelBuilder.Entity<FilmCertification>(entity =>
        {
            entity.HasKey(e => e.FilmCertificationId).HasName("PK__FilmCert__D924180A79F8CC4D");

            entity.ToTable("FilmCertification");

            entity.Property(e => e.Definition).IsUnicode(false);
            entity.Property(e => e.FilmCertificationType)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<FilmIndustryMember>(entity =>
        {
            entity.HasKey(e => e.MemberId).HasName("PK__FilmIndu__0CF04B189FECCB58");

            entity.ToTable("FilmIndustryMember");

            entity.Property(e => e.DateOfBirth).HasColumnType("date");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MemberDescription).IsUnicode(false);
            entity.Property(e => e.MemberName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Place)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<MovieDetail>(entity =>
        {
            entity.HasKey(e => e.MovieId).HasName("PK__MovieDet__4BD2941ABBBD5697");

            entity.ToTable("MovieDetail");

            entity.Property(e => e.CreateDate).HasColumnType("date");
            entity.Property(e => e.ModifiedDate).HasColumnType("date");
            entity.Property(e => e.MovieRatingOverall).HasColumnType("decimal(3, 1)");
            entity.Property(e => e.MovieTitle)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MovieType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ReleaseDate).HasColumnType("date");

            entity.HasOne(d => d.FilmCertification).WithMany(p => p.MovieDetails)
                .HasForeignKey(d => d.FilmCertificationId)
                .HasConstraintName("FK__MovieDeta__FilmC__6E01572D");
        });

        modelBuilder.Entity<MoviePhoto>(entity =>
        {
            entity.HasKey(e => e.MoviePicId).HasName("PK__MoviePho__61D4B0C15AD82921");

            entity.HasOne(d => d.Movie).WithMany(p => p.MoviePhotos)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__MoviePhot__Movie__73BA3083");
        });

        modelBuilder.Entity<MovieRating>(entity =>
        {
            entity.HasKey(e => e.MovieRatingId).HasName("PK__MovieRat__AB2CC873E857B0AF");

            entity.ToTable("MovieRating");

            entity.Property(e => e.Review)
                .IsUnicode(false)
                .HasColumnName("review");

            entity.HasOne(d => d.Movie).WithMany(p => p.MovieRatings)
                .HasForeignKey(d => d.MovieId)
                .HasConstraintName("FK__MovieRati__Movie__797309D9");

            entity.HasOne(d => d.User).WithMany(p => p.MovieRatings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__MovieRati__UserI__7A672E12");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Role__8AFACE1A1E090515");

            entity.ToTable("Role");

            entity.Property(e => e.RoleId).ValueGeneratedNever();
            entity.Property(e => e.RoleName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserData__1788CC4C7EA3C4DE");

            entity.ToTable("UserDetail");

            entity.HasIndex(e => e.UserName, "UQ__UserData__C9F28456C335EE04").IsUnique();

            entity.Property(e => e.Bio).IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("date");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastLoginDate).HasColumnType("date");
            entity.Property(e => e.ModifiedDate).HasColumnType("date");
            entity.Property(e => e.RefreshTokenExpiry).HasColumnType("date");
            entity.Property(e => e.ResetPasswordTokenExpiry).HasColumnType("date");
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Role).WithMany(p => p.UserDetails)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK__UserDetai__RoleI__3C34F16F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
