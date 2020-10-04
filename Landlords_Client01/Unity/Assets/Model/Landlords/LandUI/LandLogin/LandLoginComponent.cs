using UnityEngine;
using UnityEngine.UI;
namespace ETModel
{
    [ObjectSystem]
    public class LandLoginComponentAwakeSystem : AwakeSystem<LandMainLoginComponent>
    {
        public override void Awake(LandMainLoginComponent self)
        {
            self.Awake();
        }
    }

    public class LandMainLoginComponent : Component
    {
        //提示文本
        public Text prompt;
        public Toggle protocol;
        //是否正在登录中（避免登录请求还没响应时连续点击登录）
        public bool isLogining;
        //是否正在注册中（避免登录请求还没响应时连续点击注册）
        public bool isRegistering;

        public void Awake()
        {
            ReferenceCollector rc = this.GetParent<UI>().GameObject.GetComponent<ReferenceCollector>();

            //初始化数据
            prompt = rc.Get<GameObject>("Prompt").GetComponent<Text>();
            protocol = rc.Get<GameObject>("Protocol").GetComponent<Toggle>();
            this.isLogining = false;
            this.isRegistering = false;


            //添加按钮并且为按钮添加事件
            rc.Get<GameObject>("PhoneLoginButton").GetComponent<Button>().onClick.Add(() => PhoneLoginBtnOnClick());
            rc.Get<GameObject>("UserLoginButton").GetComponent<Button>().onClick.Add(() => UserLoginBtnOnClick());
            rc.Get<GameObject>("RegisterButton").GetComponent<Button>().onClick.Add(() => RegisterBtnOnClick());
            Debug.Log("LandLoginComponent组件数据初始化成功");
        }
        /// <summary>
        /// 手机登录事件
        /// </summary>
        public void PhoneLoginBtnOnClick()
        {
            if (protocol.isOn == true)
            {
                Debug.Log("发送手机登录按钮事件");
                //prompt.text = ("点击时候PR被选中");
            }
            else
            {
                prompt.text = ("请观看用户协议并选择同意使用");
            }
        }
        /// <summary>
        /// 用户登录事件
        /// </summary>
        public void UserLoginBtnOnClick()
        {
            if (protocol.isOn == true)
            {
                //Debug.Log("发送用户登录按钮事件");
                // Game.EventSystem.Run(UIEventType.UserLoginStart);
                Game.EventSystem.Run(UIEventType.LoginPanelStart);

            }
            else
            {
                prompt.text = ("请观看用户协议并选择同意使用");
            }
        }
        /// <summary>
        /// 用户注册事件
        /// </summary>
        public void RegisterBtnOnClick()
        {
            if (protocol.isOn == true)
            {
                // Debug.Log("发送快速注册按钮事件");
                //prompt.text = ("点击时候protocol被选中");
               // Game.EventSystem.Run(UIEventType.RegisterStart);
            }
            else
            {
                prompt.text = ("请观看用户协议并选择同意使用");
            }
        }
    }
}