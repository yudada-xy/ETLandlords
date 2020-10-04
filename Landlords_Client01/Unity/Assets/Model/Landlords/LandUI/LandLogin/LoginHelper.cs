using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
namespace ETModel
{
    public static class LoginHelper
    {
        //A0 01注册 02登录realm 03登录gate
        public static async ETVoid Login(string account, string password)
        {
            //获取场景中的LoginPanel界面
            LoginPanelComponent login = Game.Scene.GetComponent<UIComponent>().Get(LandUIType.LoginPanel).GetComponent<LoginPanelComponent>();
                //创建Realm session
                Session sessionRealm = Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);
                //登录Realm服务器
                A0002_Login_R2C messageRealm = (A0002_Login_R2C)await sessionRealm.Call(new A0002_Login_C2R() { Account = account, Password = password });
                sessionRealm.Dispose();
                login.mPrompt.text = "正在登录中...";

                //判断Realm服务器返回结果
                if (messageRealm.Error == ErrorCode.ERR_AccountOrPasswordError)
                {
                    login.mPrompt.text = "登录失败,账号或密码错误";
                    login.mAccount.text = "";
                    login.mPassword.text = "";
                    login.isLogining = false;
                    return;
                }
                //判断通过则登陆Realm成功

                //创建网关 session
                Session sessionGate = Game.Scene.GetComponent<NetOuterComponent>().Create(messageRealm.GateAddress);
                if (SessionComponent.Instance == null)
                {
                    //Log.Debug("创建唯一Session");
                    Game.Scene.AddComponent<SessionComponent>().Session = sessionGate;
                }
                else
                {
                    //存入SessionComponent方便我们随时使用
                    SessionComponent.Instance.Session = sessionGate;
                    //Game.EventSystem.Run(EventIdType.SetHotfixSession);
                }
                //登录Gate服务器
                A0003_LoginGate_G2C messageGate = (A0003_LoginGate_G2C)await sessionGate.Call(new A0003_LoginGate_C2G() { GateLoginKey = messageRealm.GateLoginKey });

                //判断登陆Gate服务器返回结果
                if (messageGate.Error == ErrorCode.ERR_ConnectGateKeyError)
                {
                    login.mPrompt.text = "连接网关服务器超时";
                    login.mAccount.text = "";
                    login.mPassword.text = "";
                    sessionGate.Dispose();
                    login.isLogining = false;
                    return;
                }
                //判断通过则登陆Gate成功

                login.mPrompt.text = "";
                User user = ComponentFactory.Create<User, long>(messageGate.UserID);
                GamerComponent.Instance.MyUser = user;
                Log.Debug("登陆成功");

                //加载透明界面 退出当前界面
                Game.EventSystem.Run(UIEventType.LoginPanelFinish);
                Game.EventSystem.Run(UIEventType.LandMainLoginFinish);
                Game.EventSystem.Run(UIEventType.MainLobbyStart);

            }
        }
    }
