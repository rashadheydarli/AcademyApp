using System;
using Core.Entities;
namespace Data.Repositories.Abstract
{
	public interface IGroupRepository
	{
		List<Group> GetAll();
		Group Get(int id);
		void Add(Group group );
		void Update(Group group);   // bunlar writedi / read olanlari yuxarida yaziriq
		void Delete(Group group);   

		// abstract metodlari ozunde saxladi
		//implement etmek ucun concrete bax
    }
}

