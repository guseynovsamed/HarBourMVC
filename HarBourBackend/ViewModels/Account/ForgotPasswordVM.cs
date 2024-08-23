using System;
using System.ComponentModel.DataAnnotations;

namespace HarBourBackEnd.ViewModels.Account
{
    public class ForgotPasswordVM
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }
}

