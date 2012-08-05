using System;
using System.Collections.Generic;
using System.Text;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace SuperSocket.Ftp.FtpService.Command
{
    public class ABOR : StringCommandBase<FtpSession>
    {
        #region StringCommandBase<FtpSession> Members

        public override void ExecuteCommand(FtpSession session, StringRequestInfo reuquestInfo)
        {
            session.Context.ResetState();
            //Close current data connection?
            session.CloseDataConnection(session.CurrentDataConnection);
            session.Send(Resource.AbortOk_226);
        }

        #endregion
    }
}