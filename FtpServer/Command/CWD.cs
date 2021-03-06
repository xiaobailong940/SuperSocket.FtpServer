using System;
using System.Collections.Generic;
using System.Text;
using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Command;
using SuperSocket.SocketBase.Protocol;

namespace SuperSocket.Ftp.FtpService.Command
{
    public class CWD : FtpCommandBase
    {
        #region StringCommandBase<FtpSession> Members

        public override void ExecuteCommand(FtpSession session, StringRequestInfo requestInfo)
        {
            if (!session.Logged)
                return;

            string path = requestInfo.Body;

            if (string.IsNullOrEmpty(path))
            {
                session.SendParameterError();
                return;
            }

            if (!path.StartsWith("/"))
            {
                var context = session.Context;
                var currentPath = context.CurrentPath;

                if (currentPath == "/")
                {
                    path = currentPath + path;
                }
                else
                {
                    path = currentPath + "/" + path;
                }
            }

            if (session.AppServer.FtpServiceProvider.IsExistFolder(session.Context, path))
            {
                session.Context.CurrentPath = path;
                session.Send(FtpCoreResource.ChangeWorkDirOk_250, path);
            }
            else
            {
                if (session.Context.Status == FtpStatus.Error)
                    session.Send(session.Context.Message);
                else
                    session.Send(FtpCoreResource.NotFound_550);
            }
        }

        #endregion
    }
}
