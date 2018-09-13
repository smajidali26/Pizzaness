using BusinessEntities;
using Core;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusinessService
{
    public class MailManager
    {
        #region DataAccess
        private IDBHelper _dbHandler = null;

        public IDBHelper DBHandler
        {
            get
            {
                if (_dbHandler == null)
                {
                    _dbHandler = DBHelper.CreateInstance(DatabaseSettings.ConnectionString);
                }

                return _dbHandler;
            }
        }
        #endregion

        #region Constructor

        public MailManager()
        {
        }

        #endregion

        #region Methods

        public Int64 AddMail(Mail mail)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@Subject",mail.Subject ),
                new Parameter("@Sender",mail.Sender),
                new Parameter("@Receiver",mail.Receiver),
                new Parameter("@MailCc",mail.MailCc),
                new Parameter("@MailBcc",mail.MailBcc),
                new Parameter("@MailBody",mail.MailBody),
                new Parameter("@IsHtml",mail.IsHtml),
                new Parameter("@ReferenceId",mail.ReferenceId),
                new Parameter("@ReferenceType",mail.ReferenceType),
            };

            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Mail_AddMail]", parameters);
            return Convert.ToInt64(result.ToString());
        }

        public Int32 UpdateMailAsSend(long mailId)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@MailId",mailId )
            };

            #endregion

            object result = DBHandler.ExecuteScalar(System.Data.CommandType.StoredProcedure, "[Mail_UpdateMailAsSend]", parameters);
            return Convert.ToInt32(result.ToString());
        }        

        public ICollection<Mail> GetAllNotSendEmails()
        {
            #region Parameters

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Mail_GetAllNotSendEmails]", null);
            return BaseEntityController.FillEntities<Mail>(reader);
        }

        public Mail GetMailByReferenceId(long referenceId,string referenceType)
        {
            #region Parameters

            IParameter[] parameters = new Parameter[]{
                new Parameter("@ReferenceId",referenceId ),
                new Parameter("@ReferenceType",referenceType )
            };

            #endregion

            IDataReader reader = DBHandler.ExecuteReader(System.Data.CommandType.StoredProcedure, "[Mail_GetMailByReferenceId]", parameters);
            return BaseEntityController.FillEntity<Mail>(reader);
        }

        #endregion
    }
}
