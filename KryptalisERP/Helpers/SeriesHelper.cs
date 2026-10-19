using Dapper;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;

namespace KryptalisERP.Helpers
{
    public static class SeriesHelper
    {
        /// <summary>
        /// Fetches document series filtered by Category and User Role.
        /// </summary>
        /// <param name="connStr">Connection String</param>
        /// <param name="companyId">Current Company ID</param>
        /// <param name="branchId">Current Branch ID (nullable for company-wide)</param>
        /// <param name="category">SALES, PURCHASE, RECEIPT, PAYMENT, INVENTORY</param>
        /// <param name="userRole">Current User's Role (e.g., "StoreKeeper")</param>
        /// <returns>List of available series</returns>
        public static List<DocumentSeriesDto> GetAvailableSeries(
            string connStr,
            int companyId,
            int? branchId,
            string category,
            string userRole)
        {
            using var conn = new SqlConnection(connStr);

            // SQL Logic:
            // 1. Match Company
            // 2. Match Branch (Specific OR Null/Global)
            // 3. Match Category
            // 4. Check Role: AllowedRoles is NULL (all allowed) OR contains userRole
            string sql = @"
                SELECT ds.Id, ds.Code, ds.Name, ds.Prefix, ds.NextNumber, dt.DocTypeName as TypeName
                FROM DocumentSeries ds
                JOIN DocumentTypes dt ON ds.DocumentTypeId = dt.Id
                WHERE ds.CompanyId = @CompanyId
                  AND ds.IsActive = 1
                  AND ds.Category = @Category
                  AND (ds.BranchId = @BranchId OR ds.BranchId IS NULL)
                  AND (ds.AllowedRoles IS NULL OR CHARINDEX(@UserRole, ds.AllowedRoles) > 0)
                ORDER BY ds.Name";

            var parameters = new
            {
                CompanyId = companyId,
                BranchId = branchId,
                Category = category,
                UserRole = userRole
            };

            return conn.Query<DocumentSeriesDto>(sql, parameters).ToList();
        }
    }

    public class DocumentSeriesDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Prefix { get; set; }
        public int NextNumber { get; set; }
        public string TypeName { get; set; }

        // Display helper for ComboBox
        public override string ToString() => $"{Name} ({Prefix}...)";
    }
}