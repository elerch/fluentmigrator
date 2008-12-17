﻿using System;

namespace FluentMigrator.Tests
{
	public class TestMigration : Migration
	{
		public override void Up()
		{
			Create.Table("Users")
				.WithColumn("UserId").AsInt32().Identity().PrimaryKey()
				.WithColumn("UserName").AsString().WithSize(32).NotNullable();

			Create.Column("Foo").OnTable("Users").AsInt16().WithSize(123).Indexed();

			Create.ForeignKey().FromTable("Users").ForeignColumn("GroupId").ToTable("Groups").PrimaryColumn("GroupId");
		}

		public override void Down()
		{
			Delete.ForeignKey().FromTable("Users").ForeignColumn("GroupId").ToTable("Groups").PrimaryColumn("GroupId");

			Delete.Column("Foo").FromTable("Users");
			Delete.Table("Users");
		}
	}
}
