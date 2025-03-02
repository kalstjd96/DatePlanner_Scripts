using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Main.User
{
    public class UserDataManager : MonoBehaviour
    {
        public TextMeshProUGUI UserEmailText;
        private string userEmail;
        private string userNickName;

        // Start is called before the first frame update
        void Awake()
        {
            UserEmailText.text = UserData.Email;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
