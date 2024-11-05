﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySocialApp.Domain;

public class FeedQueryDto
{
    public DateTimeOffset CreateAt { get; set; }
    public Guid Id { get; set; }
    public string Content { get; set; }
    public int LikesCount { get; set; }
    public bool Liked { get; set; }
    public string UserName { get; set; }
    public Guid PostLikedId { get; set; }
}
