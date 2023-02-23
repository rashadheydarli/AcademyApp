using System;
using Core.Entities;
namespace Data.Contexts
{
	public static class DbContext
	{
		static DbContext()
		{
			Groups = new List<Group>();
			Students = new List<Student>();
			Teachers = new List<Teacher>();
			Admins = new List<Admin>();             // buna gore de DbContext ilk defe call olunanda bos bir string yaradacaq
		}										   //Listin default deyeri nulldir(ref tip oldugu ucun)
												 //constructor ona gore teyin edirik ki bu bos bir Listdir
											    // bir bar bos list biri var null (null bir obyekte her hansi bir elave etmek olmaz)
												//nula her hansi bir deyer assign etmek olmur
												//DbContext call olunan zaman onlari teyin et demekdir (st cons bir defe isleyir)
												//DbContext ilk defe cagilirdigi zaman bunlari(Groups,Students,Admins) bos liste beraber etki men onlar uzerinde emeliyyat apara bilim.
        
        public static List<Group> Groups { get; set; }
		public static List<Student>Students { get; set; }
		public static List<Teacher>Teachers { get; set; }
		public static List<Admin>Admins { get; set; }

	}
}

 