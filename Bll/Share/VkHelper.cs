using System;
using System.Threading;
using System.Windows;
using FoolGame.Uil.Window;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Exception;

namespace FoolGame.Bll.Vk
{
    class VkHelper : ISharable
    {
        private readonly VkApi _vkApi;
        private string _сaptchaKey;
        private long _sid;
        private bool _isRegistered;

        public VkHelper()
        {
            _vkApi = new VkApi();
            _сaptchaKey = String.Empty;
        }

        public void Share(String email,String password)
        {
            Auth(email, password);
            if (_isRegistered)
            {
                PostToWall();
            }
        }

        private void PostToWall()
        {
            _vkApi.Wall.Post(_vkApi.UserId.Value, message: "Я победила!");
        }

        private void Auth(String email, String password)
        {
            try
            {
                if (!_сaptchaKey.Equals(String.Empty))
                {
                    #pragma warning disable 618
                    _vkApi.Authorize(Util.GetAppId(),email,password, Settings.All, captchaSid: _sid,
                    captchaKey: _сaptchaKey);
                    _isRegistered = true;
                }
                else
                {
                    _vkApi.Authorize(Util.GetAppId(), email, password, Settings.All);
                }
            }
            catch (VkApiAuthorizationException)
            {
                MessageBox.Show("Ошибка авторизации. Проверьте данные ", "Ошибка приложения", MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
            catch (CaptchaNeededException cex)
            {
                _sid = cex.Sid;
                object result = cex.Img.ToString();

                Thread thread1 = new Thread(OpenCaptcha);
                thread1.Start(result);
                CaptchaPage cap = new CaptchaPage();

                if (cap.ShowDialog() == true)
                {
                    _сaptchaKey = cap.CaptchaKey;
                    Auth(email, password);
                }
            }
        }

        private void OpenCaptcha(object captchaurl)
        {
            System.Diagnostics.Process.Start((string)captchaurl);
        }


    }
}
