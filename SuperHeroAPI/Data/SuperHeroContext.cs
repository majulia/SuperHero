﻿using Microsoft.EntityFrameworkCore;

namespace SuperHeroAPI.Data
{
    public class SuperHeroContext : DbContext
    {
        public SuperHeroContext(DbContextOptions<SuperHeroContext> options) : base(options)
        {}
        public DbSet<SuperHero> SuperHeroes => Set<SuperHero>();
    }
}
