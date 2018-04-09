﻿using CycleRoutesCore.Domain.Enums;

namespace CycleRoutesCore.Domain.Models
{
    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Complexity Complexity { get; set; }
        public Type Type { get; set; }
        public LineType LineType { get; set; }
        public string Image { get; set; }
        public double Length { get; set; }
        public bool IsDeleted { get; set; }
    }
}