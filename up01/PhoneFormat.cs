using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace up01
{
    class PhoneFormat
    {
        private string _phone;

        public PhoneFormat()
        {
            MaxLength = 11;
        }

        public string Phone
        {
            get => _phone;
            set
            {
                if (value == _phone) return;
                _phone = value;
            }
        }

        public int MaxLength { get; set; }
        public int PhoneLength { get; set; }

        public async Task PhoneMask()
        {
            var newVal = Regex.Replace(Phone, @"[^0-9]", "");
            if (PhoneLength != newVal.Length && !string.IsNullOrEmpty(newVal))
            {
                PhoneLength = newVal.Length;
                Phone = string.Empty;
                    if (newVal.Length <= 1)
                    {
                        Phone = Regex.Replace(newVal, @"(\d{1})", "+$1");
                    }
                    else if (newVal.Length <= 4)
                    {
                        Phone = Regex.Replace(newVal, @"(\d{1})(\d{0,3})", "+$1($2)");
                    }
                    else if (newVal.Length <= 7)
                    {
                        Phone = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})", "+$1($2)$3");
                    }
                    else if (newVal.Length <= 9)
                    {
                        Phone = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})(\d{0,2})", "+$1($2)$3-$4");
                    }
                    else if (newVal.Length > 9)
                    {
                        Phone = Regex.Replace(newVal, @"(\d{1})(\d{3})(\d{0,3})(\d{0,2})(\d{0,2})", "+$1($2)$3-$4-$5");
                    }
                }
            }
        }
    }
  
