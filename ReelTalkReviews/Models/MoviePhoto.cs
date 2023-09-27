using System;
using System.Collections.Generic;

namespace ReelTalkReviews.Models;

public partial class MoviePhoto
{
    public int MoviePicId { get; set; }

    public int? MovieId { get; set; }

    public byte[]? MoviePhotos { get; set; }

    public virtual MovieDetail? Movie { get; set; }
}
