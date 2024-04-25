﻿namespace Task4.Data;

public class AppDbContext: DbContext
{
  public DbSet<User> Users { get; set; }

  public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
  {
    Database.EnsureCreated();
  }
}
