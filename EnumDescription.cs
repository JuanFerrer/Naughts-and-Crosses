﻿using System;
using System.Reflection;

namespace NaughtsAndCrosses
{

    public class EnumDescription : Attribute
    {
        public string Text
        {
            get { return _text; }
        }
        private string _text;

        public EnumDescription(string text)
        {
            _text = text;
        }
    }

    public static class EnumExtensions
    {
        public static string ToDescription(this Enum enumeration)
        {
            Type type = enumeration.GetType();
            MemberInfo[] memInfo = type.GetMember(enumeration.ToString());

            if (null != memInfo && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(EnumDescription), false);
                if (null != attrs && attrs.Length > 0)
                    return ((EnumDescription)attrs[0]).Text;
            }

            return enumeration.ToString();
        }
    }
}