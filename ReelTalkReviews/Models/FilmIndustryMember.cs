using System;
using System.Collections.Generic;

namespace ReelTalkReviews.Models;

public partial class FilmIndustryMember
{
    public int MemberId { get; set; }

    public string? MemberName { get; set; }

    public string? MemberPic { get; set; }

    public DateTime? DateOfBirth { get; set; }

    public string? MemberDescription { get; set; }

    public string? Gender { get; set; }

    public string? Place { get; set; }

    public bool? IsDirector { get; set; }

    public bool? IsMusicDirector { get; set; }

    public bool? IsActor { get; set; }

    public bool? IsProducer { get; set; }

    public bool? IsCinematographer { get; set; }

    public bool? IsWriter { get; set; }

    public bool? IsDeleted { get; set; }

    public virtual ICollection<CastDetail> CastDetailLeadActorId2Navigations { get; set; } = new List<CastDetail>();

    public virtual ICollection<CastDetail> CastDetailLeadActorId3Navigations { get; set; } = new List<CastDetail>();

    public virtual ICollection<CastDetail> CastDetailLeadActorId4Navigations { get; set; } = new List<CastDetail>();

    public virtual ICollection<CastDetail> CastDetailLeadActorId5Navigations { get; set; } = new List<CastDetail>();

    public virtual ICollection<CastDetail> CastDetailLeadActorId6Navigations { get; set; } = new List<CastDetail>();

    public virtual ICollection<CastDetail> CastDetailLeadActorId7Navigations { get; set; } = new List<CastDetail>();

    public virtual ICollection<CastDetail> CastDetailLeadActors { get; set; } = new List<CastDetail>();

    public virtual ICollection<Crew> CrewCinematographerNavigations { get; set; } = new List<Crew>();

    public virtual ICollection<Crew> CrewDirectorNavigations { get; set; } = new List<Crew>();

    public virtual ICollection<Crew> CrewMusicDirectorNavigations { get; set; } = new List<Crew>();

    public virtual ICollection<Crew> CrewProducerNavigations { get; set; } = new List<Crew>();

    public virtual ICollection<Crew> CrewWriterNavigations { get; set; } = new List<Crew>();
}
