using System.Text;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace ETModel
{
    [ObjectSystem]
    public class LoginPanelComponentAwakeSystem : AwakeSystem<LoginPanelComponent>
    {
        public override void Awake(LoginPanelComponent self)
        {
            self.Awake();
        }
    }

    public class LoginPanelComponent : Component
    {
        public InputField mAccount;
        public InputField mPassword;
        public InputField mVerification;
        public Text mNBText;
        public Text mPrompt;
        public Toggle mTopToggle_left;
        public Toggle mTopToggle_right;
        public GameObject mLoginhic;
        public GameObject mRegisterhic;

        //是否正在登录中（避免登录请求还没响应时连续点击登录）
        public bool isLogining;
        //是否正在注册中（避免登录请求还没响应时连续点击注册）
        public bool isRegistering;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            //初始化数据
            mAccount = rc.Get<GameObject>("Account").GetComponent<InputField>();
            mPassword = rc.Get<GameObject>("Password").GetComponent<InputField>();
            mVerification = rc.Get<GameObject>("Verification").GetComponent<InputField>();
            mPrompt = rc.Get<GameObject>("Prompt").GetComponent<Text>();
            mNBText = rc.Get<GameObject>("NBText").GetComponent<Text>();

            mTopToggle_left = rc.Get<GameObject>("TopToggle_left").GetComponent<Toggle>();
            mTopToggle_right = rc.Get<GameObject>("TopToggle_right").GetComponent<Toggle>();
           
            mLoginhic = rc.Get<GameObject>("Loginhic");
            mRegisterhic = rc.Get<GameObject>("Registerhic");

            this.isLogining = false;
            this.isRegistering = false;

            ///添加事件
            rc.Get<GameObject>("LoginBtn").GetComponent<Button>().onClick.Add(() => LoginBtnOnClick());
            rc.Get<GameObject>("NotationBtn").GetComponent<Button>().onClick.Add(() => NotationBtnOnClick());
            rc.Get<GameObject>("CloseBtn").GetComponent<Button>().onClick.Add(() => CloseBtnOnClick());
            rc.Get<GameObject>("RegisterBtn").GetComponent<Button>().onClick.Add(() => RegisterBtnOnClick());
            rc.Get<GameObject>("TopToggle_left").GetComponent<Toggle>().onValueChanged.AddListener((bool value) => mTopToggle_leftClick(value));
            rc.Get<GameObject>("TopToggle_right").GetComponent<Toggle>().onValueChanged.AddListener((bool value) => mTopToggle_rightClick(value));

            mPrompt.text = "";

        }

        private void RegisterBtnOnClick()
        {
            if (mVerification.text == "")
            {
                mPrompt.text = ("验证码不能为空");
                return;
            }
            else if (mVerification.text != mNBText.text || mVerification.text == "获取验证码")
            {
                mPrompt.text = ("验证码错误");
                return;
            }
            else if (this.isRegistering || this.IsDisposed)
            {
                mPrompt.text = ("正在注册");
                return;
            }
            else if (mAccount.text == "" || mPassword.text == "")
            {
                mPrompt.text = ("账号密码不能为空");
                return;
            }
            this.isRegistering = true;
            RegisterHelper.Register(this.mAccount.text, this.mPassword.text).Coroutine();
        }

        private void mTopToggle_rightClick(bool value)
        {
            if (value == true)
                ToggleRight();
        }
        private void mTopToggle_leftClick(bool value)
        {
            if (value == true)
                ToggleLeft();
        }

        private void ToggleLeft() 
        {
            mPrompt.text = "";
            mLoginhic.SetActive(true);
            mRegisterhic.SetActive(false);
        }
        private void ToggleRight() 
        {
            mPrompt.text = "";
            mRegisterhic.SetActive(true);
            mLoginhic.SetActive(false);
        }




        public void LoginBtnOnClick()
        {
            //if (mVerification.text == "")
            //{
            //    mPrompt.text = ("验证码不能为空");
            //    return;
            //}
            //else if (mVerification.text != mNBText.text || mVerification.text == "获取验证码")
            //{
            //    mPrompt.text = ("验证码错误");
            //    return;
            //}
             if (this.isLogining || this.IsDisposed)
            {
                mPrompt.text = ("正在登录");
                return;
            }
            if (this.mAccount.text == "" || this.mPassword.text == "")
            {
                //UnityEditor.EditorUtility.DisplayDialog("输入错误","账号密码不能为空","确认", "取消");
                mPrompt.text = ("账号密码不能为空");
                return;
            }
            else
            {         
                this.isLogining = true;
                LoginHelper.Login(this.mAccount.text, this.mPassword.text).Coroutine();
            }
        }

        /// <summary>
        /// 关闭界面事件
        /// </summary>
        public void CloseBtnOnClick()
        {
            Game.EventSystem.Run(UIEventType.LoginPanelFinish);
        }

        /// <summary>
        /// 获取验证码事件
        /// </summary>
        public void NotationBtnOnClick()
        {
           SetDeleKey(4);
        }
        #region 验证码生成（）

        public void SetDeleKey(int Length)
        {
            Debug.Log("验证码函数调用");
            StringBuilder newRandom = new StringBuilder(62);
            System.Random rd = new System.Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]); //rd.Next(62)返回小于62的非负随机数,Append将Length次随机的码进行拼接
            }
            mNBText.text = newRandom.ToString();
            Debug.Log("验证码生成");
        }
        private static char[] constant =
    {
        '0','1','2','3','4','5','6','7','8','9',
        'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
        'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'

    };
        #endregion


    }
}