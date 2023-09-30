using System;
using System.Collections.Generic;

namespace ReelTalkReviews.Models;

public partial class UserDetail
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? Bio { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? RoleId { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public byte[]? DisplayPic { get; set; }

    public string? Token { get; set; }

    public DateTime? RefreshTokenExpiry { get; set; }

    public string? RefreshToken { get; set; }

    public virtual ICollection<MovieRating> MovieRatings { get; set; } = new List<MovieRating>();

    public virtual Role? Role { get; set; }
}
