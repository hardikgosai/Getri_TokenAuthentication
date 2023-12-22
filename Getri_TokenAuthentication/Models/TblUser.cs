using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Getri_TokenAuthentication.Models;

public partial class TblUser
{
    [Key]
    public int UserId { get; set; }

    public string? FullName { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }
}
