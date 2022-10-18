using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceHolder.API
{
    public class ApiVersionAttribute : AreaAttribute
    {
        public ApiVersionAttribute(string value) : base(value)
        {
            if(!char.TryParse(value, out char character) || ! char.IsDigit(character))
            {
                throw new ArgumentException("Expecting single digit", value);
            }
        }
    }
}
