using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Mobile_Core.CommonClass;
using Mobile_Core.DB;
using Mobile_Core.ViewModel.Intraction;
using Mobile_Infrastructure.Interface.Intraction;
using Mobile_Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Mobile_Infrastructure.Repository.Intraction
{
    public class BirthdayInteractionRepository : IBirthdayInteractionRepository
    {
        private readonly MobileDbcontext _db;
        private readonly string _connectionString;

        public BirthdayInteractionRepository(MobileDbcontext db)
        {
            _db = db;
            _connectionString = db.Database.GetDbConnection().ConnectionString;
        }
        public async Task<APIResponse> AddBirthdayInteraction(BirthdayInteraction model)
        {
            try
            {
                var result = await _db.Set<SP_Response>()
                    .FromSqlInterpolated($@"
                    EXEC SP_Mobile_ManageBirthdayInteraction
                        @Action = {"ADD"},
                        @BirthdayInteractionId = {model.BirthdayInteractionId},
                        @PostId = {model.PostId},
                        @UserId = {model.UserId},
                        @InteractionType = {model.InteractionType},
                        @ParentInteractionId = {model.ParentInteractionId},
                        @CommentText = {model.CommentText ?? (object)DBNull.Value}")
                    .ToListAsync();

                var data = result?.FirstOrDefault();
                return new APIResponse
                {
                    Status = data?.Success ?? false,
                    ResponseMessage = data?.Message ?? "Some thing went wrong!"
                };
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return new APIResponse
                {
                    Status = false,
                    ResponseMessage = "Some thing went wrong!"
                };
            }
        }

        public async Task<APIResponse> DeleteBirthdayComment(DeleteViewModel model)
        {
            try
            {
                var result = await _db.Set<SP_Response>()
                    .FromSqlInterpolated($@"
                    EXEC SP_Mobile_ManageBirthdayInteraction
                        @Action = {"DELETE"},
                        @BirthdayInteractionId = {model.Id},
                        @UserId = {model.UserId}")
                       .ToListAsync();

                var data = result?.FirstOrDefault();
                return new APIResponse
                {
                    Status = data?.Success ?? false,
                    ResponseMessage = data?.Message ?? "Some thing went wrong!"
                };
            }
            catch (Exception ex)
            {
                // Log exception here if needed
                return new APIResponse
                {
                    Status = false,
                    ResponseMessage = "Some thing went wrong!"
                };
            }
        }

        public async Task<APIResponse> GetBirthdayCommentsWithReplies(int PostId)
        {
            try
            {
                using (var connection = new SqlConnection(_connectionString))
                {
                    await connection.OpenAsync();

                    var parameters = new DynamicParameters();
                    parameters.Add("@PostId", PostId);

                    // Execute the stored procedure and get JSON result
                    var jsonResult = await connection.ExecuteScalarAsync<string>(
                        "SP_Mobile_GetBirthdayCommentsWithReplies",
                        parameters,
                        commandType: CommandType.StoredProcedure);

                    // Deserialize the JSON result
                    var comments = JsonSerializer.Deserialize<List<CommentModel>>(jsonResult);
                    if (comments == null)
                    {
                        return new APIResponse
                        {
                            Status = false,
                            ResponseMessage = "No record found!",

                        };
                    }

                    if (comments.Any())
                    {
                        return new APIResponse
                        {
                            Status = true,
                            ResponseMessage = "Comments fetched successfully",
                            Data = comments
                        };
                    }
                    else
                    {
                        return new APIResponse
                        {
                            Status = false,
                            ResponseMessage = "No record found!",

                        };
                    }
                }
            }
           
            catch (Exception ex)
            {
                // Log general errors
                return new APIResponse
                {
                    Status = false,
                    ResponseMessage = "Some thing went wrong!",
                   
                };
            }
        }
    }

}
