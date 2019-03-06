using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace HierarchyTraverserLib.Providers
{
    public class DatabaseHierarchyProvider : IHierarchyProvider<DataRow>
    {
        private readonly IDbConnection _connection;

        public DatabaseHierarchyProvider(IDbConnection connection)
        {
            _connection = connection;
        }

        public IList<DataRow> LoadChildren(DataRow node)
        {
            using (var command = GetCommand(node))
            {
                var table = new DataTable();
                table.Load(command.ExecuteReader());

                return table.Rows.Cast<DataRow>().ToList();
            }
        }

        private IDbCommand GetCommand(DataRow node)
        {
            if (node == null)
            {
                return GetRootItemsCommand();
            }

            return GetChildItemsCommand((int)node["Id"]);
        }

        private IDbCommand GetRootItemsCommand()
        {
            var sql = "SELECT * FROM Menu WHERE ParentId IS NULL";

            var command = _connection.CreateCommand();
            command.CommandText = sql;

            return command;
        }

        private IDbCommand GetChildItemsCommand(int id)
        {
            var sql = "SELECT * FROM Menu WHERE ParentId = @Id";

            var command = _connection.CreateCommand();
            command.CommandText = sql;

            var param = command.CreateParameter();
            param.ParameterName = "@Id";
            param.Value = id;

            command.Parameters.Add(param);

            return command;
        }

        public IList<DataRow> LoadRootNodes()
        {
            using (var command = GetCommand(null))
            {
                var table = new DataTable();
                table.Load(command.ExecuteReader());

                return table.Rows.Cast<DataRow>().ToList();
            }
        }
    }
}
