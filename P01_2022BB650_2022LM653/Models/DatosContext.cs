﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.SqlServer;
using P01_2022BB650_2022LM653.Models;

namespace P01_2022BB650_2022LM653.Models
{
    public class DatosContext: DbContext
    {
        public DatosContext(DbContextOptions <DatosContext> options) : base (options)
        { 
        
        }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Sucursales> Sucursales { get; set; }
        public DbSet<Reservas> Reservas {  get; set; }
        public DbSet<Espacios_Parqueo> Espacios_Parqueo { get; set; }
    }
}
