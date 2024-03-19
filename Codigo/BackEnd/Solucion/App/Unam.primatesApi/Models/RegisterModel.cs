﻿using System.ComponentModel.DataAnnotations;

namespace UNAM.PrimatesApi.Models
{
    public class RegisterModel
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Email {  get; set; }=string.Empty;        
        [Required]
        public string Password { get; set; } = string.Empty;
        public string Role {  get; set; } = string.Empty;
    }
}
