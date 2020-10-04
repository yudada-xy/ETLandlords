using System;
using ETModel;
using UnityEngine;
using UnityEngine.UI;

namespace ETModel
{
    [ObjectSystem]
    public class UserInfoPanelComponentAwakeSystem : AwakeSystem<UserInfoPanelComponent>
    {
        public override void Awake(UserInfoPanelComponent self)
        {
            self.Awake();
        }
    }

    /// <summary>
    /// 修改设置用户信息界面组件
    /// </summary>
    public class UserInfoPanelComponent : Component
    {
        public Text mNameText;
        public Text mDouzi;
        public Text mJewe;
        public Text mIdText;

        public async void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();
            //初始化组件
            mNameText = rc.Get<GameObject>("NameText").GetComponent<Text>();
            mDouzi = rc.Get<GameObject>("douziText").GetComponent<Text>();
            mJewe = rc.Get<GameObject>("JeweText").GetComponent<Text>();
            mIdText = rc.Get<GameObject>("IdText").GetComponent<Text>();

            //按钮获取事件
            rc.Get<GameObject>("ShopBtn").GetComponent<Button>().onClick.Add(() => ShopBtnOnClick());
            rc.Get<GameObject>("CloseBtn").GetComponent<Button>().onClick.Add(() => CloseBtnOnClick());

            Debug.Log("信息组件初始化成功");
            //获取玩家信息
            A1001_GetUserInfo_C2G GetUserInfo_Req = new A1001_GetUserInfo_C2G();
            A1001_GetUserInfo_G2C GetUserInfo_Ack = (A1001_GetUserInfo_G2C)await SessionComponent.Instance.Session.Call(GetUserInfo_Req);
            
            mNameText.text = GetUserInfo_Ack.UserName;
            mDouzi.text = GetUserInfo_Ack.Douzi.ToString();
            mJewe.text = GetUserInfo_Ack.Jwel.ToString() ;
            mIdText.text = GetUserInfo_Ack.UserInfoId;
         
        }

        private void ShopBtnOnClick()
        {
            Game.EventSystem.Run(UIEventType.ShopPanelStart);
            Game.EventSystem.Run(UIEventType.MainLobbyFinish);
            Game.EventSystem.Run(UIEventType.UserInfoPanelFinish);
        }

        /// <summary>
        /// 关闭界面事件
        /// </summary>
        public void CloseBtnOnClick()
        {
            Game.EventSystem.Run(UIEventType.UserInfoPanelFinish);
        }
    }
}