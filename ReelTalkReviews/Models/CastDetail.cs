using System;
using System.Collections.Generic;

namespace ReelTalkReviews.Models;

public partial class CastDetail
{
    public int CastId { get; set; }

    public int? MovieId { get; set; }

    public int? LeadActorId { get; set; }

    public string? LeadActor1As { get; set; }

    public int? LeadActorId2 { get; set; }

    public string? LeadActor2As { get; set; }

    public int? LeadActorId3 { get; set; }

    public string? LeadActor3As { get; set; }

    public int? LeadActorId4 { get; set; }

    public string? LeadActor4As { get; set; }

    public int? LeadActorId5 { get; set; }

    public string? LeadActor5As { get; set; }

    public int? LeadActorId6 { get; set; }

    public string? LeadActor6As { get; set; }

    public int? LeadActorId7 { get; set; }

    public string? LeadActor7As { get; set; }

    public virtual FilmIndustryMember? LeadActor { get; set; }

    public virtual FilmIndustryMember? LeadActorId2Navigation { get; set; }

    public virtual FilmIndustryMember? LeadActorId3Navigation { get; set; }

    public virtual FilmIndustryMember? LeadActorId4Navigation { get; set; }

    public virtual FilmIndustryMember? LeadActorId5Navigation { get; set; }

    public virtual FilmIndustryMember? LeadActorId6Navigation { get; set; }

    public virtual FilmIndustryMember? LeadActorId7Navigation { get; set; }

    public virtual MovieDetail? Movie { get; set; }
}
