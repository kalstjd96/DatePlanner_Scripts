using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class ClassSingleton<T> : ISingleton where T : class, new()
    {
        private static T _instance = null;

        public static bool Instantiated
        {
            get
            {
                return _instance != null;
            }
        }
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Instantiate();
                    Debug.Log($"ClassSingleton::{typeof(T).Name} has created");
                }
                return _instance;
            }
        }



        /// <summary>
        /// 싱글톤 객체를 강제로 생성한다.
        /// </summary>
        public static T Instantiate()
        {
            _instance = new T();
            return _instance;
        }
    }
}