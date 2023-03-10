// <auto-generated />
using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ScoutingReportDAL.Db;

namespace Heat_Scouting_Report.Migrations
{
    [DbContext(typeof(ScoutingReportDbContext))]
    [Migration("20230123014836_AddScoutingReport")]
    partial class AddScoutingReport
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Heat_Scouting_Report.Db.League", b =>
                {
                    b.Property<int>("LeagueKey")
                        .HasColumnType("int");

                    b.Property<bool?>("ActiveSource")
                        .HasColumnType("bit");

                    b.Property<string>("Country")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int?>("LeagueCustomGroupKey")
                        .HasColumnType("int");

                    b.Property<int?>("LeagueGroupKey")
                        .HasColumnType("int");

                    b.Property<string>("LeagueName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("SearchDisplayFlag")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.HasKey("LeagueKey");

                    b.ToTable("League");
                });

            modelBuilder.Entity("Heat_Scouting_Report.Db.Player", b =>
                {
                    b.Property<int>("PlayerKey")
                        .HasColumnType("int");

                    b.Property<bool>("ActiveAnalysisFlg")
                        .HasColumnType("bit");

                    b.Property<int?>("AgentKey")
                        .HasColumnType("int");

                    b.Property<string>("AgentName")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("AgentPhone")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("BboPlayerKey")
                        .HasColumnType("int");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("date");

                    b.Property<decimal?>("BodyFat")
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("BodyFatSource")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("BodyFat_Source");

                    b.Property<string>("CommittedTo")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<decimal?>("CourtRunTime34")
                        .HasColumnType("decimal(5,2)")
                        .HasColumnName("CourtRunTime_3_4");

                    b.Property<string>("CourtRunTime34Source")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("CourtRunTime_3_4_Source");

                    b.Property<DateTime>("DwhInsertDatetime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("dwh_insert_datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<DateTime?>("DwhUpdateDatetime")
                        .HasColumnType("datetime")
                        .HasColumnName("dwh_update_datetime");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("FreeAgentYear")
                        .HasColumnType("int");

                    b.Property<int?>("GlplayerKey")
                        .HasColumnType("int")
                        .HasColumnName("GLPlayerKey");

                    b.Property<string>("Hand")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<decimal?>("HandLength")
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("HandWHSource")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Hand_W_H_Source");

                    b.Property<decimal?>("HandWidth")
                        .HasColumnType("decimal(5,2)");

                    b.Property<string>("Handedness")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("HandednessSource")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Handedness_Source");

                    b.Property<decimal?>("Height")
                        .HasColumnType("decimal(6,4)");

                    b.Property<string>("HeightSource")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Height_Source");

                    b.Property<bool>("IsCustomData")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("LeagueCustomGroupKey")
                        .HasColumnType("int");

                    b.Property<int?>("PlayerStatusKey")
                        .HasColumnType("int");

                    b.Property<int?>("PositionKey")
                        .HasColumnType("int");

                    b.Property<decimal?>("StandingReach")
                        .HasColumnType("decimal(6,4)");

                    b.Property<string>("StandingReachSource")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("StandingReach_Source");

                    b.Property<string>("Urlphoto")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("URLPhoto");

                    b.Property<decimal?>("VerticalJumpMax")
                        .HasColumnType("decimal(6,4)");

                    b.Property<string>("VerticalJumpMaxSource")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("VerticalJumpMax_Source");

                    b.Property<decimal?>("VerticalJumpNoStep")
                        .HasColumnType("decimal(6,4)");

                    b.Property<string>("VerticalJumpNoStepSource")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("VerticalJumpNoStep_Source");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("decimal(6,2)");

                    b.Property<string>("WeightSource")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Weight_Source");

                    b.Property<decimal?>("Wing")
                        .HasColumnType("decimal(6,4)");

                    b.Property<string>("WingSource")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("Wing_Source");

                    b.Property<int?>("YearsOfService")
                        .HasColumnType("int");

                    b.HasKey("PlayerKey");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("Heat_Scouting_Report.Db.ScoutingReport", b =>
                {
                    b.Property<Guid>("ScoutingReportId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Comments")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("Defense")
                        .HasColumnType("int");

                    b.Property<int>("PlayerId")
                        .HasColumnType("int");

                    b.Property<int>("Rebound")
                        .HasColumnType("int");

                    b.Property<string>("ScoutId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("Shooting")
                        .HasColumnType("int");

                    b.HasKey("ScoutingReportId");

                    b.HasIndex("PlayerId");

                    b.HasIndex("ScoutId");

                    b.ToTable("ScoutingReports");
                });

            modelBuilder.Entity("Heat_Scouting_Report.Db.Team", b =>
                {
                    b.Property<int>("TeamKey")
                        .HasColumnType("int");

                    b.Property<int?>("ArenaKey")
                        .HasColumnType("int");

                    b.Property<string>("CoachName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Conference")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("CurrentNbateamFlg")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasColumnName("CurrentNBATeamFlg")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("LeagueKey")
                        .HasColumnType("int");

                    b.Property<int?>("LeagueKeyDomestic")
                        .HasColumnType("int")
                        .HasColumnName("LeagueKey_Domestic");

                    b.Property<string>("SubConference")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TeamCity")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TeamCountry")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("TeamName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("TeamNickname")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Urlphoto")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)")
                        .HasColumnName("URLPhoto");

                    b.HasKey("TeamKey");

                    b.HasIndex("LeagueKey");

                    b.HasIndex("LeagueKeyDomestic");

                    b.HasIndex(new[] { "TeamKey", "LeagueKey" }, "UK_DimTeam")
                        .IsUnique()
                        .HasFilter("[LeagueKey] IS NOT NULL");

                    b.ToTable("Team");
                });

            modelBuilder.Entity("Heat_Scouting_Report.Db.TeamPlayer", b =>
                {
                    b.Property<int>("PlayerKey")
                        .HasColumnType("int");

                    b.Property<int>("TeamKey")
                        .HasColumnType("int");

                    b.Property<int>("SeasonKey")
                        .HasColumnType("int");

                    b.Property<bool?>("ActiveTeamFlg")
                        .HasColumnType("bit");

                    b.Property<DateTime>("DwhInsertDatetime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasColumnName("dwh_insert_datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.HasKey("PlayerKey", "TeamKey", "SeasonKey");

                    b.HasIndex("TeamKey");

                    b.ToTable("TeamPlayer");
                });

            modelBuilder.Entity("Heat_Scouting_Report.Db.User", b =>
                {
                    b.Property<string>("AzureAdUserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool?>("ActiveFlag")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<DateTime?>("ModifiedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<int?>("Order")
                        .HasColumnType("int");

                    b.HasKey("AzureAdUserId")
                        .HasName("PK__User__76BABBB68573D222");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Heat_Scouting_Report.Db.ScoutingReport", b =>
                {
                    b.HasOne("Heat_Scouting_Report.Db.Player", "Player")
                        .WithMany("ScoutingReports")
                        .HasForeignKey("PlayerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Heat_Scouting_Report.Db.User", "Scout")
                        .WithMany("ScoutingReports")
                        .HasForeignKey("ScoutId");

                    b.Navigation("Player");

                    b.Navigation("Scout");
                });

            modelBuilder.Entity("Heat_Scouting_Report.Db.Team", b =>
                {
                    b.HasOne("Heat_Scouting_Report.Db.League", "LeagueKeyNavigation")
                        .WithMany("TeamLeagueKeyNavigations")
                        .HasForeignKey("LeagueKey")
                        .HasConstraintName("FK_Team_League");

                    b.HasOne("Heat_Scouting_Report.Db.League", "LeagueKeyDomesticNavigation")
                        .WithMany("TeamLeagueKeyDomesticNavigations")
                        .HasForeignKey("LeagueKeyDomestic")
                        .HasConstraintName("FK_Team_LeagueDomestic");

                    b.Navigation("LeagueKeyDomesticNavigation");

                    b.Navigation("LeagueKeyNavigation");
                });

            modelBuilder.Entity("Heat_Scouting_Report.Db.TeamPlayer", b =>
                {
                    b.HasOne("Heat_Scouting_Report.Db.Player", "PlayerKeyNavigation")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("PlayerKey")
                        .HasConstraintName("FK_TeamPlayer_Player")
                        .IsRequired();

                    b.HasOne("Heat_Scouting_Report.Db.Team", "TeamKeyNavigation")
                        .WithMany("TeamPlayers")
                        .HasForeignKey("TeamKey")
                        .HasConstraintName("FK_TeamPlayer_Team")
                        .IsRequired();

                    b.Navigation("PlayerKeyNavigation");

                    b.Navigation("TeamKeyNavigation");
                });

            modelBuilder.Entity("Heat_Scouting_Report.Db.League", b =>
                {
                    b.Navigation("TeamLeagueKeyDomesticNavigations");

                    b.Navigation("TeamLeagueKeyNavigations");
                });

            modelBuilder.Entity("Heat_Scouting_Report.Db.Player", b =>
                {
                    b.Navigation("ScoutingReports");

                    b.Navigation("TeamPlayers");
                });

            modelBuilder.Entity("Heat_Scouting_Report.Db.Team", b =>
                {
                    b.Navigation("TeamPlayers");
                });

            modelBuilder.Entity("Heat_Scouting_Report.Db.User", b =>
                {
                    b.Navigation("ScoutingReports");
                });
#pragma warning restore 612, 618
        }
    }
}
