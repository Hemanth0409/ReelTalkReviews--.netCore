using System;
using System.Collections.Generic;

namespace ReelTalkReviews.Models;

public partial class MovieRating
{
    public int MovieRatingId { get; set; }

    public int? MovieId { get; set; }

    public int? UserId { get; set; }

    public int? Rating { get; set; }

    public virtual MovieDetail? Movie { get; set; }

    public virtual UserDetail? User { get; set; }
}
