﻿namespace EduSoft.Model.DTO.Account;

public class ResetPasswordDto
{
    public string NewPassword { get; set; }
    public string Token { get; set; }
}