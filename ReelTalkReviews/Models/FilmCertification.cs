using System;
using System.Collections.Generic;

namespace ReelTalkReviews.Models;

public partial class FilmCertification
{
    public int FilmCertificationId { get; set; }

    public string? FilmCertificationType { get; set; }

    public string? Definition { get; set; }

    public virtual ICollection<MovieDetail> MovieDetails { get; set; } = new List<MovieDetail>();
}
