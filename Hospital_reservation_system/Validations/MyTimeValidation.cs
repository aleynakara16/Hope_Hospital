﻿using System.ComponentModel.DataAnnotations;

namespace Hospital_reservation_system.Validations
{
    public class MyTimeValidation : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            DateTime dt = (DateTime)value;
            if (dt.Minute == 30 || dt.Minute == 00)
                return true;
            else
                return false;
        }
    }
}
