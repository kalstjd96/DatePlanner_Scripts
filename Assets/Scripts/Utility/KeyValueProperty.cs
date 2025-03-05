using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public class KeyValueProperty
    {
        public string key = string.Empty;
        public Type typeHint;
        public object value;
        public object defaultValue;
    }
}