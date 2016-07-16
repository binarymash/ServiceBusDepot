﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using ServiceBusDepot.Core.Database;

namespace ServiceBusDepot.Core.Migrations
{
    [DbContext(typeof(ServiceBusDepotContext))]
    [Migration("20160716175045_ServiceBusConnections")]
    partial class ServiceBusConnections
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("ServiceBusDepot.Core.Entities.ServiceBusConnection", b =>
                {
                    b.Property<int>("ServiceBusConnectionId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConnectionString");

                    b.Property<string>("Description");

                    b.Property<string>("Uri");

                    b.HasKey("ServiceBusConnectionId");

                    b.ToTable("Connections");
                });
        }
    }
}
