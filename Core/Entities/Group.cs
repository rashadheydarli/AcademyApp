using System;
namespace Core.Entities
{
	public class Group : BaseEntity
	{
		public Group()
		{
			Students = new List<Student>();   //Student Groupdaki deyeri null oldugu ucun 
											  //null olmasin deye edirik
		}
		public string Name { get; set; }
		public int MaxSize { get; set; }
		public DateTime StartDate { get; set; }
		public DateTime EndDate { get; set; }
		public List<Student> Students { get; set; }

		public Teacher Teacher { get; set; }  //Groupun bir dene muellimi var 
		public int TeacherId { get; set; }
	}
}

