namespace RPRAssistant.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class PopulatePersonsTable : DbMigration
	{
		public override void Up()
		{
			Sql("INSERT INTO Persons (Name, EmailAddress) VALUES('Alejandro', 'alazo8807@gmail.com')");
			Sql("INSERT INTO Persons (Name, EmailAddress) VALUES('Mauricio', 'mauricio.miranda.16@gmail.com ')");
			Sql("INSERT INTO Persons (Name, EmailAddress) VALUES('Fernando', 'fvaldes88@gmail.com ')");
		}
		
		public override void Down()
		{
			Sql("DELETE FROM PERSONS");
		}
	}
}
