using System;
using CycleRoutesCore.Domain.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Type = CycleRoutesCore.Domain.Enums.Type;

namespace CycleRoutesCore.Domain.Models
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Landscape Landscape { get; set; }
        public Type Type { get; set; }
        public LineType LineType { get; set; }
        public LengthTimes LengthTime { get; set; }
        public string Image { get; set; }
        public double Length { get; set; }
        public string UploadDate { get; set; }
        public bool IsDeleted { get; set; }
        public int ViewsCount { get; set; }
        public int LikesCount { get; set; }

        [NotMapped]
        public bool IsLiked { get; set; }

        public User User { get; set; }
        public List<RouteImage> Images { get; set; }
    }
}