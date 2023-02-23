using System;
namespace Core.Entities
{
	public class Teacher:BaseEntity
	{
		public Teacher()
		{
			Groups = new List<Group>();      //eger teachere hec bir group elave etmesek null olacaq ,cunki LIst ref tipdi ve default deyer nulldu
		}
		public string Name { get; set; }
		public string Surname { get; set; }
		public DateTime Birthday { get; set; }
		public string  Speciality { get; set; }
		public List<Group> Groups { get; set; }   //bir muellimin cox Groupu var 
	}
}

