using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Img_socialmedia.Models
{
    public partial class db_shutterContext : DbContext
    {
        public db_shutterContext()
        {
        }

        public db_shutterContext(DbContextOptions<db_shutterContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CollectionViewModel> Collection { get; set; }
        public virtual DbSet<CollectionDetailViewModel> CollectionDetail { get; set; }
        public virtual DbSet<CommentViewModel> Comment { get; set; }
        public virtual DbSet<EventTypeViewModel> EventType { get; set; }
        public virtual DbSet<ExternalLoginViewModel> ExternalLogin { get; set; }
        public virtual DbSet<FollowViewModel> Follow { get; set; }
        public virtual DbSet<NotificationViewModel> Notification { get; set; }
        public virtual DbSet<PhotoViewModel> Photo { get; set; }
        public virtual DbSet<PostViewModel> Post { get; set; }
        public virtual DbSet<UserViewModel> User { get; set; }
        


        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    if (!optionsBuilder.IsConfigured)
        //    {
        //        optionsBuilder.UseSqlServer("Server=DESKTOP-C0LJF9I;Database=db_shutter;Trusted_Connection=True;");
        //    }
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CollectionViewModel>(entity =>
            {
                entity.ToTable("collection");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnName("create_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Collection)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_collection_user");
            });

            modelBuilder.Entity<CollectionDetailViewModel>(entity =>
            {
                entity.ToTable("collection_detail");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CollectionId).HasColumnName("collection_id");

                entity.Property(e => e.PhotoId).HasColumnName("photo_id");

                entity.HasOne(d => d.Collection)
                    .WithMany(p => p.CollectionDetail)
                    .HasForeignKey(d => d.CollectionId)
                    .HasConstraintName("FK_collection_detail_collection");

                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.CollectionDetail)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_collection_detail_photo");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.CollectionDetails)
                    .HasForeignKey(d => d.CollectionId)
                    .HasConstraintName("FK_collection_detail_post");
            });

            modelBuilder.Entity<CommentViewModel>(entity =>
            {
                entity.ToTable("comment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Contents)
                    .HasColumnName("contents")
                    .IsUnicode(false);

                entity.Property(e => e.CreateAt)
                    .HasColumnName("create_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_comment_post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comment)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_comment_user");
            });

            modelBuilder.Entity<EventTypeViewModel>(entity =>
            {
                entity.ToTable("event_type");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ExternalLoginViewModel>(entity =>
            {
                entity.HasKey(e => e.LoginProvider);

                entity.Property(e => e.LoginProvider)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProviderKey)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ExternalLogin)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ExternalLogin_user");
            });

            modelBuilder.Entity<FollowViewModel>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("follow");

                entity.Property(e => e.Followed).HasColumnName("followed");

                entity.Property(e => e.FollowerId).HasColumnName("follower_id");

                entity.HasOne(d => d.Follower)
                    .WithMany()
                    .HasForeignKey(d => d.FollowerId)
                    .HasConstraintName("FK_follow_user");
            });

            modelBuilder.Entity<NotificationViewModel>(entity =>
            {
                entity.ToTable("notification");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnName("create_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.EventId).HasColumnName("event_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TriggerUserId).HasColumnName("trigger_user_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.EventId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_notification_event_type");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_notification_post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notification)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_notification_user");
            });

            modelBuilder.Entity<PhotoViewModel>(entity =>
            {
                entity.ToTable("photo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Aperture)
                    .HasColumnName("aperture")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.CameraModel)
                    .HasColumnName("camera_model")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateAt)
                    .HasColumnName("create_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.FocalLength).HasColumnName("focal_length");

                entity.Property(e => e.Height).HasColumnName("height");

                entity.Property(e => e.Iso).HasColumnName("ISO");

                entity.Property(e => e.Location)
                    .HasColumnName("location")
                    .HasColumnType("text");

                entity.Property(e => e.ShutterSpeed)
                    .HasColumnName("shutter_speed")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasColumnName("url")
                    .HasColumnType("ntext");

                entity.Property(e => e.Width).HasColumnName("width");
            });

            modelBuilder.Entity<PostViewModel>(entity =>
            {
                entity.ToTable("post");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreateAt)
                    .HasColumnName("create_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.PhotoId).HasColumnName("photo_id");

                entity.Property(e => e.Tags)
                    .HasColumnName("tags")
                    .IsUnicode(false);
                entity.Property(e => e.hasban).HasColumnName("hasban");
                entity.Property(e => e.triggeredBy).HasColumnName("triggeredBy");
                entity.Property(e => e.TotalLike).HasColumnName("total_like");

                entity.Property(e => e.TotalViews).HasColumnName("total_views");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.HasOne(d => d.Photo)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.PhotoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_post_photo");
                entity.HasOne(d => d.User)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_post_user");
            });

            modelBuilder.Entity<UserViewModel>(entity =>
            {
                entity.ToTable("user");

               

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Bio)
                    .HasColumnName("bio")
                    .IsUnicode(false);

                entity.Property(e => e.CreateAt)
                    .HasColumnName("create_at")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Firstname)
                    .HasColumnName("firstname")
                    .HasMaxLength(70);

                entity.Property(e => e.FollowerCount).HasColumnName("follower_count");

                entity.Property(e => e.Lastname)
                    .HasColumnName("lastname")
                    .HasMaxLength(70);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasColumnName("password")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProfileImg)
                    .HasColumnName("profile_img")
                    .IsUnicode(false);

                entity.Property(e => e.Tags)
                    .HasColumnName("tags")
                    .IsUnicode(false);

                entity.Property(e => e.TokenId)
                    .HasColumnName("token_id")
                    .IsUnicode(false);

                //entity.Property(e => e.Username)
                //    .IsRequired()
                //    .HasColumnName("username")
                //    .HasMaxLength(50)
                //    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
