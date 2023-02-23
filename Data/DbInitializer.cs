using System;
using Core.Entities;
using Core.Helpers;
using Data.Contexts;

namespace Data
{
	public static class DbInitializer			//bu classin meqsedi app run olunanda dbye mueyyen seyleri(adminleri bazaya elave etme) set(sead) elemek
	{                                           // app run olunan zaman artiq adminler bazada olmalidi (studenleri ise sonradan yaradiriq).
		static int id;										// bir basa bizden username ve password istemelidi
												//app run olunanda bilmeliyem ki bu hansi adminle daxil olub.yeni adminler bazada olmalidi
		public static void SeadAdmins()				
		{
			var admins = new List<Admin>
			{
				new Admin
				{
					Id= ++id,
					Username = "admin1",
					Password = PasswordHasher.Encrypt("1234"),
					CreatedBy = "System"
				},

				new Admin
				{
					Id=++id,
					Username ="admin2",
					Password = PasswordHasher.Encrypt("salam123"),
                    CreatedBy = "System"
                }
			};
			DbContext.Admins.AddRange(admins);   //birden cox oludugu ucun (list ) gozleryir AddRange edirik
		}
		//adminleri birbasa burda ona gore yaradiriq ki bu digerlerinden ferqli olaraq admine verdyimiz ozellik deyil appin oz ozelleyidi 
	}
}

