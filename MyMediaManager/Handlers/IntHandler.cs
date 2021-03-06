﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyMediaManager.Handlers
{
    public static class IntHandler
    {
        private static readonly Regex _regex = new Regex("[^0-9]");

        public static bool IsTextInt(string text)
        {
            return !_regex.IsMatch(text);
        }
    }
}
