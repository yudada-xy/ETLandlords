using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
namespace ETModel
{
    public static class RegisterHelper
    {
        
        public static async ETVoid Register(string account, string password)
        {
            Session session = Game.Scene.GetComponent<NetOuterComponent>().Create(GlobalConfigComponent.Instance.GlobalProto.Address);
            A0001_Register_R2C message = (A0001_Register_R2C)await session.Call(new A0001_Register_C2R() { Account = account, Password = password });
            session.Dispose();

            LoginPanelComponent register = Game.Scene.GetComponent<UIComponent>().Get(LandUIType.LoginPanel).GetComponent<LoginPanelComponent>();
            register.isRegistering = false;

            if (message.Error == ErrorCode.ERR_AccountAlreadyRegisted)
            {
                register.mPrompt.text = "注册失败，账号已被注册";
                register.mAccount.text = "";
                register.mPassword.text = "";
                return;
            }

            if (message.Error == ErrorCode.ERR_RepeatedAccountExist)
            {
                register.mPrompt.text = "注册失败，出现重复账号";
                register.mAccount.text = "";
                register.mPassword.text = "";
                return;
            }

            register.mPrompt.text = "注册成功";
        }

    }
}