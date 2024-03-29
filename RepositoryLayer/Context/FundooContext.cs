﻿using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Context
{
    public class FundooContext : DbContext
    {
        public FundooContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<UserEntity> UsersTable1 { get; set; } //Model class Passed inside Dbset and Table Name
        public DbSet<CustomerEntity> Customer1 { get; set; }

        public DbSet<NotesEntity> UserNotes { get; set; }

        public DbSet<LabelEntity> Label { get; set; }
    }
}
