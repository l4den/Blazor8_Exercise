using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUD.frontend.Helpers
{
    public class FutureDateValidator : ValidationAttribute
    {
        public FutureDateValidator()
        {
            ErrorMessage = "The date cannot be a date in the future";
        }
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date < DateTime.Now;
            }

            return false;
        }
    }
}