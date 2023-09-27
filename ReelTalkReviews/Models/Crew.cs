using System;
using System.Collections.Generic;

namespace ReelTalkReviews.Models;

public partial class Crew
{
    public int CrewId { get; set; }

    public int? MovieId { get; set; }

    public int? Director { get; set; }

    public int? MusicDirector { get; set; }

    public int? Producer { get; set; }

    public int? Cinematographer { get; set; }

    public int? Writer { get; set; }

    public virtual FilmIndustryMember? CinematographerNavigation { get; set; }

    public virtual FilmIndustryMember? DirectorNavigation { get; set; }

    public virtual MovieDetail? Movie { get; set; }

    public virtual FilmIndustryMember? MusicDirectorNavigation { get; set; }

    public virtual FilmIndustryMember? ProducerNavigation { get; set; }

    public virtual FilmIndustryMember? WriterNavigation { get; set; }
}
