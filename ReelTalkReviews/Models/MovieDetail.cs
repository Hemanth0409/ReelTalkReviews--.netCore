using System;
using System.Collections.Generic;

namespace ReelTalkReviews.Models;

public partial class MovieDetail
{
    public int MovieId { get; set; }

    public string? MovieTitle { get; set; }

    public string? MovieType { get; set; }

    public string? MoviePoster { get; set; }

    public decimal? MovieRatingOverall { get; set; }

    public int? FilmCertificationId { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreateDate { get; set; }

    public DateTime? ModifiedDate { get; set; }

    public DateTime? ReleaseDate { get; set; }

    public int? RatingCount { get; set; }

    public string? MovieDescription { get; set; }

    public string? MoviePoster2 { get; set; }

    public string? Actor1 { get; set; }

    public string? Actor2 { get; set; }

    public string? Actor3 { get; set; }

    public string? Director { get; set; }

    public string? MusicDirector { get; set; }

    public virtual ICollection<CastDetail> CastDetails { get; set; } = new List<CastDetail>();

    public virtual ICollection<Crew> Crews { get; set; } = new List<Crew>();

    public virtual FilmCertification? FilmCertification { get; set; }

    public virtual ICollection<MovieRating> MovieRatings { get; set; } = new List<MovieRating>();
}
