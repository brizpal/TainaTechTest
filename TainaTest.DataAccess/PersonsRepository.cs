using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TainaTest.Common;
using TainaTest.DataAccess.Common;
using TainaTest.Model;

namespace TainaTest.DataAccess
{
    /// <summary>
    /// Purpose: Data Access Repository Class [PersonsRepository] for the table [HR].[Persons].
    /// </summary>
    public class PersonsRepository : IRepository<PersonsEntity>, IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private DataHandler _dataHandler;
        private ConfigurationHandler _configurationHandler;
        private DbProviderFactory _dbProviderFactory;
        private string _connectionString;
        private string _connectionProvider;
        private int _errorCode, _rowsAffected;
        private bool _bDisposed;

        #endregion

        #region Class Methods

        public bool Insert(PersonsEntity entity)
        {
            try
            {
                var sb = new StringBuilder();
                sb.Append("INSERT [TechTest].[Persons] ");
                sb.Append("( ");
                sb.Append("[PersonId], ");
                sb.Append("[Firstname], ");
                sb.Append("[Surname], ");
                sb.Append("[Gender], ");
                sb.Append("[EmailAddress], ");
                sb.Append("[PhoneNumber] ");
                sb.Append(") ");
                sb.Append("VALUES ");
                sb.Append("( ");
                sb.Append(" @intId, ");
                sb.Append(" @chnName, ");
                sb.Append(" @intAge, ");
                sb.Append(" @dtmHiringDate, ");
                sb.Append(" @decGrossSalary, ");
                sb.Append(" ISNULL(@dtmModifiedDate, (getdate())) ");
                sb.Append(") ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Insert command for entity [Persons] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@personId", CsType.Int, ParameterDirection.Input, entity.PersonId);
                        _dataHandler.AddParameterToCommand(dbCommand, "@firstName", CsType.Int, ParameterDirection.Input, entity.Firstname);
                        _dataHandler.AddParameterToCommand(dbCommand, "@surname", CsType.Int, ParameterDirection.Input, entity.Surname);
                        _dataHandler.AddParameterToCommand(dbCommand, "@gender", CsType.Int, ParameterDirection.Input, entity.Gender);
                        _dataHandler.AddParameterToCommand(dbCommand, "@emailAddress", CsType.Int, ParameterDirection.Input, entity.EmailAddress);
                        _dataHandler.AddParameterToCommand(dbCommand, "@phoneNumber", CsType.Int, ParameterDirection.Input, entity.PhoneNumber);

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The Insert method for entity [Persons] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("PersonsRepository::Insert::Error occured.", ex);
            }
        }

