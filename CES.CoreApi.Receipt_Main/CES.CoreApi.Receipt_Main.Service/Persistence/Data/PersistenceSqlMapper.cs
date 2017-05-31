using CES.Data.Sql;

namespace CES.CoreApi.Shared.Persistence.Data
{
    public  class PersistenceSqlMapper
    {

        public readonly SqlMapper SqlMapper = null;

        public PersistenceSqlMapper(SqlMapper sqlMapper)
        {
            SqlMapper = sqlMapper;
        }
        public PersistenceDataCommand CreateCommand(string connectionKey, string sprocName)
        {
            var sql = SqlMapper.CreateCommand(connectionKey, sprocName);
            var customCommand = new PersistenceDataCommand((DataCommand)sql, sprocName);


            return customCommand;
        }

        public PersistenceDataQuery CreateQueryAgain(string connectionKey, string sprocName)
        {
            var sql = SqlMapper.CreateQueryAgain(connectionKey, sprocName);
            var customDataQuery = new PersistenceDataQuery((DataQuery)sql, sprocName);

            return customDataQuery;
        }

        public PersistenceDataQuery CreateQuery(string connectionKey, string sprocName)
        {
            var sql = SqlMapper.CreateQuery(connectionKey, sprocName);
            var customDataQuery = new PersistenceDataQuery((DataQuery)sql, sprocName);

            return customDataQuery;
        }

    }

}