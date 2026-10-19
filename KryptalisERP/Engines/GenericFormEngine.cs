using Dapper;
using Microsoft.Data.SqlClient;
using System;

namespace KryptalisERP.Engines
{
    public class GenericFormEngine
    {
        private readonly string _connStr;

        public GenericFormEngine(string connStr)
        {
            _connStr = connStr;
        }

        public class ModuleConfig
        {
            public int Id { get; set; }
            public string ModuleName { get; set; }
            public string DisplayName { get; set; }
            public string TargetTable { get; set; }
            public bool IsMasterDetail { get; set; }
        }

        // Public method για χρήση στο MainForm
        public ModuleConfig GetModule(string name)
        {
            using var conn = new SqlConnection(_connStr);
            return conn.QueryFirstOrDefault<ModuleConfig>(
                "SELECT * FROM AppModules WHERE ModuleName = @Name AND IsActive = 1",
                new { Name = name });
        }

        // Μπορείς να κρατήσεις TableExists αν θέλεις έλεγχο ασφαλείας
        public bool TableExists(string tableName)
        {
            using var conn = new SqlConnection(_connStr);
            var count = conn.ExecuteScalar<int>("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = @T", new { T = tableName });
            return count > 0;
        }
    }
}