        public bool Update(PersonsEntity entity)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT DMY; ");
                sb.Append("UPDATE [HR].[Persons] ");
                sb.Append("SET ");
                sb.Append("[Name] = @chnName, ");
                sb.Append("[Age] = @intAge, ");
                sb.Append("[HiringDate] = @dtmHiringDate, ");
                sb.Append("[GrossSalary] = @decGrossSalary, ");
                sb.Append("[ModifiedDate] = ISNULL(@dtmModifiedDate, (getdate())) ");
                sb.Append("WHERE ");
                sb.Append("[Id] = @intId ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Update command for entity [Persons] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@personId", CsType.Int, ParameterDirection.Input, entity.PersonId);
                        _dataHandler.AddParameterToCommand(dbCommand, "@firstName", CsType.Int, ParameterDirection.Input, entity.Firstname);
                        _dataHandler.AddParameterToCommand(dbCommand, "@surname", CsType.Int, ParameterDirection.Input, entity.Surname);
                        _dataHandler.AddParameterToCommand(dbCommand, "@gender", CsType.Int, ParameterDirection.Input, entity.Gender);
                        _dataHandler.AddParameterToCommand(dbCommand, "@emailAddress", CsType.Int, ParameterDirection.Input, entity.EmailAddress);
                        _dataHandler.AddParameterToCommand(dbCommand, "@phoneNumber", CsType.Int, ParameterDirection.Input, entity.PhoneNumber);

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The Update method for entity [Persons] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("PersonsRepository::Update::Error occured.", ex);
            }
        }

        public bool DeleteById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            try
            {
                var sb = new StringBuilder();
                sb.Append("DELETE FROM [TechTest].[Persons] ");
                sb.Append("WHERE ");
                sb.Append("[PersonId] = @personId ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db Delete command for entity [Persons] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@personId", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        _rowsAffected = dbCommand.ExecuteNonQuery();
                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The Delete method for entity [Persons] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("PersonsRepository::Delete::Error occured.", ex);
            }
        }

        public PersonsEntity SelectById(int id)
        {
            _errorCode = 0;
            _rowsAffected = 0;

            PersonsEntity returnedEntity = null;

            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT DMY; ");
                sb.Append("SELECT ");
                sb.Append("[Id], ");
                sb.Append("[Name], ");
                sb.Append("[Age], ");
                sb.Append("[HiringDate], ");
                sb.Append("[GrossSalary], ");
                sb.Append("[ModifiedDate] ");
                sb.Append("FROM [HR].[Persons] ");
                sb.Append("WHERE ");
                sb.Append("[Id] = @intId ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db SelectById command for entity [Persons] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intId", CsType.Int, ParameterDirection.Input, id);

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new PersonsEntity();
                                    entity.PersonId= reader.GetInt32(0);
                                    entity.Firstname = reader.GetString(1);
                                    entity.Surname = reader.GetString(2);
                                    entity.Gender = reader.GetString(3);
                                    entity.EmailAddress = reader.GetString(4);
                                    entity.PhoneNumber = reader.GetString(5);
                                    returnedEntity = entity;
                                    break;
                                }
                            }
                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectById method for entity [Persons] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("PersonsRepository::SelectById::Error occured.", ex);
            }
        }

        public List<PersonsEntity> SelectAll()
        {
            _errorCode = 0;
            _rowsAffected = 0;

            var returnedEntities = new List<PersonsEntity>();

            try
            {
                var sb = new StringBuilder();
                sb.Append("SET DATEFORMAT DMY; ");
                sb.Append("SELECT ");
                sb.Append("[Id], ");
                sb.Append("[Name], ");
                sb.Append("[Age], ");
                sb.Append("[HiringDate], ");
                sb.Append("[GrossSalary], ");
                sb.Append("[ModifiedDate] ");
                sb.Append("FROM [HR].[Persons] ");
                sb.Append("ORDER BY [Name] ");
                sb.Append("SELECT @intErrorCode=@@ERROR; ");

                var commandText = sb.ToString();
                sb.Clear();

                using (var dbConnection = _dbProviderFactory.CreateConnection())
                {
                    if (dbConnection == null)
                        throw new ArgumentNullException("dbConnection", "The db connection can't be null.");

                    dbConnection.ConnectionString = _connectionString;

                    using (var dbCommand = _dbProviderFactory.CreateCommand())
                    {
                        if (dbCommand == null)
                            throw new ArgumentNullException("dbCommand" + " The db command for entity [Persons] can't be null. ");

                        dbCommand.Connection = dbConnection;
                        dbCommand.CommandText = commandText;

                        //Input Parameters - None

                        //Output Parameters
                        _dataHandler.AddParameterToCommand(dbCommand, "@intErrorCode", CsType.Int, ParameterDirection.Output, null);

                        //Open Connection
                        if (dbConnection.State != ConnectionState.Open)
                            dbConnection.Open();

                        //Execute query.
                        using (var reader = dbCommand.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    var entity = new PersonsEntity();
                                    entity.PersonId = reader.GetInt32(0);
                                    entity.Firstname = reader.GetString(1);
                                    entity.Surname = reader.GetString(2);
                                    entity.Gender = reader.GetString(3);
                                    entity.EmailAddress = reader.GetString(4);
                                    entity.PhoneNumber = reader.GetString(5);
                                    returnedEntities.Add(entity);
                                }
                            }

                        }

                        _errorCode = int.Parse(dbCommand.Parameters["@intErrorCode"].Value.ToString());

                        if (_errorCode != 0)
                        {
                            // Throw error.
                            throw new Exception("The SelectAll method for entity [Persons] reported the Database ErrorCode: " + _errorCode);
                        }
                    }
                }

                return returnedEntities;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                //Bubble error to caller and encapsulate Exception object
                throw new Exception("PersonsRepository::SelectAll::Error occured.", ex);
            }
        }

        public PersonsRepository()
        {
            //Repository Initializations
            _configurationHandler = new ConfigurationHandler();
            _loggingHandler = new LoggingHandler();
            _dataHandler = new DataHandler();
            _connectionString = _configurationHandler.ConnectionString;
            _connectionProvider = _configurationHandler.ConnectionProvider;
            _dbProviderFactory = DbProviderFactories.GetFactory(_connectionProvider);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool bDisposing)
        {
            // Check to see if Dispose has already been called.
            if (!_bDisposed)
            {
                if (bDisposing)
                {
                    // Dispose managed resources.
                    _configurationHandler = null;
                    _loggingHandler = null;
                    _dataHandler = null;
                    _dbProviderFactory = null;
                }
            }
            _bDisposed = true;
        }

        #endregion
    }
}
