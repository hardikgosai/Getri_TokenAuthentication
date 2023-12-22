using System;
using System.Collections.Generic;
using Getri_TokenAuthentication.Models;
using Microsoft.EntityFrameworkCore;

namespace Getri_TokenAuthentication.EntityFramework;

public partial class GetriTokenAuthenticationContext : DbContext
{
    public GetriTokenAuthenticationContext()
    {
    }

    public GetriTokenAuthenticationContext(DbContextOptions<GetriTokenAuthenticationContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblUser> TblUser { get; set; }
}
