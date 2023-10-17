using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ReelTalkReviews.Models;

public partial class UserDetail
{
    public int UserId { get; set; }

    [Required]
    public string UserName { get; set; } = null!;
    [Required]
    public string? Email { get; set; }
    [Required]
    [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,15}$")]

    public string? Password { get; set; }

    public string? Bio { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public int? RoleId { get; set; }

    public DateTime? LastLoginDate { get; set; }

    public string? DisplayPic { get; set; }

    public string? Token { get; set; }

    public DateTime? RefreshTokenExpiry { get; set; }

    public string? RefreshToken { get; set; }

    public string? ResetPasswordToken { get; set; }

    public DateTime? ResetPasswordTokenExpiry { get; set; }

    public virtual ICollection<MovieRating> MovieRatings { get; set; } = new List<MovieRating>();

    public virtual Role? Role { get; set; }
}
