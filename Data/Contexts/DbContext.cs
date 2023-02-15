using System;
using Core.Entities;
namespace Data.Contexts
{
	public static class DbContext
	{
		static DbContext()
		{
			Groups = new List<Group>();     // bir bar bos list biri var null (null bir obyekte her hansi bir elave etmek olmaz)
											// buna gore de DbContext ilk defe call olunanda bos bir string yaradacaq 
		}
		public static List<Group> Groups { get; set; }

	}
}

 