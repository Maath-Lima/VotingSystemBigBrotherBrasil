using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VotingSystemBigBrotherBrasil.Consumer.Data.Migrations
{
    /// <inheritdoc />
    public partial class spIncrementVote : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[InsertVote]
                    @ParticipantName varchar(50)
                AS
                BEGIN
                    SET NOCOUNT ON;
                    DECLARE @vote_count INT;

                    SET @vote_count = (
                        SELECT 
                                Votes
                        FROM
                                Votes
                    ) + 1
                
                    UPDATE Votes SET Votes = @vote_count WHERE ParticipantName = @ParticipantName
                END";

            migrationBuilder.Sql(sp);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
