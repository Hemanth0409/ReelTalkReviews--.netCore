using System;
using System.Collections.Generic;

namespace ReelTalkReviews.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string? RoleName { get; set; }

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
