using System;
using System.Collections.Generic;
using TainaTest.Common;
using TainaTest.DataAccess;
using TainaTest.Model;

namespace TainaTest.BusinessLogic
{
    /// <summary>
    /// Purpose: Business Logic Class [PersonsBusiness] for handling the business constrains on table [HR].[Persons].
    /// </summary>
    public class PersonsBusiness : IDisposable
    {
        #region Class Declarations

        private LoggingHandler _loggingHandler;
        private bool _bDisposed;

        #endregion

        #region Class Methods

        public bool InsertPerson(PersonsEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new PersonsRepository())
                {
                    bOpDoneSuccessfully = repository.Insert(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:PersonsBusiness::InsertPerson::Error occured.", ex);
            }
        }

        public bool UpdatePerson(PersonsEntity entity)
        {
            try
            {
                bool bOpDoneSuccessfully;
                using (var repository = new PersonsRepository())
                {
                    bOpDoneSuccessfully = repository.Update(entity);
                }

                return bOpDoneSuccessfully;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:PersonsBusiness::UpdatePerson::Error occured.", ex);
            }
        }

        public bool DeletePersonById(int empId)
        {
            try
            {
                using (var repository = new PersonsRepository())
                {
                    return repository.DeleteById(empId);
                }
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:PersonsBusiness::DeletePersonById::Error occured.", ex);
            }
        }

        public PersonsEntity SelectPersonById(int personId)
        {
            try
            {
                PersonsEntity returnedEntity;
                using (var repository = new PersonsRepository())
                {
                    returnedEntity = repository.SelectById(personId);
                }

                return returnedEntity;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:PersonsBusiness::SelectPersonById::Error occured.", ex);
            }
        }

        public List<PersonsEntity> SelectAllPersons()
        {
            var returnedEntities = new List<PersonsEntity>();

            try
            {
                using (var repository = new PersonsRepository())
                {
                    foreach (var entity in repository.SelectAll())
                    {
                        returnedEntities.Add(entity);
                    }
                }

                return returnedEntities;
            }
            catch (Exception ex)
            {
                //Log exception error
                _loggingHandler.LogEntry(ExceptionHandler.GetExceptionMessageFormatted(ex), true);

                throw new Exception("BusinessLogic:PersonsBusiness::SelectAllPersons::Error occured.", ex);
            }
        }

        

        public PersonsBusiness()
        {
            _loggingHandler = new LoggingHandler();
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
                    _loggingHandler = null;
                }
            }
            _bDisposed = true;
        }
        #endregion
    }
}

