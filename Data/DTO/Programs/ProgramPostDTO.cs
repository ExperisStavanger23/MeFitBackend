﻿using MeFitBackend.Data.Enums;

namespace MeFitBackend.Data.DTO.Programs
{
    public class ProgramPostDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ProgramCategory Category { get; set; }
        public string RecomendedLvl { get; set; }
        public string Image { get; set; }
        public int Duration { get; set; }
    }
}