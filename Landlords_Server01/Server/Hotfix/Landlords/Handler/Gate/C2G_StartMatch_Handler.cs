using System;
using ETModel;

namespace ETHotfix
{
    [MessageHandler(AppType.Gate)]
    public class C2G_StartMatch_Handler : AMRpcHandler<C2G_StartMatch_Req, G2C_StartMatch_Back>
    {
        protected override async ETTask Run(Session session, C2G_StartMatch_Req request, G2C_StartMatch_Back response, Action reply)
        {
            try
            {
                Log.Debug("玩家开始匹配");
                //验证Session
                if (!GateHelper.SignSession(session))
                {
                    response.Error = ErrorCode.ERR_SignError;
                    reply();
                    return;
                }
                //获取Gate服务器上绑定的User对象
                User user = session.GetComponent<SessionUserComponent>().User;
                //验证玩家是否符合进入房间要求,默认为100底分局
                RoomConfig roomConfig = GateHelper.GetLandlordsConfig(RoomLevel.Lv100);
                //获取User对象的UserInfo(用户信息)数据
                UserInfo userInfo = await Game.Scene.GetComponent<DBProxyComponent>().Query<UserInfo>(user.UserID);
                //判断此对象的金币是否符合准备要求
                if (userInfo.Douzi < roomConfig.MinThreshold)
                {
                    response.Error = ErrorCode.ERR_UserMoneyLessError;
                    reply();
                    return;
                }
                reply();

                //获取斗地主Map服务器的Session
                //通知Map服务器创建地图上的Gamer，发送请求匹配通知
                Session mapSession = GateHelper.GetMapSession();
                mapSession.Send(new EnterMatchs_G2M()
                {
                    UserID = user.UserID,
                    GActorID = user.InstanceId,
                    CActorID = user.GateSessionID
                });

                await ETTask.CompletedTask;
            }
            catch (Exception e)
            {
                ReplyError(response, e, reply);
            }
        }
    }
}