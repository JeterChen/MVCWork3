using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVCWork3
{
    public sealed class DateValueAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return false;
            }

            if(DateTime.TryParse(value.ToString(), out DateTime thisDate))
            {
                if(thisDate <= DateTime.Now)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
